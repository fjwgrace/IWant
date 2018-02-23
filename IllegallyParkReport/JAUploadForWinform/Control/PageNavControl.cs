using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
namespace JAUploadForWinform.Control
{
    public delegate int EventPagingHandler(EventPagingArg e);
    public partial class PageNavControl : UserControl
    {
        public PageNavControl()
        {
            InitializeComponent();
        }
        public event EventPagingHandler EventPaging;

        private int _pageSize = 20;
        public int PageSize
        {
            get { return _pageSize; }
            set {
                _pageSize = value;
                GetPageCount();
            }
        }
        private int _maxNum = 0;
        public int MaxNum
        {
            get { return _maxNum; }
            set { _maxNum = value;
                 GetPageCount();
            }
        }

        private int _pageCount = 0;
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        private int _pageCurrent = 0;
        public int PageCurrent
        {
            get { return _pageCurrent; }
            set { _pageCurrent = value; }
        }
        private void GetPageCount()
        {
            if(this.MaxNum>0)
            {
                this.PageCount= Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.MaxNum) / Convert.ToDouble(this.PageSize)));
            }
            else
            {
                this.PageCount = 0;
            }
        }
        public void Bind()
        {
            if(this.EventPaging!=null)
            {
                this.MaxNum = this.EventPaging(new EventPagingArg(this.PageCurrent));
            }
            if(this.MaxNum!=0&&this.PageCurrent==0)
            {
                PageCurrent = 1;
            }
            if(this.PageCurrent>this.PageCount)
            {
                this.PageCurrent = this.PageCount;
            }
            if(this.PageCount==1)
            {
                this.PageCurrent = 1;
            }
            lblPageCount.Text = this.PageCount.ToString();
            this.lblNum.Text = string.Format("共{0}条记录", this.MaxNum);
            this.txtPage.Text = this.PageCurrent.ToString();
            if(this.PageCurrent==1)
            {
                this.btnPrePage.Enabled = false;
                this.btnFirstPage.Enabled = false;
            }
            else
            {
                this.btnPrePage.Enabled = true;
                this.btnFirstPage.Enabled = true;
            }
            if(this.PageCurrent==this.PageCount)
            {
                this.btnLastPage.Enabled = false;
                this.btnNextPage.Enabled = false;
            }
            else
            {
                this.btnLastPage.Enabled = true;
                this.btnNextPage.Enabled = true;
            }
            if(this.MaxNum==0)
            {
                btnNextPage.Enabled = false;
                btnLastPage.Enabled = false;
                btnFirstPage.Enabled = false;
                btnPrePage.Enabled = false;
            }
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            PageCurrent = 1;
            Bind();
        }

        private void btnPrePage_Click(object sender, EventArgs e)
        {
            PageCurrent -= 1;
            if(PageCurrent<=0)
            {
                PageCurrent = 1;
            }
            this.Bind();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            this.PageCurrent += 1;
            if(PageCurrent>PageCount)
            {
                PageCurrent = PageCount;
            }
            this.Bind();
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            PageCurrent = PageCount;
            this.Bind();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(this.txtPage.Text))
            {
                if(Int32.TryParse(txtPage.Text,out _pageCurrent))
                {
                    this.Bind();
                }
                else
                {
                    MessageBoxEx.Show("输入数字格式错误");
                }
            }
        }

        private void txtPage_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                btnGo.RaiseClick();
            }
        }
    }
    /// <summary>
    /// 自定义事件数据基类
    /// </summary>
    public class EventPagingArg : EventArgs
    {
        private int _intPageIndex;
        public EventPagingArg(int PageIndex)
        {
            _intPageIndex = PageIndex;
        }
    }
}
