using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAUploadForWinform.Model.JsonObject
{
    public class ResItemJson
    {
        public string ResCode { get; set; }
        public string ResName { get; set; }
        public int ResType { get; set; }
        public int ResSubType { get; set; }
        public int ResStatus { get; set; }
        public int ResExtStatus { get; set; }
        public int ResIsBeShare { get; set; }
        public string OrgCode { get; set; }
        public int StreamNum { get; set; }
        public int ResIsForeign { get; set; }
    }
}
