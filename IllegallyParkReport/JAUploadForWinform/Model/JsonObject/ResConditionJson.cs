using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAUploadForWinform.Model.JsonObject
{
    public class ResConditionJson
    {
        public int ResNum { get; set; }
        public List<ResCommonConditionJson> ResList { get; set; }
    }
    public class ResCommonConditionJson
    {
        public string ResCode { get; set; }
        public string OrgCode { get; set; }
        public string ResIdCode { get; set; }
        public string ResName { get; set; }
        public string OrgName { get; set; }
        public int ResSubType { get; set; }
        public int ResType { get; set; }
    }
}
