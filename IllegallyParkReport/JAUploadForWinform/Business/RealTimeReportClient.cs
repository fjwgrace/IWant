using Core.Common;
using Core.Net;
using JAUploadForWinform.Model;
using JAUploadForWinform.Model.JsonObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiseMan.Business.Common;

namespace JAUploadForWinform.Business
{
    class RealTimeReportClient
    {
        static int successCount;
        static int failCount;

        public static event NoticeNums EventNoticeNum;

        static readonly object locker = new object();

        public static int SuccessCount
        {
            get { return successCount; }
        }
        public static int FailCount
        {
            get { return failCount; }
        }
        DESEncrypt EncryptTool = new DESEncrypt();

        List<AlarmInfoJson> uploadList = null;
        bool _needFilter;
        public void  SendToCenter(List<AlarmInfoJson> itemList,bool filter)
        {
            if(itemList!=null&&itemList.Count>0)
            {
                uploadList = itemList;
                _needFilter = filter;
                Task task = new Task(() => SendToCenter()); //启动线程立即发送，保证前端采集不堵塞
                task.Start();
            }
        }
        private void GetFilter()
        {
            if(TollgateFilterManage.TollgateFilter.Count<=0)
            {
                return;
            }
            int count = uploadList.Count;
            uploadList.RemoveAll((Predicate<AlarmInfoJson>)delegate (AlarmInfoJson item)
            {
                if(!TollgateFilterManage.TollgateFilter.ContainsKey(item.TollgateCode))
                {
                    return false;
                }
                return CheckDateTime(item.TollgateCode,item.PassTime);
            });
            int filter = count - uploadList.Count;
            if(filter>0)
            {
                Console.WriteLine("过滤：" + filter);
            }
        }
        private bool CheckDateTime(string kkcode, string passTime)
        {
            try
            {
                kkinfo info = TollgateFilterManage.TollgateFilter[kkcode];
                DateTime dt1 = Convert.ToDateTime(DateTime.Parse(passTime).ToString("HH:mm:ss"));

                DateTime dtStart1 = Convert.ToDateTime(((DateTime)info.startTime1).ToString("HH:mm:ss"));
                DateTime dtEnd1 = Convert.ToDateTime(((DateTime)info.endTime1).ToString("HH:mm:ss"));
              
                //判断第一个时间
                if(dt1>=dtStart1&&dt1<=dtEnd1)
                {
                    return true;
                }

                //判断第二个时间
                if(info.startTime2.HasValue)
                {
                    DateTime dtStart2 = Convert.ToDateTime(((DateTime)info.startTime2).ToString("HH:mm:ss"));
                    DateTime dtEnd2 = Convert.ToDateTime(((DateTime)info.endTime2).ToString("HH:mm:ss"));
                    if (dt1 >= dtStart2 && dt1 <= dtEnd2)
                    {
                        return true;
                    }
                    else //如果不在第二个时间段内，则判断第三个
                    {
                        if(info.startTime3.HasValue)
                        {
                            DateTime dtStart3 = Convert.ToDateTime(((DateTime)info.startTime3).ToString("HH:mm:ss"));
                            DateTime dtEnd3 = Convert.ToDateTime(((DateTime)info.endTime3).ToString("HH:mm:ss"));
                            if(dt1>=dtStart3&&dt1<=dtEnd3)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                }
                else
                {
                    return false;
                }
             
            }
            catch(Exception ex)
            {
                LogHelper.Error("校验时间出错", ex);
                return false;
            }
         
        }

        public void SendToCenter()
        {
            try
            {
                if (_needFilter)
                {
                    GetFilter();  //数据过滤
                }
                if(ApiUrls.LimitNum!=-1) //如果设置了上传的受限的量。
                {
                    if(successCount>=ApiUrls.LimitNum)
                    {
                        Console.WriteLine("到达受限制的量，不让传了");
                        return;  //到达受限的量就不让传了
                    }
                }
                 List<RealTimeReportJson> jsonList = new List<RealTimeReportJson>();
                foreach(var item in uploadList)
                {
                    jsonList.Clear();
                    RealTimeReportJson rpJson = new RealTimeReportJson();
                   
                    int index1 = item.LicensePlateList.IndexOf("\"");
                    int index2 = item.LicensePlateList.IndexOf(",");
                    item.LicensePlateList= item.LicensePlateList.Substring(index1 + 1, index2 - index1 - 2);
                    rpJson.HPHM = item.LicensePlateList;
                    rpJson.HPYS =ColorManage.GetColor(item.LicensePlateColor);
                    rpJson.JGSJ = item.PassTime;
                    int index3 = item.TollgateName.IndexOf("-");
                    if(index3!=-1)
                    {
                        rpJson.WFDD = item.TollgateName.Substring(index3 + 1);
                    }
                    else
                    {
                        rpJson.WFDD = item.TollgateName;
                    }
                
                    rpJson.WFLX = "违法停车";
                    //rpJson.XZQH = TollgateFilterManage.TollgateFilter[item.TollgateCode].dptCode;
                    rpJson.XZQH = "310600";
                    jsonList.Add(rpJson);

                    if(SendToRemote(jsonList)==true)
                    {
                        UpdateNums(1, 0, false);
                    }
                    else
                    {
                        SaveToDb(item);
                        UpdateNums(0, 1, false);
                    }
                }
                
            }
            catch (Exception ex)
            {
                LogHelper.Error("即时告知平台数据发送出错",ex);
            }
        }
        public bool SendToRemote(List<RealTimeReportJson> jsonList)
        {
            try
            {
                string jsonP = JSONHelper.ObjectToJson<List<RealTimeReportJson>>(jsonList, Encoding.UTF8);

                string jsonPE = EncryptTool.Encrypt(jsonP, ApiUrls.SecretKey); //加密

               // LogHelper.Info(jsonPE);

                string url = string.Format("{0}?param={1}", ApiUrls.RealTimeReportURL, jsonPE);
                HttpItem httpRequest = GetHttpItem(url, "GET", string.Empty, false, null, null);

                HttpResult result = new HttpHelper().GetHtml(httpRequest);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string decryptStr = EncryptTool.Decrypt(result.Html, ApiUrls.SecretKey);

                    List<RealTimeReportResponseJson> rpsJson = JSONHelper.JsonToObject<List<RealTimeReportResponseJson>>(decryptStr, Encoding.UTF8);

         
                    if (rpsJson[0].ZT == "0")
                    {
                        LogHelper.Info("上传失败" + rpsJson[0].CWMS);
                        return false;

                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    LogHelper.Error("即时告知平台数据发送失败，HTTP错误码:" + result.StatusDescription);
                    LogHelper.Error(jsonP);
                    return false;
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error("发送到告知平台失败", ex);
                return false;
            }
        }
        private void SaveToDb(AlarmInfoJson item)
        {
            try
            {
                uploadfail fail = new uploadfail();
                fail.hphm = item.LicensePlateList;
                fail.cjjg = "310600";
                fail.hpys = ColorManage.GetColor(item.LicensePlateColor);
                fail.passTime = DateTime.Parse(item.PassTime);
                fail.wfdd = item.TollgateName;
                fail.wflx = "违法停车";
                fail.kkcode = item.TollgateCode;
                fail.kkName = item.TollgateName;
                using (dbEntities db = new dbEntities())
                {
                    db.uploadfail.Add(fail);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error("保存上传即使告知平台失败列表出错", ex);
            }
        }
        public  HttpItem GetHttpItem(string url, string method, string data, bool auth, string authKey, string authValue)
        {
            HttpItem item = new HttpItem();
            item.URL = url;
            item.ContentType = "application/json";
            item.Encoding = Encoding.UTF8;
            if (method.ToLower() == "post")
            {
                item.Postdata = data;
                item.PostDataType = PostDataType.String;
                item.PostEncoding = Encoding.UTF8;
            }
            if (auth == true)
            {
                item.Header.Add(authKey, authValue);
            }
            item.Method = method;
            item.Accept = "application/json";
            item.ResultType = ResultType.String;
            return item;
        }

        public static void UpdateNums(int success, int fail, bool reset)
        {
            lock (locker)
            {
                if (!reset)
                {
                    successCount += success;
                    fail += fail;
                }
                else
                {
                    successCount = 0;
                    fail = 0;
                }
            }
            if(EventNoticeNum!=null)
            {
                Console.WriteLine("累计成功上传" + successCount + "条");
                EventNoticeNum.BeginInvoke(successCount, failCount, null, null);  //异步通知界面
            }
        }
    }
}
