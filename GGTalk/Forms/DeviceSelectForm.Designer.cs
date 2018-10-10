namespace GGTalk
{
    partial class DeviceSelectForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboBox_speaker = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_mic = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label_error3 = new System.Windows.Forms.Label();
            this.label_error2 = new System.Windows.Forms.Label();
            this.label_error = new System.Windows.Forms.Label();
            this.button_start = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.comboBox_camera = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_refresh = new System.Windows.Forms.Button();
            this.cameraConnector1 = new OMCS.Passive.Video.CameraConnector();
            this.microphoneConnector1 = new OMCS.Passive.Audio.MicrophoneConnector(this.components);
            this.label_info = new System.Windows.Forms.Label();
            this.decibelDisplayer1 = new GGTalk.Controls.DecibelDisplayer();
            this.SuspendLayout();
            // 
            // comboBox_speaker
            // 
            this.comboBox_speaker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_speaker.FormattingEnabled = true;
            this.comboBox_speaker.Location = new System.Drawing.Point(29, 104);
            this.comboBox_speaker.Name = "comboBox_speaker";
            this.comboBox_speaker.Size = new System.Drawing.Size(261, 20);
            this.comboBox_speaker.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 26;
            this.label3.Text = "扬声器：";
            // 
            // comboBox_mic
            // 
            this.comboBox_mic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_mic.FormattingEnabled = true;
            this.comboBox_mic.Location = new System.Drawing.Point(30, 63);
            this.comboBox_mic.Name = "comboBox_mic";
            this.comboBox_mic.Size = new System.Drawing.Size(261, 20);
            this.comboBox_mic.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "麦克风：";
            // 
            // label_error3
            // 
            this.label_error3.AutoSize = true;
            this.label_error3.ForeColor = System.Drawing.Color.Red;
            this.label_error3.Location = new System.Drawing.Point(88, 89);
            this.label_error3.Name = "label_error3";
            this.label_error3.Size = new System.Drawing.Size(71, 12);
            this.label_error3.TabIndex = 22;
            this.label_error3.Text = "label_error";
            this.label_error3.Visible = false;
            // 
            // label_error2
            // 
            this.label_error2.AutoSize = true;
            this.label_error2.ForeColor = System.Drawing.Color.Red;
            this.label_error2.Location = new System.Drawing.Point(88, 48);
            this.label_error2.Name = "label_error2";
            this.label_error2.Size = new System.Drawing.Size(71, 12);
            this.label_error2.TabIndex = 23;
            this.label_error2.Text = "label_error";
            this.label_error2.Visible = false;
            // 
            // label_error
            // 
            this.label_error.AutoSize = true;
            this.label_error.ForeColor = System.Drawing.Color.Red;
            this.label_error.Location = new System.Drawing.Point(88, 9);
            this.label_error.Name = "label_error";
            this.label_error.Size = new System.Drawing.Size(71, 12);
            this.label_error.TabIndex = 21;
            this.label_error.Text = "label_error";
            this.label_error.Visible = false;
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(131, 143);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 19;
            this.button_start.Text = "测试";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_stop
            // 
            this.button_stop.Enabled = false;
            this.button_stop.Location = new System.Drawing.Point(212, 143);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(75, 23);
            this.button_stop.TabIndex = 18;
            this.button_stop.Text = "停止";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // comboBox_camera
            // 
            this.comboBox_camera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_camera.FormattingEnabled = true;
            this.comboBox_camera.Location = new System.Drawing.Point(29, 25);
            this.comboBox_camera.Name = "comboBox_camera";
            this.comboBox_camera.Size = new System.Drawing.Size(261, 20);
            this.comboBox_camera.TabIndex = 17;
            this.comboBox_camera.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "摄像头：";
            // 
            // button_refresh
            // 
            this.button_refresh.Location = new System.Drawing.Point(29, 143);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(75, 23);
            this.button_refresh.TabIndex = 28;
            this.button_refresh.Text = "刷新设备";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // cameraConnector1
            // 
            this.cameraConnector1.AutoSynchronizeVideoToAudio = true;
            this.cameraConnector1.BackColor = System.Drawing.Color.Black;
            this.cameraConnector1.Location = new System.Drawing.Point(300, 21);
            this.cameraConnector1.Name = "cameraConnector1";
            this.cameraConnector1.Size = new System.Drawing.Size(160, 120);
            this.cameraConnector1.TabIndex = 20;
            this.cameraConnector1.WaitOwnerOnlineSpanInSecs = 0;
            // 
            // microphoneConnector1
            // 
            this.microphoneConnector1.Mute = false;
            this.microphoneConnector1.SpringReceivedEventWhenMute = false;
            this.microphoneConnector1.WaitOwnerOnlineSpanInSecs = 0;
            // 
            // label_info
            // 
            this.label_info.AutoSize = true;
            this.label_info.ForeColor = System.Drawing.Color.Red;
            this.label_info.Location = new System.Drawing.Point(298, 148);
            this.label_info.Name = "label_info";
            this.label_info.Size = new System.Drawing.Size(89, 12);
            this.label_info.TabIndex = 22;
            this.label_info.Text = "正在启动 . . .";
            this.label_info.Visible = false;
            // 
            // decibelDisplayer1
            // 
            this.decibelDisplayer1.BackColor = System.Drawing.Color.White;
            this.decibelDisplayer1.Location = new System.Drawing.Point(423, 143);
            this.decibelDisplayer1.Name = "decibelDisplayer1";
            this.decibelDisplayer1.Size = new System.Drawing.Size(37, 10);
            this.decibelDisplayer1.TabIndex = 29;
            this.decibelDisplayer1.ValueVisialbe = false;
            this.decibelDisplayer1.Working = true;
            // 
            // DeviceSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 190);
            this.Controls.Add(this.decibelDisplayer1);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.comboBox_speaker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_mic);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_info);
            this.Controls.Add(this.label_error3);
            this.Controls.Add(this.label_error2);
            this.Controls.Add(this.label_error);
            this.Controls.Add(this.cameraConnector1);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.comboBox_camera);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeviceSelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择音视频设备";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeviceSelectForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OMCS.Passive.Audio.MicrophoneConnector microphoneConnector1;
        private System.Windows.Forms.ComboBox comboBox_speaker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_mic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_error3;
        private System.Windows.Forms.Label label_error2;
        private System.Windows.Forms.Label label_error;
        private OMCS.Passive.Video.CameraConnector cameraConnector1;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.ComboBox comboBox_camera;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_refresh;
        private System.Windows.Forms.Label label_info;
        private GGTalk.Controls.DecibelDisplayer decibelDisplayer1;
    }
}

