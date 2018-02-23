namespace JAUploadForWinform.Presentation
{
    partial class WaitingForm
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
            this.crProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // crProgress
            // 
            this.crProgress.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.crProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.crProgress.FocusCuesEnabled = false;
            this.crProgress.Location = new System.Drawing.Point(219, 12);
            this.crProgress.Name = "crProgress";
            this.crProgress.Size = new System.Drawing.Size(193, 103);
            this.crProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.crProgress.TabIndex = 0;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(252, 121);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(162, 37);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "加载中，请稍等……";
            // 
            // WaitingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 170);
            this.ControlBox = false;
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.crProgress);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaitingForm";
            this.RenderFormIcon = false;
            this.RenderFormText = false;
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.CircularProgress crProgress;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}