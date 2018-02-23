using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAUploadForWinform.Model.JsonObject
{
    public class NestConditionJson
    {
        public int NestConditionNum { get; set; }
        public NestCommonConditionJson NestConditionList { get; set; }
    }
    public class NestCommonConditionJson
    {
        public string QueryDataList { get; set; }
        public int LogicFlag { get; set; }
        public int QueryType { get; set; }
        public int QueryDataNum { get; set; }
    }
}
