using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace JAUploadForWinform.Business
{
    public delegate void NoticeStatus(string message);
    public delegate void NoticeNums(int num1, int num2);
    public delegate void RemoveControl(TimeControl ctrl);
}
