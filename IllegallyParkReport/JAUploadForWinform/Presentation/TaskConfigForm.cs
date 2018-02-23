using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using JAUploadForWinform.Business;
using JAUploadForWinform.Control;
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
using AdvNode = DevComponents.AdvTree.Node;
namespace JAUploadForWinform.Presentation
{
    public partial class TaskConfigForm : MetroForm
    {
        public TaskConfigForm()
        {
            InitializeComponent();
            this.advTree1.AfterCheck += AdvTree1_AfterCheck;
          
        }
        DateTime? startTime1, endTime1, startTime2, endTime2, startTime3, endTime3;
        DateTime? tempStartDay, tempEndDay;
        DateTime? tempStartTime1, tempEndTime1, tempStartTime2, tempEndTime2, tempStartTime3, tempEndTime3;

        private void AdvTree1_AfterCheck(object sender, DevComponents.AdvTree.AdvTreeCellEventArgs e)
        {
            try
            {
                AdvNode node = e.Cell.Parent;
                if (node.Nodes.Count > 0)
                {
                    bool NoFalse = true;
                    foreach (AdvNode tn in node.Nodes)
                    {
                        if (tn.Checked == false)
                        {
                            NoFalse = false;
                        }
                    }
                    if (node.Checked == true || NoFalse)
                    {
                        foreach (AdvNode tn in node.Nodes)
                        {
                            if (tn.Checked != node.Checked)
                            {
                                tn.Checked = node.Checked;
                            }
                        }
                    }
                }
                if (node.Parent != null && node.Parent is AdvNode)
                {
                    bool ParentNode = true;
                    foreach (AdvNode tn in node.Parent.Nodes)
                    {
                        if (tn.Checked == false)
                        {
                            ParentNode = false;
                        }
                    }
                    if (node.Parent.Checked != ParentNode && (node.Checked == false || node.Checked == true && node.Parent.Checked == false))
                    {
                        node.Parent.Checked = ParentNode;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
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

        private void BindToDgv()
        {
            dataGridViewX1.Rows.Clear();
            List<ResourceItem> resList = new List<ResourceItem>();
            try
            {
                foreach(AdvNode node in advTree1.CheckedNodes)
                {
                    ResourceItem res = node.DataKey as ResourceItem;
                    if(res==null)
                    {
                        continue;
                    }
                    if(res.ResType!=EmResType.IMOS_TYPE_TOLLGATE)
                    {
                        continue;
                    }
                    else
                    {
                        resList.Add(res);
                    }
                }
                Console.WriteLine(resList.Count);
                if(resList.Count>0)
                {
                    object[] objs = new object[5];
                    foreach ( var item in resList)
                    {
                        objs[0] = false;
                        objs[1] = item.ResCode;
                        objs[2] = item.ResName;
                        objs[3] = item.OrgName;
                        objs[4] = item.OrgCode;
                        this.dataGridViewX1.Rows.Add(objs);
                    }
                }
            }
            catch
            {

            }
        }

        private void TaskConfigForm_Load(object sender, EventArgs e)
        {
            this.pnlButton.BackColor = Color.White;
            this.pnlTimeFilter.BackColor = Color.White;
            this.pnlTimeFilterTemp.BackColor = Color.White;
            this.Width = ScreenManage.ScreenWidth - 100;
            this.Height = ScreenManage.ScreenHeight - 50;
            this.tableLayoutPanel1.BackColor = Color.White;
            this.Location = new Point((ScreenManage.ScreenWidth - this.Width) / 2, (ScreenManage.ScreenHeight - this.Height) / 2);
            //添加全选框
            AddCustomCheckBox();

            //加载树
            LoadTree();

            //绑定采集机关列表
            foreach(var item in CodeParse.DptCollections)
            {
                ComboBoxItem cbo = new ComboBoxItem(item.Key,item.Value);
                cboCjjg.Items.Add(cbo);
            }
            if(cboCjjg.Items.Count>3)
            {
                cboCjjg.SelectedIndex = 3;
            }
     

        }
        void LoadTree()
        {
            advTree1.Nodes.Clear();
            IEnumerable<KeyValuePair<string, ResourceItem>> itemList = TreeManage.Organazations.Where(i => i.Value.OrgCode == "iccsid"); //找到根节点
            if(itemList==null||itemList.Count()==0)
            {
                LogHelper.Info("卡口加载为空");
                return;
            }
            // //加载摄像机树
            AdvNode firstNode = new AdvNode(itemList.First().Value.OrgName); //设置跟节点
            firstNode.CheckBoxVisible = false;
            ResourceItem rootItem = new ResourceItem { ResCode = "iccsid", ResName = firstNode.Text, ResType = EmResType.IMOS_TYPE_LOCAL_DOMAIN };
            firstNode.DataKey = rootItem;
            AddSubOrg(firstNode);
            advTree1.Nodes.Add(firstNode);
            advTree1.ExpandAll();
        }

        void LoadTree(string txtCondition)
        {
            advTree1.Nodes.Clear();

            IEnumerable<KeyValuePair<string, ResourceItem>> itemList = TreeManage.Cameras.Where(i => i.Value.ResCode.Contains(txtCondition.TrimEnd())||i.Value.ResName.Contains(txtCondition.TrimEnd())); //找到符合条件的相机列表
            if (itemList == null || itemList.Count() == 0)
            {
                LogHelper.Info("卡口查询为空");
                return;
            }
            // //加载摄像机树
            AdvNode firstNode = new AdvNode(string.Format("\"{0}\"的查找结果",txtCondition)); //设置跟节点
            firstNode.CheckBoxVisible = true;


            foreach (KeyValuePair<string, ResourceItem> item in itemList)
            {
                AdvNode subNode = new AdvNode(item.Value.ResName);
                subNode.Text = item.Value.ResName;
                subNode.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.CheckBox;
                subNode.CheckBoxVisible = true;
                ResourceItem rootItem = new ResourceItem { ResCode = item.Key, ResName = item.Value.ResName, ResType = EmResType.IMOS_TYPE_TOLLGATE, OrgCode = item.Value.OrgCode, OrgName = item.Value.OrgName };
                subNode.DataKey = rootItem;
                firstNode.Nodes.Add(subNode);
            }
            advTree1.Nodes.Add(firstNode);
            advTree1.ExpandAll();
        }
        private void AddSubOrg(AdvNode node)
        {
            try
            {
                ResourceItem resItem = node.DataKey as ResourceItem;
                if (resItem != null)
                {
                    IEnumerable<KeyValuePair<string, ResourceItem>> itemList = TreeManage.Organazations.Where(i => i.Value.OrgCode == resItem.ResCode);

                    if (itemList == null || itemList.Count() == 0)  //如果组织加载完了，则加载相机
                    {
                        AddSubCam(node);
                    }
                    foreach (KeyValuePair<string, ResourceItem> item in itemList)
                    {
                        AdvNode subNode = new AdvNode(item.Value.ResName);
                        subNode.Text = item.Value.ResName;
                        subNode.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.CheckBox;
                        subNode.CheckBoxVisible = true;
                        ResourceItem rootItem = new ResourceItem { ResCode = item.Key, ResName = item.Value.ResName, ResType = EmResType.IMOS_TYPE_ORG };
                        subNode.DataKey = rootItem;
                        AddSubOrg(subNode);          //遍历加载所有组织节点
                        node.Nodes.Add(subNode);
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error("加载组织树出错", ex);
            }

        }

        private void AddSubCam(AdvNode node)
        {
            try
            {
                ResourceItem resItem = node.DataKey as ResourceItem;
                if (resItem != null)
                {
                    IEnumerable<KeyValuePair<string, ResourceItem>> itemList = TreeManage.Cameras.Where(i => i.Value.OrgCode == resItem.ResCode);

                    foreach (KeyValuePair<string, ResourceItem> item in itemList)
                    {
                        AdvNode subNode = new AdvNode(item.Value.ResName);
                        subNode.Text = item.Value.ResName;
                        subNode.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.CheckBox;
                        subNode.CheckBoxVisible = true;
                        ResourceItem rootItem = new ResourceItem { ResCode = item.Key, ResName = item.Value.ResName, ResType = EmResType.IMOS_TYPE_TOLLGATE, OrgCode = item.Value.OrgCode, OrgName = item.Value.OrgName };
                        subNode.DataKey = rootItem;
                        node.Nodes.Add(subNode);
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error("加载摄像机树节点出错", ex);
            }
          
        }

        private void btnAddFilter_Click(object sender, EventArgs e)
        {
            if(pnlTimeFilter.Controls.Count>=3)
            {
                MessageBoxEx.Show("最多只能添加3个筛选时间");
                return;
            }
            TimeControl tcl = new TimeControl();
            tcl.timeType = TimeType.Day;
            tcl.EventRemove += Tcl_EventRemove;
            pnlTimeFilter.Controls.Add(tcl);
        }

        private void Tcl_EventRemove(TimeControl ctrl)
        {
          foreach(System.Windows.Forms.Control ctr in pnlTimeFilter.Controls)
            {
                TimeControl tcl=ctr as TimeControl;
                if(tcl!=null)
                {
                    if (ctrl == tcl)
                    {
                        pnlTimeFilter.Controls.Remove(ctr);
                        break;
                    }
                }
            }
        }
        private void Tcl_EventRemoveV2(TimeControl ctrl)
        {
            foreach (System.Windows.Forms.Control ctr in pnlTimeFilter.Controls)
            {
                TimeControl tcl = ctr as TimeControl;
                if (tcl != null)
                {
                    if (ctrl == tcl)
                    {
                        if(ctrl.timeType==TimeType.Month)
                        {
                            pnlTimeFilterTemp.Controls.Clear();
                            break;
                        }
                        else
                        {
                            pnlTimeFilterTemp.Controls.Remove(ctr);
                            break;
                        }

                    }
                }
            }
        }
        List<kkinfo> selectKK = new List<kkinfo>();

        public List<kkinfo> SelctKK
        {
            get { return selectKK; }
        }



        private void tlsmOK_Click(object sender, EventArgs e)
        {
            BindToDgv();
        }

        private void btnAddTemp_Click(object sender, EventArgs e)
        {
            if (pnlTimeFilterTemp.Controls.Count >= 4)
            {
                MessageBoxEx.Show("最多只能添加3个筛选时间");
                return;
            }
            bool containMonth = false;
            foreach(System.Windows.Forms.Control ctl in pnlTimeFilterTemp.Controls)
            {
                TimeControl tmp = ctl as TimeControl;
                if(tmp.timeType==TimeType.Month)
                {
                    containMonth = true;
                    break;
                }
            }
            if(containMonth)
            {
                TimeControl tcl = new TimeControl();
                tcl.timeType = TimeType.Day;
                tcl.EventRemove += Tcl_EventRemoveV2;
                pnlTimeFilterTemp.Controls.Add(tcl);
            }
            else
            {
                TimeControl tcl = new TimeControl();
                tcl.timeType = TimeType.Month;
                tcl.EventRemove += Tcl_EventRemoveV2;
                pnlTimeFilterTemp.Controls.Add(tcl);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //检查列表是否为空
            if(dataGridViewX1.Rows.Count<=0)
            {
                MessageBoxEx.Show("请在左侧资源数勾选卡口");
                return;
            }
            //检查时间筛选是否为空
            //检查时间筛选是否为空
            if (pnlTimeFilter.Controls.Count <= 0 && pnlTimeFilterTemp.Controls.Count <= 1)
            {
                MessageBoxEx.Show("请添加筛选时间点，永久筛选时间点和临时筛选时间点不能同时为空");
                return;
            }
            //判断临时筛选时间点是否正确

            if (!CheckYJTime())
            {
                MessageBoxEx.Show("永久筛选时间点配置有误，请检查");
                return;
            }

            if(!CheckTempTime())
            {
                MessageBoxEx.Show("临时筛选时间点配置有误，请检查");
                return;
            }

            ComboBoxItem cbo = cboCjjg.Items[cboCjjg.SelectedIndex] as ComboBoxItem;
            string dptCode = cbo.Name;
            string dptName = cbo.Text;
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                kkinfo info = new kkinfo();
                info.createTime = DateTime.Now;
                info.creator = IMOS.UserName;
                info.dptCode = dptCode;
                info.dptName = dptName;
                info.kkCode = row.Cells["cmnCode"].Value.ToString();
                info.kkName = row.Cells["cmnName"].Value.ToString();
                info.orgCode = row.Cells["cmnOrgCode"].Value.ToString();
                info.orgName = row.Cells["cmnOrg"].Value.ToString();
                info.startTime1 = startTime1;
                info.endTime1 = endTime1;
                info.startTime2 = startTime2;
                info.endTime2 = endTime2;
                info.startTime3 = startTime3;
                info.endTime3 = endTime3;
                info.tempDayStart = tempStartDay;
                info.tempDayEnd = tempEndDay;
                info.tempStartTime1 = tempStartTime1;
                info.tempEndTime1 = tempEndTime1;
                info.tempStartTime2 = tempStartTime2;
                info.tempEndTime2 = tempEndTime2;
                info.tempStartTime3 = tempStartTime3;
                info.tempEndTime3 = tempEndTime3;
                selectKK.Add(info);
            }
            this.DialogResult = DialogResult.Yes;
        }
        private bool CheckYJTime()
        {
            for (int i = 0; i < pnlTimeFilter.Controls.Count; i++)
            {
                TimeControl ctl = pnlTimeFilter.Controls[i] as TimeControl;
                if (ctl.DtStart>=ctl.DtEnd)
                {
                    return false;
                }
                if(i==0)
                {
                    startTime1 = ctl.DtStart;
                    endTime1 = ctl.DtEnd;
                }
                else if(i==1)
                {
                    startTime2 = ctl.DtStart;
                    endTime2 = ctl.DtEnd;
                }
                else
                {
                    startTime3 = ctl.DtStart;
                    endTime3 = ctl.DtEnd;
                }
            }
            if(startTime3.HasValue)
            {
                return ((startTime3 > endTime2) && (startTime2 > endTime1));
            }
            else if(startTime2.HasValue)
            {
                return (startTime2 > endTime1);
            }
            return true;
        }
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    LoadTree();
                }
                else
                {
                    LoadTree(txtSearch.Text);
                }
            }
        }
        private bool CheckTempTime()
        {
            if(pnlTimeFilterTemp.Controls.Count<=0)
            {
                return true;
            }
            TimeControl daySet = pnlTimeFilterTemp.Controls[0] as TimeControl;
            if(daySet.DtStart>daySet.DtEnd)
            {
                return false;
            }
            tempStartDay = daySet.DtStart;
            tempEndDay = daySet.DtEnd;

            for (int i = 1; i < pnlTimeFilterTemp.Controls.Count; i++)
            {
                TimeControl ctl = pnlTimeFilterTemp.Controls[i] as TimeControl;
                if (ctl.DtStart >= ctl.DtEnd)
                {
                    return false;
                }
                if (i == 1)
                {
                    tempStartTime1 = ctl.DtStart;
                    tempEndTime1 = ctl.DtEnd;
                }
                else if (i == 2)
                {
                    tempStartTime2 = ctl.DtStart;
                    tempEndTime2 = ctl.DtEnd;
                }
                else
                {
                    tempStartTime3 = ctl.DtStart;
                    tempEndTime3 = ctl.DtEnd;
                }
            }
            if (tempStartTime3.HasValue)
            {
                return ((tempStartTime3 > tempEndTime2) && (tempStartTime2 > tempEndTime1));
            }
            else if (tempStartTime2.HasValue)
            {
                return (tempStartTime2 > tempEndTime1);
            }
            return true;
        }
        private void btnRefresh_Click(object sender,EventArgs e)
        {
            Task task = new Task((Action)delegate ()
            {
                TreeManage tm = new TreeManage();
                tm.LoadOrg();
                tm.LoadCameras();
                WaitingForm.Instance.CloseForm();
            });
            task.Start();
            WaitingForm.Instance.ShowDialog();
            LoadTree();
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                LoadTree();
            }
            else
            {
                LoadTree(txtSearch.Text);
            }
        }
    }
}
