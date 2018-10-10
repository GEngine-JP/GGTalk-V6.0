using GGTalk.Controls;
namespace GGTalk
{
    partial class ChatRecordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatRecordForm));
            this.skinRadioButton_Server = new CCWin.SkinControl.SkinRadioButton();
            this.skinRadioButton1 = new CCWin.SkinControl.SkinRadioButton();
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel_totalCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox_pageIndex = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.skinComboBox1 = new CCWin.SkinControl.SkinComboBox();
            this.chatBox_history = new ChatBox();
            this.skinToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinRadioButton_Server
            // 
            this.skinRadioButton_Server.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.skinRadioButton_Server.AutoSize = true;
            this.skinRadioButton_Server.BackColor = System.Drawing.Color.Transparent;
            this.skinRadioButton_Server.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinRadioButton_Server.DownBack = null;
            this.skinRadioButton_Server.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinRadioButton_Server.Location = new System.Drawing.Point(688, 47);
            this.skinRadioButton_Server.MouseBack = null;
            this.skinRadioButton_Server.Name = "skinRadioButton_Server";
            this.skinRadioButton_Server.NormlBack = null;
            this.skinRadioButton_Server.SelectedDownBack = null;
            this.skinRadioButton_Server.SelectedMouseBack = null;
            this.skinRadioButton_Server.SelectedNormlBack = null;
            this.skinRadioButton_Server.Size = new System.Drawing.Size(98, 21);
            this.skinRadioButton_Server.TabIndex = 142;
            this.skinRadioButton_Server.Text = "从服务器获取";
            this.skinRadioButton_Server.UseVisualStyleBackColor = false;
            // 
            // skinRadioButton1
            // 
            this.skinRadioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.skinRadioButton1.AutoSize = true;
            this.skinRadioButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinRadioButton1.Checked = true;
            this.skinRadioButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinRadioButton1.DownBack = null;
            this.skinRadioButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinRadioButton1.Location = new System.Drawing.Point(596, 47);
            this.skinRadioButton1.MouseBack = null;
            this.skinRadioButton1.Name = "skinRadioButton1";
            this.skinRadioButton1.NormlBack = null;
            this.skinRadioButton1.SelectedDownBack = null;
            this.skinRadioButton1.SelectedMouseBack = null;
            this.skinRadioButton1.SelectedNormlBack = null;
            this.skinRadioButton1.Size = new System.Drawing.Size(86, 21);
            this.skinRadioButton1.TabIndex = 142;
            this.skinRadioButton1.TabStop = true;
            this.skinRadioButton1.Text = "从本地获取";
            this.skinRadioButton1.UseVisualStyleBackColor = false;
            this.skinRadioButton1.CheckedChanged += new System.EventHandler(this.skinRadioButton1_CheckedChanged);
            // 
            // skinToolStrip1
            // 
            this.skinToolStrip1.Arrow = System.Drawing.Color.Black;
            this.skinToolStrip1.Back = System.Drawing.Color.White;
            this.skinToolStrip1.BackRadius = 4;
            this.skinToolStrip1.BackRectangle = new System.Drawing.Rectangle(10, 10, 10, 10);
            this.skinToolStrip1.Base = System.Drawing.Color.Transparent;
            this.skinToolStrip1.BaseFore = System.Drawing.Color.Black;
            this.skinToolStrip1.BaseForeAnamorphosis = false;
            this.skinToolStrip1.BaseForeAnamorphosisBorder = 4;
            this.skinToolStrip1.BaseForeAnamorphosisColor = System.Drawing.Color.White;
            this.skinToolStrip1.BaseForeOffset = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.BaseHoverFore = System.Drawing.Color.White;
            this.skinToolStrip1.BaseItemAnamorphosis = true;
            this.skinToolStrip1.BaseItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.BaseItemBorderShow = true;
            this.skinToolStrip1.BaseItemDown = ((System.Drawing.Image)(resources.GetObject("skinToolStrip1.BaseItemDown")));
            this.skinToolStrip1.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.BaseItemMouse = ((System.Drawing.Image)(resources.GetObject("skinToolStrip1.BaseItemMouse")));
            this.skinToolStrip1.BaseItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.BaseItemRadius = 4;
            this.skinToolStrip1.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.BaseItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.skinToolStrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.skinToolStrip1.Fore = System.Drawing.Color.Black;
            this.skinToolStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.skinToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.skinToolStrip1.HoverFore = System.Drawing.Color.White;
            this.skinToolStrip1.ItemAnamorphosis = true;
            this.skinToolStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.ItemBorderShow = true;
            this.skinToolStrip1.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.ItemRadius = 4;
            this.skinToolStrip1.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripButton3,
            this.toolStripLabel_totalCount,
            this.toolStripLabel2,
            this.toolStripTextBox_pageIndex,
            this.toolStripLabel1,
            this.toolStripButton2,
            this.toolStripButton1});
            this.skinToolStrip1.Location = new System.Drawing.Point(4, 603);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(785, 25);
            this.skinToolStrip1.SkinAllColor = true;
            this.skinToolStrip1.TabIndex = 143;
            this.skinToolStrip1.Text = "skinToolStrip1";
            this.skinToolStrip1.TitleAnamorphosis = true;
            this.skinToolStrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.skinToolStrip1.TitleRadius = 4;
            this.skinToolStrip1.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "末页";
            this.toolStripButton4.Click += new System.EventHandler(this.skinButton_last_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "下一页";
            this.toolStripButton3.Click += new System.EventHandler(this.skinButton_next_Click);
            // 
            // toolStripLabel_totalCount
            // 
            this.toolStripLabel_totalCount.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel_totalCount.Name = "toolStripLabel_totalCount";
            this.toolStripLabel_totalCount.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel_totalCount.Text = "/ 1页";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(20, 22);
            this.toolStripLabel2.Text = "页";
            // 
            // toolStripTextBox_pageIndex
            // 
            this.toolStripTextBox_pageIndex.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_pageIndex.Name = "toolStripTextBox_pageIndex";
            this.toolStripTextBox_pageIndex.Size = new System.Drawing.Size(30, 25);
            this.toolStripTextBox_pageIndex.Text = "1";
            this.toolStripTextBox_pageIndex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox_pageIndex_KeyDown);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(20, 22);
            this.toolStripLabel1.Text = "第";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "上一页";
            this.toolStripButton2.Click += new System.EventHandler(this.skinButton_pre_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "首页";
            this.toolStripButton1.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // skinLabel1
            // 
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(7, 49);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(68, 17);
            this.skinLabel1.TabIndex = 144;
            this.skinLabel1.Text = "时间范围：";
            // 
            // skinComboBox1
            // 
            this.skinComboBox1.BaseColor = System.Drawing.Color.Transparent;
            this.skinComboBox1.BorderColor = System.Drawing.Color.Transparent;
            this.skinComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skinComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.skinComboBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinComboBox1.FormattingEnabled = true;
            this.skinComboBox1.Items.AddRange(new object[] {
            "最近一周",
            "最近一月",
            "最近三月",
            "全部"});
            this.skinComboBox1.Location = new System.Drawing.Point(71, 45);
            this.skinComboBox1.Name = "skinComboBox1";
            this.skinComboBox1.Size = new System.Drawing.Size(91, 24);
            this.skinComboBox1.TabIndex = 145;
            this.skinComboBox1.WaterText = "";
            this.skinComboBox1.SelectedIndexChanged += new System.EventHandler(this.skinComboBox1_SelectedIndexChanged);
            // 
            // chatBox_history
            // 
            this.chatBox_history.AllowDrop = true;
            this.chatBox_history.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatBox_history.BackColor = System.Drawing.Color.White;
            this.chatBox_history.ContextMenuMode = ChatBoxContextMenuMode.None;
            this.chatBox_history.Location = new System.Drawing.Point(4, 75);
            this.chatBox_history.Name = "chatBox_history";
            this.chatBox_history.ReadOnly = true;
            this.chatBox_history.Size = new System.Drawing.Size(785, 525);
            this.chatBox_history.TabIndex = 146;
            this.chatBox_history.Text = "";
            // 
            // ChatRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Back = ((System.Drawing.Image)(resources.GetObject("$this.Back")));
            this.BorderPalace = ((System.Drawing.Image)(resources.GetObject("$this.BorderPalace")));
            this.CanResize = true;
            this.ClientSize = new System.Drawing.Size(793, 632);
            this.CloseDownBack = global::GGTalk.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::GGTalk.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::GGTalk.Properties.Resources.btn_close_disable;
            this.Controls.Add(this.chatBox_history);
            this.Controls.Add(this.skinComboBox1);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.skinToolStrip1);
            this.Controls.Add(this.skinRadioButton1);
            this.Controls.Add(this.skinRadioButton_Server);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaxDownBack = global::GGTalk.Properties.Resources.btn_max_down;
            this.MaximizeBox = true;
            this.MaxMouseBack = global::GGTalk.Properties.Resources.btn_max_highlight;
            this.MaxNormlBack = global::GGTalk.Properties.Resources.btn_max_normal;
            this.MiniDownBack = global::GGTalk.Properties.Resources.btn_mini_down;
            this.MinimizeBox = true;
            this.MiniMouseBack = global::GGTalk.Properties.Resources.btn_mini_highlight;
            this.MiniNormlBack = global::GGTalk.Properties.Resources.btn_mini_normal;
            this.Name = "ChatRecordForm";
            this.RestoreDownBack = global::GGTalk.Properties.Resources.btn_restore_down;
            this.RestoreMouseBack = global::GGTalk.Properties.Resources.btn_restore_highlight;
            this.RestoreNormlBack = global::GGTalk.Properties.Resources.btn_restore_normal;
            this.ShowInTaskbar = true;
            this.Text = "消息记录";
            this.TopMost = false;
            this.Shown += new System.EventHandler(this.MessageRecordForm_Shown);
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinRadioButton skinRadioButton_Server;
        private CCWin.SkinControl.SkinRadioButton skinRadioButton1;
        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_pageIndex;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_totalCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinComboBox skinComboBox1;
        private ChatBox chatBox_history;

    }
}