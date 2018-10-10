namespace GGTalk.UnitViews
{
    partial class GroupListBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupListBox));
            this.skinContextMenuStrip_Group = new CCWin.SkinControl.SkinContextMenuStrip();
            this.消息记录ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.解散该群ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chatListBox_group = new CCWin.SkinControl.ChatListBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.skinContextMenuStrip_Group.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinContextMenuStrip_Group
            // 
            this.skinContextMenuStrip_Group.Arrow = System.Drawing.Color.Black;
            this.skinContextMenuStrip_Group.Back = System.Drawing.Color.White;
            this.skinContextMenuStrip_Group.BackRadius = 4;
            this.skinContextMenuStrip_Group.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
            this.skinContextMenuStrip_Group.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.skinContextMenuStrip_Group.Fore = System.Drawing.Color.Black;
            this.skinContextMenuStrip_Group.HoverFore = System.Drawing.Color.White;
            this.skinContextMenuStrip_Group.ItemAnamorphosis = true;
            this.skinContextMenuStrip_Group.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinContextMenuStrip_Group.ItemBorderShow = true;
            this.skinContextMenuStrip_Group.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinContextMenuStrip_Group.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinContextMenuStrip_Group.ItemRadius = 4;
            this.skinContextMenuStrip_Group.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinContextMenuStrip_Group.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.消息记录ToolStripMenuItem1,
            this.toolStripMenuItem1,
            this.解散该群ToolStripMenuItem});
            this.skinContextMenuStrip_Group.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinContextMenuStrip_Group.Name = "skinContextMenuStrip1";
            this.skinContextMenuStrip_Group.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinContextMenuStrip_Group.Size = new System.Drawing.Size(139, 76);
            this.skinContextMenuStrip_Group.SkinAllColor = true;
            this.skinContextMenuStrip_Group.TitleAnamorphosis = true;
            this.skinContextMenuStrip_Group.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.skinContextMenuStrip_Group.TitleRadius = 4;
            this.skinContextMenuStrip_Group.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // 消息记录ToolStripMenuItem1
            // 
            this.消息记录ToolStripMenuItem1.Name = "消息记录ToolStripMenuItem1";
            this.消息记录ToolStripMenuItem1.Size = new System.Drawing.Size(138, 24);
            this.消息记录ToolStripMenuItem1.Text = "消息记录";
            this.消息记录ToolStripMenuItem1.Click += new System.EventHandler(this.消息记录ToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(138, 24);
            this.toolStripMenuItem1.Text = "退出该群";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // 解散该群ToolStripMenuItem
            // 
            this.解散该群ToolStripMenuItem.Name = "解散该群ToolStripMenuItem";
            this.解散该群ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.解散该群ToolStripMenuItem.Text = "解散该群";
            this.解散该群ToolStripMenuItem.Click += new System.EventHandler(this.解散该群ToolStripMenuItem_Click);
            // 
            // chatListBox_group
            // 
            this.chatListBox_group.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chatListBox_group.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatListBox_group.DrawContentType = CCWin.SkinControl.DrawContentType.PersonalMsg;
            this.chatListBox_group.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chatListBox_group.ForeColor = System.Drawing.Color.Black;
            this.chatListBox_group.FriendsMobile = false;
            chatListItem1.Bounds = new System.Drawing.Rectangle(0, 1, 226, 25);
            chatListItem1.IsOpen = true;
            chatListItem1.IsTwinkleHide = false;
            chatListItem1.OwnerChatListBox = this.chatListBox_group;
            chatListItem1.Text = "我的群";
            chatListItem1.TwinkleSubItemNumber = 0;
            this.chatListBox_group.Items.AddRange(new CCWin.SkinControl.ChatListItem[] {
            chatListItem1});
            this.chatListBox_group.ListHadOpenGroup = null;
            this.chatListBox_group.ListSubItemMenu = this.skinContextMenuStrip_Group;
            this.chatListBox_group.Location = new System.Drawing.Point(0, 0);
            this.chatListBox_group.Margin = new System.Windows.Forms.Padding(0);
            this.chatListBox_group.Name = "chatListBox_group";
            this.chatListBox_group.SelectSubItem = null;
            this.chatListBox_group.ShowNicName = false;
            this.chatListBox_group.Size = new System.Drawing.Size(226, 446);
            this.chatListBox_group.SubItemMenu = null;
            this.chatListBox_group.TabIndex = 133;
            this.chatListBox_group.DoubleClickSubItem += new CCWin.SkinControl.ChatListBox.ChatListEventHandler(this.chatListBox_group_DoubleClickSubItem);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Group1.png");
            // 
            // GroupListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chatListBox_group);
            this.Name = "GroupListBox";
            this.Size = new System.Drawing.Size(226, 446);
            this.skinContextMenuStrip_Group.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinContextMenuStrip skinContextMenuStrip_Group;
        private System.Windows.Forms.ToolStripMenuItem 消息记录ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 解散该群ToolStripMenuItem;
        private CCWin.SkinControl.ChatListBox chatListBox_group;
        private System.Windows.Forms.ImageList imageList1;
    }
}
