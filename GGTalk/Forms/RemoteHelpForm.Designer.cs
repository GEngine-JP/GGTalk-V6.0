namespace GGTalk
{
    partial class RemoteHelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteHelpForm));
            this.desktopConnector1 = new OMCS.Passive.RemoteDesktop.DesktopConnector();
            this.skinPanel2 = new CCWin.SkinControl.SkinPanel();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.skinComboBox_quality = new CCWin.SkinControl.SkinComboBox();
            this.skinLabel_quality = new CCWin.SkinControl.SkinLabel();
            this.SuspendLayout();
            // 
            // desktopConnector1
            // 
            this.desktopConnector1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.desktopConnector1.BackColor = System.Drawing.Color.White;            
            this.desktopConnector1.Location = new System.Drawing.Point(2, 30);          
            this.desktopConnector1.Name = "desktopConnector1";
            this.desktopConnector1.ShowMouseCursor = true;
            this.desktopConnector1.Size = new System.Drawing.Size(750, 534);
            this.desktopConnector1.TabIndex = 0;
            this.desktopConnector1.WaitOwnerOnlineSpanInSecs = 0;
            this.desktopConnector1.WatchingOnly = true;
            // 
            // skinPanel2
            // 
            this.skinPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.skinPanel2.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel2.BackgroundImage = global::GGTalk.Properties.Resources.RemoteHelp;
            this.skinPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.skinPanel2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel2.DownBack = null;
            this.skinPanel2.Location = new System.Drawing.Point(323, 207);
            this.skinPanel2.MouseBack = null;
            this.skinPanel2.Name = "skinPanel2";
            this.skinPanel2.NormlBack = null;
            this.skinPanel2.Size = new System.Drawing.Size(96, 96);
            this.skinPanel2.TabIndex = 131;
            // 
            // skinLabel1
            // 
            this.skinLabel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(309, 317);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(125, 17);
            this.skinLabel1.TabIndex = 130;
            this.skinLabel1.Text = "正在连接对方桌面 . . .";
            // 
            // skinComboBox_quality
            // 
            this.skinComboBox_quality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skinComboBox_quality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.skinComboBox_quality.FormattingEnabled = true;
            this.skinComboBox_quality.Items.AddRange(new object[] {
            "优",
            "良",
            "中",
            "差"});
            this.skinComboBox_quality.Location = new System.Drawing.Point(612, 4);
            this.skinComboBox_quality.Name = "skinComboBox_quality";
            this.skinComboBox_quality.Size = new System.Drawing.Size(39, 22);
            this.skinComboBox_quality.TabIndex = 132;
            this.skinComboBox_quality.Visible = false;
            this.skinComboBox_quality.WaterText = "";
            this.skinComboBox_quality.SelectedIndexChanged += new System.EventHandler(this.skinComboBox1_SelectedIndexChanged);
            // 
            // skinLabel_quality
            // 
            this.skinLabel_quality.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel_quality.AutoSize = true;
            this.skinLabel_quality.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_quality.BorderColor = System.Drawing.Color.White;
            this.skinLabel_quality.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel_quality.Location = new System.Drawing.Point(560, 7);
            this.skinLabel_quality.Name = "skinLabel_quality";
            this.skinLabel_quality.Size = new System.Drawing.Size(56, 17);
            this.skinLabel_quality.TabIndex = 133;
            this.skinLabel_quality.Text = "清晰度：";
            this.skinLabel_quality.Visible = false;
            // 
            // RemoteHelpForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Back = ((System.Drawing.Image)(resources.GetObject("$this.Back")));
            this.BorderPalace = ((System.Drawing.Image)(resources.GetObject("$this.BorderPalace")));
            this.ClientSize = new System.Drawing.Size(755, 569);
            this.CloseDownBack = global::GGTalk.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::GGTalk.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::GGTalk.Properties.Resources.btn_close_disable;
            this.Controls.Add(this.skinComboBox_quality);
            this.Controls.Add(this.skinLabel_quality);
            this.Controls.Add(this.desktopConnector1);
            this.Controls.Add(this.skinPanel2);
            this.Controls.Add(this.skinLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaxDownBack = global::GGTalk.Properties.Resources.btn_max_down;
            this.MaximizeBox = true;
            this.MaxMouseBack = global::GGTalk.Properties.Resources.btn_max_highlight;
            this.MaxNormlBack = global::GGTalk.Properties.Resources.btn_max_normal;
            this.MiniDownBack = global::GGTalk.Properties.Resources.btn_mini_down;
            this.MinimizeBox = true;
            this.MiniMouseBack = global::GGTalk.Properties.Resources.btn_mini_highlight;
            this.MiniNormlBack = global::GGTalk.Properties.Resources.btn_mini_normal;
            this.Name = "RemoteHelpForm";
            this.RestoreDownBack = global::GGTalk.Properties.Resources.btn_restore_down;
            this.RestoreMouseBack = global::GGTalk.Properties.Resources.btn_restore_highlight;
            this.RestoreNormlBack = global::GGTalk.Properties.Resources.btn_restore_normal;
            this.Shadow = false;
            this.ShowInTaskbar = true;
            this.Text = "远程桌面";
            this.TopMost = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RemoteHelpForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OMCS.Passive.RemoteDesktop.DesktopConnector desktopConnector1;
        private CCWin.SkinControl.SkinPanel skinPanel2;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinComboBox skinComboBox_quality;
        private CCWin.SkinControl.SkinLabel skinLabel_quality;

    }
}