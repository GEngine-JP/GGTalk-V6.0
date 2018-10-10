namespace GGTalk
{
    partial class SystemSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemSettingForm));
            this.btnClose = new CCWin.SkinControl.SkinButton();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.skinRadioButton_hide = new CCWin.SkinControl.SkinRadioButton();
            this.skinRadioButton2 = new CCWin.SkinControl.SkinRadioButton();
            this.skinCheckBox_autoRun = new CCWin.SkinControl.SkinCheckBox();
            this.skinCheckBox_autoLogin = new CCWin.SkinControl.SkinCheckBox();
            this.skinCheckBox_ring = new CCWin.SkinControl.SkinCheckBox();
            this.skinLabel4 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel5 = new CCWin.SkinControl.SkinLabel();
            this.skinCheckBox_lastWords = new CCWin.SkinControl.SkinCheckBox();
            this.skinLabel6 = new CCWin.SkinControl.SkinLabel();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.btnClose.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.DownBack = ((System.Drawing.Image)(resources.GetObject("btnClose.DownBack")));
            this.btnClose.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(266, 178);
            this.btnClose.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnClose.MouseBack")));
            this.btnClose.Name = "btnClose";
            this.btnClose.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnClose.NormlBack")));
            this.btnClose.Size = new System.Drawing.Size(69, 24);
            this.btnClose.TabIndex = 133;
            this.btnClose.Text = "确定";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // skinLabel2
            // 
            this.skinLabel2.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel2.AutoSize = true;
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(11, 51);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(92, 17);
            this.skinLabel2.TabIndex = 135;
            this.skinLabel2.Text = "关闭主面板时：";
            // 
            // skinRadioButton_hide
            // 
            this.skinRadioButton_hide.AutoSize = true;
            this.skinRadioButton_hide.BackColor = System.Drawing.Color.Transparent;
            this.skinRadioButton_hide.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinRadioButton_hide.DownBack = null;
            this.skinRadioButton_hide.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinRadioButton_hide.Location = new System.Drawing.Point(109, 49);
            this.skinRadioButton_hide.MouseBack = null;
            this.skinRadioButton_hide.Name = "skinRadioButton_hide";
            this.skinRadioButton_hide.NormlBack = null;
            this.skinRadioButton_hide.SelectedDownBack = null;
            this.skinRadioButton_hide.SelectedMouseBack = null;
            this.skinRadioButton_hide.SelectedNormlBack = null;
            this.skinRadioButton_hide.Size = new System.Drawing.Size(146, 21);
            this.skinRadioButton_hide.TabIndex = 136;
            this.skinRadioButton_hide.Text = "隐藏到任务栏通知区域";
            this.skinRadioButton_hide.UseVisualStyleBackColor = false;
            // 
            // skinRadioButton2
            // 
            this.skinRadioButton2.AutoSize = true;
            this.skinRadioButton2.BackColor = System.Drawing.Color.Transparent;
            this.skinRadioButton2.Checked = true;
            this.skinRadioButton2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinRadioButton2.DownBack = null;
            this.skinRadioButton2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinRadioButton2.Location = new System.Drawing.Point(261, 49);
            this.skinRadioButton2.MouseBack = null;
            this.skinRadioButton2.Name = "skinRadioButton2";
            this.skinRadioButton2.NormlBack = null;
            this.skinRadioButton2.SelectedDownBack = null;
            this.skinRadioButton2.SelectedMouseBack = null;
            this.skinRadioButton2.SelectedNormlBack = null;
            this.skinRadioButton2.Size = new System.Drawing.Size(74, 21);
            this.skinRadioButton2.TabIndex = 136;
            this.skinRadioButton2.TabStop = true;
            this.skinRadioButton2.Text = "退出程序";
            this.skinRadioButton2.UseVisualStyleBackColor = false;
            // 
            // skinCheckBox_autoRun
            // 
            this.skinCheckBox_autoRun.AutoSize = true;
            this.skinCheckBox_autoRun.BackColor = System.Drawing.Color.Transparent;
            this.skinCheckBox_autoRun.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinCheckBox_autoRun.DownBack = null;
            this.skinCheckBox_autoRun.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinCheckBox_autoRun.Location = new System.Drawing.Point(109, 132);
            this.skinCheckBox_autoRun.MouseBack = null;
            this.skinCheckBox_autoRun.Name = "skinCheckBox_autoRun";
            this.skinCheckBox_autoRun.NormlBack = null;
            this.skinCheckBox_autoRun.SelectedDownBack = null;
            this.skinCheckBox_autoRun.SelectedMouseBack = null;
            this.skinCheckBox_autoRun.SelectedNormlBack = null;
            this.skinCheckBox_autoRun.Size = new System.Drawing.Size(111, 21);
            this.skinCheckBox_autoRun.TabIndex = 138;
            this.skinCheckBox_autoRun.Text = "开机时自动启动";
            this.skinCheckBox_autoRun.UseVisualStyleBackColor = false;
            // 
            // skinCheckBox_autoLogin
            // 
            this.skinCheckBox_autoLogin.AutoSize = true;
            this.skinCheckBox_autoLogin.BackColor = System.Drawing.Color.Transparent;
            this.skinCheckBox_autoLogin.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinCheckBox_autoLogin.DownBack = null;
            this.skinCheckBox_autoLogin.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinCheckBox_autoLogin.Location = new System.Drawing.Point(225, 132);
            this.skinCheckBox_autoLogin.MouseBack = null;
            this.skinCheckBox_autoLogin.Name = "skinCheckBox_autoLogin";
            this.skinCheckBox_autoLogin.NormlBack = null;
            this.skinCheckBox_autoLogin.SelectedDownBack = null;
            this.skinCheckBox_autoLogin.SelectedMouseBack = null;
            this.skinCheckBox_autoLogin.SelectedNormlBack = null;
            this.skinCheckBox_autoLogin.Size = new System.Drawing.Size(75, 21);
            this.skinCheckBox_autoLogin.TabIndex = 139;
            this.skinCheckBox_autoLogin.Text = "自动登录";
            this.skinCheckBox_autoLogin.UseVisualStyleBackColor = false;
            // 
            // skinCheckBox_ring
            // 
            this.skinCheckBox_ring.AutoSize = true;
            this.skinCheckBox_ring.BackColor = System.Drawing.Color.Transparent;
            this.skinCheckBox_ring.Checked = true;
            this.skinCheckBox_ring.CheckState = System.Windows.Forms.CheckState.Checked;
            this.skinCheckBox_ring.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinCheckBox_ring.DownBack = null;
            this.skinCheckBox_ring.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinCheckBox_ring.Location = new System.Drawing.Point(109, 105);
            this.skinCheckBox_ring.MouseBack = null;
            this.skinCheckBox_ring.Name = "skinCheckBox_ring";
            this.skinCheckBox_ring.NormlBack = null;
            this.skinCheckBox_ring.SelectedDownBack = null;
            this.skinCheckBox_ring.SelectedMouseBack = null;
            this.skinCheckBox_ring.SelectedNormlBack = null;
            this.skinCheckBox_ring.Size = new System.Drawing.Size(195, 21);
            this.skinCheckBox_ring.TabIndex = 138;
            this.skinCheckBox_ring.Text = "当收到信息时，播放信息提示音";
            this.skinCheckBox_ring.UseVisualStyleBackColor = false;
            // 
            // skinLabel4
            // 
            this.skinLabel4.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel4.AutoSize = true;
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel4.Location = new System.Drawing.Point(23, 106);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(80, 17);
            this.skinLabel4.TabIndex = 135;
            this.skinLabel4.Text = "信息提示音：";
            // 
            // skinLabel5
            // 
            this.skinLabel5.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel5.AutoSize = true;
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel5.Location = new System.Drawing.Point(35, 133);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(68, 17);
            this.skinLabel5.TabIndex = 135;
            this.skinLabel5.Text = "自动启动：";
            // 
            // skinCheckBox_lastWords
            // 
            this.skinCheckBox_lastWords.AutoSize = true;
            this.skinCheckBox_lastWords.BackColor = System.Drawing.Color.Transparent;
            this.skinCheckBox_lastWords.Checked = true;
            this.skinCheckBox_lastWords.CheckState = System.Windows.Forms.CheckState.Checked;
            this.skinCheckBox_lastWords.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinCheckBox_lastWords.DownBack = null;
            this.skinCheckBox_lastWords.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinCheckBox_lastWords.Location = new System.Drawing.Point(109, 78);
            this.skinCheckBox_lastWords.MouseBack = null;
            this.skinCheckBox_lastWords.Name = "skinCheckBox_lastWords";
            this.skinCheckBox_lastWords.NormlBack = null;
            this.skinCheckBox_lastWords.SelectedDownBack = null;
            this.skinCheckBox_lastWords.SelectedMouseBack = null;
            this.skinCheckBox_lastWords.SelectedNormlBack = null;
            this.skinCheckBox_lastWords.Size = new System.Drawing.Size(171, 21);
            this.skinCheckBox_lastWords.TabIndex = 138;
            this.skinCheckBox_lastWords.Text = "显示上次交谈的最后一句话";
            this.skinCheckBox_lastWords.UseVisualStyleBackColor = false;
            // 
            // skinLabel6
            // 
            this.skinLabel6.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel6.AutoSize = true;
            this.skinLabel6.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel6.BorderColor = System.Drawing.Color.White;
            this.skinLabel6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel6.Location = new System.Drawing.Point(23, 80);
            this.skinLabel6.Name = "skinLabel6";
            this.skinLabel6.Size = new System.Drawing.Size(80, 17);
            this.skinLabel6.TabIndex = 135;
            this.skinLabel6.Text = "打开聊天窗：";
            // 
            // SystemSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Back = ((System.Drawing.Image)(resources.GetObject("$this.Back")));
            this.BorderPalace = ((System.Drawing.Image)(resources.GetObject("$this.BorderPalace")));
            this.ClientSize = new System.Drawing.Size(355, 221);
            this.CloseDownBack = global::GGTalk.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::GGTalk.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::GGTalk.Properties.Resources.btn_close_disable;
            this.Controls.Add(this.skinCheckBox_autoLogin);
            this.Controls.Add(this.skinCheckBox_lastWords);
            this.Controls.Add(this.skinCheckBox_ring);
            this.Controls.Add(this.skinCheckBox_autoRun);
            this.Controls.Add(this.skinRadioButton2);
            this.Controls.Add(this.skinRadioButton_hide);
            this.Controls.Add(this.skinLabel5);
            this.Controls.Add(this.skinLabel6);
            this.Controls.Add(this.skinLabel4);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaxDownBack = global::GGTalk.Properties.Resources.btn_max_down;
            this.MaxMouseBack = global::GGTalk.Properties.Resources.btn_max_highlight;
            this.MaxNormlBack = global::GGTalk.Properties.Resources.btn_max_normal;
            this.MiniDownBack = global::GGTalk.Properties.Resources.btn_mini_down;
            this.MiniMouseBack = global::GGTalk.Properties.Resources.btn_mini_highlight;
            this.MiniNormlBack = global::GGTalk.Properties.Resources.btn_mini_normal;
            this.Name = "SystemSettingForm";
            this.RestoreDownBack = global::GGTalk.Properties.Resources.btn_restore_down;
            this.RestoreMouseBack = global::GGTalk.Properties.Resources.btn_restore_highlight;
            this.RestoreNormlBack = global::GGTalk.Properties.Resources.btn_restore_normal;
            this.Text = "系统设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinButton btnClose;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinRadioButton skinRadioButton_hide;
        private CCWin.SkinControl.SkinRadioButton skinRadioButton2;
        private CCWin.SkinControl.SkinCheckBox skinCheckBox_autoRun;
        private CCWin.SkinControl.SkinCheckBox skinCheckBox_autoLogin;
        private CCWin.SkinControl.SkinCheckBox skinCheckBox_ring;
        private CCWin.SkinControl.SkinLabel skinLabel4;
        private CCWin.SkinControl.SkinLabel skinLabel5;
        private CCWin.SkinControl.SkinCheckBox skinCheckBox_lastWords;
        private CCWin.SkinControl.SkinLabel skinLabel6;
    }
}