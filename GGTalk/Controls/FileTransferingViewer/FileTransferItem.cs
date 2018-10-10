using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ESBasic;
using ESBasic.Helpers;
using ESPlus.FileTransceiver;

namespace GGTalk.Controls
{
    /// <summary>
    /// 用于显示单个文件传送的进度状态。
    /// </summary>
    public partial class FileTransferItem : UserControl
    {
        private TransferingProject transmittingFileInfo;
        private DateTime lastDisplaySpeedTime = DateTime.Now;
        private ulong lastTransmittedPreSecond = 0;
        private bool isTransfering = false;
        public bool IsTransfering
        {
            get { return isTransfering; }
            set { isTransfering = value; }
        }

        public FileTransferItem()
        {
            InitializeComponent();
        }
   
        /// <summary>
        /// FileCanceled 当点击“取消”按钮时，将触发此事件。
        /// </summary>
        public event CbFileCanceled FileCanceled;
        /// <summary>
        /// 当 点击 “接收”按钮 时，触发
        /// </summary>
        public event CbFileReceived FileReceived;
        /// <summary>
        /// 当点击“拒绝”按钮时，触发
        /// </summary>
        public event CbFileRejected FileRejected;

        #region CancelEnabled
        public bool CancelEnabled
        {
            get
            {
                return this.skinLabel_cancel.Visible;
            }
            set
            {
                this.skinLabel_cancel.Visible = value;
            }
        } 
        #endregion

        #region Initialize
        public void Initialize(TransferingProject info, bool offlineFile, bool doneAgreed)
        {
            this.transmittingFileInfo = info;
            this.ShowIcon(info.ProjectName ,info.IsFolder);
           

            this.skinLabel_FileName.Text = this.transmittingFileInfo.ProjectName;
            this.toolTip1.SetToolTip(this.skinLabel_FileName, this.transmittingFileInfo.ProjectName);
            if (info.IsSender)
            {
                this.pictureBox_send.Visible = true;
                this.pictureBox_receive.Visible = false;
                this.skinLabel_receive.Visible = false;
                this.skinLabel_receive.Enabled = false;
                this.skinLabel_cancel.Visible = true;
                this.skinLabel_cancel.Enabled = true;
            }
            else
            {
                this.pictureBox_send.Visible = false;
                this.pictureBox_receive.Visible = true;
                this.skinLabel_receive.Visible = true;
                this.skinLabel_receive.Enabled = true;
                this.skinLabel_cancel.Visible = true;
                this.skinLabel_cancel.Enabled = true;
                this.skinLabel_cancel.Text = "拒绝";

                if (doneAgreed)
                {
                    this.skinLabel_receive.Enabled = false;
                    this.skinLabel_receive.Visible = false;
                    this.skinLabel_cancel.Text = "取消";
                }
            }         
            this.label_speed.Visible = false;
            this.skinLabel_speedTitle.Visible = false;

            var sizeStr = PublicHelper.GetSizeString((ulong)this.transmittingFileInfo.TotalSize);
            this.label_fileSize.Text = sizeStr;
        }
        private void ShowIcon(string fileName ,bool isFolder)
        {
            Image bmp = null;
            if (isFolder)
            {
                bmp = this.imageList1.Images[0];
            }
            else
            {
                var ary = fileName.Split('.');
                if (ary.Length == 1)
                {
                    var icon = WindowsHelper.GetSystemIconByFileType(".txt", true);
                    bmp = icon.ToBitmap();
                }
                else
                {
                    var extendName = "." + ary[ary.Length - 1].ToLower();
                    var icon = WindowsHelper.GetSystemIconByFileType(extendName, true);
                    bmp = icon.ToBitmap();
                }
            }

            if (bmp != null)
            {              
                this.pictureBox1.Image = bmp;
            }
        }
        
        #endregion

        public void CheckZeroSpeed()
        {
            var span = DateTime.Now - this.lastDisplaySpeedTime;

            if (span.TotalSeconds >= 1)
            {
                this.SetProgress(this.totalSize, this.lastTransmitted);
            }
        }

