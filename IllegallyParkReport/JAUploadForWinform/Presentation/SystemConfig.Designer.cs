namespace JAUploadForWinform.Presentation
{
    partial class SystemConfig
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtRealIP = new DevComponents.Editors.IpAddressInput();
            this.txtImosIP = new DevComponents.Editors.IpAddressInput();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtLimit = new DevComponents.Editors.IntegerInput();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.rdbNo = new System.Windows.Forms.RadioButton();
            this.rdbYes = new System.Windows.Forms.RadioButton();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealIP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImosIP)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(15, 28);
            this.labelX1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(150, 31);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "短信告知平台地址：";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.ForeColor = System.Drawing.Color.Black;
            this.labelX2.Location = new System.Drawing.Point(15, 86);
            this.labelX2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(150, 31);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "违法数据平台地址：";
            // 
            // txtRealIP
            // 
            this.txtRealIP.AutoOverwrite = true;
            this.txtRealIP.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtRealIP.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtRealIP.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRealIP.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtRealIP.ForeColor = System.Drawing.Color.Black;
            this.txtRealIP.Location = new System.Drawing.Point(157, 28);
            this.txtRealIP.Name = "txtRealIP";
            this.txtRealIP.Size = new System.Drawing.Size(203, 27);
            this.txtRealIP.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.txtRealIP.TabIndex = 2;
            // 
            // txtImosIP
            // 
            this.txtImosIP.AutoOverwrite = true;
            this.txtImosIP.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtImosIP.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtImosIP.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtImosIP.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtImosIP.ForeColor = System.Drawing.Color.Black;
            this.txtImosIP.Location = new System.Drawing.Point(157, 86);
            this.txtImosIP.Name = "txtImosIP";
            this.txtImosIP.Size = new System.Drawing.Size(203, 27);
            this.txtImosIP.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.txtImosIP.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.txtLimit);
            this.groupBox1.Controls.Add(this.labelX3);
            this.groupBox1.Controls.Add(this.rdbNo);
            this.groupBox1.Controls.Add(this.rdbYes);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(15, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 123);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "是否限制上传数量";
            // 
            // txtLimit
            // 
            this.txtLimit.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtLimit.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtLimit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLimit.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtLimit.ForeColor = System.Drawing.Color.Black;
            this.txtLimit.Location = new System.Drawing.Point(116, 76);
            this.txtLimit.Name = "txtLimit";
            this.txtLimit.ShowUpDown = true;
            this.txtLimit.Size = new System.Drawing.Size(100, 27);
            this.txtLimit.TabIndex = 3;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.ForeColor = System.Drawing.Color.Black;
            this.labelX3.Location = new System.Drawing.Point(24, 76);
            this.labelX3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(85, 31);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "限制数量：";
            // 
            // rdbNo
            // 
            this.rdbNo.AutoSize = true;
            this.rdbNo.BackColor = System.Drawing.Color.White;
            this.rdbNo.ForeColor = System.Drawing.Color.Black;
            this.rdbNo.Location = new System.Drawing.Point(105, 33);
            this.rdbNo.Name = "rdbNo";
            this.rdbNo.Size = new System.Drawing.Size(45, 24);
            this.rdbNo.TabIndex = 1;
            this.rdbNo.TabStop = true;
            this.rdbNo.Text = "否";
            this.rdbNo.UseVisualStyleBackColor = false;
            this.rdbNo.CheckedChanged += new System.EventHandler(this.rdbNo_CheckedChanged);
            // 
            // rdbYes
            // 
            this.rdbYes.AutoSize = true;
            this.rdbYes.BackColor = System.Drawing.Color.White;
            this.rdbYes.ForeColor = System.Drawing.Color.Black;
            this.rdbYes.Location = new System.Drawing.Point(24, 33);
            this.rdbYes.Name = "rdbYes";
            this.rdbYes.Size = new System.Drawing.Size(45, 24);
            this.rdbYes.TabIndex = 0;
            this.rdbYes.TabStop = true;
            this.rdbYes.Text = "是";
            this.rdbYes.UseVisualStyleBackColor = false;
            this.rdbYes.CheckedChanged += new System.EventHandler(this.rdbYes_CheckedChanged);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(131, 281);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(5);
            this.buttonX1.Size = new System.Drawing.Size(100, 48);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 5;
            this.buttonX1.Text = "保存";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // SystemConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 353);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtImosIP);
            this.Controls.Add(this.txtRealIP);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SystemConfig";
            this.RenderFormIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            ((System.ComponentModel.ISupportInitialize)(this.txtRealIP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImosIP)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLimit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.IpAddressInput txtRealIP;
        private DevComponents.Editors.IpAddressInput txtImosIP;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.Editors.IntegerInput txtLimit;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.RadioButton rdbNo;
        private System.Windows.Forms.RadioButton rdbYes;
        private DevComponents.DotNetBar.ButtonX buttonX1;
    }
}