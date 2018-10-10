namespace GGTalk
{
    partial class SystemNotifySendForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemNotifySendForm));
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.skinTextBox_id = new CCWin.SkinControl.SkinTextBox();
            this.btnClose = new CCWin.SkinControl.SkinButton();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.skinRadioButton_all = new CCWin.SkinControl.SkinRadioButton();
            this.skinRadioButton_group = new CCWin.SkinControl.SkinRadioButton();
            this.skinLabel_gID = new CCWin.SkinControl.SkinLabel();
            this.skinTextBox_groupID = new CCWin.SkinControl.SkinTextBox();
            this.skinTextBox_id.SuspendLayout();
            this.skinTextBox_groupID.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinLabel1
            // 
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(15, 84);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(44, 17);
            this.skinLabel1.TabIndex = 0;
            this.skinLabel1.Text = "标题：";
            // 
            // skinTextBox_id
            // 
            this.skinTextBox_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinTextBox_id.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox_id.Icon = null;
            this.skinTextBox_id.IconIsButton = false;
            this.skinTextBox_id.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_id.Location = new System.Drawing.Point(65, 84);
            this.skinTextBox_id.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_id.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox_id.MouseBack = null;
            this.skinTextBox_id.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_id.Name = "skinTextBox_id";
            this.skinTextBox_id.NormlBack = null;
            this.skinTextBox_id.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox_id.Size = new System.Drawing.Size(502, 28);
            // 
            // skinTextBox_id.BaseText
            // 
            this.skinTextBox_id.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_id.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_id.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_id.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox_id.SkinTxt.Name = "BaseText";
            this.skinTextBox_id.SkinTxt.Size = new System.Drawing.Size(492, 18);
            this.skinTextBox_id.SkinTxt.TabIndex = 0;
            this.skinTextBox_id.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox_id.SkinTxt.WaterText = "";
            this.skinTextBox_id.TabIndex = 129;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.btnClose.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.DownBack = ((System.Drawing.Image)(resources.GetObject("btnClose.DownBack")));
            this.btnClose.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(498, 353);
            this.btnClose.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnClose.MouseBack")));
            this.btnClose.Name = "btnClose";
            this.btnClose.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnClose.NormlBack")));
            this.btnClose.Size = new System.Drawing.Size(69, 24);
            this.btnClose.TabIndex = 133;
            this.btnClose.Text = "发送";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // skinLabel2
            // 
            this.skinLabel2.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel2.AutoSize = true;
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(15, 115);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(44, 17);
            this.skinLabel2.TabIndex = 0;
            this.skinLabel2.Text = "内容：";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(65, 115);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(502, 220);
            this.richTextBox1.TabIndex = 134;
            this.richTextBox1.Text = "";
            // 
            // skinRadioButton_all
            // 
            this.skinRadioButton_all.AutoSize = true;
            this.skinRadioButton_all.BackColor = System.Drawing.Color.Transparent;
            this.skinRadioButton_all.Checked = true;
            this.skinRadioButton_all.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinRadioButton_all.DownBack = null;
            this.skinRadioButton_all.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinRadioButton_all.Location = new System.Drawing.Point(65, 49);
            this.skinRadioButton_all.MouseBack = null;
            this.skinRadioButton_all.Name = "skinRadioButton_all";
            this.skinRadioButton_all.NormlBack = null;
            this.skinRadioButton_all.SelectedDownBack = null;
            this.skinRadioButton_all.SelectedMouseBack = null;
            this.skinRadioButton_all.SelectedNormlBack = null;
            this.skinRadioButton_all.Size = new System.Drawing.Size(134, 21);
            this.skinRadioButton_all.TabIndex = 135;
            this.skinRadioButton_all.TabStop = true;
            this.skinRadioButton_all.Text = "发送给所有在线用户";
            this.skinRadioButton_all.UseVisualStyleBackColor = false;
            // 
            // skinRadioButton_group
            // 
            this.skinRadioButton_group.AutoSize = true;
            this.skinRadioButton_group.BackColor = System.Drawing.Color.Transparent;
            this.skinRadioButton_group.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinRadioButton_group.DownBack = null;
            this.skinRadioButton_group.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinRadioButton_group.Location = new System.Drawing.Point(214, 49);
            this.skinRadioButton_group.MouseBack = null;
            this.skinRadioButton_group.Name = "skinRadioButton_group";
            this.skinRadioButton_group.NormlBack = null;
            this.skinRadioButton_group.SelectedDownBack = null;
            this.skinRadioButton_group.SelectedMouseBack = null;
            this.skinRadioButton_group.SelectedNormlBack = null;
            this.skinRadioButton_group.Size = new System.Drawing.Size(98, 21);
            this.skinRadioButton_group.TabIndex = 135;
            this.skinRadioButton_group.Text = "发送给指定组";
            this.skinRadioButton_group.UseVisualStyleBackColor = false;
            this.skinRadioButton_group.CheckedChanged += new System.EventHandler(this.skinRadioButton_group_CheckedChanged);
            // 
            // skinLabel_gID
            // 
            this.skinLabel_gID.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel_gID.AutoSize = true;
            this.skinLabel_gID.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_gID.BorderColor = System.Drawing.Color.White;
            this.skinLabel_gID.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel_gID.Location = new System.Drawing.Point(15, 342);
            this.skinLabel_gID.Name = "skinLabel_gID";
            this.skinLabel_gID.Size = new System.Drawing.Size(45, 17);
            this.skinLabel_gID.TabIndex = 0;
            this.skinLabel_gID.Text = "组ID：";
            this.skinLabel_gID.Visible = false;
            // 
            // skinTextBox_groupID
            // 
            this.skinTextBox_groupID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinTextBox_groupID.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox_groupID.Icon = null;
            this.skinTextBox_groupID.IconIsButton = false;
            this.skinTextBox_groupID.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_groupID.Location = new System.Drawing.Point(63, 338);
            this.skinTextBox_groupID.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_groupID.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox_groupID.MouseBack = null;
            this.skinTextBox_groupID.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_groupID.Name = "skinTextBox_groupID";
            this.skinTextBox_groupID.NormlBack = null;
            this.skinTextBox_groupID.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox_groupID.Size = new System.Drawing.Size(119, 28);
            // 
            // skinTextBox_groupID.BaseText
            // 
            this.skinTextBox_groupID.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_groupID.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_groupID.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_groupID.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox_groupID.SkinTxt.Name = "BaseText";
            this.skinTextBox_groupID.SkinTxt.Size = new System.Drawing.Size(109, 18);
            this.skinTextBox_groupID.SkinTxt.TabIndex = 0;
            this.skinTextBox_groupID.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox_groupID.SkinTxt.WaterText = "";
            this.skinTextBox_groupID.TabIndex = 136;
            this.skinTextBox_groupID.Visible = false;
            // 
            // SystemNotifySendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Back = ((System.Drawing.Image)(resources.GetObject("$this.Back")));
            this.BorderPalace = ((System.Drawing.Image)(resources.GetObject("$this.BorderPalace")));
            this.ClientSize = new System.Drawing.Size(574, 395);
            this.CloseDownBack = global::GGTalk.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::GGTalk.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::GGTalk.Properties.Resources.btn_close_disable;
            this.Controls.Add(this.skinTextBox_groupID);
            this.Controls.Add(this.skinRadioButton_group);
            this.Controls.Add(this.skinRadioButton_all);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.skinTextBox_id);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.skinLabel_gID);
            this.Controls.Add(this.skinLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaxDownBack = global::GGTalk.Properties.Resources.btn_max_down;
            this.MaxMouseBack = global::GGTalk.Properties.Resources.btn_max_highlight;
            this.MaxNormlBack = global::GGTalk.Properties.Resources.btn_max_normal;
            this.MiniDownBack = global::GGTalk.Properties.Resources.btn_mini_down;
            this.MiniMouseBack = global::GGTalk.Properties.Resources.btn_mini_highlight;
            this.MiniNormlBack = global::GGTalk.Properties.Resources.btn_mini_normal;
            this.Name = "SystemNotifySendForm";
            this.RestoreDownBack = global::GGTalk.Properties.Resources.btn_restore_down;
            this.RestoreMouseBack = global::GGTalk.Properties.Resources.btn_restore_highlight;
            this.RestoreNormlBack = global::GGTalk.Properties.Resources.btn_restore_normal;
            this.Text = "发送系统通知";
            this.Load += new System.EventHandler(this.SystemNotifySendForm_Load);
            this.skinTextBox_id.ResumeLayout(false);
            this.skinTextBox_id.PerformLayout();
            this.skinTextBox_groupID.ResumeLayout(false);
            this.skinTextBox_groupID.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinTextBox skinTextBox_id;
        private CCWin.SkinControl.SkinButton btnClose;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private CCWin.SkinControl.SkinRadioButton skinRadioButton_all;
        private CCWin.SkinControl.SkinRadioButton skinRadioButton_group;
        private CCWin.SkinControl.SkinLabel skinLabel_gID;
        private CCWin.SkinControl.SkinTextBox skinTextBox_groupID;
    }
}