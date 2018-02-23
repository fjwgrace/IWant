using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAUploadForWinform.Business
{
    class TaskController : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            AlarmManage.QueryAlarm(QuartzManage.dataTimeBegin.AddSeconds(-30), QuartzManage.dataTimeBegin); //每一分钟查询一次
            QuartzManage.dataTimeBegin= QuartzManage.dataTimeBegin.AddSeconds(30);
           
        }
    }
    class TaskClearController : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            RealTimeReportClient.UpdateNums(0, 0, true);
        }


    }
}
