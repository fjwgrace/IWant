using Commons;
using Core.Common;
using Core.Net;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using JAUploadForWinform.Business;
using JAUploadForWinform.Control;
using JAUploadForWinform.Model;
using JAUploadForWinform.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WiseMan.Business.Common;

namespace JAUploadForWinform
{
    public partial class MainForm :MetroForm
    {
        public delegate void datagridviewcheckboxHeaderEventHander(object sender, datagridviewCheckboxHeaderEventArgs e);

        private string SearchCondition = string.Empty;
        private int TotalCount;

        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
            AddCustomCheckBox();
            this.pageNavControl1.EventPaging += PageNavControl1_EventPaging;
            this.Width = ScreenManage.ScreenWidth;
            this.Height = ScreenManage.ScreenHeight;
            RealTimeReportClient.EventNoticeNum += RealTimeReportClient_EventNoticeNum;
            TollgateFilterManage.EventNoticeStatus += TollgateFilterManage_EventNoticeStatus;
        }

        private void TollgateFilterManage_EventNoticeStatus(string message)
        {
            MessageBoxEx.Show(message);
        }

        private void RealTimeReportClient_EventNoticeNum(int num1, int num2)
        {
            this.Invoke((Action)delegate ()
            {
                this.lblSuccess.Text = num1.ToString();
                this.lblFailCount.Text = num2.ToString();
            });
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBoxEx.Show("确定退出?","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes)
            {
                QuartzManage.ShutDownTask(); //停止任务
                IMOS.Logout();                //退出登录
            }
            else
            {
                e.Cancel = true;
            }
                      

        }

        private int PageNavControl1_EventPaging(EventPagingArg e)
        {
            Task tasks = new Task((Action)delegate ()
              {
                  DgvBind();
                  WaitingForm.Instance.CloseForm();
              });
            tasks.Start();
            WaitingForm.Instance.ShowDialog();
            return TotalCount;
        }
        private void DgvBind()
        {
           try
            {
                int pageRow = this.pageNavControl1.PageSize;
                int pageOffset = 0;
                if(this.pageNavControl1.PageCurrent==0)
                {
                    pageOffset = 0;
                }
                else
                {
                    pageOffset = (this.pageNavControl1.PageCurrent - 1) * pageRow;
                }
                using (dbEntities db = new dbEntities())
                {
                    var kks = (from item in db.kkinfo
                               where item.kkName.Contains(SearchCondition)
                               orderby item.createTime descending
                               select item).Skip(pageOffset).Take(pageRow);

                    TotalCount = db.kkinfo.Where(m=>m.kkName.Contains(SearchCondition)).Count();

                    this.Invoke((Action)delegate()
                    {
                        this.dataGridViewX1.Rows.Clear();
                        DataGridViewCheckBoxColumn checkCell = dataGridViewX1.Columns["cmnCheck"] as DataGridViewCheckBoxColumn;
                        CustomDatagridviewCheckboxHeaderCell cell = checkCell.HeaderCell as CustomDatagridviewCheckboxHeaderCell;
                        if(cell.ClientCheck)
                        {
                            cell.OnCheckBoxClicked -= ch_OnCheckBoxClicked;
                            AddCustomCheckBox();
                        }
                    });
               
                    object[] objs = new object[13];
                    List<DataGridViewRow> rows = new List<DataGridViewRow>();
                    foreach (var item in kks)
                    {
                        objs[0] = false;
                        objs[1] = item.kkCode;
                        objs[2] = item.kkName;
                        objs[3] = item.orgName;
                        string startTime1 = item.startTime1.HasValue ? ((DateTime)item.startTime1).ToString("HH:mm:ss") : string.Empty;
                        string endTime1 = item.endTime1.HasValue ? ((DateTime)item.endTime1).ToString("HH:mm:ss") : string.Empty;
                        if (!string.IsNullOrEmpty(startTime1))
                        {
                            objs[4] = string.Format("{0}-{1}", startTime1, endTime1);
                        }
                        else
                        {
                            objs[4] = string.Empty;
                        }
                        string startTime2= item.startTime2.HasValue ? ((DateTime)item.startTime2).ToString("HH:mm:ss") : string.Empty;
                        string endTime2= item.endTime2.HasValue ? ((DateTime)item.endTime2).ToString("HH:mm:ss") : string.Empty;
                        string startTime3= item.startTime3.HasValue ? ((DateTime)item.startTime3).ToString("HH:mm:ss") : string.Empty;
                        string endTime3= item.endTime3.HasValue ? ((DateTime)item.endTime3).ToString("HH:mm:ss") : string.Empty;
                        if (!string.IsNullOrEmpty(startTime2))
                        {
                            objs[5] = string.Format("{0}-{1}", startTime2, endTime2);
                        }
                        else
                        {
                            objs[5] = string.Empty;
                        }
                        if(!string.IsNullOrEmpty(startTime3))
                        { 
                            objs[6] = string.Format("{0}-{1}", startTime3, endTime3);
                        }
                        else
                        {
                            objs[6] = string.Empty;
                        }
                        string dayStart = item.tempDayStart.HasValue ? ((DateTime)item.tempDayStart).ToString("yyyy/MM/dd") : string.Empty;
                        string dayEnd= item.tempDayEnd.HasValue ? ((DateTime)item.tempDayEnd).ToString("yyyy/MM/dd") : string.Empty;
                        if(!string.IsNullOrEmpty(dayStart))
                        {
                            objs[7] = string.Format("{0}-{1}", dayStart, dayEnd);
                        }
                        else
                        {
                            objs[7] = string.Empty;
                        }
                        string tempStartTime1 = item.tempStartTime1.HasValue ? ((DateTime)item.tempStartTime1).ToString("HH:mm:ss") : string.Empty;
                        string tempEndTime1= item.tempEndTime1.HasValue ? ((DateTime)item.tempEndTime1).ToString("HH:mm:ss") : string.Empty;
                        string tempStartTime2 = item.tempStartTime2.HasValue ? ((DateTime)item.tempStartTime2).ToString("HH:mm:ss") : string.Empty;
                        string tempEndTime2 = item.tempEndTime2.HasValue ? ((DateTime)item.tempEndTime2).ToString("HH:mm:ss") : string.Empty;
                        string tempStartTime3 = item.tempStartTime3.HasValue ? ((DateTime)item.tempStartTime3).ToString("HH:mm:ss") : string.Empty;
                        string tempEndTime3 = item.tempEndTime3.HasValue ? ((DateTime)item.tempEndTime3).ToString("HH:mm:ss") : string.Empty;
                        if (!string.IsNullOrEmpty(tempStartTime1))
                        {
                            objs[8]= string.Format("{0}-{1}", tempStartTime1, tempEndTime1);
                        }
                        else
                        {
                            objs[8] = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(tempStartTime2))
                        {
                            objs[9] = string.Format("{0}-{1}", tempStartTime2, tempEndTime2);
                        }
                        else
                        {
                            objs[9] = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(tempStartTime3))
                        {
                            objs[10] = string.Format("{0}-{1}", tempStartTime3, tempEndTime3);
                        }
                        else
                        {
                            objs[10] = string.Empty;
                        }
                        objs[11] = item.createTime.ToString("yyyy-MM-dd HH:mm:ss");
                        objs[12] = item.creator;
                        this.Invoke((Action)delegate ()
                        {
                            this.dataGridViewX1.Rows.Add(objs);
                        });
                    }   
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error("列表加载失败", ex);
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            Task task = new Task((Action)delegate ()
              {
                  TreeManage tm = new TreeManage();
                  tm.LoadOrg();
                  tm.LoadCameras();
                  CodeParse.LoadXml();
                  TollgateFilterManage.LoadFilters();
                  ColorManage.LoadColorConverter();
                  WaitingForm.Instance.CloseForm();
              });
            task.Start();
            WaitingForm.Instance.ShowDialog();
            this.pageNavControl1.Bind();
            QuartzManage.dataTimeBegin = DateTime.Now.AddSeconds(-30);
            QuartzManage.StartTask();
        }
#region dataGridView 操作
        void ch_OnCheckBoxClicked(object sender, datagridviewCheckboxHeaderEventArgs e)
        {
            dataGridViewX1.EndEdit();
            foreach (DataGridViewRow dgvRow in dataGridViewX1.Rows)
            {
                if (e.CheckedState)
                {
                    dgvRow.Cells[0].Value = true;
                }
                else
                {
                    dgvRow.Cells[0].Value = false;
                }
            }
        }
        void AddCustomCheckBox()
        {
            CustomDatagridviewCheckboxHeaderCell ch = new CustomDatagridviewCheckboxHeaderCell();
            ch.OnCheckBoxClicked += ch_OnCheckBoxClicked;
            DataGridViewCheckBoxColumn checkboxCol = dataGridViewX1.Columns["cmnCheck"] as DataGridViewCheckBoxColumn;
            checkboxCol.HeaderCell = ch;
            checkboxCol.HeaderCell.Value = "";//消除列头checkbox旁出现的文字  
        }
        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TaskConfigForm tcf = new TaskConfigForm();
           if(tcf.ShowDialog()==DialogResult.Yes)
            {
                try
                {
                    //新增记录
                    //using (dbEntities db = new dbEntities())
                    //{
                    //    db.kkinfo.AddRange(tcf.SelctKK);
                    //    db.SaveChanges();
                    //}
                    TollgateFilterManage.AddFilters(tcf.SelctKK);
                    //刷新列表
                    ReloadData();
                }
                catch(Exception ex)
                {
                    MessageBoxEx.Show("添加失败，请确保数据库连接正常，并确保新添加的无重复项");
                    LogHelper.Error("新增失败", ex);
                }
                finally
                {
                    tcf.Dispose();
                }
            }
        }
        private void ReloadData()
        {
            this.pageNavControl1.PageCurrent = 0;
            txtCond.Text = string.Empty;
            SearchCondition = txtCond.Text;
            this.pageNavControl1.Bind();
        }
        private void btnConfig_Click(object sender, EventArgs e)
        {
            SystemConfig config = new SystemConfig();
            config.ShowDialog();
            config.Dispose();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.pageNavControl1.PageCurrent = 0;
            SearchCondition = txtCond.Text;
            this.pageNavControl1.Bind();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
          if( MessageBoxEx.Show("确定移除?","警告",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
          {
               try
                {
                     List<string> infos = new List<string>();
                     foreach(DataGridViewRow row in dataGridViewX1.Rows)
                     {
                        bool result = (bool)row.Cells[0].EditedFormattedValue;
                        if(result)
                        {
                            infos.Add(row.Cells["cmnCode"].Value.ToString());
                        }
                     }
                    using (dbEntities db = new dbEntities())
                    {
                        var items = from kk in db.kkinfo
                                    where infos.Contains(kk.kkCode)
                                    select kk;
                        db.kkinfo.RemoveRange(items);
                        db.SaveChanges();
                    }
                    //从字典中移除筛选项
                    TollgateFilterManage.RemoveFilters(infos);
                    ReloadData();
                }
                catch(Exception ex)
                {
                    LogHelper.Error("删除记录出错", ex);
                }
          }
        }
        bool started = false;

        FailListTotalForm FailListForm;

        private void btnViewFail_Click(object sender, EventArgs e)
        {
           if(FailListForm!=null)
            {
                FailListForm.ShowDialog();
            }
           else
            {
                FailListForm = new FailListTotalForm();
                FailListForm.ShowDialog();
            }
           if(FailListForm.Uploading==false)
            {
                FailListForm.Dispose();
                FailListForm = null;
            }
        }
    }
}
