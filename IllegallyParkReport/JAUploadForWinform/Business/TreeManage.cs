using Core.Net;
using JAUploadForWinform.Model;
using JAUploadForWinform.Model.JsonObject;
using JAUploadForWinform.Model.JsonObject.CommonQuery;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WiseMan.Business.Common;

namespace JAUploadForWinform.Business
{
    class TreeManage
    {
       public static Dictionary<string, ResourceItem> Cameras = new Dictionary<string, ResourceItem>();

       public static Dictionary<string, ResourceItem> Organazations = new Dictionary<string, ResourceItem>();

        Dictionary<string, ResourceItem>[] camerasMultiple; //多线程

        Task[] TasksArray;

        CancellationTokenSource[] cancelTokens;

        const int PageRow = 200;

        Stopwatch watch = new Stopwatch();

        public bool LoadOrg()
        {
            Organazations.Clear();
            watch.Start();

            //设置查询条件
            ConditionJson cond = new ConditionJson();
            cond.Condition = new List<CommonConditionJson>();

            //查询类型为组织
            CommonConditionJson item1 = new CommonConditionJson();
            item1.QueryType = 256;
            item1.LogicFlag = 0;
            item1.QueryData = "1";
            cond.Condition.Add(item1);

            //查询下级
            CommonConditionJson item2 = new CommonConditionJson();
            item2.QueryType = 257;
            item2.LogicFlag = 0;
            item2.QueryData = "1";
            cond.Condition.Add(item2);

            cond.ItemNum = 2;
            cond.QueryCount = 1;

            cond.PageFirstRowNumber = 0;
            cond.PageRowNum = 200;
            //获得TotalNum
            ResResponseJson rpsJson = GetRes(cond);
            //using (FileStream fs = File.Create("AB.bin"))
            //{
            //    string s = JSONHelper.ObjectToJson<ResResponseJson>(rpsJson, Encoding.UTF8);
            //    byte[] bytes = Encoding.UTF8.GetBytes(s);
            //    fs.Write(bytes, 0, bytes.Length);
            //    fs.Flush();
            //}
                //ResResponseJson rpsJson = DemoOrg("AB.bin");
                if (rpsJson == null)
                {
                    LogHelper.Error("组织反序列化失败");
                    return false;
                }
            if (rpsJson.ErrCode != 0)
            {
                LogHelper.Error(string.Format("组织加载失败,错误码:{0},错误信息{1}", rpsJson.ErrCode, rpsJson.ErrMsg));
                return false;
            }

            Console.WriteLine("组织总数为:" + rpsJson.Result.RspPageInfo.TotalRowNum);

            if (rpsJson.Result.RspPageInfo.RowNum > 0)
            {
                AddToOrgs(rpsJson.Result.InfoList);
            }
            int remainCount = rpsJson.Result.RspPageInfo.TotalRowNum - PageRow;
            int totalRowNum = rpsJson.Result.RspPageInfo.TotalRowNum;
            int loop = 0;
            //如果超过了200个，则遍历查询获取所有结果集合
            while (remainCount > 0)
            {
                Console.WriteLine("开始遍历查询");
                cond.QueryCount = 0;
                cond.PageFirstRowNumber = PageRow + loop * PageRow;
                ResResponseJson subquery = GetRes(cond);
                if (subquery.ErrCode != 0)
                {
                    LogHelper.Error(string.Format("组织加载失败,错误码:{0},错误信息{1}", subquery.ErrCode, subquery.ErrMsg));
                    return false;
                }
                if (subquery.Result.RspPageInfo.RowNum > 0)
                {
                    AddToOrgs(subquery.Result.InfoList);
                }
                loop++;
                remainCount = totalRowNum - (loop + 1) * PageRow;
            }
            //至此获取到全部的摄像机资源
            LogHelper.Info(string.Format("组织域加载完成,耗时:{0}毫秒", watch.ElapsedMilliseconds));
            watch.Stop();
            return true;
        }
        private ResResponseJson DemoOrg(string txtName)
        {
            ResResponseJson json = new ResResponseJson();
            try
            {
                using (FileStream fs = File.Open(txtName, FileMode.Open))
                {
                    byte[] bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    fs.Flush();
                    string js = Encoding.UTF8.GetString(bytes);

                    json = JSONHelper.JsonToObject<ResResponseJson>(js, Encoding.UTF8);
                }
            }
            catch(Exception ex)
            {

            }
            return json;
        }
        private void AddToCameras(List<ResInfoJson> resList)
        {
            foreach (ResInfoJson info in resList)
            {
                if(!Cameras.ContainsKey(info.ResItemV1.ResCode))
                {
                    Cameras.Add(info.ResItemV1.ResCode, new ResourceItem() { ResCode = info.ResItemV1.ResCode, ResName = info.ResItemV1.ResName, ResType = (EmResType)info.ResItemV1.ResType, OrgCode = info.ResItemV1.OrgCode, OrgName = info.OrgName });
                }
                else
                {
                    Console.WriteLine("重复相机");
                }
            }
        }

