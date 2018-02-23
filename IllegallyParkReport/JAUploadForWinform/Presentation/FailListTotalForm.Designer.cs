namespace JAUploadForWinform.Presentation
{
    partial class FailListTotalForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.cmnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmnOrg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmnHphm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmnHpys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmnWfdd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmnCjjg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmnCjjgCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageNavControl1 = new JAUploadForWinform.Control.PageNavControl();
            this.pnlQuery = new DevComponents.DotNetBar.PanelEx();
            this.btnUpload = new DevComponents.DotNetBar.ButtonX();
            this.progressBarX1 = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.pnlInterval2 = new DevComponents.DotNetBar.PanelEx();
            this.lblProcess = new DevComponents.DotNetBar.LabelX();
            this.bar2 = new DevComponents.DotNetBar.Bar();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.pnlQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dataGridViewX1);
            this.panelEx1.Controls.Add(this.pageNavControl1);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 43);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Padding = new System.Windows.Forms.Padding(8, 8, 0, 8);
            this.panelEx1.Size = new System.Drawing.Size(837, 498);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 4;
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewX1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewX1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewX1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewX1.ColumnHeadersHeight = 40;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cmnCode,
            this.cmnName,
            this.cmnOrg,
            this.cmnHphm,
            this.cmnHpys,
            this.cmnWfdd,
            this.cmnCjjg,
            this.cmnCjjgCode});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewX1.EnableHeadersVisualStyles = false;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(8, 8);
            this.dataGridViewX1.Name = "dataGridViewX1";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewX1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewX1.RowHeadersVisible = false;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewX1.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewX1.RowTemplate.Height = 27;
            this.dataGridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewX1.Size = new System.Drawing.Size(829, 439);
            this.dataGridViewX1.TabIndex = 13;
            // 
            // cmnCode
            // 
            this.cmnCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cmnCode.DefaultCellStyle = dataGridViewCellStyle3;
            this.cmnCode.FillWeight = 20F;
            this.cmnCode.HeaderText = "卡口编号";
            this.cmnCode.Name = "cmnCode";
            this.cmnCode.Visible = false;
            // 
            // cmnName
            // 
            this.cmnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cmnName.DefaultCellStyle = dataGridViewCellStyle4;
            this.cmnName.FillWeight = 30F;
            this.cmnName.HeaderText = "卡口名称";
            this.cmnName.Name = "cmnName";
            // 
            // cmnOrg
            // 
            this.cmnOrg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cmnOrg.DefaultCellStyle = dataGridViewCellStyle5;
            this.cmnOrg.FillWeight = 20F;
            this.cmnOrg.HeaderText = "经过时间";
            this.cmnOrg.Name = "cmnOrg";
            // 
            // cmnHphm
            // 
            this.cmnHphm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cmnHphm.DefaultCellStyle = dataGridViewCellStyle6;
            this.cmnHphm.FillWeight = 35F;
            this.cmnHphm.HeaderText = "号牌号码";
            this.cmnHphm.Name = "cmnHphm";
            // 
            // cmnHpys
            // 
            this.cmnHpys.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cmnHpys.DefaultCellStyle = dataGridViewCellStyle7;
            this.cmnHpys.FillWeight = 25F;
            this.cmnHpys.HeaderText = "号牌颜色";
            this.cmnHpys.Name = "cmnHpys";
            // 
            // cmnWfdd
            // 
            this.cmnWfdd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmnWfdd.FillWeight = 50F;
            this.cmnWfdd.HeaderText = "违法地点";
            this.cmnWfdd.Name = "cmnWfdd";
            // 
            // cmnCjjg
            // 
            this.cmnCjjg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmnCjjg.FillWeight = 40F;
            this.cmnCjjg.HeaderText = "采集机关";
            this.cmnCjjg.Name = "cmnCjjg";
            // 
            // cmnCjjgCode
            // 
            this.cmnCjjgCode.HeaderText = "采集机关编号";
            this.cmnCjjgCode.Name = "cmnCjjgCode";
            this.cmnCjjgCode.Visible = false;
            // 
            // pageNavControl1
            // 
            this.pageNavControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pageNavControl1.ForeColor = System.Drawing.Color.Black;
            this.pageNavControl1.Location = new System.Drawing.Point(8, 447);
            this.pageNavControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pageNavControl1.MaxNum = 0;
            this.pageNavControl1.Name = "pageNavControl1";
            this.pageNavControl1.PageCount = 0;
            this.pageNavControl1.PageCurrent = 0;
            this.pageNavControl1.PageSize = 20;
            this.pageNavControl1.Size = new System.Drawing.Size(829, 43);
            this.pageNavControl1.TabIndex = 14;
            // 
            // pnlQuery
            // 
            this.pnlQuery.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlQuery.Controls.Add(this.lblProcess);
            this.pnlQuery.Controls.Add(this.btnUpload);
            this.pnlQuery.Controls.Add(this.progressBarX1);
            this.pnlQuery.Controls.Add(this.labelX1);
            this.pnlQuery.DisabledBackColor = System.Drawing.Color.Empty;
            this.pnlQuery.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlQuery.Location = new System.Drawing.Point(0, 553);
            this.pnlQuery.Name = "pnlQuery";
            this.pnlQuery.Size = new System.Drawing.Size(837, 100);
            this.pnlQuery.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlQuery.Style.BackColor1.Color = System.Drawing.Color.White;
            this.pnlQuery.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlQuery.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlQuery.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlQuery.Style.GradientAngle = 90;
            this.pnlQuery.TabIndex = 8;
            // 
            // btnUpload
            // 
            this.btnUpload.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUpload.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUpload.Location = new System.Drawing.Point(694, 23);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(105, 53);
            this.btnUpload.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "重新上传";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // progressBarX1
            // 
            // 
            // 
            // 
            this.progressBarX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progressBarX1.ForeColor = System.Drawing.Color.Black;
            this.progressBarX1.Location = new System.Drawing.Point(110, 36);
            this.progressBarX1.Name = "progressBarX1";
            this.progressBarX1.Size = new System.Drawing.Size(518, 23);
            this.progressBarX1.TabIndex = 2;
            this.progressBarX1.Text = "progressBarX1";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(24, 36);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(96, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "上传进度：";
            // 
            // pnlInterval2
            // 
            this.pnlInterval2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlInterval2.DisabledBackColor = System.Drawing.Color.Empty;
            this.pnlInterval2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInterval2.Location = new System.Drawing.Point(0, 541);
            this.pnlInterval2.Name = "pnlInterval2";
            this.pnlInterval2.Size = new System.Drawing.Size(837, 12);
            this.pnlInterval2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlInterval2.Style.BackColor1.Color = System.Drawing.Color.White;
            this.pnlInterval2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlInterval2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlInterval2.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.pnlInterval2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlInterval2.Style.GradientAngle = 90;
            this.pnlInterval2.TabIndex = 15;
            // 
            // lblProcess
            // 
            // 
            // 
            // 
            this.lblProcess.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblProcess.ForeColor = System.Drawing.Color.Black;
            this.lblProcess.Location = new System.Drawing.Point(635, 36);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(53, 23);
            this.lblProcess.TabIndex = 4;
            // 
            // bar2
            // 
            this.bar2.AntiAlias = true;
            this.bar2.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar2.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.bar2.IsMaximized = false;
            this.bar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnRefresh});
            this.bar2.Location = new System.Drawing.Point(0, 0);
            this.bar2.Name = "bar2";
            this.bar2.Size = new System.Drawing.Size(837, 43);
            this.bar2.Stretch = true;
            this.bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar2.TabIndex = 22;
            this.bar2.TabStop = false;
            this.bar2.Text = "bar2";
            // 
            // btnRefresh
            // 
            this.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Symbol = "";
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // FailListTotalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 653);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.bar2);
            this.Controls.Add(this.pnlInterval2);
            this.Controls.Add(this.pnlQuery);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FailListTotalForm";
            this.RenderFormIcon = false;
            this.RenderFormText = false;
            this.Text = "失败重传";
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.pnlQuery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private Control.PageNavControl pageNavControl1;
        private DevComponents.DotNetBar.PanelEx pnlQuery;
        private DevComponents.DotNetBar.Controls.ProgressBarX progressBarX1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmnCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmnOrg;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmnHphm;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmnHpys;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmnWfdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmnCjjg;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmnCjjgCode;
        private DevComponents.DotNetBar.PanelEx pnlInterval2;
        private DevComponents.DotNetBar.ButtonX btnUpload;
        private DevComponents.DotNetBar.LabelX lblProcess;
        private DevComponents.DotNetBar.Bar bar2;
        private DevComponents.DotNetBar.ButtonItem btnRefresh;
    }
}