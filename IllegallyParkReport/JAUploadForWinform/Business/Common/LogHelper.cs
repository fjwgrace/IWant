using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiseMan.Business.Common
{
    public class LogHelper
    {
        private LogHelper()
        {

        }
        public static readonly log4net.ILog logInfo = log4net.LogManager.GetLogger("loginfo");
        public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");
        public static void Info (string message)
        {
                logInfo.Info(message);
                Console.WriteLine(message);
        }
        public static void Error (string message,Exception exception)
        {
                logerror.Error(message, exception);
                //Console.WriteLine(message+exception.Message);
               
        }
        public static void Error(string message)
        {
                logerror.Error(message);
                //Console.WriteLine(message);
        }
    }
}
