using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using JAUploadForWinform.Business;
using JAUploadForWinform.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WiseMan.Business.Common;

namespace JAUploadForWinform.Presentation
{
    public partial class LoginForm : MetroForm
    {
        public LoginForm()
        {
            InitializeComponent();
            this.Load += LoginForm_Load;
        }
        Dictionary<string, User> users = new Dictionary<string, User>();

        private void LoginForm_Load(object sender, EventArgs e)
        {
            //  读取配置文件寻找记住的用户名和密码
            FileStream fs = new FileStream("data.bin", FileMode.OpenOrCreate);
            if (fs.Length > 0)
            {
                BinaryFormatter bf = new BinaryFormatter();
                users = bf.Deserialize(fs) as Dictionary<string, User>;
                foreach (User user in users.Values)
                {
                    this.cboUserName.Items.Add(user.UserName);
                }

                for (int i = 0; i < users.Count; i++)
                {
                    if (this.cboUserName.Text != "")
                    {
                        if (users.ContainsKey(this.cboUserName.Text))
                        {
                            this.txtPwd.Text = users[this.cboUserName.Text].Password;
                            this.cboKeepPwd.Checked = true;
                        }
                    }
                }
            }
            fs.Close();
            //  用户名默认选中第一个
            if (this.cboUserName.Items.Count > 0)
            {
                this.cboUserName.SelectedIndex = this.cboUserName.Items.Count - 1;
            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = this.cboUserName.Text.Trim();
            string password = this.txtPwd.Text.Trim();

            User user = new User();
            try
            {
                FileStream fs = new FileStream("data.bin", FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                user.UserName = username;
                if (this.cboKeepPwd.Checked)       //  如果单击了记住密码的功能
                {   //  在文件中保存密码
                    user.Password = password;
                }
                else
                {   //  不在文件中保存密码
                    user.Password = "";
                }

                //  选在集合中是否存在用户名 
                if (users.ContainsKey(user.UserName))
                {
                    users.Remove(user.UserName);
                }
                users.Add(user.UserName, user);
                //要先将User类先设为可以序列化(即在类的前面加[Serializable])
                bf.Serialize(fs, users);
                //user.Password = this.PassWord.Text;
                fs.Close();

                IMOS.UserName = user.UserName;
                IMOS.UserPwd = user.Password;
                bool result = false;
                try
                {
                  result =IMOS.Login();
                }
                catch (Exception ex)
                {
                    LogHelper.Error("登录失败", ex);
                    MessageBoxEx.Show("登录失败");
                }


                if (result)
                {
                    MainForm mainForm = new MainForm();
                    this.Hide();
                    mainForm.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error("登录失败", ex);
            }
        }

        private void cboUserName_SelectedValueChanged(object sender, EventArgs e)
        {
            //  首先读取记住密码的配置文件
            //FileStream fs = new FileStream("data.bin", FileMode.OpenOrCreate);

            //if (fs.Length > 0)
            //{
            //    BinaryFormatter bf = new BinaryFormatter();

            //    users = bf.Deserialize(fs) as Dictionary<string, User>;

                for (int i = 0; i < users.Count; i++)
                {
                    if (this.cboUserName.Text != "")
                    {
                        if (users.ContainsKey(cboUserName.Text) && users[cboUserName.Text].Password != "")
                        {
                            this.txtPwd.Text = users[cboUserName.Text].Password;
                            this.cboKeepPwd.Checked = true;
                        }
                        else
                        {
                            this.txtPwd.Text = "";
                            this.cboKeepPwd.Checked = false;
                        }
                    }
                }
            //}
            //fs.Close();

        }
    }
}
