using Commons;
using Core.Net;
using JAUploadForWinform.Model;
using JAUploadForWinform.Model.JsonObject;
using JAUploadForWinform.Model.JsonObject.CommonQuery;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WiseMan.Business.Common;

namespace JAUploadForWinform.Business
{
    class IMOS
    {
        public static string UserName { get; set; }
        public static string UserPwd { get; set; }
        private static string UserPwdMd5 { get; set; }
        private static string AccessCode { get; set; }
        public static string AccessToken { get; set; }
        public const string AuthKey = "Authorization";
        public const int PageRow = 200;
        static int NUM = 0;

        public static List<AlarmInfoJson> alarms = new List<AlarmInfoJson>();


        
        public static bool Login()
        {
            try
            {
                HttpItem item = RequestGenerater.GetHttpItem(ApiUrls.LoginURL, "POST", string.Empty,false,null,null);
                HttpResult result = new HttpHelper().GetHtml(item);
                if(result.StatusCode==System.Net.HttpStatusCode.OK)
                {
                    //获得AccessCode
                    LoginResponseJson rpsJson = JSONHelper.JsonToObject<LoginResponseJson>(result.Html, Encoding.UTF8);
                    AccessCode = rpsJson.AccessCode;

                    //组成第二次请求体
                    LoginRequestJson rqstJson = new LoginRequestJson();
                    rqstJson.AccessCode = AccessCode;
                    rqstJson.UserName = UserName;
                    rqstJson.LoginSignature = MD5Util.GetMD5_32(string.Format("{0}{1}{2}", Convert.ToBase64String(Encoding.UTF8.GetBytes(UserName)), AccessCode, MD5Util.GetMD5_32(UserPwd)));

                    string requestJson = JSONHelper.ObjectToJson<LoginRequestJson>(rqstJson, Encoding.UTF8);
                    HttpItem item2 = RequestGenerater.GetHttpItem(ApiUrls.LoginURL, "POST", requestJson, false, null, null);
                    HttpResult result2 = new HttpHelper().GetHtml(item2);
                    if(result.StatusCode==System.Net.HttpStatusCode.OK)
                    {
                        LoginTokenJson token = JSONHelper.JsonToObject<LoginTokenJson>(result2.Html, Encoding.UTF8);
                        AccessToken = token.AccessToken;
                        LogHelper.Info("登录成功AccessToken:" + AccessToken);
                        return true;
                    }
                    else
                    {
                        LogHelper.Error("登录获取Token失败,错误码" + result2.StatusCode);
                        return false;

                    }
                }
                else
                {
                    LogHelper.Error("登录获取AccessCode失败，错误码" + result.StatusCode);
                    return false;
                }
              
            }
            catch(Exception ex)
            {
                LogHelper.Error("登录失败", ex);
                return false;
            }
        }

        public static bool Logout()
        {
            try
            {
                HttpItem item = RequestGenerater.GetHttpItem(ApiUrls.LogoutURL, "POST", string.Empty, true, AuthKey, AccessToken);
                HttpResult result = new HttpHelper().GetHtml(item);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //获得AccessCode
                    LogoutResponseJson rpsJson = JSONHelper.JsonToObject<LogoutResponseJson>(result.Html, Encoding.UTF8);
                    if (rpsJson.ErrCode == 0)
                    {
                        return true;
                    }
                    else
                    {
                        LogHelper.Error(string.Format("退出登录失败，错误码:{0},错误描述:{1}" + rpsJson.ErrCode, rpsJson.ErrMsg));
                        return false;
                    }
                }
                else
                {
                    LogHelper.Error("退出登录失败，错误码" + result.StatusCode);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("退出登录失败", ex);
                return false;
            }
        }
        /// <summary>
        /// 查询违法数据，根据开始时间和截止时间
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        public static void QueryAlarm(DateTime dtStart,DateTime dtEnd)
        {
            alarms.Clear();
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
            cond.PageRowNum = 200;
            //获得TotalNum
            AlarmResponseJson rpsJson = GetRes(cond);
            if (rpsJson == null)
            {
                LogHelper.Error("反序列化失败");
            }
            if (rpsJson.ErrCode != 0)
            {
                LogHelper.Error(string.Format("违法查询失败,错误码:{0},错误信息{1}", rpsJson.ErrCode, rpsJson.ErrMsg));
            }
            if(rpsJson.Result.RspPageInfo.RowNum>0)
            {
                AddToAlarm(rpsJson.Result.InfoList);
            }
            Console.WriteLine("非法总数为:" + rpsJson.Result.RspPageInfo.TotalRowNum);

            int remainCount = rpsJson.Result.RspPageInfo.TotalRowNum - PageRow;
            int totalRowNum = rpsJson.Result.RspPageInfo.TotalRowNum;
            int loop = 0;
            //如果超过了200个，则遍历查询获取所有结果集合
            while (remainCount > 0)
            {
                cond.QueryCount = 0;
                cond.PageFirstRowNumber = PageRow + loop * PageRow;
                AlarmResponseJson subquery = GetRes(cond);
                if (subquery.ErrCode != 0)
                {
                    LogHelper.Error(string.Format("组织加载失败,错误码:{0},错误信息{1}", subquery.ErrCode, subquery.ErrMsg));
                }
                if (subquery.Result.RspPageInfo.RowNum > 0)
                {
                   AddToAlarm(subquery.Result.InfoList);
                }
                loop++;
                remainCount = totalRowNum - (loop + 1) * PageRow;
            }
            foreach(var itm in alarms)
            {
                Console.WriteLine(string.Format("车牌号：{0},号牌颜色：{1}，经过时间{2}，地点{3},采集机关{4}", itm.LicensePlateList, itm.LicensePlateColor, itm.PassTime, itm.PlaceName, itm.DeptCode));
            }
            NUM += alarms.Count;
            Console.WriteLine(string.Format("{0}——{1}查询到{2}个结果,累计查询到{3}条记录", dtStart.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"), alarms.Count, NUM));
        }
        static void AddToAlarm(List<AlarmInfoJson> infos)
        {
            foreach(var item in infos)
            {
                alarms.Add(item);
            }
        }

        private static AlarmResponseJson GetRes(ConditionJson cond)
        {
            AlarmResponseJson resResult = new AlarmResponseJson();
            string conditionStr = JSONHelper.ObjectToJson<ConditionJson>(cond, Encoding.UTF8);
            string conditionStrUrlEncode = System.Web.HttpUtility.UrlEncode(conditionStr, Encoding.UTF8);
            string requestUrl = string.Format("{0}?condition={1}", ApiUrls.VehiceleAlarmURL, conditionStrUrlEncode);
            LogHelper.Info("请求路径:" + conditionStr);
            try
            {
                HttpItem httpRequest = RequestGenerater.GetHttpItem(requestUrl, "GET", string.Empty, true, IMOS.AuthKey, IMOS.AccessToken);
                HttpResult result = new HttpHelper().GetHtml(httpRequest);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //获得TotalNum
                    AlarmResponseJson rpsJson = JSONHelper.JsonToObject<AlarmResponseJson>(result.Html, Encoding.UTF8);
                    if(rpsJson==null)
                    {
                        Console.WriteLine("反序列化失败");
                    }
                    resResult = rpsJson;
                }
                else
                {
                    resResult.ErrCode = (int)result.StatusCode;
                    resResult.ErrMsg = result.StatusDescription;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
            return resResult;
        }
    }
}
