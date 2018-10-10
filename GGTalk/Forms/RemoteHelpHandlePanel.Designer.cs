namespace GGTalk
{
    partial class RemoteHelpHandlePanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteHelpHandlePanel));
            this.skinPanel2 = new CCWin.SkinControl.SkinPanel();
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.skinLabel_msg = new CCWin.SkinControl.SkinLabel();
            this.timerLabel1 = new ESBasic.Widget.TimerLabel();
            this.SuspendLayout();
            // 
            // skinPanel2
            // 
            this.skinPanel2.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel2.BackgroundImage = global::GGTalk.Properties.Resources.RemoteHelp;
            this.skinPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.skinPanel2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel2.DownBack = null;
            this.skinPanel2.Location = new System.Drawing.Point(48, 55);
            this.skinPanel2.MouseBack = null;
            this.skinPanel2.Name = "skinPanel2";
            this.skinPanel2.NormlBack = null;
            this.skinPanel2.Size = new System.Drawing.Size(96, 96);
            this.skinPanel2.TabIndex = 129;
            // 
            // skinButton1
            // 
            this.skinButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.skinButton1.DownBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.DownBack")));
            this.skinButton1.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.skinButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinButton1.Image = global::GGTalk.Properties.Resources.AV_Refuse;
            this.skinButton1.Location = new System.Drawing.Point(66, 188);
            this.skinButton1.MouseBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.MouseBack")));
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.NormlBack")));
            this.skinButton1.Size = new System.Drawing.Size(69, 24);
            this.skinButton1.TabIndex = 131;
            this.skinButton1.Text = "终止";
            this.skinButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.skinButtomReject_Click);
            // 
            // skinLabel_msg
            // 
            this.skinLabel_msg.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.skinLabel_msg.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel_msg.AutoSize = true;
            this.skinLabel_msg.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_msg.BorderColor = System.Drawing.Color.White;
            this.skinLabel_msg.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel_msg.Location = new System.Drawing.Point(38, 165);
            this.skinLabel_msg.Name = "skinLabel_msg";
            this.skinLabel_msg.Size = new System.Drawing.Size(125, 17);
            this.skinLabel_msg.TabIndex = 130;
            this.skinLabel_msg.Text = "正在等待对方回复 . . .";
            // 
            // timerLabel1
            // 
            this.timerLabel1.AutoSize = true;
            this.timerLabel1.BackColor = System.Drawing.Color.Transparent;
            this.timerLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.timerLabel1.Location = new System.Drawing.Point(58, 164);
            this.timerLabel1.Name = "timerLabel1";
            this.timerLabel1.Size = new System.Drawing.Size(76, 17);
            this.timerLabel1.TabIndex = 132;
            this.timerLabel1.Text = "timerLabel1";
            this.timerLabel1.Visible = false;
            // 
            // RemoteHelpHandlePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.timerLabel1);
            this.Controls.Add(this.skinButton1);
            this.Controls.Add(this.skinLabel_msg);
            this.Controls.Add(this.skinPanel2);
            this.Name = "RemoteHelpHandlePanel";
            this.Size = new System.Drawing.Size(192, 419);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinPanel skinPanel2;
        private CCWin.SkinControl.SkinButton skinButton1;
        private CCWin.SkinControl.SkinLabel skinLabel_msg;
        private ESBasic.Widget.TimerLabel timerLabel1;
    }
}
