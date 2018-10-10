namespace GGTalk
{
    partial class UserInformationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInformationForm));
            this.timShow = new System.Windows.Forms.Timer(this.components);
            this.pnlTx = new CCWin.SkinControl.SkinPanel();
            this.pnlImgTx = new CCWin.SkinControl.SkinPanel();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel_id = new CCWin.SkinControl.SkinLabel();
            this.skinLabel_name = new CCWin.SkinControl.SkinLabel();
            this.lblQm = new CCWin.SkinControl.SkinLabel();
            this.pnlTx.SuspendLayout();
            this.SuspendLayout();
            // 
            // timShow
            // 
            this.timShow.Enabled = true;
            this.timShow.Interval = 500;
            this.timShow.Tick += new System.EventHandler(this.timShow_Tick);
            // 
            // pnlTx
            // 
            this.pnlTx.BackColor = System.Drawing.Color.Transparent;
            this.pnlTx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlTx.Controls.Add(this.pnlImgTx);
            this.pnlTx.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.pnlTx.DownBack = ((System.Drawing.Image)(resources.GetObject("pnlTx.DownBack")));
            this.pnlTx.Location = new System.Drawing.Point(4, 13);
            this.pnlTx.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTx.MouseBack = ((System.Drawing.Image)(resources.GetObject("pnlTx.MouseBack")));
            this.pnlTx.Name = "pnlTx";
            this.pnlTx.NormlBack = ((System.Drawing.Image)(resources.GetObject("pnlTx.NormlBack")));
            this.pnlTx.Palace = true;
            this.pnlTx.Size = new System.Drawing.Size(57, 57);
            this.pnlTx.TabIndex = 136;
            // 
            // pnlImgTx
            // 
            this.pnlImgTx.BackColor = System.Drawing.Color.Transparent;
            this.pnlImgTx.BackgroundImage = global::GGTalk.Properties.Resources.GG64;
            this.pnlImgTx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlImgTx.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.pnlImgTx.DownBack = null;
            this.pnlImgTx.Location = new System.Drawing.Point(2, 2);
            this.pnlImgTx.Margin = new System.Windows.Forms.Padding(0);
            this.pnlImgTx.MouseBack = null;
            this.pnlImgTx.Name = "pnlImgTx";
            this.pnlImgTx.NormlBack = null;
            this.pnlImgTx.Radius = 4;
            this.pnlImgTx.Size = new System.Drawing.Size(53, 53);
            this.pnlImgTx.TabIndex = 6;
            // 
            // skinLabel1
            // 
            this.skinLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.Anamorphosis;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.Black;
            this.skinLabel1.BorderSize = 4;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabel1.ForeColor = System.Drawing.Color.White;
            this.skinLabel1.Location = new System.Drawing.Point(65, 10);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(53, 20);
            this.skinLabel1.TabIndex = 102;
            this.skinLabel1.Text = "帐号：";
            // 
            // skinLabel2
            // 
            this.skinLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinLabel2.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.Anamorphosis;
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.Black;
            this.skinLabel2.BorderSize = 4;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabel2.ForeColor = System.Drawing.Color.White;
            this.skinLabel2.Location = new System.Drawing.Point(65, 42);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(53, 20);
            this.skinLabel2.TabIndex = 102;
            this.skinLabel2.Text = "名称：";
            // 
            // skinLabel_id
            // 
            this.skinLabel_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinLabel_id.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.Anamorphosis;
            this.skinLabel_id.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_id.BorderColor = System.Drawing.Color.Black;
            this.skinLabel_id.BorderSize = 4;
            this.skinLabel_id.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabel_id.ForeColor = System.Drawing.Color.White;
            this.skinLabel_id.Location = new System.Drawing.Point(110, 10);
            this.skinLabel_id.Name = "skinLabel_id";
            this.skinLabel_id.Size = new System.Drawing.Size(162, 20);
            this.skinLabel_id.TabIndex = 102;
            this.skinLabel_id.Text = "10010";
            // 
            // skinLabel_name
            // 
            this.skinLabel_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinLabel_name.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.Anamorphosis;
            this.skinLabel_name.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_name.BorderColor = System.Drawing.Color.Black;
            this.skinLabel_name.BorderSize = 4;
            this.skinLabel_name.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabel_name.ForeColor = System.Drawing.Color.White;
            this.skinLabel_name.Location = new System.Drawing.Point(110, 42);
            this.skinLabel_name.Name = "skinLabel_name";
            this.skinLabel_name.Size = new System.Drawing.Size(162, 20);
            this.skinLabel_name.TabIndex = 102;
            this.skinLabel_name.Text = "Tom";
            // 
            // lblQm
            // 
            this.lblQm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQm.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.Anamorphosis;
            this.lblQm.BackColor = System.Drawing.Color.Transparent;
            this.lblQm.BorderColor = System.Drawing.Color.Black;
            this.lblQm.BorderSize = 4;
            this.lblQm.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblQm.ForeColor = System.Drawing.Color.White;
            this.lblQm.Location = new System.Drawing.Point(7, 81);
            this.lblQm.Name = "lblQm";
            this.lblQm.Size = new System.Drawing.Size(257, 20);
            this.lblQm.TabIndex = 102;
            this.lblQm.Text = "退一步海阔天空！";
            // 
            // UserInformationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderPalace = global::GGTalk.Properties.Resources.BackPalace;
            this.ClientSize = new System.Drawing.Size(279, 181);
            this.CloseDownBack = global::GGTalk.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::GGTalk.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::GGTalk.Properties.Resources.btn_close_disable;
            this.ControlBox = false;
            this.Controls.Add(this.skinLabel_name);
            this.Controls.Add(this.pnlTx);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.skinLabel_id);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.lblQm);
            this.MaxDownBack = global::GGTalk.Properties.Resources.btn_max_down;
            this.MaxMouseBack = global::GGTalk.Properties.Resources.btn_max_highlight;
            this.MaxNormlBack = global::GGTalk.Properties.Resources.btn_max_normal;
            this.MiniDownBack = global::GGTalk.Properties.Resources.btn_mini_down;
            this.MiniMouseBack = global::GGTalk.Properties.Resources.btn_mini_highlight;
            this.MiniNormlBack = global::GGTalk.Properties.Resources.btn_mini_normal;
            this.Mobile = CCWin.MobileStyle.None;
            this.Name = "UserInformationForm";
            this.RestoreDownBack = global::GGTalk.Properties.Resources.btn_restore_down;
            this.RestoreMouseBack = global::GGTalk.Properties.Resources.btn_restore_highlight;
            this.RestoreNormlBack = global::GGTalk.Properties.Resources.btn_restore_normal;
            this.ShowBorder = false;
            this.ShowDrawIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.UserInformationForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmUserInformation_Paint);
            this.pnlTx.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timShow;
        private CCWin.SkinControl.SkinPanel pnlTx;
        private CCWin.SkinControl.SkinPanel pnlImgTx;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinLabel skinLabel_id;
        private CCWin.SkinControl.SkinLabel skinLabel_name;
        private CCWin.SkinControl.SkinLabel lblQm;
    }
}