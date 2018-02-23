using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdvNode = DevComponents.AdvTree.Node;
using JAUploadForWinform.Business;
using JAUploadForWinform.Model;

namespace JAUploadForWinform.Control
{
    public partial class TreeControl : UserControl
    {
        public delegate void DoEvent();

        public TreeControl()
        {
            InitializeComponent();
            this.Load += TreeControl_Load;
        }

        private void TreeControl_Load(object sender, EventArgs e)
        {
            IEnumerable<KeyValuePair<string, ResourceItem>> itemList = TreeManage.Organazations.Where(i => i.Value.OrgCode == "iccsid"); //找到根节点
            // //加载摄像机树
            AdvNode firstNode = new AdvNode(itemList.First().Value.OrgName); //设置跟节点
            firstNode.CheckBoxVisible = false;
            ResourceItem rootItem = new ResourceItem { ResCode = "iccsid", ResName = firstNode.Text, ResType = EmResType.IMOS_TYPE_LOCAL_DOMAIN };
            firstNode.DataKey = rootItem;
            AddSubOrg(firstNode);
            advTree1.Nodes.Add(firstNode);
        }
        private void AddSubOrg(AdvNode node)
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
        private void AddSubCam(AdvNode node)
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
                    ResourceItem rootItem = new ResourceItem { ResCode = item.Key, ResName = item.Value.ResName, ResType = EmResType.IMOS_TYPE_TOLLGATE };
                    subNode.DataKey = rootItem;
                    node.Nodes.Add(subNode);
                }
            }
        }
    }
}
