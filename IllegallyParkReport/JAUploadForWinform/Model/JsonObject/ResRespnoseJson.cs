using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAUploadForWinform.Model.JsonObject
{
    public class ResResponseJson
    {
        public int ErrCode { get; set; }
        public string ErrMsg { get; set; }
        public ResResponseResultJson Result { get; set; }
    }
    public class ResResponseResultJson
    {
        public RspPageInfoJson RspPageInfo { get; set; }
        public List<ResInfoJson> InfoList { get; set; }
    }
}
