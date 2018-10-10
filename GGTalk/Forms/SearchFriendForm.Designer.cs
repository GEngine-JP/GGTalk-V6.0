namespace GGTalk
{
    partial class SearchFriendForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchFriendForm));
            this.skinLabel_noResult = new CCWin.SkinControl.SkinLabel();
            this.chatListBox = new CCWin.SkinControl.ChatListBox();
            this.btnClose = new CCWin.SkinControl.SkinButton();
            this.skinTextBox_id = new CCWin.SkinControl.SkinTextBox();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.skinTextBox_id.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinLabel_noResult
            // 
            this.skinLabel_noResult.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel_noResult.AutoSize = true;
            this.skinLabel_noResult.BackColor = System.Drawing.Color.White;
            this.skinLabel_noResult.BorderColor = System.Drawing.Color.White;
            this.skinLabel_noResult.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel_noResult.Location = new System.Drawing.Point(143, 118);
            this.skinLabel_noResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel_noResult.Name = "skinLabel_noResult";
            this.skinLabel_noResult.Size = new System.Drawing.Size(96, 20);
            this.skinLabel_noResult.TabIndex = 135;
            this.skinLabel_noResult.Text = "无搜索结果...";
            this.skinLabel_noResult.Visible = false;
            // 
            // chatListBox
            // 
            this.chatListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chatListBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chatListBox.DrawContentType = CCWin.SkinControl.DrawContentType.None;
            this.chatListBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chatListBox.ForeColor = System.Drawing.Color.Black;
            this.chatListBox.FriendsMobile = true;
            this.chatListBox.ListHadOpenGroup = null;
            this.chatListBox.ListSubItemMenu = null;
            this.chatListBox.Location = new System.Drawing.Point(12, 101);
            this.chatListBox.Margin = new System.Windows.Forms.Padding(0);
            this.chatListBox.Name = "chatListBox";
            this.chatListBox.SelectSubItem = null;
            this.chatListBox.ShowNicName = true;
            this.chatListBox.Size = new System.Drawing.Size(371, 630);
            this.chatListBox.SubItemMenu = null;
            this.chatListBox.TabIndex = 134;
            this.chatListBox.DoubleClickSubItem += new CCWin.SkinControl.ChatListBox.ChatListEventHandler(this.chatListBox_DoubleClickSubItem);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.btnClose.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.DownBack = ((System.Drawing.Image)(resources.GetObject("btnClose.DownBack")));
            this.btnClose.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(300, 60);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnClose.MouseBack")));
            this.btnClose.Name = "btnClose";
            this.btnClose.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnClose.NormlBack")));
            this.btnClose.Size = new System.Drawing.Size(80, 30);
            this.btnClose.TabIndex = 133;
            this.btnClose.Text = "确定";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // skinTextBox_id
            // 
            this.skinTextBox_id.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox_id.Icon = null;
            this.skinTextBox_id.IconIsButton = false;
            this.skinTextBox_id.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_id.Location = new System.Drawing.Point(109, 58);
            this.skinTextBox_id.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_id.MinimumSize = new System.Drawing.Size(37, 35);
            this.skinTextBox_id.MouseBack = null;
            this.skinTextBox_id.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_id.Name = "skinTextBox_id";
            this.skinTextBox_id.NormlBack = null;
            this.skinTextBox_id.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.skinTextBox_id.Size = new System.Drawing.Size(181, 35);
            // 
            // skinTextBox_id.BaseText
            // 
            this.skinTextBox_id.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_id.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_id.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_id.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.skinTextBox_id.SkinTxt.Name = "BaseText";
            this.skinTextBox_id.SkinTxt.Size = new System.Drawing.Size(167, 22);
            this.skinTextBox_id.SkinTxt.TabIndex = 0;
            this.skinTextBox_id.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox_id.SkinTxt.WaterText = "";
            this.skinTextBox_id.TabIndex = 129;
            // 
            // skinLabel1
            // 
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(8, 65);
            this.skinLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(99, 20);
            this.skinLabel1.TabIndex = 0;
            this.skinLabel1.Text = "帐号或名称：";
            // 
            // SearchFriendForm
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Back = ((System.Drawing.Image)(resources.GetObject("$this.Back")));
            this.BorderPalace = ((System.Drawing.Image)(resources.GetObject("$this.BorderPalace")));
            this.ClientSize = new System.Drawing.Size(392, 748);
            this.CloseDownBack = global::GGTalk.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::GGTalk.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::GGTalk.Properties.Resources.btn_close_disable;
            this.Controls.Add(this.skinLabel_noResult);
            this.Controls.Add(this.chatListBox);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.skinTextBox_id);
            this.Controls.Add(this.skinLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaxDownBack = global::GGTalk.Properties.Resources.btn_max_down;
            this.MaxMouseBack = global::GGTalk.Properties.Resources.btn_max_highlight;
            this.MaxNormlBack = global::GGTalk.Properties.Resources.btn_max_normal;
            this.MiniDownBack = global::GGTalk.Properties.Resources.btn_mini_down;
            this.MiniMouseBack = global::GGTalk.Properties.Resources.btn_mini_highlight;
            this.MiniNormlBack = global::GGTalk.Properties.Resources.btn_mini_normal;
            this.Name = "SearchFriendForm";
            this.RestoreDownBack = global::GGTalk.Properties.Resources.btn_restore_down;
            this.RestoreMouseBack = global::GGTalk.Properties.Resources.btn_restore_highlight;
            this.RestoreNormlBack = global::GGTalk.Properties.Resources.btn_restore_normal;
            this.Text = "查找好友";
            this.skinTextBox_id.ResumeLayout(false);
            this.skinTextBox_id.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinTextBox skinTextBox_id;
        private CCWin.SkinControl.SkinButton btnClose;
        private CCWin.SkinControl.ChatListBox chatListBox;
        private CCWin.SkinControl.SkinLabel skinLabel_noResult;
    }
}