        private DateTime lastSetTime = DateTime.Now;
        public void SetProgress(ulong total, ulong transmitted)
        {
            var span = DateTime.Now - this.lastSetTime;
            if (span.TotalSeconds < 0.2)
            {
                return;
            }

            this.lastSetTime = DateTime.Now;
            this.SetProgress2(total, transmitted);
        }

        private ulong lastSpeed = 0;
        private bool firstSecond = true; //解决续传时，初始速度非常大的bug
        private ulong totalSize = 1; //解决0速度的问题
        private ulong lastTransmitted = 0;
        private void SetProgress2(ulong total, ulong transmitted)
        {            
            if (this.InvokeRequired)
            {
                object[] args = { total, transmitted };
                this.Invoke(new CbGeneric<ulong, ulong>(this.SetProgress2), args);
            }
            else
            {
                this.label_speed.Visible = true;
                this.skinLabel_speedTitle.Visible = true;
                this.totalSize = total;
                this.lastTransmitted = transmitted;

                this.skinProgressBar2.Maximum = 1000;

                this.skinProgressBar2.Value = (int)(transmitted * 1000 / total);               

                var now = DateTime.Now;
                var span = now - this.lastDisplaySpeedTime;

                if (span.TotalSeconds >= 1)
                {
                    if (!this.firstSecond)
                    {
                        if (lastSpeed == 0)
                        {
                            lastSpeed = (ulong)((transmitted - this.lastTransmittedPreSecond) / span.TotalSeconds); ;
                        }    

                        var transferSpeed = (ulong)((transmitted - this.lastTransmittedPreSecond) / span.TotalSeconds);
                        //transferSpeed = (transferSpeed + 7 * this.lastSpeed) / 8;
                        this.lastSpeed = transferSpeed;
                        byte littleNum = 0;
                        if (transferSpeed > 1024 * 1024)
                        {
                            littleNum = 1;
                        }
                        this.label_speed.Text = PublicHelper.GetSizeString((ulong)transferSpeed, littleNum) + "/s";
                        var leftSecs = transferSpeed == 0 ? 10000 : (int)((total - transmitted) / transferSpeed);
                        var hour = leftSecs / 3600;
                        var min = (leftSecs % 3600) / 60;
                        var sec = ((leftSecs % 3600) % 60) % 60;                       
                        this.lastDisplaySpeedTime = now;
                    }

                    this.lastTransmittedPreSecond = transmitted;

                    if (this.firstSecond)
                    {
                        this.firstSecond = false;
                    }
                }          
            }
        }

        public TransferingProject TransmittingProject
        {
            get
            {
                return this.transmittingFileInfo;
            }
        }  

        private void linkLabel_receive_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                var savePath = ESBasic.Helpers.FileHelper.GetPathToSave("保存", this.transmittingFileInfo.ProjectName, null);
                if (!string.IsNullOrEmpty(savePath))
                {
                    //if (ESBasic.Helpers.MachineHelper.GetDiskFreeSpace(savePath.Substring(0, 3)) < (ulong)transmittingFileInfo.TotalSize)
                    //{
                    //    MessageBox.Show("磁盘空间不足", "GGTalk.Controls");
                    //    return;
                    //}
                    if (this.FileReceived != null)
                    {
                        this.skinLabel_receive.Enabled = false;
                        this.skinLabel_receive.Visible = false;
                        this.skinLabel_cancel.Text = "取消";
                        this.FileReceived(this, this.transmittingFileInfo.ProjectID, this.transmittingFileInfo.IsSender, savePath);
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "GGTalk.Controls");
            }
        }

        private void skinLabel2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.skinLabel_cancel.Text == "拒绝")
                {
                    if (this.FileRejected != null)
                    {
                        this.FileRejected(this.transmittingFileInfo.ProjectID);
                    }
                }
                else
                {
                    if (this.FileCanceled != null)
                    {
                        this.FileCanceled(this, this.transmittingFileInfo.ProjectID, this.transmittingFileInfo.IsSender);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "GGTalk.Controls");
            }
        }       
    }

    public delegate void CbFileCanceled(FileTransferItem item, string projectID, bool isSend);
    public  delegate  void  CbFileReceived(FileTransferItem item, string projectID, bool isSend, string savePath);
    public delegate void CbFileRejected(string projectID);
     
}
  