        private void AddMutiCameras(List<ResInfoJson>resList,int index)
        {
            foreach (ResInfoJson info in resList)
            {
                camerasMultiple[index].Add(info.ResItemV1.ResCode, new ResourceItem() { ResCode = info.ResItemV1.ResCode, ResName = info.ResItemV1.ResName, ResType = (EmResType)info.ResItemV1.ResType, OrgCode = info.ResItemV1.OrgCode, OrgName = info.OrgName });
            }
        }
        public void AddToOrgs(List<ResInfoJson> orgList)
        {
            foreach (ResInfoJson info in orgList)
            {
                Organazations.Add(info.ResItemV1.ResCode,new ResourceItem() { ResCode = info.ResItemV1.ResCode, ResName = info.ResItemV1.ResName, ResType = (EmResType)info.ResItemV1.ResType,OrgCode=info.ResItemV1.OrgCode,OrgName=info.OrgName });
            }
        }

        private ResResponseJson GetRes(ConditionJson cond)
        {
            ResResponseJson resResult = new ResResponseJson();
            string conditionStr = JSONHelper.ObjectToJson<ConditionJson>(cond, Encoding.UTF8);
            string conditionStrUrlEncode = System.Web.HttpUtility.UrlEncode(conditionStr, Encoding.UTF8);
            string requestUrl = string.Format("{0}?condition={1}", ApiUrls.ResQueryURL, conditionStrUrlEncode);
            try
            {
            HttpItem httpRequest = RequestGenerater.GetHttpItem(requestUrl, "GET", string.Empty, true, IMOS.AuthKey, IMOS.AccessToken);
            HttpResult result = new HttpHelper().GetHtml(httpRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                    //LogHelper.Error(result.Html);
                //获得TotalNum
                ResResponseJson rpsJson = JSONHelper.JsonToObject<ResResponseJson>(result.Html, Encoding.UTF8);
                 
                resResult = rpsJson;
            }
            else
            {
                resResult.ErrCode = (int)result.StatusCode;
                resResult.ErrMsg = result.StatusDescription;
            } }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
            return resResult;

        }

        public bool LoadCameras()
        {
            Console.WriteLine("单线程加载摄像机树");
            watch.Reset();
            watch.Start();
            //设置查询条件
            ConditionJson cond = new ConditionJson();
            cond.Condition = new List<CommonConditionJson>();

            ////查询类型为卡口
            CommonConditionJson item1 = new CommonConditionJson();
            item1.QueryType = 256;
            item1.LogicFlag = 0;
            item1.QueryData = "31";
            cond.Condition.Add(item1);


            //根据资源子类型排序
            CommonConditionJson item2 = new CommonConditionJson();
            item2.QueryType = 11;
            item2.LogicFlag = 6;
            item2.QueryData = "";
            cond.Condition.Add(item2);

            //根据编码类型排序
            CommonConditionJson item3 = new CommonConditionJson();
            item3.QueryType = 0;
            item3.LogicFlag = 6;
            item3.QueryData = "";
            cond.Condition.Add(item3);



            //查询下级
            CommonConditionJson item4 = new CommonConditionJson();
            item4.QueryType = 257;
            item4.LogicFlag = 0;
            item4.QueryData = "1";
            cond.Condition.Add(item4);


            cond.ItemNum = 4;
            cond.QueryCount = 1;

            cond.PageFirstRowNumber = 0;
            cond.PageRowNum = PageRow;


            //获得TotalNum
           ResResponseJson rpsJson = GetRes(cond);
           //ResResponseJson rpsJson = DemoOrg("Cam1.txt");
            if (rpsJson.ErrCode != 0)
            {
                LogHelper.Error(string.Format("摄像机加载失败,错误码:{0},错误信息{1}", rpsJson.ErrCode, rpsJson.ErrMsg));
                return false;
            }
            if (rpsJson.Result.RspPageInfo.RowNum > 0)
            {
                AddToCameras(rpsJson.Result.InfoList);
            }
            Console.WriteLine("摄像机总数为：" + rpsJson.Result.RspPageInfo.TotalRowNum);
            int remainCount = rpsJson.Result.RspPageInfo.TotalRowNum - PageRow;
            int totalRowCount = rpsJson.Result.RspPageInfo.TotalRowNum;
            int loop = 0;
            //如果超过了200个，则遍历查询获取所有结果集合
            while (remainCount > 0)
            {
                cond.QueryCount = 0;
                cond.PageFirstRowNumber = PageRow + loop * PageRow;
                ResResponseJson subquery = GetRes(cond);
                //ResResponseJson subquery = DemoOrg("Cam" + (loop + 2)+".txt");
                if (subquery.ErrCode != 0)
                {
                    LogHelper.Error(string.Format("摄像机循环加载失败,错误码:{0},错误信息{1}", subquery.ErrCode, subquery.ErrMsg));
                    return false;
                }
                if (subquery.Result.RspPageInfo.RowNum > 0)
                {
                    AddToCameras(subquery.Result.InfoList);
                }
               
                loop++;
                remainCount = totalRowCount -(loop+1)*PageRow;
            }
            //至此获取到全部的摄像机资源
            LogHelper.Info(string.Format("摄像机加载完成,耗时:{0}ms", watch.ElapsedMilliseconds));
            watch.Stop();
            return true;
        }

