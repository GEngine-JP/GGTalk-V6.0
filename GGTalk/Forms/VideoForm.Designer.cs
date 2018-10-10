using GGTalk.Controls;
namespace GGTalk
{
    partial class VideoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.skinButton_State = new CCWin.SkinControl.SkinButton();
            this.pictureBox_disMic = new System.Windows.Forms.PictureBox();
            this.pictureBox_disCamera = new System.Windows.Forms.PictureBox();
            this.skinCheckBox_HighR = new CCWin.SkinControl.SkinCheckBox();
            this.skinCheckBox_autoAdjustQulity = new CCWin.SkinControl.SkinCheckBox();
            this.skinComboBox_quality = new CCWin.SkinControl.SkinComboBox();
            this.skinLabel_tip = new CCWin.SkinControl.SkinLabel();
            this.skinPanel_tool = new CCWin.SkinControl.SkinPanel();
            this.button_record = new CCWin.SkinControl.SkinButton();
            this.decibelDisplayer_speaker = new GGTalk.Controls.DecibelDisplayer();
            this.decibelDisplayer_mic = new GGTalk.Controls.DecibelDisplayer();
            this.skinCheckBox_camera = new CCWin.SkinControl.SkinCheckBox();
            this.skinCheckBox_my = new CCWin.SkinControl.SkinCheckBox();
            this.timerLabel1 = new ESBasic.Widget.TimerLabel();
            this.channelQualityDisplayer1 = new GGTalk.Controls.ChannelQualityDisplayer();
            this.skinCheckBox_mic = new CCWin.SkinControl.SkinCheckBox();
            this.skinCheckBox_speaker = new CCWin.SkinControl.SkinCheckBox();
            this.skinPanel1 = new CCWin.SkinControl.SkinPanel();
            this.skinLabel_cameraError = new CCWin.SkinControl.SkinLabel();
            this.label_record = new System.Windows.Forms.Label();
            this.skinPanel_Myself = new CCWin.SkinControl.SkinPanel();
            this.cameraConnector1 = new OMCS.Passive.Video.CameraConnector();
            this.dynamicCameraConnector1 = new OMCS.Passive.Video.DynamicCameraConnector(this.components);
            this.microphoneConnector1 = new OMCS.Passive.Audio.MicrophoneConnector(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_disMic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_disCamera)).BeginInit();
            this.skinPanel_tool.SuspendLayout();
            this.skinPanel1.SuspendLayout();
            this.skinPanel_Myself.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "GVideoTurnOnVideo.png");
            this.imageList1.Images.SetKeyName(1, "GVideoTurnOffVideo.png");
            this.imageList1.Images.SetKeyName(2, "remind_highlight.png");
            this.imageList1.Images.SetKeyName(3, "GVShareVideoCloseSpk_MouseOver.png");
            this.imageList1.Images.SetKeyName(4, "AM_MenuICON.png");
            this.imageList1.Images.SetKeyName(5, "AV_New_Mic_Style3.png");
            // 
            // skinButton_State
            // 
            this.skinButton_State.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.skinButton_State.BackColor = System.Drawing.Color.Transparent;
            this.skinButton_State.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.skinButton_State.BackRectangle = new System.Drawing.Rectangle(4, 4, 4, 4);
            this.skinButton_State.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.skinButton_State.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton_State.DownBack = global::GGTalk.Properties.Resources.allbtn_down;
            this.skinButton_State.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.skinButton_State.Image = global::GGTalk.Properties.Resources.HDVideoHangs;
            this.skinButton_State.ImageSize = new System.Drawing.Size(23, 20);
            this.skinButton_State.Location = new System.Drawing.Point(479, 422);
            this.skinButton_State.Margin = new System.Windows.Forms.Padding(0);
            this.skinButton_State.MouseBack = global::GGTalk.Properties.Resources.allbtn_highlight;
            this.skinButton_State.Name = "skinButton_State";
            this.skinButton_State.NormlBack = null;
            this.skinButton_State.Palace = true;
            this.skinButton_State.Size = new System.Drawing.Size(29, 23);
            this.skinButton_State.TabIndex = 129;
            this.skinButton_State.Tag = "1";
            this.toolTip1.SetToolTip(this.skinButton_State, "挂断");
            this.skinButton_State.UseVisualStyleBackColor = false;
            this.skinButton_State.Click += new System.EventHandler(this.skinButton_State_Click);
            this.skinButton_State.MouseEnter += new System.EventHandler(this.FocusCurrent);
            // 
            // pictureBox_disMic
            // 
            this.pictureBox_disMic.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_disMic.BackgroundImage")));
            this.pictureBox_disMic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_disMic.Location = new System.Drawing.Point(3, 3);
            this.pictureBox_disMic.Name = "pictureBox_disMic";
            this.pictureBox_disMic.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_disMic.TabIndex = 1;
            this.pictureBox_disMic.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox_disMic, "对方禁用了麦克风");
            this.pictureBox_disMic.Visible = false;
            this.pictureBox_disMic.MouseEnter += new System.EventHandler(this.FocusCurrent);
            // 
            // pictureBox_disCamera
            // 
            this.pictureBox_disCamera.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox_disCamera.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_disCamera.Image")));
            this.pictureBox_disCamera.Location = new System.Drawing.Point(219, 171);
            this.pictureBox_disCamera.Name = "pictureBox_disCamera";
            this.pictureBox_disCamera.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_disCamera.TabIndex = 1;
            this.pictureBox_disCamera.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox_disCamera, "对方禁用了摄像头");
            this.pictureBox_disCamera.Visible = false;
            this.pictureBox_disCamera.MouseEnter += new System.EventHandler(this.FocusCurrent);
            // 
            // skinCheckBox_HighR
            // 
            this.skinCheckBox_HighR.AutoSize = true;
            this.skinCheckBox_HighR.BackColor = System.Drawing.Color.Transparent;
            this.skinCheckBox_HighR.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinCheckBox_HighR.DownBack = null;
            this.skinCheckBox_HighR.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinCheckBox_HighR.Location = new System.Drawing.Point(174, 23);
            this.skinCheckBox_HighR.MouseBack = null;
            this.skinCheckBox_HighR.Name = "skinCheckBox_HighR";
            this.skinCheckBox_HighR.NormlBack = null;
            this.skinCheckBox_HighR.SelectedDownBack = null;
            this.skinCheckBox_HighR.SelectedMouseBack = null;
            this.skinCheckBox_HighR.SelectedNormlBack = null;
            this.skinCheckBox_HighR.Size = new System.Drawing.Size(75, 21);
            this.skinCheckBox_HighR.TabIndex = 8;
            this.skinCheckBox_HighR.Text = "高分辨率";
            this.toolTip1.SetToolTip(this.skinCheckBox_HighR, "高分辨视频需要更大的带宽支持");
            this.skinCheckBox_HighR.UseVisualStyleBackColor = false;
            this.skinCheckBox_HighR.CheckedChanged += new System.EventHandler(this.skinCheckBox_HighR_CheckedChanged);
            // 
            // skinCheckBox_autoAdjustQulity
            // 
            this.skinCheckBox_autoAdjustQulity.AutoSize = true;
            this.skinCheckBox_autoAdjustQulity.BackColor = System.Drawing.Color.Transparent;
            this.skinCheckBox_autoAdjustQulity.Checked = true;
            this.skinCheckBox_autoAdjustQulity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.skinCheckBox_autoAdjustQulity.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinCheckBox_autoAdjustQulity.DownBack = null;
            this.skinCheckBox_autoAdjustQulity.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinCheckBox_autoAdjustQulity.Location = new System.Drawing.Point(175, 2);
            this.skinCheckBox_autoAdjustQulity.MouseBack = null;
            this.skinCheckBox_autoAdjustQulity.Name = "skinCheckBox_autoAdjustQulity";
            this.skinCheckBox_autoAdjustQulity.NormlBack = null;
            this.skinCheckBox_autoAdjustQulity.SelectedDownBack = null;
            this.skinCheckBox_autoAdjustQulity.SelectedMouseBack = null;
            this.skinCheckBox_autoAdjustQulity.SelectedNormlBack = null;
            this.skinCheckBox_autoAdjustQulity.Size = new System.Drawing.Size(123, 21);
            this.skinCheckBox_autoAdjustQulity.TabIndex = 9;
            this.skinCheckBox_autoAdjustQulity.Text = "自动调整视频质量";
            this.toolTip1.SetToolTip(this.skinCheckBox_autoAdjustQulity, "根据网络快慢自动调节好友视频的质量");
            this.skinCheckBox_autoAdjustQulity.UseVisualStyleBackColor = false;
            this.skinCheckBox_autoAdjustQulity.CheckedChanged += new System.EventHandler(this.skinCheckBox_autoAdjustQulity_CheckedChanged);
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
            this.skinComboBox_quality.Location = new System.Drawing.Point(294, 1);
            this.skinComboBox_quality.Name = "skinComboBox_quality";
            this.skinComboBox_quality.Size = new System.Drawing.Size(46, 22);
            this.skinComboBox_quality.TabIndex = 5;
            this.toolTip1.SetToolTip(this.skinComboBox_quality, "手动控制好友视频的质量");
            this.skinComboBox_quality.Visible = false;
            this.skinComboBox_quality.WaterText = "";
            this.skinComboBox_quality.SelectedIndexChanged += new System.EventHandler(this.skinComboBox_quality_SelectedIndexChanged);
            // 
            // skinLabel_tip
            // 
            this.skinLabel_tip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.skinLabel_tip.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel_tip.AutoSize = true;
            this.skinLabel_tip.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_tip.BorderColor = System.Drawing.Color.White;
            this.skinLabel_tip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel_tip.Location = new System.Drawing.Point(182, 52);
            this.skinLabel_tip.Name = "skinLabel_tip";
            this.skinLabel_tip.Size = new System.Drawing.Size(149, 17);
            this.skinLabel_tip.TabIndex = 4;
            this.skinLabel_tip.Text = "正在等待对方接收邀请 . . .";
            // 
            // skinPanel_tool
            // 
            this.skinPanel_tool.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinPanel_tool.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel_tool.Controls.Add(this.button_record);
            this.skinPanel_tool.Controls.Add(this.decibelDisplayer_speaker);
            this.skinPanel_tool.Controls.Add(this.decibelDisplayer_mic);
            this.skinPanel_tool.Controls.Add(this.skinCheckBox_camera);
            this.skinPanel_tool.Controls.Add(this.skinCheckBox_HighR);
            this.skinPanel_tool.Controls.Add(this.skinComboBox_quality);
            this.skinPanel_tool.Controls.Add(this.skinCheckBox_my);
            this.skinPanel_tool.Controls.Add(this.timerLabel1);
            this.skinPanel_tool.Controls.Add(this.channelQualityDisplayer1);
            this.skinPanel_tool.Controls.Add(this.skinCheckBox_mic);
            this.skinPanel_tool.Controls.Add(this.skinCheckBox_autoAdjustQulity);
            this.skinPanel_tool.Controls.Add(this.skinCheckBox_speaker);
            this.skinPanel_tool.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel_tool.DownBack = null;
            this.skinPanel_tool.Location = new System.Drawing.Point(5, 412);
            this.skinPanel_tool.MouseBack = null;
            this.skinPanel_tool.Name = "skinPanel_tool";
            this.skinPanel_tool.NormlBack = null;
            this.skinPanel_tool.Size = new System.Drawing.Size(468, 45);
            this.skinPanel_tool.TabIndex = 3;
            this.skinPanel_tool.Visible = false;
            // 
            // button_record
            // 
            this.button_record.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_record.BackColor = System.Drawing.Color.Transparent;
            this.button_record.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.button_record.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.button_record.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_record.DownBack = ((System.Drawing.Image)(resources.GetObject("button_record.DownBack")));
            this.button_record.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.button_record.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_record.Location = new System.Drawing.Point(349, 10);
            this.button_record.MouseBack = ((System.Drawing.Image)(resources.GetObject("button_record.MouseBack")));
            this.button_record.Name = "button_record";
            this.button_record.NormlBack = ((System.Drawing.Image)(resources.GetObject("button_record.NormlBack")));
            this.button_record.Size = new System.Drawing.Size(65, 32);
            this.button_record.TabIndex = 128;
            this.button_record.Text = "开始录制";
            this.button_record.UseVisualStyleBackColor = false;
            this.button_record.Visible = false;
            this.button_record.Click += new System.EventHandler(this.button_record_Click);
            // 
            // decibelDisplayer_speaker
            // 
            this.decibelDisplayer_speaker.BackColor = System.Drawing.Color.White;
            this.decibelDisplayer_speaker.Location = new System.Drawing.Point(3, 7);
            this.decibelDisplayer_speaker.Name = "decibelDisplayer_speaker";
            this.decibelDisplayer_speaker.Size = new System.Drawing.Size(20, 10);
            this.decibelDisplayer_speaker.TabIndex = 10;
            this.decibelDisplayer_speaker.ValueVisialbe = false;
            this.decibelDisplayer_speaker.Working = true;
            // 
            // decibelDisplayer_mic
            // 
            this.decibelDisplayer_mic.BackColor = System.Drawing.Color.White;
            this.decibelDisplayer_mic.Location = new System.Drawing.Point(3, 28);
            this.decibelDisplayer_mic.Name = "decibelDisplayer_mic";
            this.decibelDisplayer_mic.Size = new System.Drawing.Size(20, 10);
            this.decibelDisplayer_mic.TabIndex = 10;
            this.decibelDisplayer_mic.ValueVisialbe = false;
            this.decibelDisplayer_mic.Working = true;
            // 
            // skinCheckBox_camera
            // 
            this.skinCheckBox_camera.AutoSize = true;
            this.skinCheckBox_camera.BackColor = System.Drawing.Color.Transparent;
            this.skinCheckBox_camera.Checked = true;
            this.skinCheckBox_camera.CheckState = System.Windows.Forms.CheckState.Checked;
            this.skinCheckBox_camera.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinCheckBox_camera.DownBack = null;
            this.skinCheckBox_camera.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinCheckBox_camera.Location = new System.Drawing.Point(94, 23);
            this.skinCheckBox_camera.MouseBack = null;
            this.skinCheckBox_camera.Name = "skinCheckBox_camera";
            this.skinCheckBox_camera.NormlBack = null;
            this.skinCheckBox_camera.SelectedDownBack = null;
            this.skinCheckBox_camera.SelectedMouseBack = null;
            this.skinCheckBox_camera.SelectedNormlBack = null;
            this.skinCheckBox_camera.Size = new System.Drawing.Size(63, 21);
            this.skinCheckBox_camera.TabIndex = 2;
            this.skinCheckBox_camera.Text = "摄像头";
            this.skinCheckBox_camera.UseVisualStyleBackColor = false;
            this.skinCheckBox_camera.CheckedChanged += new System.EventHandler(this.skinCheckBox_camera_CheckedChanged);
            // 
            // skinCheckBox_my
            // 
            this.skinCheckBox_my.AutoSize = true;
            this.skinCheckBox_my.BackColor = System.Drawing.Color.Transparent;
            this.skinCheckBox_my.Checked = true;
            this.skinCheckBox_my.CheckState = System.Windows.Forms.CheckState.Checked;
            this.skinCheckBox_my.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinCheckBox_my.DownBack = null;
            this.skinCheckBox_my.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinCheckBox_my.Location = new System.Drawing.Point(94, 2);
            this.skinCheckBox_my.MouseBack = null;
            this.skinCheckBox_my.Name = "skinCheckBox_my";
            this.skinCheckBox_my.NormlBack = null;
            this.skinCheckBox_my.SelectedDownBack = null;
            this.skinCheckBox_my.SelectedMouseBack = null;
            this.skinCheckBox_my.SelectedNormlBack = null;
            this.skinCheckBox_my.Size = new System.Drawing.Size(63, 21);
            this.skinCheckBox_my.TabIndex = 2;
            this.skinCheckBox_my.Text = "小窗口";
            this.skinCheckBox_my.UseVisualStyleBackColor = false;
            this.skinCheckBox_my.CheckedChanged += new System.EventHandler(this.skinCheckBox_my_CheckedChanged);
            // 
            // timerLabel1
            // 
            this.timerLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timerLabel1.AutoSize = true;
            this.timerLabel1.BackColor = System.Drawing.Color.Transparent;
            this.timerLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.timerLabel1.Location = new System.Drawing.Point(425, 25);
            this.timerLabel1.Name = "timerLabel1";
            this.timerLabel1.Size = new System.Drawing.Size(39, 17);
            this.timerLabel1.TabIndex = 7;
            this.timerLabel1.Text = "00:00";
            // 
            // channelQualityDisplayer1
            // 
            this.channelQualityDisplayer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.channelQualityDisplayer1.BackColor = System.Drawing.Color.Transparent;
            this.channelQualityDisplayer1.ColorBadSignal = System.Drawing.Color.Red;
            this.channelQualityDisplayer1.ColorNoSignal = System.Drawing.Color.LightGray;
            this.channelQualityDisplayer1.ColorSignal = System.Drawing.Color.Green;
            this.channelQualityDisplayer1.Location = new System.Drawing.Point(417, 4);
            this.channelQualityDisplayer1.Name = "channelQualityDisplayer1";
            this.channelQualityDisplayer1.Size = new System.Drawing.Size(45, 16);
            this.channelQualityDisplayer1.TabIndex = 6;
            // 
            // skinCheckBox_mic
            // 
            this.skinCheckBox_mic.AutoSize = true;
            this.skinCheckBox_mic.BackColor = System.Drawing.Color.Transparent;
            this.skinCheckBox_mic.Checked = true;
            this.skinCheckBox_mic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.skinCheckBox_mic.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinCheckBox_mic.DownBack = null;
            this.skinCheckBox_mic.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinCheckBox_mic.Location = new System.Drawing.Point(25, 23);
            this.skinCheckBox_mic.MouseBack = null;
            this.skinCheckBox_mic.Name = "skinCheckBox_mic";
            this.skinCheckBox_mic.NormlBack = null;
            this.skinCheckBox_mic.SelectedDownBack = null;
            this.skinCheckBox_mic.SelectedMouseBack = null;
            this.skinCheckBox_mic.SelectedNormlBack = null;
            this.skinCheckBox_mic.Size = new System.Drawing.Size(63, 21);
            this.skinCheckBox_mic.TabIndex = 2;
            this.skinCheckBox_mic.Text = "麦克风";
            this.skinCheckBox_mic.UseVisualStyleBackColor = false;
            this.skinCheckBox_mic.CheckedChanged += new System.EventHandler(this.skinCheckBox_mic_CheckedChanged);
            // 
            // skinCheckBox_speaker
            // 
            this.skinCheckBox_speaker.AutoSize = true;
            this.skinCheckBox_speaker.BackColor = System.Drawing.Color.Transparent;
            this.skinCheckBox_speaker.Checked = true;
            this.skinCheckBox_speaker.CheckState = System.Windows.Forms.CheckState.Checked;
            this.skinCheckBox_speaker.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinCheckBox_speaker.DownBack = null;
            this.skinCheckBox_speaker.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinCheckBox_speaker.Location = new System.Drawing.Point(25, 2);
            this.skinCheckBox_speaker.MouseBack = null;
            this.skinCheckBox_speaker.Name = "skinCheckBox_speaker";
            this.skinCheckBox_speaker.NormlBack = null;
            this.skinCheckBox_speaker.SelectedDownBack = null;
            this.skinCheckBox_speaker.SelectedMouseBack = null;
            this.skinCheckBox_speaker.SelectedNormlBack = null;
            this.skinCheckBox_speaker.Size = new System.Drawing.Size(63, 21);
            this.skinCheckBox_speaker.TabIndex = 2;
            this.skinCheckBox_speaker.Text = "扬声器";
            this.skinCheckBox_speaker.UseVisualStyleBackColor = false;
            this.skinCheckBox_speaker.CheckedChanged += new System.EventHandler(this.skinCheckBox_speaker_CheckedChanged);
            // 
            // skinPanel1
            // 
            this.skinPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinPanel1.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel1.Controls.Add(this.skinLabel_cameraError);
            this.skinPanel1.Controls.Add(this.label_record);
            this.skinPanel1.Controls.Add(this.pictureBox_disMic);
            this.skinPanel1.Controls.Add(this.pictureBox_disCamera);
            this.skinPanel1.Controls.Add(this.skinPanel_Myself);
            this.skinPanel1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel1.DownBack = null;
            this.skinPanel1.Location = new System.Drawing.Point(5, 29);
            this.skinPanel1.MouseBack = null;
            this.skinPanel1.Name = "skinPanel1";
            this.skinPanel1.NormlBack = null;
            this.skinPanel1.Radius = 5;
            this.skinPanel1.Size = new System.Drawing.Size(507, 380);
            this.skinPanel1.TabIndex = 1;
            // 
            // skinLabel_cameraError
            // 
            this.skinLabel_cameraError.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.skinLabel_cameraError.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel_cameraError.AutoSize = true;
            this.skinLabel_cameraError.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_cameraError.BorderColor = System.Drawing.Color.White;
            this.skinLabel_cameraError.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel_cameraError.Location = new System.Drawing.Point(195, 151);
            this.skinLabel_cameraError.Name = "skinLabel_cameraError";
            this.skinLabel_cameraError.Size = new System.Drawing.Size(90, 17);
            this.skinLabel_cameraError.TabIndex = 4;
            this.skinLabel_cameraError.Text = "DeviceInUsing";
            this.skinLabel_cameraError.Visible = false;
            // 
            // label_record
            // 
            this.label_record.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_record.AutoSize = true;
            this.label_record.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_record.ForeColor = System.Drawing.Color.Red;
            this.label_record.Location = new System.Drawing.Point(2, 357);
            this.label_record.Name = "label_record";
            this.label_record.Size = new System.Drawing.Size(44, 17);
            this.label_record.TabIndex = 2;
            this.label_record.Text = "录制中";
            this.label_record.Visible = false;
            // 
            // skinPanel_Myself
            // 
            this.skinPanel_Myself.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.skinPanel_Myself.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel_Myself.BackgroundImage = global::GGTalk.Properties.Resources.AV_VDC_Bkg;
            this.skinPanel_Myself.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.skinPanel_Myself.Controls.Add(this.cameraConnector1);
            this.skinPanel_Myself.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel_Myself.DownBack = null;
            this.skinPanel_Myself.Location = new System.Drawing.Point(374, 278);
            this.skinPanel_Myself.MouseBack = null;
            this.skinPanel_Myself.Name = "skinPanel_Myself";
            this.skinPanel_Myself.NormlBack = null;
            this.skinPanel_Myself.Radius = 4;
            this.skinPanel_Myself.Size = new System.Drawing.Size(130, 99);
            this.skinPanel_Myself.TabIndex = 0;
            // 
            // cameraConnector1
            // 
            this.cameraConnector1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cameraConnector1.AutoSynchronizeVideoToAudio = true;
            this.cameraConnector1.BackColor = System.Drawing.Color.White;
            this.cameraConnector1.BackgroundImage = global::GGTalk.Properties.Resources.VideoWaitToAnswer;
            this.cameraConnector1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cameraConnector1.DisplayVideoParameters = false;
            this.cameraConnector1.Location = new System.Drawing.Point(4, 4);
            this.cameraConnector1.Name = "cameraConnector1";
            this.cameraConnector1.Size = new System.Drawing.Size(122, 91);
            this.cameraConnector1.TabIndex = 0;
            this.cameraConnector1.WaitOwnerOnlineSpanInSecs = 0;
            // 
            // dynamicCameraConnector1
            // 
            this.dynamicCameraConnector1.AutoSynchronizeVideoToAudio = true;
            this.dynamicCameraConnector1.DisplayVideoParameters = false;
            this.dynamicCameraConnector1.WaitOwnerOnlineSpanInSecs = 0;
            this.dynamicCameraConnector1.OwnerCameraVideoSizeChanged += new ESBasic.CbGeneric<System.Drawing.Size>(this.dynamicCameraConnector1_OwnerCameraVideoSizeChanged);
            this.dynamicCameraConnector1.OwnerOutputChanged += new ESBasic.CbGeneric(this.dynamicCameraConnector1_OwnerOutputChanged);
            this.dynamicCameraConnector1.ConnectEnded += new ESBasic.CbGeneric<OMCS.Passive.ConnectResult>(this.dynamicCameraConnector1_ConnectEnded);
            // 
            // microphoneConnector1
            // 
            this.microphoneConnector1.Mute = false;
            this.microphoneConnector1.SpringReceivedEventWhenMute = true;
            this.microphoneConnector1.WaitOwnerOnlineSpanInSecs = 0;
            this.microphoneConnector1.OwnerOutputChanged += new ESBasic.CbGeneric(this.microphoneConnector1_OwnerOutputChanged);
            this.microphoneConnector1.ConnectEnded += new ESBasic.CbGeneric<OMCS.Passive.ConnectResult>(this.microphoneConnector1_ConnectEnded);
            // 
            // VideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.BorderPalace = ((System.Drawing.Image)(resources.GetObject("$this.BorderPalace")));
            this.ClientSize = new System.Drawing.Size(519, 462);
            this.CloseDownBack = global::GGTalk.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::GGTalk.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::GGTalk.Properties.Resources.btn_close_disable;
            this.Controls.Add(this.skinLabel_tip);
            this.Controls.Add(this.skinButton_State);
            this.Controls.Add(this.skinPanel_tool);
            this.Controls.Add(this.skinPanel1);
            this.MaxDownBack = global::GGTalk.Properties.Resources.btn_max_down;
            this.MaxMouseBack = global::GGTalk.Properties.Resources.btn_max_highlight;
            this.MaxNormlBack = global::GGTalk.Properties.Resources.btn_max_normal;
            this.MiniDownBack = global::GGTalk.Properties.Resources.btn_mini_down;
            this.MiniMouseBack = global::GGTalk.Properties.Resources.btn_mini_highlight;
            this.MinimumSize = new System.Drawing.Size(515, 450);
            this.MiniNormlBack = global::GGTalk.Properties.Resources.btn_mini_normal;
            this.Name = "VideoForm";
            this.RestoreDownBack = global::GGTalk.Properties.Resources.btn_restore_down;
            this.RestoreMouseBack = global::GGTalk.Properties.Resources.btn_restore_highlight;
            this.RestoreNormlBack = global::GGTalk.Properties.Resources.btn_restore_normal;
            this.ShowDrawIcon = false;
            this.ShowInTaskbar = true;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "正在和David视频会话";
            this.TopMost = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VideoForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_disMic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_disCamera)).EndInit();
            this.skinPanel_tool.ResumeLayout(false);
            this.skinPanel_tool.PerformLayout();
            this.skinPanel1.ResumeLayout(false);
            this.skinPanel1.PerformLayout();
            this.skinPanel_Myself.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinPanel skinPanel1;
        private OMCS.Passive.Video.DynamicCameraConnector dynamicCameraConnector1;
        private OMCS.Passive.Audio.MicrophoneConnector microphoneConnector1;
        private CCWin.SkinControl.SkinPanel skinPanel_Myself;
        private OMCS.Passive.Video.CameraConnector cameraConnector1;
        private System.Windows.Forms.ImageList imageList1;
        private CCWin.SkinControl.SkinCheckBox skinCheckBox_camera;
        private CCWin.SkinControl.SkinCheckBox skinCheckBox_mic;
        private CCWin.SkinControl.SkinCheckBox skinCheckBox_speaker;
        private CCWin.SkinControl.SkinCheckBox skinCheckBox_my;
        private CCWin.SkinControl.SkinPanel skinPanel_tool;
        private CCWin.SkinControl.SkinLabel skinLabel_tip;
        private CCWin.SkinControl.SkinButton skinButton_State;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pictureBox_disCamera;
        private System.Windows.Forms.PictureBox pictureBox_disMic;
        private ChannelQualityDisplayer channelQualityDisplayer1;
        private ESBasic.Widget.TimerLabel timerLabel1;
        private CCWin.SkinControl.SkinCheckBox skinCheckBox_HighR;
        private CCWin.SkinControl.SkinCheckBox skinCheckBox_autoAdjustQulity;
        private CCWin.SkinControl.SkinComboBox skinComboBox_quality;
        private DecibelDisplayer decibelDisplayer_speaker;
        private DecibelDisplayer decibelDisplayer_mic;
        private CCWin.SkinControl.SkinLabel skinLabel_cameraError;
        private System.Windows.Forms.Label label_record;
        private CCWin.SkinControl.SkinButton button_record;
    }
}