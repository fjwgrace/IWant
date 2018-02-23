using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAUploadForWinform.Model.JsonObject.CommonQuery
{
    public class ConditionJson
    {
        /// <summary>
        /// 通用查询添加个数，多个查询条件之间与关系
        /// </summary>
        public int ItemNum { get; set; }
        /// <summary>
        /// 通用查询条件
        /// </summary>
        public List<CommonConditionJson> Condition { get; set; }
        /// <summary>
        /// 是否查询总数
        /// </summary>
        public int QueryCount { get; set; }
        /// <summary>
        /// 分页起始序号
        /// </summary>
        public int PageFirstRowNumber { get; set; }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageRowNum { get; set; }
    }
}
