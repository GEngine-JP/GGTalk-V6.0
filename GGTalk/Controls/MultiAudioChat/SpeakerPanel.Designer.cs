using JustLib;
using GGTalk.Controls;

namespace GGTalk.Controls
{
    partial class SpeakerPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpeakerPanel));
            this.skinLabel_name = new CCWin.SkinControl.SkinLabel();
            this.pictureBox_Mic = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.decibelDisplayer1 = new DecibelDisplayer();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.skinPictureBox1 = new CCWin.SkinControl.SkinPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Mic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.skinPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // skinLabel_name
            // 
            this.skinLabel_name.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel_name.AutoSize = true;
            this.skinLabel_name.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_name.BorderColor = System.Drawing.Color.White;
            this.skinLabel_name.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel_name.Location = new System.Drawing.Point(24, 12);
            this.skinLabel_name.Name = "skinLabel_name";
            this.skinLabel_name.Size = new System.Drawing.Size(129, 17);
            this.skinLabel_name.TabIndex = 6;
            this.skinLabel_name.Text = "春天来了（1282938）";
            // 
            // pictureBox_Mic
            // 
            this.pictureBox_Mic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_Mic.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_Mic.BackgroundImage")));
            this.pictureBox_Mic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_Mic.Location = new System.Drawing.Point(220, 8);
            this.pictureBox_Mic.Name = "pictureBox_Mic";
            this.pictureBox_Mic.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_Mic.TabIndex = 7;
            this.pictureBox_Mic.TabStop = false;
            this.pictureBox_Mic.Click += new System.EventHandler(this.pictureBox_Mic_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "b9m0_0.png");
            this.imageList1.Images.SetKeyName(1, "micDis.png");
            this.imageList1.Images.SetKeyName(2, "micFail.png");
            // 
            // decibelDisplayer1
            // 
            this.decibelDisplayer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.decibelDisplayer1.BackColor = System.Drawing.Color.White;
            this.decibelDisplayer1.Location = new System.Drawing.Point(195, 16);
            this.decibelDisplayer1.Name = "decibelDisplayer1";
            this.decibelDisplayer1.Size = new System.Drawing.Size(20, 8);
            this.decibelDisplayer1.TabIndex = 9;
            this.toolTip1.SetToolTip(this.decibelDisplayer1, "声音强度");
            this.decibelDisplayer1.ValueVisialbe = false;
            this.decibelDisplayer1.Working = true;
            // 
            // skinPictureBox1
            // 
            this.skinPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.skinPictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("skinPictureBox1.BackgroundImage")));
            this.skinPictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.skinPictureBox1.Location = new System.Drawing.Point(0, 8);
            this.skinPictureBox1.Name = "skinPictureBox1";
            this.skinPictureBox1.Size = new System.Drawing.Size(24, 24);
            this.skinPictureBox1.TabIndex = 11;
            this.skinPictureBox1.TabStop = false;
            // 
            // SpeakerPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.skinPictureBox1);
            this.Controls.Add(this.decibelDisplayer1);
            this.Controls.Add(this.pictureBox_Mic);
            this.Controls.Add(this.skinLabel_name);
            this.Name = "SpeakerPanel";
            this.Size = new System.Drawing.Size(247, 39);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Mic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.skinPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel skinLabel_name;
        private System.Windows.Forms.PictureBox pictureBox_Mic;
        private DecibelDisplayer decibelDisplayer1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private CCWin.SkinControl.SkinPictureBox skinPictureBox1;
    }
}