        public void LoadCamerasMutiple()
        {
            bool multiple = false;

            Cameras.Clear();
            Console.WriteLine("多线程加载摄像机树");
            watch.Reset();
            watch.Start();
            //设置查询条件
            ConditionJson cond = new ConditionJson();
            cond.Condition = new List<CommonConditionJson>();

            ////查询类型为卡口
            CommonConditionJson item1 = new CommonConditionJson();
            item1.QueryType = 256;
            item1.LogicFlag = 0;
            item1.QueryData = "31";
            cond.Condition.Add(item1);


            //根据资源子类型排序
            CommonConditionJson item2 = new CommonConditionJson();
            item2.QueryType = 11;
            item2.LogicFlag = 6;
            item2.QueryData = "";
            cond.Condition.Add(item2);

            //根据编码类型排序
            CommonConditionJson item3 = new CommonConditionJson();
            item3.QueryType = 0;
            item3.LogicFlag = 6;
            item3.QueryData = "";
            cond.Condition.Add(item3);

            //查询下级
            CommonConditionJson item4 = new CommonConditionJson();
            item4.QueryType = 257;
            item4.LogicFlag = 0;
            item4.QueryData = "1";
            cond.Condition.Add(item4);


            cond.ItemNum = 4;
            cond.QueryCount = 1;

            cond.PageFirstRowNumber = 0;
            cond.PageRowNum = PageRow;


            //获得TotalNum
            ResResponseJson rpsJson = GetRes(cond);
            if (rpsJson.ErrCode != 0)
            {
                LogHelper.Error(string.Format("摄像机加载失败,错误码:{0},错误信息{1}", rpsJson.ErrCode, rpsJson.ErrMsg));
            }
            if (rpsJson.Result.RspPageInfo.RowNum > 0)
            {
                AddToCameras(rpsJson.Result.InfoList);
            }
            int remainCount = rpsJson.Result.RspPageInfo.TotalRowNum - PageRow;//剩余还有多少条。
         
            int totalRowNum = rpsJson.Result.RspPageInfo.TotalRowNum;
            Console.WriteLine("共有" + totalRowNum + "条数据");

            if (remainCount>=PageRow*4)
            {
                Console.WriteLine("启动4个线程去加载");
                //启动四个线程去加载
                multiple = true;
                TasksArray = new Task[4];
                camerasMultiple = new Dictionary<string, ResourceItem>[4];

                int aveNum = remainCount / 4; //平均每一个要处理多少个。还剩下的
                int ys = remainCount - aveNum * 4; //平均分完还剩下的个数。

                for(int i=0;i<4;i++)
                {

                    if(i==3)
                    {
                        TasksArray[i] = new Task(() => LoadSubCameras(PageRow + i * aveNum, PageRow + (i + 1) * aveNum+ys, i));

                        Console.WriteLine(string.Format("线程分配：{0}线程，{1}，{2}", i, PageRow + i * aveNum, PageRow + (i + 1) * aveNum+ys));
                    }
                    else
                    {
                        TasksArray[i] = new Task(() => LoadSubCameras(PageRow + i * aveNum, PageRow + (i + 1) * aveNum, i));

                        Console.WriteLine(string.Format("线程分配：{0}线程，{1}，{2}", i, PageRow + i * aveNum, PageRow + (i + 1) * aveNum));
                    }
                }
                Console.WriteLine("线程分配完毕");
            }
            else if(remainCount>=PageRow*2&&remainCount<=PageRow*4)
            {
                //启动两个线程去加载
                multiple = true;
                TasksArray = new Task[2];
                camerasMultiple = new Dictionary<string, ResourceItem>[2];
                int aveNum = remainCount / 2; //平均每一个要处理多少个。还剩下的
                int ys = remainCount - aveNum * 2; //平均分完还剩下的个数。
                for(int i=0;i<2;i++)
                {
                    TasksArray[i] = new Task(() => LoadSubCameras(PageRow + i * aveNum, PageRow + (i + 1) * aveNum, i));
                    if(i==1)
                    {
                        TasksArray[i] = new Task(() => LoadSubCameras(PageRow + i * aveNum, PageRow + (i + 1) * aveNum + ys, i));
                    }              
                }
            }
            else //否则只用一个线程加载就够。
            {
                int loop = 0;
                //如果超过了200个，则遍历查询获取所有结果集合
                while (remainCount > 0)
                {
                    cond.QueryCount = 0;
                    cond.PageFirstRowNumber = PageRow + loop * PageRow;
                    ResResponseJson subquery = GetRes(cond);
                    if (subquery.ErrCode != 0)
                    {
                        LogHelper.Error(string.Format("摄像机循环加载失败,错误码:{0},错误信息{1}", subquery.ErrCode, subquery.ErrMsg));
                    }
                    if (subquery.Result.RspPageInfo.RowNum > 0)
                    {
                        AddToCameras(subquery.Result.InfoList);
                    }
                   
                    loop++;
                    remainCount = totalRowNum - (loop + 1) * PageRow;
                }
            }
            if(multiple)
            {
                Console.WriteLine("开始启动线程加载");
                for(int i=0;i<TasksArray.Length;i++)
                {
                    Console.WriteLine("线程" + i + "启动");
                    TasksArray[i].Start();
                }
                Task.WaitAll(TasksArray);
                foreach(Dictionary<string,ResourceItem> item in camerasMultiple)
                {
                    foreach(KeyValuePair<string,ResourceItem> subItem in item)
                    {
                        Cameras.Add(subItem.Key,subItem.Value);
                    }
                }
            }
            //至此获取到全部的摄像机资源
            LogHelper.Info(string.Format("多线程摄像机加载完成,耗时:{0}ms", watch.ElapsedMilliseconds));
            watch.Stop();
        }

