using DevComponents.DotNetBar.Metro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JAUploadForWinform.Presentation
{
    public partial class WaitingForm : MetroForm
    {
        private WaitingForm()
        {
            InitializeComponent();
            this.Load += WaitingForm_Load;
            this.FormClosing += WaitingForm_FormClosing;
        }
        private static WaitingForm instance;
        public static WaitingForm Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WaitingForm();
                    instance.StartPosition = FormStartPosition.CenterScreen;
                }
                return instance;
            }
        }
        public void CloseForm()
        {
            if(instance!=null)
            {
                this.Invoke((Action)delegate ()
                {
                    instance.Close();
                });
            }
        }
        private void WaitingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void WaitingForm_Load(object sender, EventArgs e)
        {
            crProgress.IsRunning = true;
        }
    }
}
