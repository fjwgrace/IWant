using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiseMan.Business.Common;

namespace JAUploadForWinform.Model
{
    class ApiUrls
    {
        public static string SecretKey = "shjjdianjing";

        private static string loginURL;
        public static string LoginURL
        {
            get {
                if(loginURL==null)
                {
                    LoadURL();
                }
                return loginURL; }
        }

        private static string logoutURL;
        public static string LogoutURL
        {
            get { return logoutURL; }
        }

        private static string realTimeURL;
        public static string RealTimetURL
        {
            get
            {
                if(realTimeURL==null)
                {
                    LoadURL();
                }
                return realTimeURL;
            }
        }
        private static string imosURL;
        public static string IMOSURL
        {
            get
            {
                if (imosURL==null)
                {
                    LoadURL();
                }
                return imosURL;
            }
        }
        private static string realTimeReportURL;
        public static string RealTimeReportURL
        {

            get {
                if(realTimeReportURL==null)
                {
                    LoadURL();
                }
                return realTimeReportURL;
            }
        }
        private static string resQueryURL;
        public static string ResQueryURL
        {
            get
            {
                if(resQueryURL == null)
                {
                    LoadURL();
                }
                return resQueryURL;
            }
        }

        private static string vehicleQueryURL;
        public static string VehicleQueryURL
        {
            get {
                if(vehicleQueryURL==null)
                {
                    LoadURL();
                }
                return vehicleQueryURL; }
        }

        private static string vehicleAlarmURL;
        public static string VehiceleAlarmURL
        {
            get {
                if(vehicleAlarmURL==null)
                {
                    LoadURL();
                }
                return vehicleAlarmURL; }
        }
        private static int limitNum;
        public static int LimitNum
        {
            get { return limitNum; }
        }

        public static void SaveAppSettings(string realTimeIP,string imosIP,bool limit,int limitNum)
        {
            //获取Configuration对象
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //写入<add>元素的Value
            config.AppSettings.Settings["reportUrl"].Value = realTimeIP;

            //写入<add>元素的Value
            config.AppSettings.Settings["imosUrl"].Value = imosIP;

          if(limit)
            {
                //根据Key读取<add>元素的Value
                string name3 = config.AppSettings.Settings["limitNum"].Value;
                //写入<add>元素的Value
                config.AppSettings.Settings["limitNum"].Value = limitNum.ToString();
            }
          else
            {
                config.AppSettings.Settings["limitNum"].Value = limitNum.ToString();
            }
            //一定要记得保存，写不带参数的config.Save()也可以
            config.Save(ConfigurationSaveMode.Modified);
            //刷新，否则程序读取的还是之前的值（可能已装入内存）
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");

            LoadURL(); //重新加载IP地址
        }
        private static void LoadURL()
        {
            try
            {
                realTimeURL = ConfigurationManager.AppSettings["reportUrl"].ToString();
                imosURL = ConfigurationManager.AppSettings["imosUrl"].ToString();
                realTimeReportURL = string.Format("http://{0}/hlwService/save", realTimeURL);
                loginURL = string.Format("http://{0}:8088/VIID/login", imosURL);
                logoutURL = string.Format("http://{0}:8088/VIID/logout", imosURL);
                resQueryURL = string.Format("http://{0}:8088/VIID/query", imosURL);
                vehicleQueryURL = string.Format("http://{0}:8088/VIID/query/vehicle", imosURL);
                vehicleAlarmURL = string.Format("http://{0}:8088/VIID/query/vehicle/alarm", imosURL);
                limitNum = int.Parse(ConfigurationManager.AppSettings["limitNum"]);
            }
            catch(Exception ex)
            {
                LogHelper.Error("加载服务器路径出错", ex);
            }
          
        }
    }
}
