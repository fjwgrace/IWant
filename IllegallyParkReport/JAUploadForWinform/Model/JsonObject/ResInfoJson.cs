using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAUploadForWinform.Model.JsonObject
{
    public class ResInfoJson
    {
        public ResItemJson ResItemV1 { get; set; }
        public string OrgName { get; set; }
        public int ResAttribute { get; set; }
        public int DevEncodeSet { get; set; }
        public int VoiceStatus { get; set; }
        public int HasBrdSuRes { get; set; }
        public int IsBind { get; set; }
        public string ResBindCode { get; set; }
        public int SubTypeOfSubType { get; set; }
        public string Reserve { get; set; }
    }
}
