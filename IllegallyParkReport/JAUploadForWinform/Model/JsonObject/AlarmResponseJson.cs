using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAUploadForWinform.Model.JsonObject
{
    public class AlarmResponseJson
    {
        public string ErrMsg { get; set; }
        public int ErrCode { get; set; }
        public RspResultJson Result { get; set; }
    }
}
