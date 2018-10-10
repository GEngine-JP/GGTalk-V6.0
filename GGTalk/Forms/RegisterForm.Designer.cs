namespace GGTalk
{
    partial class RegisterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            this.btnRegister = new CCWin.SkinControl.SkinButton();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.pnlTx = new CCWin.SkinControl.SkinPanel();
            this.pnlImgTx = new CCWin.SkinControl.SkinPanel();
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.skinTextBox_signature = new CCWin.SkinControl.SkinTextBox();
            this.skinTextBox_nickName = new CCWin.SkinControl.SkinTextBox();
            this.skinTextBox_pwd2 = new CCWin.SkinControl.SkinTextBox();
            this.skinTextBox_pwd = new CCWin.SkinControl.SkinTextBox();
            this.skinTextBox_id = new CCWin.SkinControl.SkinTextBox();
            this.skinLabel5 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel4 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.pnlTx.SuspendLayout();
            this.skinTextBox_signature.SuspendLayout();
            this.skinTextBox_nickName.SuspendLayout();
            this.skinTextBox_pwd2.SuspendLayout();
            this.skinTextBox_pwd.SuspendLayout();
            this.skinTextBox_id.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRegister
            // 
            this.btnRegister.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRegister.BackColor = System.Drawing.Color.Transparent;
            this.btnRegister.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.btnRegister.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnRegister.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRegister.DownBack = ((System.Drawing.Image)(resources.GetObject("btnRegister.DownBack")));
            this.btnRegister.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnRegister.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRegister.Location = new System.Drawing.Point(245, 247);
            this.btnRegister.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnRegister.MouseBack")));
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnRegister.NormlBack")));
            this.btnRegister.Size = new System.Drawing.Size(69, 24);
            this.btnRegister.TabIndex = 6;
            this.btnRegister.Text = "确定";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel1.Location = new System.Drawing.Point(275, 76);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(56, 17);
            this.linkLabel1.TabIndex = 135;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "自拍头像";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked_1);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel2.Location = new System.Drawing.Point(275, 51);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(56, 17);
            this.linkLabel2.TabIndex = 135;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "更换头像";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // pnlTx
            // 
            this.pnlTx.BackColor = System.Drawing.Color.Transparent;
            this.pnlTx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlTx.Controls.Add(this.pnlImgTx);
            this.pnlTx.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.pnlTx.DownBack = ((System.Drawing.Image)(resources.GetObject("pnlTx.DownBack")));
            this.pnlTx.Location = new System.Drawing.Point(332, 51);
            this.pnlTx.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTx.MouseBack = ((System.Drawing.Image)(resources.GetObject("pnlTx.MouseBack")));
            this.pnlTx.Name = "pnlTx";
            this.pnlTx.NormlBack = ((System.Drawing.Image)(resources.GetObject("pnlTx.NormlBack")));
            this.pnlTx.Palace = true;
            this.pnlTx.Size = new System.Drawing.Size(104, 104);
            this.pnlTx.TabIndex = 134;
            // 
            // pnlImgTx
            // 
            this.pnlImgTx.BackColor = System.Drawing.Color.Transparent;
            this.pnlImgTx.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlImgTx.BackgroundImage")));
            this.pnlImgTx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlImgTx.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.pnlImgTx.DownBack = null;
            this.pnlImgTx.Location = new System.Drawing.Point(2, 2);
            this.pnlImgTx.Margin = new System.Windows.Forms.Padding(0);
            this.pnlImgTx.MouseBack = null;
            this.pnlImgTx.Name = "pnlImgTx";
            this.pnlImgTx.NormlBack = null;
            this.pnlImgTx.Radius = 4;
            this.pnlImgTx.Size = new System.Drawing.Size(100, 100);
            this.pnlImgTx.TabIndex = 6;
            // 
            // skinButton1
            // 
            this.skinButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.skinButton1.DownBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.DownBack")));
            this.skinButton1.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.skinButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinButton1.Location = new System.Drawing.Point(170, 247);
            this.skinButton1.MouseBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.MouseBack")));
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.NormlBack")));
            this.skinButton1.Size = new System.Drawing.Size(69, 24);
            this.skinButton1.TabIndex = 7;
            this.skinButton1.Text = "取消";
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // skinTextBox_signature
            // 
            this.skinTextBox_signature.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox_signature.Icon = null;
            this.skinTextBox_signature.IconIsButton = false;
            this.skinTextBox_signature.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_signature.Location = new System.Drawing.Point(83, 201);
            this.skinTextBox_signature.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_signature.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox_signature.MouseBack = null;
            this.skinTextBox_signature.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_signature.Name = "skinTextBox_signature";
            this.skinTextBox_signature.NormlBack = null;
            this.skinTextBox_signature.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox_signature.Size = new System.Drawing.Size(346, 28);
            // 
            // skinTextBox_signature.BaseText
            // 
            this.skinTextBox_signature.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_signature.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_signature.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_signature.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox_signature.SkinTxt.Name = "BaseText";
            this.skinTextBox_signature.SkinTxt.Size = new System.Drawing.Size(336, 18);
            this.skinTextBox_signature.SkinTxt.TabIndex = 5;
            this.skinTextBox_signature.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox_signature.SkinTxt.WaterText = "";
            this.skinTextBox_signature.TabIndex = 5;
            // 
            // skinTextBox_nickName
            // 
            this.skinTextBox_nickName.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox_nickName.Icon = null;
            this.skinTextBox_nickName.IconIsButton = false;
            this.skinTextBox_nickName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_nickName.Location = new System.Drawing.Point(83, 91);
            this.skinTextBox_nickName.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_nickName.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox_nickName.MouseBack = null;
            this.skinTextBox_nickName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_nickName.Name = "skinTextBox_nickName";
            this.skinTextBox_nickName.NormlBack = null;
            this.skinTextBox_nickName.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox_nickName.Size = new System.Drawing.Size(189, 28);
            // 
            // skinTextBox_nickName.BaseText
            // 
            this.skinTextBox_nickName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_nickName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_nickName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_nickName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox_nickName.SkinTxt.Name = "BaseText";
            this.skinTextBox_nickName.SkinTxt.Size = new System.Drawing.Size(179, 18);
            this.skinTextBox_nickName.SkinTxt.TabIndex = 1;
            this.skinTextBox_nickName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox_nickName.SkinTxt.WaterText = "";
            this.skinTextBox_nickName.TabIndex = 1;
            // 
            // skinTextBox_pwd2
            // 
            this.skinTextBox_pwd2.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox_pwd2.Icon = null;
            this.skinTextBox_pwd2.IconIsButton = false;
            this.skinTextBox_pwd2.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_pwd2.Location = new System.Drawing.Point(83, 127);
            this.skinTextBox_pwd2.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_pwd2.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox_pwd2.MouseBack = null;
            this.skinTextBox_pwd2.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_pwd2.Name = "skinTextBox_pwd2";
            this.skinTextBox_pwd2.NormlBack = null;
            this.skinTextBox_pwd2.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox_pwd2.Size = new System.Drawing.Size(239, 28);
            // 
            // skinTextBox_pwd2.BaseText
            // 
            this.skinTextBox_pwd2.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_pwd2.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_pwd2.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_pwd2.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox_pwd2.SkinTxt.Name = "BaseText";
            this.skinTextBox_pwd2.SkinTxt.PasswordChar = '*';
            this.skinTextBox_pwd2.SkinTxt.Size = new System.Drawing.Size(229, 18);
            this.skinTextBox_pwd2.SkinTxt.TabIndex = 2;
            this.skinTextBox_pwd2.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox_pwd2.SkinTxt.WaterText = "";
            this.skinTextBox_pwd2.TabIndex = 2;
            // 
            // skinTextBox_pwd
            // 
            this.skinTextBox_pwd.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox_pwd.Icon = null;
            this.skinTextBox_pwd.IconIsButton = false;
            this.skinTextBox_pwd.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_pwd.Location = new System.Drawing.Point(83, 165);
            this.skinTextBox_pwd.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_pwd.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox_pwd.MouseBack = null;
            this.skinTextBox_pwd.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_pwd.Name = "skinTextBox_pwd";
            this.skinTextBox_pwd.NormlBack = null;
            this.skinTextBox_pwd.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox_pwd.Size = new System.Drawing.Size(239, 28);
            // 
            // skinTextBox_pwd.BaseText
            // 
            this.skinTextBox_pwd.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_pwd.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_pwd.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_pwd.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox_pwd.SkinTxt.Name = "BaseText";
            this.skinTextBox_pwd.SkinTxt.PasswordChar = '*';
            this.skinTextBox_pwd.SkinTxt.Size = new System.Drawing.Size(229, 18);
            this.skinTextBox_pwd.SkinTxt.TabIndex = 3;
            this.skinTextBox_pwd.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox_pwd.SkinTxt.WaterText = "";
            this.skinTextBox_pwd.TabIndex = 3;
            // 
            // skinTextBox_id
            // 
            this.skinTextBox_id.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox_id.Icon = null;
            this.skinTextBox_id.IconIsButton = false;
            this.skinTextBox_id.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_id.Location = new System.Drawing.Point(83, 55);
            this.skinTextBox_id.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_id.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox_id.MouseBack = null;
            this.skinTextBox_id.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_id.Name = "skinTextBox_id";
            this.skinTextBox_id.NormlBack = null;
            this.skinTextBox_id.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox_id.Size = new System.Drawing.Size(189, 28);
            // 
            // skinTextBox_id.BaseText
            // 
            this.skinTextBox_id.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_id.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_id.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_id.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox_id.SkinTxt.Name = "BaseText";
            this.skinTextBox_id.SkinTxt.Size = new System.Drawing.Size(179, 18);
            this.skinTextBox_id.SkinTxt.TabIndex = 0;
            this.skinTextBox_id.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox_id.SkinTxt.WaterText = "";
            this.skinTextBox_id.TabIndex = 0;
            // 
            // skinLabel5
            // 
            this.skinLabel5.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel5.AutoSize = true;
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel5.Location = new System.Drawing.Point(36, 205);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(44, 17);
            this.skinLabel5.TabIndex = 0;
            this.skinLabel5.Text = "签名：";
            // 
            // skinLabel3
            // 
            this.skinLabel3.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel3.AutoSize = true;
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel3.Location = new System.Drawing.Point(12, 171);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(68, 17);
            this.skinLabel3.TabIndex = 0;
            this.skinLabel3.Text = "确认密码：";
            // 
            // skinLabel4
            // 
            this.skinLabel4.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel4.AutoSize = true;
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel4.Location = new System.Drawing.Point(36, 133);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(44, 17);
            this.skinLabel4.TabIndex = 0;
            this.skinLabel4.Text = "密码：";
            // 
            // skinLabel2
            // 
            this.skinLabel2.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel2.AutoSize = true;
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(36, 96);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(44, 17);
            this.skinLabel2.TabIndex = 0;
            this.skinLabel2.Text = "姓名：";
            // 
            // skinLabel1
            // 
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(36, 60);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(44, 17);
            this.skinLabel1.TabIndex = 0;
            this.skinLabel1.Text = "帐号：";
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel3.Location = new System.Drawing.Point(275, 101);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(56, 17);
            this.linkLabel3.TabIndex = 136;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "上传头像";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // RegisterForm
            // 
            this.AcceptButton = this.btnRegister;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Back = ((System.Drawing.Image)(resources.GetObject("$this.Back")));
            this.BorderPalace = ((System.Drawing.Image)(resources.GetObject("$this.BorderPalace")));
            this.ClientSize = new System.Drawing.Size(444, 291);
            this.CloseDownBack = global::GGTalk.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::GGTalk.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::GGTalk.Properties.Resources.btn_close_disable;
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.pnlTx);
            this.Controls.Add(this.skinButton1);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.skinTextBox_signature);
            this.Controls.Add(this.skinTextBox_nickName);
            this.Controls.Add(this.skinTextBox_pwd2);
            this.Controls.Add(this.skinTextBox_pwd);
            this.Controls.Add(this.skinTextBox_id);
            this.Controls.Add(this.skinLabel5);
            this.Controls.Add(this.skinLabel3);
            this.Controls.Add(this.skinLabel4);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.skinLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaxDownBack = global::GGTalk.Properties.Resources.btn_max_down;
            this.MaxMouseBack = global::GGTalk.Properties.Resources.btn_max_highlight;
            this.MaxNormlBack = global::GGTalk.Properties.Resources.btn_max_normal;
            this.MiniDownBack = global::GGTalk.Properties.Resources.btn_mini_down;
            this.MiniMouseBack = global::GGTalk.Properties.Resources.btn_mini_highlight;
            this.MiniNormlBack = global::GGTalk.Properties.Resources.btn_mini_normal;
            this.Name = "RegisterForm";
            this.RestoreDownBack = global::GGTalk.Properties.Resources.btn_restore_down;
            this.RestoreMouseBack = global::GGTalk.Properties.Resources.btn_restore_highlight;
            this.RestoreNormlBack = global::GGTalk.Properties.Resources.btn_restore_normal;
            this.Text = "注册";
            this.pnlTx.ResumeLayout(false);
            this.skinTextBox_signature.ResumeLayout(false);
            this.skinTextBox_signature.PerformLayout();
            this.skinTextBox_nickName.ResumeLayout(false);
            this.skinTextBox_nickName.PerformLayout();
            this.skinTextBox_pwd2.ResumeLayout(false);
            this.skinTextBox_pwd2.PerformLayout();
            this.skinTextBox_pwd.ResumeLayout(false);
            this.skinTextBox_pwd.PerformLayout();
            this.skinTextBox_id.ResumeLayout(false);
            this.skinTextBox_id.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinLabel skinLabel4;
        private CCWin.SkinControl.SkinLabel skinLabel5;
        private CCWin.SkinControl.SkinTextBox skinTextBox_id;
        private CCWin.SkinControl.SkinTextBox skinTextBox_pwd;
        private CCWin.SkinControl.SkinTextBox skinTextBox_nickName;
        private CCWin.SkinControl.SkinTextBox skinTextBox_signature;
        private CCWin.SkinControl.SkinButton btnRegister;
        private CCWin.SkinControl.SkinButton skinButton1;
        private CCWin.SkinControl.SkinPanel pnlTx;
        private CCWin.SkinControl.SkinPanel pnlImgTx;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinTextBox skinTextBox_pwd2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel3;
    }
}