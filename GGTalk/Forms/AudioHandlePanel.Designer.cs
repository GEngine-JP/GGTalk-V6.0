using GGTalk.Controls;
namespace GGTalk
{
    partial class AudioHandlePanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioHandlePanel));
            this.microphoneConnector1 = new OMCS.Passive.Audio.MicrophoneConnector(this.components);
            this.timerLabel1 = new ESBasic.Widget.TimerLabel();
            this.skinButtomReject = new CCWin.SkinControl.SkinButton();
            this.btnAccept = new CCWin.SkinControl.SkinButton();
            this.skinButton_HungUp = new CCWin.SkinControl.SkinButton();
            this.skinLabel_msg = new CCWin.SkinControl.SkinLabel();
            this.skinPanel2 = new CCWin.SkinControl.SkinPanel();
            this.channelQualityDisplayer1 = new ChannelQualityDisplayer();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.panel_decibel = new System.Windows.Forms.Panel();
            this.decibelDisplayer2 = new DecibelDisplayer();
            this.decibelDisplayer1 = new DecibelDisplayer();
            this.panel_decibel.SuspendLayout();
            this.SuspendLayout();
            // 
            // microphoneConnector1
            // 
            this.microphoneConnector1.Mute = false;
            this.microphoneConnector1.WaitOwnerOnlineSpanInSecs = 0;
            // 
            // timerLabel1
            // 
            this.timerLabel1.AutoSize = true;
            this.timerLabel1.BackColor = System.Drawing.Color.Transparent;
            this.timerLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.timerLabel1.Location = new System.Drawing.Point(59, 165);
            this.timerLabel1.Name = "timerLabel1";
            this.timerLabel1.Size = new System.Drawing.Size(77, 17);
            this.timerLabel1.TabIndex = 134;
            this.timerLabel1.Text = "正在连接 . . .";
            // 
            // skinButtomReject
            // 
            this.skinButtomReject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.skinButtomReject.BackColor = System.Drawing.Color.Transparent;
            this.skinButtomReject.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButtomReject.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.skinButtomReject.DownBack = ((System.Drawing.Image)(resources.GetObject("skinButtomReject.DownBack")));
            this.skinButtomReject.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.skinButtomReject.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinButtomReject.Image = global::GGTalk.Properties.Resources.AV_Refuse;
            this.skinButtomReject.Location = new System.Drawing.Point(63, 195);
            this.skinButtomReject.MouseBack = ((System.Drawing.Image)(resources.GetObject("skinButtomReject.MouseBack")));
            this.skinButtomReject.Name = "skinButtomReject";
            this.skinButtomReject.NormlBack = ((System.Drawing.Image)(resources.GetObject("skinButtomReject.NormlBack")));
            this.skinButtomReject.Size = new System.Drawing.Size(69, 24);
            this.skinButtomReject.TabIndex = 132;
            this.skinButtomReject.Text = "拒绝";
            this.skinButtomReject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.skinButtomReject.UseVisualStyleBackColor = false;
            this.skinButtomReject.Click += new System.EventHandler(this.skinButtomReject_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.BackColor = System.Drawing.Color.Transparent;
            this.btnAccept.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAccept.DownBack = ((System.Drawing.Image)(resources.GetObject("btnAccept.DownBack")));
            this.btnAccept.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnAccept.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAccept.Image = global::GGTalk.Properties.Resources.AV_Accept;
            this.btnAccept.Location = new System.Drawing.Point(63, 165);
            this.btnAccept.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnAccept.MouseBack")));
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnAccept.NormlBack")));
            this.btnAccept.Size = new System.Drawing.Size(69, 24);
            this.btnAccept.TabIndex = 133;
            this.btnAccept.Text = "接受";
            this.btnAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // skinButton_HungUp
            // 
            this.skinButton_HungUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.skinButton_HungUp.BackColor = System.Drawing.Color.Transparent;
            this.skinButton_HungUp.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton_HungUp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.skinButton_HungUp.DownBack = ((System.Drawing.Image)(resources.GetObject("skinButton_HungUp.DownBack")));
            this.skinButton_HungUp.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.skinButton_HungUp.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinButton_HungUp.Image = global::GGTalk.Properties.Resources.AV_Refuse;
            this.skinButton_HungUp.Location = new System.Drawing.Point(63, 188);
            this.skinButton_HungUp.MouseBack = ((System.Drawing.Image)(resources.GetObject("skinButton_HungUp.MouseBack")));
            this.skinButton_HungUp.Name = "skinButton_HungUp";
            this.skinButton_HungUp.NormlBack = ((System.Drawing.Image)(resources.GetObject("skinButton_HungUp.NormlBack")));
            this.skinButton_HungUp.Size = new System.Drawing.Size(69, 24);
            this.skinButton_HungUp.TabIndex = 131;
            this.skinButton_HungUp.Text = "挂断";
            this.skinButton_HungUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.skinButton_HungUp.UseVisualStyleBackColor = false;
            this.skinButton_HungUp.Click += new System.EventHandler(this.skinButton_HungUp_Click);
            // 
            // skinLabel_msg
            // 
            this.skinLabel_msg.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.skinLabel_msg.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel_msg.AutoSize = true;
            this.skinLabel_msg.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_msg.BorderColor = System.Drawing.Color.White;
            this.skinLabel_msg.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel_msg.Location = new System.Drawing.Point(35, 165);
            this.skinLabel_msg.Name = "skinLabel_msg";
            this.skinLabel_msg.Size = new System.Drawing.Size(125, 17);
            this.skinLabel_msg.TabIndex = 130;
            this.skinLabel_msg.Text = "正在等待对方回复 . . .";
            // 
            // skinPanel2
            // 
            this.skinPanel2.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel2.BackgroundImage = global::GGTalk.Properties.Resources.mic2;
            this.skinPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.skinPanel2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel2.DownBack = null;
            this.skinPanel2.Location = new System.Drawing.Point(49, 55);
            this.skinPanel2.MouseBack = null;
            this.skinPanel2.Name = "skinPanel2";
            this.skinPanel2.NormlBack = null;
            this.skinPanel2.Size = new System.Drawing.Size(96, 96);
            this.skinPanel2.TabIndex = 129;
            // 
            // channelQualityDisplayer1
            // 
            this.channelQualityDisplayer1.BackColor = System.Drawing.Color.Transparent;
            this.channelQualityDisplayer1.ColorBadSignal = System.Drawing.Color.Red;
            this.channelQualityDisplayer1.ColorNoSignal = System.Drawing.Color.LightGray;
            this.channelQualityDisplayer1.ColorSignal = System.Drawing.Color.Green;
            this.channelQualityDisplayer1.Location = new System.Drawing.Point(75, 152);
            this.channelQualityDisplayer1.Name = "channelQualityDisplayer1";
            this.channelQualityDisplayer1.Size = new System.Drawing.Size(44, 16);
            this.channelQualityDisplayer1.TabIndex = 135;
            this.channelQualityDisplayer1.Visible = false;
            // 
            // skinLabel1
            // 
            this.skinLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(92, 5);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(44, 17);
            this.skinLabel1.TabIndex = 130;
            this.skinLabel1.Text = "喇叭：";
            // 
            // skinLabel2
            // 
            this.skinLabel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.skinLabel2.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel2.AutoSize = true;
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(22, 5);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(44, 17);
            this.skinLabel2.TabIndex = 130;
            this.skinLabel2.Text = "话筒：";
            // 
            // panel_decibel
            // 
            this.panel_decibel.Controls.Add(this.decibelDisplayer1);
            this.panel_decibel.Controls.Add(this.decibelDisplayer2);
            this.panel_decibel.Controls.Add(this.skinLabel2);
            this.panel_decibel.Controls.Add(this.skinLabel1);
            this.panel_decibel.Location = new System.Drawing.Point(9, 213);
            this.panel_decibel.Name = "panel_decibel";
            this.panel_decibel.Size = new System.Drawing.Size(174, 26);
            this.panel_decibel.TabIndex = 137;
            this.panel_decibel.Visible = false;
            // 
            // decibelDisplayer2
            // 
            this.decibelDisplayer2.BackColor = System.Drawing.Color.White;
            this.decibelDisplayer2.Location = new System.Drawing.Point(60, 10);
            this.decibelDisplayer2.Name = "decibelDisplayer2";
            this.decibelDisplayer2.Size = new System.Drawing.Size(25, 10);
            this.decibelDisplayer2.TabIndex = 136;
            this.decibelDisplayer2.ValueVisialbe = false;
            // 
            // decibelDisplayer1
            // 
            this.decibelDisplayer1.BackColor = System.Drawing.Color.White;
            this.decibelDisplayer1.Location = new System.Drawing.Point(130, 10);
            this.decibelDisplayer1.Name = "decibelDisplayer1";
            this.decibelDisplayer1.Size = new System.Drawing.Size(25, 10);
            this.decibelDisplayer1.TabIndex = 136;
            this.decibelDisplayer1.ValueVisialbe = false;
            // 
            // AudioHandlePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel_decibel);
            this.Controls.Add(this.channelQualityDisplayer1);
            this.Controls.Add(this.timerLabel1);
            this.Controls.Add(this.skinButtomReject);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.skinButton_HungUp);
            this.Controls.Add(this.skinLabel_msg);
            this.Controls.Add(this.skinPanel2);
            this.Name = "AudioHandlePanel";
            this.Size = new System.Drawing.Size(192, 419);
            this.panel_decibel.ResumeLayout(false);
            this.panel_decibel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinPanel skinPanel2;
        private CCWin.SkinControl.SkinButton skinButton_HungUp;
        private CCWin.SkinControl.SkinLabel skinLabel_msg;
        private CCWin.SkinControl.SkinButton skinButtomReject;
        private CCWin.SkinControl.SkinButton btnAccept;
        private OMCS.Passive.Audio.MicrophoneConnector microphoneConnector1;
        private ESBasic.Widget.TimerLabel timerLabel1;
        private ChannelQualityDisplayer channelQualityDisplayer1;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private DecibelDisplayer decibelDisplayer1;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private DecibelDisplayer decibelDisplayer2;
        private System.Windows.Forms.Panel panel_decibel;
    }
}
