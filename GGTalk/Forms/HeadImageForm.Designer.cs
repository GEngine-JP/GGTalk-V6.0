namespace GGTalk
{
    partial class HeadImageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeadImageForm));
            this.regionSelecter1 = new OMCS.Tools.RegionSelecter(this.components);
            this.btnRegister = new CCWin.SkinControl.SkinButton();
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.skinButton2 = new CCWin.SkinControl.SkinButton();
            this.imagePartSelecter1 = new OMCS.Tools.ImagePartSelecter();
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
            this.btnRegister.Location = new System.Drawing.Point(268, 410);
            this.btnRegister.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnRegister.MouseBack")));
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnRegister.NormlBack")));
            this.btnRegister.Size = new System.Drawing.Size(88, 29);
            this.btnRegister.TabIndex = 139;
            this.btnRegister.Text = "确定";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
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
            this.skinButton1.Location = new System.Drawing.Point(120, 410);
            this.skinButton1.MouseBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.MouseBack")));
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.NormlBack")));
            this.skinButton1.Size = new System.Drawing.Size(88, 29);
            this.skinButton1.TabIndex = 140;
            this.skinButton1.Text = "取消";
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.skinButton1_Click_1);
            // 
            // skinButton2
            // 
            this.skinButton2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.skinButton2.BackColor = System.Drawing.Color.Transparent;
            this.skinButton2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.skinButton2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.skinButton2.DownBack = ((System.Drawing.Image)(resources.GetObject("skinButton2.DownBack")));
            this.skinButton2.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.skinButton2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinButton2.Location = new System.Drawing.Point(376, 45);
            this.skinButton2.MouseBack = ((System.Drawing.Image)(resources.GetObject("skinButton2.MouseBack")));
            this.skinButton2.Name = "skinButton2";
            this.skinButton2.NormlBack = ((System.Drawing.Image)(resources.GetObject("skinButton2.NormlBack")));
            this.skinButton2.Size = new System.Drawing.Size(88, 29);
            this.skinButton2.TabIndex = 139;
            this.skinButton2.Text = "选择图片";
            this.skinButton2.UseVisualStyleBackColor = false;
            this.skinButton2.Click += new System.EventHandler(this.skinButton2_Click);
            // 
            // imagePartSelecter1
            // 
            this.imagePartSelecter1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.imagePartSelecter1.BackColor = System.Drawing.Color.White;
            this.imagePartSelecter1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imagePartSelecter1.Location = new System.Drawing.Point(19, 80);
            this.imagePartSelecter1.Name = "imagePartSelecter1";
            this.imagePartSelecter1.Size = new System.Drawing.Size(450, 302);
            this.imagePartSelecter1.TabIndex = 141;
            // 
            // HeadImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 454);
            this.Controls.Add(this.imagePartSelecter1);
            this.Controls.Add(this.skinButton1);
            this.Controls.Add(this.skinButton2);
            this.Controls.Add(this.btnRegister);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HeadImageForm";
            this.Text = "上传头像";          
            this.ResumeLayout(false);

        }

        #endregion

        private OMCS.Tools.RegionSelecter regionSelecter1;
        private CCWin.SkinControl.SkinButton btnRegister;
        private CCWin.SkinControl.SkinButton skinButton1;
        private CCWin.SkinControl.SkinButton skinButton2;
        private OMCS.Tools.ImagePartSelecter imagePartSelecter1;
    }
}