using JAUploadForWinform.Business;
using JAUploadForWinform.Model;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WiseMan.Business.Common;
using DevComponents.DotNetBar;
using DevComponents.AdvTree;
using AdvNode = DevComponents.AdvTree.Node;
using System.Diagnostics;
using JAUploadForWinform.Model.JsonObject;
using Core.Common;
using System.Timers;

namespace JAUploadForWinform
{
    public partial class Demo : Form
    {
        System.Timers.Timer globalTimer;
        DateTime InitTime = DateTime.Parse("2018-1-25 13:10:00");

        public Demo()
        {
            InitializeComponent();
           
            ColorManage.LoadColorConverter();
            CodeParse.LoadXml();
            globalTimer = new System.Timers.Timer();
            globalTimer.Interval = 30000;
            globalTimer.Elapsed += GlobalTimer_Elapsed;
        }

        private void GlobalTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            AlarmManage.QueryAlarm(DateTime.Now.AddMinutes(-1).AddSeconds(-5), DateTime.Now.AddSeconds(-5));
        }

        public static IScheduler sched = null;
        static ISchedulerFactory schedf = new StdSchedulerFactory();

        private void button1_Click(object sender, EventArgs e)
        {
            IMOS.UserName = "loadmin";
            IMOS.UserPwd = "JAWTadmin123";
            IMOS.Login();
        }
        //加载树
        private void button2_Click(object sender, EventArgs e)
        {
            TreeManage tm = new TreeManage();
            tm.LoadOrg();
            tm.LoadCameras();
            foreach(KeyValuePair<string , ResourceItem> item in TreeManage.Organazations)
            {
                Console.WriteLine(item.Key + ":" + item.Value.ResName);
            }
            Console.WriteLine("相机总数:" + TreeManage.Cameras.Count);
        }
        
        //启动任务
        private void button3_Click(object sender, EventArgs e)
        {
            sched = schedf.GetScheduler();
            //创建出来一个具体的作业
            IJobDetail job = JobBuilder.Create<JobDemo>().Build();
            //NextGivenSecondDate：如果第一个参数为null则表名当前时间往后推迟2秒的时间点。
            DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(DateTime.Now.AddSeconds(1), 2);
            DateTimeOffset endTime = DateBuilder.NextGivenSecondDate(DateTime.Now.AddYears(2), 3);
            //创建并配置一个触发器
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create().StartAt(startTime)
                                        .WithCronSchedule("0 0/1 * ? * *")
                                        .Build();
            sched.ScheduleJob(job, trigger);
            //开始运行
            sched.Start();
        }

        private void Demo_Load(object sender, EventArgs e)
        {
            TollgateFilterManage.LoadFilters();
        }

