namespace GGTalk
{
    partial class JoinGroupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JoinGroupForm));
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.btnClose = new CCWin.SkinControl.SkinButton();
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.skinTextBox_id = new CCWin.SkinControl.SkinTextBox();
            this.skinTextBox_id.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinLabel1
            // 
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(20, 74);
            this.skinLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(69, 20);
            this.skinLabel1.TabIndex = 0;
            this.skinLabel1.Text = "群帐号：";
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
            this.btnClose.Location = new System.Drawing.Point(263, 119);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnClose.MouseBack")));
            this.btnClose.Name = "btnClose";
            this.btnClose.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnClose.NormlBack")));
            this.btnClose.Size = new System.Drawing.Size(92, 30);
            this.btnClose.TabIndex = 133;
            this.btnClose.Text = "确定";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // skinButton1
            // 
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.skinButton1.DownBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.DownBack")));
            this.skinButton1.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.skinButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinButton1.Location = new System.Drawing.Point(163, 119);
            this.skinButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.skinButton1.MouseBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.MouseBack")));
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.NormlBack")));
            this.skinButton1.Size = new System.Drawing.Size(92, 30);
            this.skinButton1.TabIndex = 133;
            this.skinButton1.Text = "取消";
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // skinTextBox_id
            // 
            this.skinTextBox_id.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox_id.Icon = null;
            this.skinTextBox_id.IconIsButton = false;
            this.skinTextBox_id.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_id.Location = new System.Drawing.Point(99, 66);
            this.skinTextBox_id.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_id.MinimumSize = new System.Drawing.Size(37, 35);
            this.skinTextBox_id.MouseBack = null;
            this.skinTextBox_id.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_id.Name = "skinTextBox_id";
            this.skinTextBox_id.NormlBack = null;
            this.skinTextBox_id.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.skinTextBox_id.Size = new System.Drawing.Size(257, 35);
            // 
            // skinTextBox_id.BaseText
            // 
            this.skinTextBox_id.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_id.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_id.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_id.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.skinTextBox_id.SkinTxt.Name = "BaseText";
            this.skinTextBox_id.SkinTxt.Size = new System.Drawing.Size(243, 22);
            this.skinTextBox_id.SkinTxt.TabIndex = 0;
            this.skinTextBox_id.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox_id.SkinTxt.WaterText = "";
            this.skinTextBox_id.TabIndex = 134;
            // 
            // JoinGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Back = ((System.Drawing.Image)(resources.GetObject("$this.Back")));
            this.BorderPalace = ((System.Drawing.Image)(resources.GetObject("$this.BorderPalace")));
            this.ClientSize = new System.Drawing.Size(391, 172);
            this.CloseDownBack = global::GGTalk.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::GGTalk.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::GGTalk.Properties.Resources.btn_close_disable;
            this.Controls.Add(this.skinTextBox_id);
            this.Controls.Add(this.skinButton1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.skinLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaxDownBack = global::GGTalk.Properties.Resources.btn_max_down;
            this.MaxMouseBack = global::GGTalk.Properties.Resources.btn_max_highlight;
            this.MaxNormlBack = global::GGTalk.Properties.Resources.btn_max_normal;
            this.MiniDownBack = global::GGTalk.Properties.Resources.btn_mini_down;
            this.MiniMouseBack = global::GGTalk.Properties.Resources.btn_mini_highlight;
            this.MiniNormlBack = global::GGTalk.Properties.Resources.btn_mini_normal;
            this.Name = "JoinGroupForm";
            this.RestoreDownBack = global::GGTalk.Properties.Resources.btn_restore_down;
            this.RestoreMouseBack = global::GGTalk.Properties.Resources.btn_restore_highlight;
            this.RestoreNormlBack = global::GGTalk.Properties.Resources.btn_restore_normal;
            this.Text = "加入群";
            this.UseCustomIcon = true;
            this.skinTextBox_id.ResumeLayout(false);
            this.skinTextBox_id.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinButton btnClose;
        private CCWin.SkinControl.SkinButton skinButton1;
        private CCWin.SkinControl.SkinTextBox skinTextBox_id;
    }
}