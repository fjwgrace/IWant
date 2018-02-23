using JAUploadForWinform.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiseMan.Business.Common;

namespace JAUploadForWinform.Business
{
    class TollgateFilterManage
    {
        public static event NoticeStatus EventNoticeStatus;

        private static ConcurrentDictionary<string, kkinfo> tollgateFilters = new ConcurrentDictionary<string, kkinfo>();

        public static ConcurrentDictionary<string,kkinfo> TollgateFilter
        {
            get { return tollgateFilters; }
        }

        public static void LoadFilters()
        {
           try
           {
                using (dbEntities db = new dbEntities())
                {
                    var kkInfos = db.kkinfo;
                    foreach(kkinfo info in kkInfos)
                    {
                        tollgateFilters.TryAdd(info.kkCode, info);
                    }
                }
           }
           catch(Exception ex)
            {
                LogHelper.Error("本地卡口列表加载失败", ex);
            }
        }
        public static void AddFilters(List<kkinfo> filters)
        {
            List<kkinfo> failList = new List<kkinfo>();
            foreach(var item in filters)
            {
              if( !tollgateFilters.TryAdd(item.kkCode, item))
                {
                    failList.Add(item);
                }
            }
            if (failList.Count > 0)
            {
                foreach (kkinfo item in failList)
                {
                    filters.Remove(item);
                }
            }
            //保存到数据库
            using (dbEntities db = new dbEntities())
            {
                db.kkinfo.AddRange(filters);
                db.SaveChanges();
            }
            if(failList.Count>0)
            {
                if (EventNoticeStatus != null)
                {
                    string message;

                    if (failList.Count >= 3)
                    {
                        message = string.Format("卡口[{0}]，[{1}]，[{2}]等已配置，不能重复添加", failList[0].kkName, failList[1].kkName, failList[2].kkName);
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("卡口");
                        foreach (var itm in failList)
                        {
                            sb.Append(string.Format("[{0}]，", itm.kkName));
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("已配置,不能重复添加");
                        message = sb.ToString();
                    }
                    EventNoticeStatus.BeginInvoke(message, null, null);
                }
            }
        }

        public static void RemoveFilters(List<string>filters)
        {
            try
            {
                kkinfo info;
                foreach(string str in filters)
                {
                    tollgateFilters.TryRemove(str, out info);
                }
    
            }
            catch (Exception ex)
            {
                LogHelper.Error("移除卡口筛选器失败", ex);
            }
        }
    }
}
