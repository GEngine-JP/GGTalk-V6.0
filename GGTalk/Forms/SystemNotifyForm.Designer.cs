namespace GGTalk
{
    partial class SystemNotifyForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemNotifyForm));
            this.timShow = new System.Windows.Forms.Timer(this.components);
            this.pnlImgTx = new CCWin.SkinControl.SkinPanel();
            this.skinLabel_content = new CCWin.SkinControl.SkinLabel();
            this.skinButtom1 = new CCWin.SkinControl.SkinButton();
            this.skinLabel_title = new CCWin.SkinControl.SkinLabel();
            this.SuspendLayout();
            // 
            // timShow
            // 
            this.timShow.Enabled = true;
            this.timShow.Interval = 6000;
            this.timShow.Tick += new System.EventHandler(this.timShow_Tick);
            // 
            // pnlImgTx
            // 
            this.pnlImgTx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlImgTx.BackColor = System.Drawing.Color.Transparent;
            this.pnlImgTx.BackgroundImage = global::GGTalk.Properties.Resources.GG64;
            this.pnlImgTx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlImgTx.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.pnlImgTx.DownBack = null;
            this.pnlImgTx.Location = new System.Drawing.Point(334, 28);
            this.pnlImgTx.Margin = new System.Windows.Forms.Padding(0);
            this.pnlImgTx.MouseBack = null;
            this.pnlImgTx.Name = "pnlImgTx";
            this.pnlImgTx.NormlBack = null;
            this.pnlImgTx.Radius = 4;
            this.pnlImgTx.Size = new System.Drawing.Size(32, 32);
            this.pnlImgTx.TabIndex = 6;
            // 
            // skinLabel_content
            // 
            this.skinLabel_content.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel_content.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_content.BorderColor = System.Drawing.Color.White;
            this.skinLabel_content.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel_content.Location = new System.Drawing.Point(16, 68);
            this.skinLabel_content.Margin = new System.Windows.Forms.Padding(0);
            this.skinLabel_content.Name = "skinLabel_content";
            this.skinLabel_content.Size = new System.Drawing.Size(334, 135);
            this.skinLabel_content.TabIndex = 22;
            this.skinLabel_content.Text = "GGTalk";
            // 
            // skinButtom1
            // 
            this.skinButtom1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.skinButtom1.BackColor = System.Drawing.Color.Transparent;
            this.skinButtom1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.skinButtom1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButtom1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.skinButtom1.DownBack = null;
            this.skinButtom1.DrawType = CCWin.SkinControl.DrawStyle.None;
            this.skinButtom1.Image = ((System.Drawing.Image)(resources.GetObject("skinButtom1.Image")));
            this.skinButtom1.Location = new System.Drawing.Point(350, 203);
            this.skinButtom1.Margin = new System.Windows.Forms.Padding(0);
            this.skinButtom1.MouseBack = null;
            this.skinButtom1.Name = "skinButtom1";
            this.skinButtom1.NormlBack = null;
            this.skinButtom1.Size = new System.Drawing.Size(16, 16);
            this.skinButtom1.TabIndex = 127;
            this.skinButtom1.UseVisualStyleBackColor = false;
            this.skinButtom1.Click += new System.EventHandler(this.skinButtom1_Click);
            // 
            // skinLabel_title
            // 
            this.skinLabel_title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinLabel_title.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel_title.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_title.BorderColor = System.Drawing.Color.White;
            this.skinLabel_title.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel_title.Location = new System.Drawing.Point(12, 28);
            this.skinLabel_title.Name = "skinLabel_title";
            this.skinLabel_title.Size = new System.Drawing.Size(351, 30);
            this.skinLabel_title.TabIndex = 0;
            this.skinLabel_title.Text = "国庆放假通知";
            this.skinLabel_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SystemNotifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Back = ((System.Drawing.Image)(resources.GetObject("$this.Back")));
            this.BorderPalace = ((System.Drawing.Image)(resources.GetObject("$this.BorderPalace")));
            this.ClientSize = new System.Drawing.Size(370, 223);
            this.CloseDownBack = global::GGTalk.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::GGTalk.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::GGTalk.Properties.Resources.btn_close_disable;
            this.Controls.Add(this.pnlImgTx);
            this.Controls.Add(this.skinButtom1);
            this.Controls.Add(this.skinLabel_content);
            this.Controls.Add(this.skinLabel_title);
            this.MaxDownBack = global::GGTalk.Properties.Resources.btn_max_down;
            this.MaxMouseBack = global::GGTalk.Properties.Resources.btn_max_highlight;
            this.MaxNormlBack = global::GGTalk.Properties.Resources.btn_max_normal;
            this.MiniDownBack = global::GGTalk.Properties.Resources.btn_mini_down;
            this.MiniMouseBack = global::GGTalk.Properties.Resources.btn_mini_highlight;
            this.MiniNormlBack = global::GGTalk.Properties.Resources.btn_mini_normal;
            this.Name = "SystemNotifyForm";
            this.RestoreDownBack = global::GGTalk.Properties.Resources.btn_restore_down;
            this.RestoreMouseBack = global::GGTalk.Properties.Resources.btn_restore_highlight;
            this.RestoreNormlBack = global::GGTalk.Properties.Resources.btn_restore_normal;
            this.ShowDrawIcon = false;
            this.Text = "GGTalk 系统通知";
            this.Load += new System.EventHandler(this.FrmInformation_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timShow;
        private CCWin.SkinControl.SkinLabel skinLabel_content;
        private CCWin.SkinControl.SkinPanel pnlImgTx;
        private CCWin.SkinControl.SkinButton skinButtom1;
        private CCWin.SkinControl.SkinLabel skinLabel_title;
    }
}