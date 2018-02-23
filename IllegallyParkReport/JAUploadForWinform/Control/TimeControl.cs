using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JAUploadForWinform.Business;

namespace JAUploadForWinform
{
    public enum TimeType
    {
        Day,
        Month
    }
    public partial class TimeControl : UserControl
    {
        public event RemoveControl EventRemove;
        TimeType CustomTimeType;

        public TimeType timeType
        {
            set { this.CustomTimeType = value; }
            get { return CustomTimeType; }
        }

        public TimeControl()
        {
            InitializeComponent();
            this.Load += TimeControl_Load;
        }

        private void TimeControl_Load(object sender, EventArgs e)
        {
          if(CustomTimeType==TimeType.Day)
            {
                dtpStart.Format = DateTimePickerFormat.Custom;
                dtpStart.CustomFormat = "HH:mm:ss";
                dtpEnd.Format = DateTimePickerFormat.Custom;
                dtpEnd.CustomFormat = "HH:mm:ss";
            }
          else
            {
                dtpStart.Format = DateTimePickerFormat.Custom;
                dtpStart.CustomFormat = "yyyy-MM-dd";
                dtpEnd.Format = DateTimePickerFormat.Custom;
                dtpEnd.CustomFormat = "yyyy-MM-dd";
            }
        }

        public DateTime DtStart
        {
            get { return DateTime.Parse(dtpStart.Value.ToString("yyyy/MM/dd HH:mm:ss")); }
        }
        public DateTime DtEnd
        {
            get { return DateTime.Parse(dtpEnd.Value.ToString("yyyy/MM/dd HH:mm:ss")); }
        }

        private void TimeControl_Paint(object sender, PaintEventArgs e)
        {
            Color borderColor = Color.FromArgb(216, 220, 222);
            Rectangle mytangle = new Rectangle(0, 0, this.Width, this.Height);
            ControlPaint.DrawBorder(e.Graphics, mytangle, borderColor, 1, ButtonBorderStyle.Solid,
                                   borderColor, 1, ButtonBorderStyle.Solid,
                                   borderColor, 1, ButtonBorderStyle.Solid,
                                   borderColor, 1, ButtonBorderStyle.Solid);

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if(EventRemove!=null)
            {
                EventRemove(this);
            }
        }
    }
}
