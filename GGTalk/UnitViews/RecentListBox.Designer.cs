namespace GGTalk.UnitViews
{
    partial class RecentListBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            CCWin.SkinControl.ChatListItem chatListItem1 = new CCWin.SkinControl.ChatListItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecentListBox));
            this.chatListBox = new CCWin.SkinControl.ChatListBox();
            this.skinContextMenuStrip_recent = new CCWin.SkinControl.SkinContextMenuStrip();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.从列表中移除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.skinContextMenuStrip_recent.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatListBox
            // 
            this.chatListBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chatListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatListBox.DrawContentType = CCWin.SkinControl.DrawContentType.LastWords;
            this.chatListBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chatListBox.ForeColor = System.Drawing.Color.Black;
            this.chatListBox.FriendsMobile = false;
            chatListItem1.Bounds = new System.Drawing.Rectangle(0, 1, 251, 25);
            chatListItem1.IsOpen = true;
            chatListItem1.IsTwinkleHide = false;
            chatListItem1.OwnerChatListBox = this.chatListBox;
            chatListItem1.Text = "最近联系人";
            chatListItem1.TwinkleSubItemNumber = 0;
            this.chatListBox.Items.AddRange(new CCWin.SkinControl.ChatListItem[] {
            chatListItem1});
            this.chatListBox.ListHadOpenGroup = null;
            this.chatListBox.ListSubItemMenu = this.skinContextMenuStrip_recent;
            this.chatListBox.Location = new System.Drawing.Point(0, 0);
            this.chatListBox.Margin = new System.Windows.Forms.Padding(0);
            this.chatListBox.Name = "chatListBox";
            this.chatListBox.SelectSubItem = null;
            this.chatListBox.ShowNicName = false;
            this.chatListBox.Size = new System.Drawing.Size(251, 415);
            this.chatListBox.SubItemMenu = null;
            this.chatListBox.TabIndex = 134;
            this.chatListBox.DoubleClickSubItem += new CCWin.SkinControl.ChatListBox.ChatListEventHandler(this.chatListBox_DoubleClickSubItem);
            // 
            // skinContextMenuStrip_recent
            // 
            this.skinContextMenuStrip_recent.Arrow = System.Drawing.Color.Gray;
            this.skinContextMenuStrip_recent.Back = System.Drawing.Color.White;
            this.skinContextMenuStrip_recent.BackRadius = 4;
            this.skinContextMenuStrip_recent.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
            this.skinContextMenuStrip_recent.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.skinContextMenuStrip_recent.Fore = System.Drawing.Color.Black;
            this.skinContextMenuStrip_recent.HoverFore = System.Drawing.Color.White;
            this.skinContextMenuStrip_recent.ImageScalingSize = new System.Drawing.Size(11, 11);
            this.skinContextMenuStrip_recent.ItemAnamorphosis = false;
            this.skinContextMenuStrip_recent.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinContextMenuStrip_recent.ItemBorderShow = false;
            this.skinContextMenuStrip_recent.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinContextMenuStrip_recent.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinContextMenuStrip_recent.ItemRadius = 4;
            this.skinContextMenuStrip_recent.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.None;
            this.skinContextMenuStrip_recent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.从列表中移除ToolStripMenuItem});
            this.skinContextMenuStrip_recent.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.skinContextMenuStrip_recent.Name = "MenuState";
            this.skinContextMenuStrip_recent.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinContextMenuStrip_recent.Size = new System.Drawing.Size(149, 70);
            this.skinContextMenuStrip_recent.SkinAllColor = true;
            this.skinContextMenuStrip_recent.TitleAnamorphosis = false;
            this.skinContextMenuStrip_recent.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.skinContextMenuStrip_recent.TitleRadius = 4;
            this.skinContextMenuStrip_recent.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem2.Tag = "1";
            this.toolStripMenuItem2.Text = "发送即时消息";
            this.toolStripMenuItem2.ToolTipText = "表示希望好友看到您在线。\r\n声音：开启\r\n消息提醒框：开启\r\n会话消息：任务栏头像闪动\r\n";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem3.Text = "消息记录";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // 从列表中移除ToolStripMenuItem
            // 
            this.从列表中移除ToolStripMenuItem.Name = "从列表中移除ToolStripMenuItem";
            this.从列表中移除ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.从列表中移除ToolStripMenuItem.Text = "从列表中移除";
            this.从列表中移除ToolStripMenuItem.Click += new System.EventHandler(this.从列表中移除ToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Group1.png");
            // 
            // RecentListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chatListBox);
            this.Name = "RecentListBox";
            this.Size = new System.Drawing.Size(251, 415);
            this.skinContextMenuStrip_recent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinContextMenuStrip skinContextMenuStrip_recent;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private CCWin.SkinControl.ChatListBox chatListBox;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem 从列表中移除ToolStripMenuItem;
    }
}
