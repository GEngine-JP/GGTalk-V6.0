namespace GGTalk.Controls
{
    partial class FileTransferItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileTransferItem));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox_receive = new System.Windows.Forms.PictureBox();
            this.pictureBox_send = new System.Windows.Forms.PictureBox();
            this.skinLabel_cancel = new System.Windows.Forms.Label();
            this.skinLabel_receive = new System.Windows.Forms.Label();
            this.label_fileSize = new System.Windows.Forms.Label();
            this.label_speed = new System.Windows.Forms.Label();
            this.skinLabel_speedTitle = new System.Windows.Forms.Label();
            this.skinLabel_FileName = new System.Windows.Forms.Label();
            this.skinProgressBar2 = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_receive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_send)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_receive
            // 
            resources.ApplyResources(this.pictureBox_receive, "pictureBox_receive");
            this.pictureBox_receive.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_receive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_receive.Image = global::GGTalk.Properties.Resources.editUndo;
            this.pictureBox_receive.Name = "pictureBox_receive";
            this.pictureBox_receive.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox_receive, resources.GetString("pictureBox_receive.ToolTip"));
            // 
            // pictureBox_send
            // 
            resources.ApplyResources(this.pictureBox_send, "pictureBox_send");
            this.pictureBox_send.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_send.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_send.Image = global::GGTalk.Properties.Resources.editRedo;
            this.pictureBox_send.Name = "pictureBox_send";
            this.pictureBox_send.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox_send, resources.GetString("pictureBox_send.ToolTip"));
            // 
            // skinLabel_cancel
            // 
            resources.ApplyResources(this.skinLabel_cancel, "skinLabel_cancel");
            this.skinLabel_cancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.skinLabel_cancel.ForeColor = System.Drawing.Color.Blue;
            this.skinLabel_cancel.Name = "skinLabel_cancel";
            this.skinLabel_cancel.Click += new System.EventHandler(this.skinLabel2_Click);
            // 
            // skinLabel_receive
            // 
            resources.ApplyResources(this.skinLabel_receive, "skinLabel_receive");
            this.skinLabel_receive.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_receive.Cursor = System.Windows.Forms.Cursors.Default;
            this.skinLabel_receive.ForeColor = System.Drawing.Color.Blue;
            this.skinLabel_receive.Name = "skinLabel_receive";
            this.skinLabel_receive.Click += new System.EventHandler(this.linkLabel_receive_LinkClicked);
            // 
            // label_fileSize
            // 
            resources.ApplyResources(this.label_fileSize, "label_fileSize");
            this.label_fileSize.BackColor = System.Drawing.Color.Transparent;
            this.label_fileSize.Name = "label_fileSize";
            // 
            // label_speed
            // 
            resources.ApplyResources(this.label_speed, "label_speed");
            this.label_speed.BackColor = System.Drawing.Color.Transparent;
            this.label_speed.Name = "label_speed";
            // 
            // skinLabel_speedTitle
            // 
            resources.ApplyResources(this.skinLabel_speedTitle, "skinLabel_speedTitle");
            this.skinLabel_speedTitle.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_speedTitle.Name = "skinLabel_speedTitle";
            // 
            // skinLabel_FileName
            // 
            resources.ApplyResources(this.skinLabel_FileName, "skinLabel_FileName");
            this.skinLabel_FileName.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_FileName.Name = "skinLabel_FileName";
            // 
            // skinProgressBar2
            // 
            resources.ApplyResources(this.skinProgressBar2, "skinProgressBar2");
            this.skinProgressBar2.BackColor = System.Drawing.Color.White;
            this.skinProgressBar2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.skinProgressBar2.Name = "skinProgressBar2";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Folder.png");
            // 
            // FileTransferItem
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pictureBox_receive);
            this.Controls.Add(this.pictureBox_send);
            this.Controls.Add(this.skinProgressBar2);
            this.Controls.Add(this.skinLabel_cancel);
            this.Controls.Add(this.skinLabel_receive);
            this.Controls.Add(this.label_fileSize);
            this.Controls.Add(this.label_speed);
            this.Controls.Add(this.skinLabel_speedTitle);
            this.Controls.Add(this.skinLabel_FileName);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FileTransferItem";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_receive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_send)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label skinLabel_FileName;
        private System.Windows.Forms.Label skinLabel_speedTitle;
        private System.Windows.Forms.Label label_speed;
        private System.Windows.Forms.Label label_fileSize;
        private System.Windows.Forms.Label skinLabel_receive;
        private System.Windows.Forms.Label skinLabel_cancel;
        private System.Windows.Forms.ProgressBar skinProgressBar2;
        private System.Windows.Forms.PictureBox pictureBox_send;
        private System.Windows.Forms.PictureBox pictureBox_receive;
        private System.Windows.Forms.ImageList imageList1;
    }
}
