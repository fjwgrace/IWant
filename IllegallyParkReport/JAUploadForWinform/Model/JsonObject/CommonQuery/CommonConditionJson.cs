using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAUploadForWinform.Model.JsonObject.CommonQuery
{
   public class CommonConditionJson
    {
        public int QueryType { get; set; }
        public int LogicFlag { get; set; }
        public string QueryData { get; set; }
    }
    public enum EmLogicFlag
    {
        EQUAL_FLAG = 0, /**<等于 */
        GREAT_FLAG = 1, /**<大于 */
        LITTLE_FLAG = 2, /**<小于 */
        GEQUAL_FLAG = 3, /**<大于等于 */
        LEQUAL_FLAG = 4, /**<小于等于 */
        LIKE_FLAG = 5, /**<模糊查询 */
        ASCEND_FLAG = 6, /**<升序 */
        DESCEND_FLAG = 7, /**<降序 */
        NEQUAL_FLAG = 8, /**<不等于 */
        NLIKE_FLAG = 13, /**< NOT LIKE */
        LOGIC_FLAG_MAX /**<无效值 */
    }
}
