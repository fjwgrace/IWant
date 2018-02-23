using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAUploadForWinform.Model.JsonObject
{
   public class RspResultJson
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public RspPageInfoJson RspPageInfo { get; set; }
        /// <summary>
        /// 告警车辆信息列表
        /// </summary>
        public List<AlarmInfoJson> InfoList { get; set; }
    }
}
