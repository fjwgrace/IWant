using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAUploadForWinform.Model
{
    public class RealTimeReportJson
    {
        /// <summary>
        /// 经过时间，精确到秒，格式yyyy-MM-dd hh:mm:ss
        /// </summary>
        public string JGSJ { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
        public string HPHM { get; set; }
        /// <summary>
        /// 号牌颜色
        /// </summary>
        public string HPYS { get; set; }
        /// <summary>
        /// 违法地点
        /// </summary>
        public string WFDD { get; set; }
        /// <summary>
        /// 违法类型
        /// </summary>
        public string WFLX { get; set; }
        /// <summary>
        /// 采集机关
        /// </summary>
        public string XZQH { get; set; }
    }

    public class RealTimeReportResponseJson
    {
        public string ZT { get; set; }
        public string CWMS { get; set; }
    }
}
