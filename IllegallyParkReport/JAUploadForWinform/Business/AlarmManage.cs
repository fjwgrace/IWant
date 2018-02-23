using Core.Net;
using JAUploadForWinform.Model;
using JAUploadForWinform.Model.JsonObject;
using JAUploadForWinform.Model.JsonObject.CommonQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WiseMan.Business.Common;

namespace JAUploadForWinform.Business
{
    public class AlarmManage
    {
        static List<AlarmInfoJson> AlarmList = new List<AlarmInfoJson>();
        const int PageRow = 200;
        public static void QueryAlarm(DateTime dtStart, DateTime dtEnd)
        {
            //设置查询条件
            ConditionJson cond = new ConditionJson();
            cond.Condition = new List<CommonConditionJson>();

            //查询类型为组织
            CommonConditionJson item1 = new CommonConditionJson();
            item1.QueryType = 8;
            item1.LogicFlag = 3;
            item1.QueryData = dtStart.ToString("yyyy-MM-dd HH:mm:ss");
            cond.Condition.Add(item1);

            //查询下级
            CommonConditionJson item2 = new CommonConditionJson();
            item2.QueryType = 8;
            item2.LogicFlag = 4;
            item2.QueryData = dtEnd.ToString("yyyy-MM-dd HH:mm:ss");
            cond.Condition.Add(item2);


            cond.ItemNum = 2;
            cond.QueryCount = 1;

            cond.PageFirstRowNumber = 0;
            cond.PageRowNum = PageRow;

            //获得TotalNum
            AlarmResponseJson rpsJson = GetRes(cond);

           
            if (rpsJson == null)
            {
                LogHelper.Error("反序列化失败");
                return;
            }
            
            if (rpsJson.ErrCode != 0)
            {
                LogHelper.Error(string.Format("违法查询失败,错误码:{0},错误信息{1}", rpsJson.ErrCode, rpsJson.ErrMsg));
                return;
            }
            Console.WriteLine("获取违法总数:" + rpsJson.Result.RspPageInfo.TotalRowNum + "条  " + cond.Condition[0].QueryData + "--" + cond.Condition[1].QueryData);
            if (rpsJson.Result.RspPageInfo.RowNum > 0)
            {
                //new RealTimeReportClient().SendToCenter(rpsJson.Result.InfoList);

            }
            int remainCount = rpsJson.Result.RspPageInfo.TotalRowNum;
            //int totalRowNum = rpsJson.Result.RspPageInfo.TotalRowNum;
            int loop = 0;
            //如果超过了200个，则遍历查询获取所有结果集合
            while (remainCount > 0)
            {
                cond.QueryCount = 0;
                cond.PageFirstRowNumber = loop*PageRow;
                AlarmResponseJson subquery = GetRes(cond);
                if (subquery.ErrCode != 0)
                {
                    LogHelper.Error(string.Format("组织加载失败,错误码:{0},错误信息{1}", subquery.ErrCode, subquery.ErrMsg));
                }
                if (subquery.Result.RspPageInfo.RowNum > 0)
                {
                    new RealTimeReportClient().SendToCenter(subquery.Result.InfoList,true);
                    //foreach(var item in subquery.Result.InfoList)
                    //{
                    //    int index1 = item.LicensePlateList.IndexOf("\"");
                    //    int index2 = item.LicensePlateList.IndexOf(",");

                    //    Console.WriteLine(string.Format("经过时间{0},车牌号码{1}，车牌颜色{2}，违法地点{3}",item.AlarmTime, item.LicensePlateList.Substring(index1+1,index2-index1-2),item.LicensePlateColor,item.TollgateName));
                    //}
                }
                loop++;
                remainCount = remainCount - loop * PageRow;
            }
        }
        private static AlarmResponseJson GetRes(ConditionJson cond)
        {
            AlarmResponseJson resResult = new AlarmResponseJson();
            string conditionStr = JSONHelper.ObjectToJson<ConditionJson>(cond, Encoding.UTF8);
            //LogHelper.Info(conditionStr);
            string conditionStrUrlEncode = System.Web.HttpUtility.UrlEncode(conditionStr, Encoding.UTF8);
            string requestUrl = string.Format("{0}?condition={1}", ApiUrls.VehiceleAlarmURL, conditionStrUrlEncode);
            try
            {
                HttpItem httpRequest = RequestGenerater.GetHttpItem(requestUrl, "GET", string.Empty, true, IMOS.AuthKey, IMOS.AccessToken);
                HttpResult result = new HttpHelper().GetHtml(httpRequest);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //获得TotalNum
                    AlarmResponseJson rpsJson = JSONHelper.JsonToObject<AlarmResponseJson>(result.Html, Encoding.UTF8);
                    //LogHelper.Info(result.Html);
                    resResult = rpsJson;
                }
                else
                {
                    resResult.ErrCode = (int)result.StatusCode;
                    resResult.ErrMsg = result.StatusDescription;
                    Console.WriteLine("请求失败" + result.StatusDescription);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("查询违法信息出错", ex);
            }
            return resResult;

        }
    }
}