        private void Demo_FormClosing(object sender, FormClosingEventArgs e)
        {
           if(IMOS.Logout())
            {
                LogHelper.Info("退出成功");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sched.Shutdown();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TreeManage tm = new TreeManage();
            tm.LoadCamerasMutiple();
        }
        Stopwatch watch = new Stopwatch();
        private void button6_Click(object sender, EventArgs e)
        {
            watch.Start();
            IEnumerable<KeyValuePair<string,ResourceItem>> itemList = TreeManage.Organazations.Where(i => i.Value.OrgCode == "iccsid"); //找到根节点

            // //加载摄像机树
            AdvNode firstNode = new AdvNode(itemList.First().Value.OrgName); //设置跟节点
            firstNode.CheckBoxVisible = false;
            ResourceItem rootItem = new ResourceItem { ResCode = "iccsid", ResName = firstNode.Text, ResType = EmResType.IMOS_TYPE_LOCAL_DOMAIN };
            firstNode.DataKey = rootItem;
            AddSubOrg(firstNode);
            advTree1.Nodes.Add(firstNode);
            Console.WriteLine("加载树耗时：" + watch.ElapsedMilliseconds + "毫秒");
            watch.Stop();
        }
        private void AddSubOrg(AdvNode node)
        {
            ResourceItem resItem = node.DataKey as ResourceItem;
            if (resItem != null)
            {
                IEnumerable<KeyValuePair<string,ResourceItem>> itemList = TreeManage.Organazations.Where(i => i.Value.OrgCode == resItem.ResCode);
                Console.WriteLine(string.Format("OrgCode={0}的有{1}个", resItem.ResCode,itemList.Count()));
                if (itemList == null || itemList.Count() == 0)  //如果组织加载完了，则加载相机
                {
                    AddSubCam(node);
                }
                foreach (KeyValuePair<string, ResourceItem> item in itemList)
                {
                    AdvNode subNode = new AdvNode(item.Value.ResName);
                    subNode.Text = item.Value.ResName;
                    subNode.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.CheckBox;
                    subNode.CheckBoxVisible = true;
                    ResourceItem rootItem = new ResourceItem { ResCode = item.Key, ResName = item.Value.ResName, ResType = EmResType.IMOS_TYPE_ORG };
                    subNode.DataKey = rootItem;
                    AddSubOrg(subNode);          //遍历加载所有组织节点
                    node.Nodes.Add(subNode);
                }
            }
        }
        private void AddSubCam(AdvNode node)
        {
            ResourceItem resItem = node.DataKey as ResourceItem;
            if (resItem != null)
            {
                IEnumerable<KeyValuePair<string, ResourceItem>> itemList = TreeManage.Cameras.Where(i => i.Value.OrgCode == resItem.ResCode);

                foreach (KeyValuePair<string, ResourceItem> item in itemList)
                {
                    AdvNode subNode = new AdvNode(item.Value.ResName);
                    subNode.Text = item.Value.ResName;
                    subNode.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.CheckBox;
                    subNode.CheckBoxVisible = true;
                    ResourceItem rootItem = new ResourceItem { ResCode = item.Key, ResName = item.Value.ResName, ResType = EmResType.IMOS_TYPE_TOLLGATE };
                    subNode.DataKey = rootItem;
                    node.Nodes.Add(subNode);
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            AlarmManage.QueryAlarm(dateTimePicker2.Value, dateTimePicker3.Value);
        }

        private void btnUploadGz_Click(object sender, EventArgs e)
        {
            List<AlarmInfoJson> infos = new List<AlarmInfoJson>();
            //for (int i = 0; i < 1; i++)
            //{
                AlarmInfoJson info = new AlarmInfoJson();
                info.PassTime = DateTime.Parse("2018-1-23 07:59:00").ToString();
                info.LicensePlateList = "[\"浙A12345\",\"浙A12345\"]";
                info.LicensePlateColor = "2";
                info.PlaceName = "测试地点";
                info.DeptCode = "310600";
                info.TollgateCode = "B3WT-026";
                info.TollgateName = "JA30005A-长安路天目西路北约100米";
                infos.Add(info);

            AlarmInfoJson info2 = new AlarmInfoJson();
            info2.PassTime = DateTime.Parse("2018-1-23 12:19:00").ToString();
            info2.LicensePlateList = "[\"浙A12345\",\"浙A12345\"]";
            info2.LicensePlateColor = "2";
            info2.PlaceName = "测试地点";
            info2.DeptCode = "310600";
            info2.TollgateCode = "B3WT-026";
            info2.TollgateName = "JA30005A-长安路天目西路北约100米";
            infos.Add(info2);
            //}
            new RealTimeReportClient().SendToCenter(infos, true);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Console.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AlarmManage.QueryAlarm(DateTime.Now.AddMinutes(-2), DateTime.Now.AddMinutes(-1));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("加密后："+DESEncrypt.Encrypt("1234567890", ApiUrls.SecretKey));
            //Console.WriteLine("解密后："+DESEncrypt.Decrypt("05ad0885fbdcdbd61dbf3f33b9a8cc4b", ApiUrls.SecretKey));
        }
        private void button9_Click(object sender, EventArgs e)
        {
            globalTimer.Start();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            globalTimer.Stop();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            List<AlarmInfoJson> infos = new List<AlarmInfoJson>();

            AlarmInfoJson info = new AlarmInfoJson();
            info.PassTime = DateTime.Parse("2018-1-25 07:59:00").ToString("yyyy-MM-dd HH:mm:ss");
            info.LicensePlateList = "[\"浙A12345\",\"浙A12345\"]";
            info.LicensePlateColor = "2";
            info.PlaceName = "测试地点";
            info.DeptCode = "310600";
            info.TollgateCode = "B3WT-026";
            info.TollgateName = "JA30005A-长安路天目西路北约100米";
            infos.Add(info);
            new RealTimeReportClient().SendToCenter(infos, false);

        }

        private void button12_Click(object sender, EventArgs e)
        {

            List<AlarmInfoJson> infos = new List<AlarmInfoJson>();

            for (int i = 0; i < 10; i++)
            {
                AlarmInfoJson info = new AlarmInfoJson();
                info.PassTime = DateTime.Now.AddMinutes(1 + i).ToString("yyyy-MM-dd HH:mm:ss");
                info.LicensePlateList = "[\"浙A12345\",\"浙A12345\"]";
                info.LicensePlateColor = "2";
                info.PlaceName = "测试地点";
                info.DeptCode = "310600";
                info.TollgateCode = "B3WT-026";
                info.TollgateName = "JA30005A-长安路天目西路北约100米";
                infos.Add(info);
            }
            new RealTimeReportClient().SendToCenter(infos, false);
        }
    }
    public class JobDemo:IJob
    {
        DateTime InitTime = DateTime.Parse("2018-1-25 13:10:00");
        /// <summary>
        /// 这里是作业调度每次定时执行方法
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            //IMOS.QueryAlarm(DateTime.Now.AddMinutes(-1), DateTime.Now);
            //AlarmManage.QueryAlarm(DateTime.Now.AddMinutes(-1).AddSeconds(-5), DateTime.Now.AddSeconds(-5));
            //AlarmManage.QueryAlarm(DateTime.Parse("2018-1-25 09:00:55"), DateTime.Parse("2018-1-25 10:10:55"));
            InitTime = InitTime.AddMinutes(1);
            AlarmManage.QueryAlarm(InitTime, InitTime.AddMinutes(1));
        }
    }
}
