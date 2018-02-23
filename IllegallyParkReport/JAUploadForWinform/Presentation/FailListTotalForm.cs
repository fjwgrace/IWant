using DevComponents.DotNetBar.Metro;
using JAUploadForWinform.Business;
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
    public partial class FailListTotalForm : MetroForm
    {
        private int TotalCount;
        bool uploading;

        int OneTimePageSize = 500;
        int OneTimeOffset = 0;
 

        RealTimeReportClient Client = new RealTimeReportClient();

        Dictionary<long,RealTimeReportJson> FailList = new Dictionary<long,RealTimeReportJson>();

        List<long> SuccessFiles = new List<long>();

        int Counter;

        public bool Uploading
        {
            get { return uploading; }
        }

        public FailListTotalForm()
        {
            InitializeComponent();
            this.pageNavControl1.EventPaging += PageNavControl1_EventPaging;
            this.FormClosing += FailListTotalForm_FormClosing;
            this.Load += FailListTotalForm_Load;
        }

        private void FailListTotalForm_Load(object sender, EventArgs e)
        {
            if(uploading==false)
            {
                this.pageNavControl1.Bind();
            }
        }
        private void FailListTotalForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(uploading)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private int PageNavControl1_EventPaging(Control.EventPagingArg e)
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
                if (this.pageNavControl1.PageCurrent == 0)
                {
                    pageOffset = 0;
                }
                else
                {
                    pageOffset = (this.pageNavControl1.PageCurrent - 1) * pageRow;
                }
                using (dbEntities db = new dbEntities())
                {
                    var kks = (from item in db.uploadfail
                               orderby item.passTime descending
                               select item).Skip(pageOffset).Take(pageRow);

                    TotalCount = db.uploadfail.Count();

                    this.Invoke((Action)delegate ()
                    {
                        this.dataGridViewX1.Rows.Clear();
                    });

                    object[] objs = new object[7];
                    List<DataGridViewRow> rows = new List<DataGridViewRow>();
                    foreach (var item in kks)
                    {
                        objs[0] = item.kkcode;
                        objs[1] = item.kkName;
                        objs[2] = item.passTime;
                        objs[3] = item.hphm;
                        objs[4] = ColorManage.GetCenterColor(item.hpys);
                        objs[5] = item.wfdd;
                        objs[6] = CodeParse.DptCollections["310600"];

                        this.Invoke((Action)delegate ()
                        {
                            this.dataGridViewX1.Rows.Add(objs);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("列表加载失败", ex);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            uploading = true;
            Task task = new Task((Action)delegate ()
              {
              int pageCount = GetFailList(out TotalCount); //第一次查询的时候返回总数
              if (TotalCount == 0)
              {
                    this.Invoke((Action)delegate ()
                    {
                        this.btnUpload.Enabled = true;
                    });
                    return;
              }
              Counter = 0;

              this.Invoke((Action)delegate ()
              {
                  this.progressBarX1.Maximum = pageCount;
              });
              SendToCenter();
              int loop = 1;
              int remainCount = TotalCount - pageCount;
              int temp;
              while (remainCount > 0)
              {
                  OneTimeOffset = loop * OneTimePageSize;
                  pageCount = GetFailList(out temp);
                  SendToCenter();
                  remainCount = TotalCount - pageCount;
                  loop++;
              }
              ChangeDB();

            this.Invoke((Action)delegate ()
                    {
                        this.btnUpload.Enabled = true;
                        this.pageNavControl1.PageCurrent = 0;
                        this.pageNavControl1.Bind();
                    });
                  uploading = false;
              });
            task.Start();
            btnUpload.Enabled = false;
        }
        private void ChangeDB()
        {
            try
            {
                if (SuccessFiles.Count <= 0)
                {
                    return;
                }
                using (dbEntities db = new dbEntities())
                {
                    var items = from item in db.uploadfail
                                where SuccessFiles.Contains(item.id)
                                select item;
                    db.uploadfail.RemoveRange(items);
                    db.SaveChanges();
                }
            }
            catch { }

        }
        private int GetFailList(out int totalCount)
        {
            List<uploadfail> failUploadList;
            //失败列表一次最多查询500条；
            using (dbEntities db = new dbEntities())
            {
                totalCount = db.uploadfail.Count();
                failUploadList = (from item in db.uploadfail
                                  orderby item.passTime descending
                                  select item).Skip(OneTimeOffset).Take(OneTimePageSize).ToList();
            }
            
            foreach (var item in failUploadList)
            {
                RealTimeReportJson json = new RealTimeReportJson();
                json.HPHM = item.hphm;
                json.HPYS = item.hpys;
                json.JGSJ = item.passTime.ToString("yyyy-MM-dd HH:mm:ss");
                json.WFDD = item.wfdd;
                json.WFLX = "违法停车";
                json.XZQH = item.cjjg;
                FailList.Add(item.id,json);
            }
            return failUploadList.Count;
        }
        private void SendToCenter()
        {
           foreach(var item in FailList)
            {
                List<RealTimeReportJson> list = new List<RealTimeReportJson>();
                list.Add(item.Value);
                if(Client.SendToRemote(list))
                {
                    Counter++;
                    this.Invoke((Action)delegate ()
                    {
                        this.progressBarX1.Value = Counter;

                    });
                    SuccessFiles.Add(item.Key);//如果成功了，要从失败列表中删除
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.pageNavControl1.PageCurrent = 0;
            this.pageNavControl1.Bind();
        }
    }
}
