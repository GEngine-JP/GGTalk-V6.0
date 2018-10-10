namespace GGTalk
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.toolShow = new System.Windows.Forms.ToolTip(this.components);
            this.menuStripId = new CCWin.SkinControl.SkinContextMenuStrip();
            this.buttonLogin = new CCWin.SkinControl.SkinButton();
            this.textBoxId = new CCWin.SkinControl.SkinTextBox();
            this.imgLoadding = new System.Windows.Forms.PictureBox();
            this.checkBoxRememberPwd = new CCWin.SkinControl.SkinCheckBox();
            this.checkBoxAutoLogin = new CCWin.SkinControl.SkinCheckBox();
            this.skinLabel_SoftName = new CCWin.SkinControl.SkinLabel();
            this.btnRegister = new CCWin.SkinControl.SkinButton();
            this.pnlTx = new CCWin.SkinControl.SkinPanel();
            this.panelHeadImage = new CCWin.SkinControl.SkinPanel();
            this.skinButton_State = new CCWin.SkinControl.SkinButton();
            this.textBoxPwd = new CCWin.SkinControl.SkinTextBox();
            this.imageList_state = new System.Windows.Forms.ImageList(this.components);
            this.ItemImonline = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemAway = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemBusy = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemMute = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemInVisble = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripState = new CCWin.SkinControl.SkinContextMenuStrip();
            this.skinButtom1 = new CCWin.SkinControl.SkinButton();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.textBoxId.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLoadding)).BeginInit();
            this.pnlTx.SuspendLayout();
            this.panelHeadImage.SuspendLayout();
            this.textBoxPwd.SuspendLayout();
            this.menuStripState.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolShow
            // 
            this.toolShow.IsBalloon = true;
            // 
            // menuStripId
            // 
            this.menuStripId.Arrow = System.Drawing.Color.Black;
            this.menuStripId.AutoSize = false;
            this.menuStripId.Back = System.Drawing.Color.White;
            this.menuStripId.BackColor = System.Drawing.Color.White;
            this.menuStripId.BackRadius = 4;
            this.menuStripId.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
            this.menuStripId.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(209)))));
            this.menuStripId.Fore = System.Drawing.Color.Black;
            this.menuStripId.HoverFore = System.Drawing.Color.White;
            this.menuStripId.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStripId.ItemAnamorphosis = false;
            this.menuStripId.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.menuStripId.ItemBorderShow = false;
            this.menuStripId.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.menuStripId.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.menuStripId.ItemRadius = 4;
            this.menuStripId.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.None;
            this.menuStripId.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.menuStripId.Name = "MenuId";
            this.menuStripId.RadiusStyle = CCWin.SkinClass.RoundStyle.None;
            this.menuStripId.Size = new System.Drawing.Size(183, 4);
            this.menuStripId.SkinAllColor = true;
            this.menuStripId.TitleAnamorphosis = false;
            this.menuStripId.TitleColor = System.Drawing.Color.White;
            this.menuStripId.TitleRadius = 4;
            this.menuStripId.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.None;
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.Color.Transparent;
            this.buttonLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonLogin.BackRectangle = new System.Drawing.Rectangle(50, 23, 50, 23);
            this.buttonLogin.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(118)))), ((int)(((byte)(156)))));
            this.buttonLogin.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.buttonLogin.Create = true;
            this.buttonLogin.DownBack = global::GGTalk.Properties.Resources.button_login_down;
            this.buttonLogin.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.buttonLogin.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonLogin.ForeColor = System.Drawing.Color.Black;
            this.buttonLogin.Location = new System.Drawing.Point(99, 244);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(0);
            this.buttonLogin.MouseBack = global::GGTalk.Properties.Resources.button_login_hover;
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.NormlBack = global::GGTalk.Properties.Resources.button_login_normal;
            this.buttonLogin.Palace = true;
            this.buttonLogin.Size = new System.Drawing.Size(185, 49);
            this.buttonLogin.TabIndex = 5;
            this.buttonLogin.Text = "登        录";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textBoxId
            // 
            this.textBoxId.BackColor = System.Drawing.Color.Transparent;
            this.textBoxId.Icon = null;
            this.textBoxId.IconIsButton = false;
            this.textBoxId.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.textBoxId.Location = new System.Drawing.Point(112, 138);
            this.textBoxId.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxId.MinimumSize = new System.Drawing.Size(28, 28);
            this.textBoxId.MouseBack = null;
            this.textBoxId.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.NormlBack = null;
            this.textBoxId.Padding = new System.Windows.Forms.Padding(5, 5, 28, 5);
            this.textBoxId.Size = new System.Drawing.Size(250, 28);
            // 
            // textBoxId.BaseText
            // 
            this.textBoxId.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxId.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxId.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.textBoxId.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.textBoxId.SkinTxt.Name = "BaseText";
            this.textBoxId.SkinTxt.Size = new System.Drawing.Size(217, 18);
            this.textBoxId.SkinTxt.TabIndex = 0;
            this.textBoxId.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.textBoxId.SkinTxt.WaterText = "帐号";
            this.textBoxId.SkinTxt.TextChanged += new System.EventHandler(this.textBoxId_SkinTxt_TextChanged);
            this.textBoxId.TabIndex = 35;
            // 
            // imgLoadding
            // 
            this.imgLoadding.Image = ((System.Drawing.Image)(resources.GetObject("imgLoadding.Image")));
            this.imgLoadding.Location = new System.Drawing.Point(1, 242);
            this.imgLoadding.Margin = new System.Windows.Forms.Padding(0);
            this.imgLoadding.Name = "imgLoadding";
            this.imgLoadding.Size = new System.Drawing.Size(377, 2);
            this.imgLoadding.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLoadding.TabIndex = 17;
            this.imgLoadding.TabStop = false;
            this.imgLoadding.Visible = false;
            // 
            // checkBoxRememberPwd
            // 
            this.checkBoxRememberPwd.AutoSize = true;
            this.checkBoxRememberPwd.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxRememberPwd.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.checkBoxRememberPwd.DefaultCheckButtonWidth = 15;
            this.checkBoxRememberPwd.DownBack = global::GGTalk.Properties.Resources.checkbox_pushed;
            this.checkBoxRememberPwd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxRememberPwd.ForeColor = System.Drawing.Color.Black;
            this.checkBoxRememberPwd.LightEffect = false;
            this.checkBoxRememberPwd.Location = new System.Drawing.Point(113, 206);
            this.checkBoxRememberPwd.MouseBack = global::GGTalk.Properties.Resources.checkbox_hightlight;
            this.checkBoxRememberPwd.Name = "checkBoxRememberPwd";
            this.checkBoxRememberPwd.NormlBack = ((System.Drawing.Image)(resources.GetObject("checkBoxRememberPwd.NormlBack")));
            this.checkBoxRememberPwd.SelectedDownBack = global::GGTalk.Properties.Resources.checkbox_tick_pushed;
            this.checkBoxRememberPwd.SelectedMouseBack = global::GGTalk.Properties.Resources.checkbox_tick_highlight;
            this.checkBoxRememberPwd.SelectedNormlBack = global::GGTalk.Properties.Resources.checkbox_tick_normal;
            this.checkBoxRememberPwd.Size = new System.Drawing.Size(75, 21);
            this.checkBoxRememberPwd.TabIndex = 3;
            this.checkBoxRememberPwd.Text = "记住密码";
            this.checkBoxRememberPwd.UseVisualStyleBackColor = false;
            // 
            // checkBoxAutoLogin
            // 
            this.checkBoxAutoLogin.AutoSize = true;
            this.checkBoxAutoLogin.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxAutoLogin.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.checkBoxAutoLogin.DefaultCheckButtonWidth = 15;
            this.checkBoxAutoLogin.DownBack = global::GGTalk.Properties.Resources.checkbox_pushed;
            this.checkBoxAutoLogin.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.checkBoxAutoLogin.ForeColor = System.Drawing.Color.Black;
            this.checkBoxAutoLogin.LightEffect = false;
            this.checkBoxAutoLogin.Location = new System.Drawing.Point(194, 206);
            this.checkBoxAutoLogin.MouseBack = global::GGTalk.Properties.Resources.checkbox_hightlight;
            this.checkBoxAutoLogin.Name = "checkBoxAutoLogin";
            this.checkBoxAutoLogin.NormlBack = ((System.Drawing.Image)(resources.GetObject("checkBoxAutoLogin.NormlBack")));
            this.checkBoxAutoLogin.SelectedDownBack = global::GGTalk.Properties.Resources.checkbox_tick_pushed;
            this.checkBoxAutoLogin.SelectedMouseBack = global::GGTalk.Properties.Resources.checkbox_tick_highlight;
            this.checkBoxAutoLogin.SelectedNormlBack = global::GGTalk.Properties.Resources.checkbox_tick_normal;
            this.checkBoxAutoLogin.Size = new System.Drawing.Size(75, 21);
            this.checkBoxAutoLogin.TabIndex = 4;
            this.checkBoxAutoLogin.Text = "自动登录";
            this.checkBoxAutoLogin.UseVisualStyleBackColor = false;
            this.checkBoxAutoLogin.CheckedChanged += new System.EventHandler(this.checkBoxAutoLogin_CheckedChanged);
            // 
            // skinLabel_SoftName
            // 
            this.skinLabel_SoftName.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel_SoftName.AutoSize = true;
            this.skinLabel_SoftName.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_SoftName.BorderColor = System.Drawing.Color.White;
            this.skinLabel_SoftName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel_SoftName.ForeColor = System.Drawing.Color.Black;
            this.skinLabel_SoftName.Location = new System.Drawing.Point(7, 9);
            this.skinLabel_SoftName.Name = "skinLabel_SoftName";
            this.skinLabel_SoftName.Size = new System.Drawing.Size(93, 20);
            this.skinLabel_SoftName.TabIndex = 18;
            this.skinLabel_SoftName.Text = "GGTalk 2017";
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.Transparent;
            this.btnRegister.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(118)))), ((int)(((byte)(156)))));
            this.btnRegister.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnRegister.Create = true;
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.DownBack = global::GGTalk.Properties.Resources.zhuce_press;
            this.btnRegister.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnRegister.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRegister.Location = new System.Drawing.Point(311, 208);
            this.btnRegister.Margin = new System.Windows.Forms.Padding(0);
            this.btnRegister.MouseBack = global::GGTalk.Properties.Resources.zhuce_hover;
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.NormlBack = global::GGTalk.Properties.Resources.zhuce;
            this.btnRegister.Size = new System.Drawing.Size(51, 16);
            this.btnRegister.TabIndex = 8;
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // pnlTx
            // 
            this.pnlTx.BackColor = System.Drawing.Color.Transparent;
            this.pnlTx.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlTx.BackgroundImage")));
            this.pnlTx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlTx.Controls.Add(this.panelHeadImage);
            this.pnlTx.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.pnlTx.DownBack = null;
            this.pnlTx.Location = new System.Drawing.Point(16, 138);
            this.pnlTx.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTx.MouseBack = null;
            this.pnlTx.Name = "pnlTx";
            this.pnlTx.NormlBack = null;
            this.pnlTx.Size = new System.Drawing.Size(87, 87);
            this.pnlTx.TabIndex = 12;
            // 
            // panelHeadImage
            // 
            this.panelHeadImage.BackColor = System.Drawing.Color.Transparent;
            this.panelHeadImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelHeadImage.BackgroundImage")));
            this.panelHeadImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelHeadImage.Controls.Add(this.skinButton_State);
            this.panelHeadImage.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.panelHeadImage.DownBack = null;
            this.panelHeadImage.Location = new System.Drawing.Point(1, 1);
            this.panelHeadImage.Margin = new System.Windows.Forms.Padding(0);
            this.panelHeadImage.MouseBack = null;
            this.panelHeadImage.Name = "panelHeadImage";
            this.panelHeadImage.NormlBack = null;
            this.panelHeadImage.Radius = 4;
            this.panelHeadImage.Size = new System.Drawing.Size(85, 85);
            this.panelHeadImage.TabIndex = 11;
            // 
            // skinButton_State
            // 
            this.skinButton_State.BackColor = System.Drawing.Color.Transparent;
            this.skinButton_State.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.skinButton_State.BackRectangle = new System.Drawing.Rectangle(4, 4, 4, 4);
            this.skinButton_State.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(118)))), ((int)(((byte)(156)))));
            this.skinButton_State.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton_State.DownBack = global::GGTalk.Properties.Resources.allbtn_down;
            this.skinButton_State.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.skinButton_State.Image = global::GGTalk.Properties.Resources.imonline__2_;
            this.skinButton_State.ImageSize = new System.Drawing.Size(11, 11);
            this.skinButton_State.Location = new System.Drawing.Point(67, 67);
            this.skinButton_State.Margin = new System.Windows.Forms.Padding(0);
            this.skinButton_State.MouseBack = global::GGTalk.Properties.Resources.allbtn_highlight;
            this.skinButton_State.Name = "skinButton_State";
            this.skinButton_State.NormlBack = null;
            this.skinButton_State.Size = new System.Drawing.Size(18, 18);
            this.skinButton_State.TabIndex = 10;
            this.skinButton_State.Tag = "2";
            this.skinButton_State.UseVisualStyleBackColor = false;
            this.skinButton_State.Click += new System.EventHandler(this.btnState_Click);
            // 
            // textBoxPwd
            // 
            this.textBoxPwd.BackColor = System.Drawing.Color.Transparent;
            this.textBoxPwd.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.textBoxPwd.Icon = ((System.Drawing.Image)(resources.GetObject("textBoxPwd.Icon")));
            this.textBoxPwd.IconIsButton = true;
            this.textBoxPwd.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.textBoxPwd.Location = new System.Drawing.Point(112, 175);
            this.textBoxPwd.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxPwd.MinimumSize = new System.Drawing.Size(0, 28);
            this.textBoxPwd.MouseBack = null;
            this.textBoxPwd.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.textBoxPwd.Name = "textBoxPwd";
            this.textBoxPwd.NormlBack = null;
            this.textBoxPwd.Padding = new System.Windows.Forms.Padding(5, 5, 60, 5);
            this.textBoxPwd.Size = new System.Drawing.Size(250, 28);
            // 
            // textBoxPwd.BaseText
            // 
            this.textBoxPwd.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPwd.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPwd.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.textBoxPwd.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.textBoxPwd.SkinTxt.Name = "BaseText";
            this.textBoxPwd.SkinTxt.PasswordChar = '●';
            this.textBoxPwd.SkinTxt.Size = new System.Drawing.Size(185, 18);
            this.textBoxPwd.SkinTxt.TabIndex = 0;
            this.textBoxPwd.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.textBoxPwd.SkinTxt.WaterText = "密码";
            this.textBoxPwd.SkinTxt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxPwd_SkinTxt_KeyUp);
            this.textBoxPwd.TabIndex = 36;
            this.textBoxPwd.IconClick += new System.EventHandler(this.textBoxPwd_IconClick);
            // 
            // imageList_state
            // 
            this.imageList_state.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_state.ImageStream")));
            this.imageList_state.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_state.Images.SetKeyName(0, "0.png");
            this.imageList_state.Images.SetKeyName(1, "1.png");
            this.imageList_state.Images.SetKeyName(2, "2.png");
            this.imageList_state.Images.SetKeyName(3, "3.png");
            this.imageList_state.Images.SetKeyName(4, "4.png");
            this.imageList_state.Images.SetKeyName(5, "5.png");
            // 
            // ItemImonline
            // 
            this.ItemImonline.AutoSize = false;
            this.ItemImonline.Image = global::GGTalk.Properties.Resources.imonline__2_;
            this.ItemImonline.Name = "ItemImonline";
            this.ItemImonline.Size = new System.Drawing.Size(105, 22);
            this.ItemImonline.Tag = "2";
            this.ItemImonline.Text = "我在线上";
            this.ItemImonline.ToolTipText = "表示希望好友看到您在线。\r\n声音：开启\r\n消息提醒框：开启\r\n会话消息：任务栏头像闪动\r\n";
            this.ItemImonline.Click += new System.EventHandler(this.Item_Click);
            // 
            // ItemAway
            // 
            this.ItemAway.AutoSize = false;
            this.ItemAway.Image = global::GGTalk.Properties.Resources.away__2_;
            this.ItemAway.Name = "ItemAway";
            this.ItemAway.Size = new System.Drawing.Size(105, 22);
            this.ItemAway.Tag = "3";
            this.ItemAway.Text = "离开";
            this.ItemAway.ToolTipText = "表示离开，暂无法处理消息。\r\n声音：开启\r\n消息提醒框：开启\r\n会话消息：任务栏头像闪动\r\n";
            this.ItemAway.Click += new System.EventHandler(this.Item_Click);
            // 
            // ItemBusy
            // 
            this.ItemBusy.AutoSize = false;
            this.ItemBusy.Image = global::GGTalk.Properties.Resources.busy__2_;
            this.ItemBusy.Name = "ItemBusy";
            this.ItemBusy.Size = new System.Drawing.Size(105, 22);
            this.ItemBusy.Tag = "4";
            this.ItemBusy.Text = "忙碌";
            this.ItemBusy.ToolTipText = "表示忙碌，不会及时处理消息。\r\n声音：开启\r\n消息提醒框：开启\r\n会话消息：任务栏显示气泡\r\n";
            this.ItemBusy.Click += new System.EventHandler(this.Item_Click);
            // 
            // ItemMute
            // 
            this.ItemMute.AutoSize = false;
            this.ItemMute.Image = global::GGTalk.Properties.Resources.mute__2_;
            this.ItemMute.Name = "ItemMute";
            this.ItemMute.Size = new System.Drawing.Size(105, 22);
            this.ItemMute.Tag = "5";
            this.ItemMute.Text = "请勿打扰";
            this.ItemMute.ToolTipText = "表示不想被打扰。\r\n声音：关闭\r\n消息提醒框：关闭\r\n会话消息：任务栏显示气泡\r\n\r\n";
            this.ItemMute.Click += new System.EventHandler(this.Item_Click);
            // 
            // ItemInVisble
            // 
            this.ItemInVisble.AutoSize = false;
            this.ItemInVisble.Image = global::GGTalk.Properties.Resources.invisible__2_;
            this.ItemInVisble.Name = "ItemInVisble";
            this.ItemInVisble.Size = new System.Drawing.Size(105, 22);
            this.ItemInVisble.Tag = "6";
            this.ItemInVisble.Text = "隐身";
            this.ItemInVisble.ToolTipText = "表示好友看到您是离线的。\r\n声音：开启\r\n消息提醒框：开启\r\n会话消息：任务栏头像闪动\r\n";
            this.ItemInVisble.Click += new System.EventHandler(this.Item_Click);
            // 
            // menuStripState
            // 
            this.menuStripState.Arrow = System.Drawing.Color.Black;
            this.menuStripState.Back = System.Drawing.Color.White;
            this.menuStripState.BackRadius = 4;
            this.menuStripState.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
            this.menuStripState.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.menuStripState.Fore = System.Drawing.Color.Black;
            this.menuStripState.HoverFore = System.Drawing.Color.White;
            this.menuStripState.ImageScalingSize = new System.Drawing.Size(11, 11);
            this.menuStripState.ItemAnamorphosis = false;
            this.menuStripState.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.menuStripState.ItemBorderShow = false;
            this.menuStripState.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.menuStripState.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.menuStripState.ItemRadius = 4;
            this.menuStripState.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.None;
            this.menuStripState.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemImonline,
            this.ItemAway,
            this.ItemBusy,
            this.ItemMute,
            this.ItemInVisble});
            this.menuStripState.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.menuStripState.Name = "MenuState";
            this.menuStripState.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.menuStripState.Size = new System.Drawing.Size(125, 114);
            this.menuStripState.SkinAllColor = true;
            this.menuStripState.TitleAnamorphosis = false;
            this.menuStripState.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.menuStripState.TitleRadius = 4;
            this.menuStripState.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // skinButtom1
            // 
            this.skinButtom1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.skinButtom1.BackColor = System.Drawing.Color.Transparent;
            this.skinButtom1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.skinButtom1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButtom1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.skinButtom1.DownBack = null;
            this.skinButtom1.DrawType = CCWin.SkinControl.DrawStyle.None;
            this.skinButtom1.Image = ((System.Drawing.Image)(resources.GetObject("skinButtom1.Image")));
            this.skinButtom1.Location = new System.Drawing.Point(359, 272);
            this.skinButtom1.Margin = new System.Windows.Forms.Padding(0);
            this.skinButtom1.MouseBack = null;
            this.skinButtom1.Name = "skinButtom1";
            this.skinButtom1.NormlBack = null;
            this.skinButtom1.Size = new System.Drawing.Size(16, 16);
            this.skinButtom1.TabIndex = 128;
            this.skinButtom1.UseVisualStyleBackColor = false;
            this.skinButtom1.Click += new System.EventHandler(this.skinButtom1_Click);
            // 
            // skinLabel1
            // 
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.ForeColor = System.Drawing.Color.DimGray;
            this.skinLabel1.Location = new System.Drawing.Point(209, 94);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(153, 20);
            this.skinLabel1.TabIndex = 18;
            this.skinLabel1.Text = "作者QQ：2027224508";
            this.skinLabel1.Click += new System.EventHandler(this.skinLabel1_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.buttonLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackPalace = global::GGTalk.Properties.Resources.texture;
            this.BackToColor = false;
            this.BorderPalace = global::GGTalk.Properties.Resources.BackPalace;
            this.ClientSize = new System.Drawing.Size(379, 292);
            this.CloseDownBack = global::GGTalk.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::GGTalk.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::GGTalk.Properties.Resources.btn_close_disable;
            this.Controls.Add(this.skinButtom1);
            this.Controls.Add(this.textBoxId);
            this.Controls.Add(this.imgLoadding);
            this.Controls.Add(this.checkBoxRememberPwd);
            this.Controls.Add(this.checkBoxAutoLogin);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.skinLabel_SoftName);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.pnlTx);
            this.Controls.Add(this.textBoxPwd);
            this.MaxDownBack = global::GGTalk.Properties.Resources.btn_max_down;
            this.MaxMouseBack = global::GGTalk.Properties.Resources.btn_max_highlight;
            this.MaxNormlBack = global::GGTalk.Properties.Resources.btn_max_normal;
            this.MiniDownBack = global::GGTalk.Properties.Resources.btn_mini_down;
            this.MiniMouseBack = global::GGTalk.Properties.Resources.btn_mini_highlight;
            this.MiniNormlBack = global::GGTalk.Properties.Resources.btn_mini_normal;
            this.Name = "LoginForm";
            this.RestoreDownBack = global::GGTalk.Properties.Resources.btn_restore_down;
            this.RestoreMouseBack = global::GGTalk.Properties.Resources.btn_restore_highlight;
            this.RestoreNormlBack = global::GGTalk.Properties.Resources.btn_restore_normal;
            this.ShowBorder = false;
            this.ShowDrawIcon = false;
            this.UseCustomBackImage = true;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.textBoxId.ResumeLayout(false);
            this.textBoxId.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLoadding)).EndInit();
            this.pnlTx.ResumeLayout(false);
            this.panelHeadImage.ResumeLayout(false);
            this.textBoxPwd.ResumeLayout(false);
            this.textBoxPwd.PerformLayout();
            this.menuStripState.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinButton buttonLogin;
        private CCWin.SkinControl.SkinCheckBox checkBoxAutoLogin;
        private CCWin.SkinControl.SkinButton btnRegister;
        private CCWin.SkinControl.SkinPanel pnlTx;
        private CCWin.SkinControl.SkinPanel panelHeadImage;
        private CCWin.SkinControl.SkinButton skinButton_State;
        private System.Windows.Forms.ToolTip toolShow;
        private CCWin.SkinControl.SkinContextMenuStrip menuStripId;
        private CCWin.SkinControl.SkinCheckBox checkBoxRememberPwd;
        private System.Windows.Forms.PictureBox imgLoadding;
        private CCWin.SkinControl.SkinLabel skinLabel_SoftName;
        private CCWin.SkinControl.SkinTextBox textBoxPwd;
        private CCWin.SkinControl.SkinTextBox textBoxId;
        private System.Windows.Forms.ImageList imageList_state;
        private System.Windows.Forms.ToolStripMenuItem ItemImonline;
        private System.Windows.Forms.ToolStripMenuItem ItemAway;
        private System.Windows.Forms.ToolStripMenuItem ItemBusy;
        private System.Windows.Forms.ToolStripMenuItem ItemMute;
        private System.Windows.Forms.ToolStripMenuItem ItemInVisble;
        private CCWin.SkinControl.SkinContextMenuStrip menuStripState;
        private CCWin.SkinControl.SkinButton skinButtom1;
        private CCWin.SkinControl.SkinLabel skinLabel1;
    }
}