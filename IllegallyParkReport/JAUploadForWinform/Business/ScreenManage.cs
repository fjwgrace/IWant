using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JAUploadForWinform.Business
{
    class ScreenManage
    {
        public static int ScreenWidth
        {
            get
            {
                return System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            }
        }
        public static int ScreenHeight
        {
            get
            {
                return System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            }
        }
    }
}