        private void LoadSubCameras(int start,int end,int index)
        {
            Console.WriteLine(string.Format("线程{0}启动，Start:{1},End:{2}", index, start, end));
            //设置查询条件
            ConditionJson cond = new ConditionJson();
            cond.Condition = new List<CommonConditionJson>();

            ////查询类型为卡口
            CommonConditionJson item1 = new CommonConditionJson();
            item1.QueryType = 256;
            item1.LogicFlag = 0;
            item1.QueryData = "31";
            cond.Condition.Add(item1);


            //根据资源子类型排序
            CommonConditionJson item2 = new CommonConditionJson();
            item2.QueryType = 11;
            item2.LogicFlag = 6;
            item2.QueryData = "";
            cond.Condition.Add(item2);

            //根据编码类型排序
            CommonConditionJson item3 = new CommonConditionJson();
            item3.QueryType = 0;
            item3.LogicFlag = 6;
            item3.QueryData = "";
            cond.Condition.Add(item3);

            //查询下级
            CommonConditionJson item4 = new CommonConditionJson();
            item4.QueryType = 257;
            item4.LogicFlag = 0;
            item4.QueryData = "1";
            cond.Condition.Add(item4);

            cond.ItemNum = 4;
            cond.QueryCount = 0;
            cond.PageFirstRowNumber = start;
            cond.PageRowNum = PageRow;

            int loop = 0;
            //如果超过了200个，则遍历查询获取所有结果集合
            int remainCount = end - start;
            while (remainCount >= 0)
            {
                cond.PageFirstRowNumber = start + loop * PageRow;
                ResResponseJson subquery = GetRes(cond);
                if (subquery.ErrCode != 0)
                {
                    LogHelper.Error(string.Format("摄像机循环加载失败,错误码:{0},错误信息{1}", subquery.ErrCode, subquery.ErrMsg));
                }
                if (subquery.Result.RspPageInfo.RowNum > 0)
                {
                    AddMutiCameras(subquery.Result.InfoList, index);
                }
                loop++;
                remainCount = end - (loop+1) * start;
            }
        }
    }
}
