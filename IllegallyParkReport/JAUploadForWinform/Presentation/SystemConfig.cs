using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using JAUploadForWinform.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WiseMan.Business.Common;

namespace JAUploadForWinform.Presentation
{
    public partial class SystemConfig : MetroForm
    {
        public SystemConfig()
        {
            InitializeComponent();
            this.Load += SystemConfig_Load;
        }

        private void SystemConfig_Load(object sender, EventArgs e)
        {
          if(!string.IsNullOrEmpty(ApiUrls.IMOSURL))
            {
                txtImosIP.Value = ApiUrls.IMOSURL;
            }
          if(!string.IsNullOrEmpty(ApiUrls.RealTimetURL))
            {
                txtRealIP.Value = ApiUrls.RealTimetURL;
            }
          if(ApiUrls.LimitNum!=-1)
            {
                rdbYes.Checked = true;
                txtLimit.Value = ApiUrls.LimitNum;
            }
          else
            {
                rdbNo.Checked = true;
                txtLimit.Enabled = false;
            }
        }

        private void rdbYes_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbYes.Checked)
            {
                txtLimit.Enabled = true;
            }
        }

        private void rdbNo_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbNo.Checked)
            {
                txtLimit.Enabled = false;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRealIP.Value))
                {
                    MessageBoxEx.Show("即时告知平台IP未配置");
                    return;
                }
                if (string.IsNullOrEmpty(txtImosIP.Value))
                {
                    MessageBoxEx.Show("违法数据平台IP未配置");
                    return;
                }
                if (rdbYes.Checked)
                {
                    if (txtLimit.Value == 0)
                    {
                        MessageBoxEx.Show("最大限制上传数量不能为0");
                        return;
                    }
                    ApiUrls.SaveAppSettings(txtRealIP.Value, txtImosIP.Value, true, txtLimit.Value);
                }
                else
                {
                    ApiUrls.SaveAppSettings(txtRealIP.Value, txtImosIP.Value, false, -1);
                }
            }
            catch(Exception ex)
            {
                MessageBoxEx.Show("保存失败");
                LogHelper.Error("系统配置保存失败", ex);
                return;
            }
            this.Hide();
            MessageBoxEx.Show("保存成功");
            this.DialogResult = DialogResult.OK;
    
        }
    }
}
