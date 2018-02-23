using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JAUploadForWinform.Model
{
    public class TollgateFilter
    {
      public string Code { get; set; } 
      public string Name { get; set; }
      public DateTime? DtStart1 { get; set; }
      public DateTime? DtEnd1 { get; set; }
      public DateTime? DtStart2 { get; set; }
      public DateTime? DtEnd2 { get; set; }
      public DateTime? DtStart3 { get; set; }
      public DateTime? DtEnd3 { get; set; }
      public DateTime CreateTime { get; set; }
      public string Creator { get; set; }
    }
}
