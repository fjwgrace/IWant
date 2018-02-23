using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiseMan.Business.Common;

namespace JAUploadForWinform.Business
{
   public  class QuartzManage
    {
        const string corn_task = "0/30 * * ? * *";  //定时每分钟查询一次
        const string corn_clear = "0 0 0 ? * *";   //每天凌晨启动一次清理

        static ISchedulerFactory schedf = new StdSchedulerFactory();
        static IScheduler sched = null;

        public static DateTime dataTimeBegin;

        public static void StartTask()
        {
            try
            {
                sched = schedf.GetScheduler();
                //创建Job1
                IJobDetail job1 = JobBuilder.Create<TaskController>().Build();
                DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(DateTime.Now.AddSeconds(1), 2);
                //创建每分钟启动一次的触发器
                ICronTrigger trigger1 = (ICronTrigger)TriggerBuilder.Create().StartAt(startTime)
                             .WithCronSchedule(corn_task)
                             .Build();
                sched.ScheduleJob(job1, trigger1);
                
                //创建Job2
                IJobDetail job2 = JobBuilder.Create<TaskClearController>().Build();
                ICronTrigger trigger2 = (ICronTrigger)TriggerBuilder.Create().StartAt(startTime)
                           .WithCronSchedule(corn_clear)
                           .Build();
                sched.ScheduleJob(job2, trigger2);
                sched.Start();
            }
            catch(Exception ex)
            {
                LogHelper.Error("Quartz 任务启动失败", ex);
            }
   
        }
        public static void ShutDownTask()
        {
            try
            {
                if(sched==null)
                {
                    return;
                }
                if(!sched.IsShutdown)
                {
                    sched.Shutdown();
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error("Quartz 任务停止失败", ex);
            }
        }
    }
}
