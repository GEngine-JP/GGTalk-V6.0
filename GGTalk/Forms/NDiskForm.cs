using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CCWin;
using CCWin.Win32;
using CCWin.Win32.Const;
using System.Diagnostics;
using ESBasic;
using OMCS.Passive;
using ESPlus.Application.FileTransfering.Passive;
using JustLib.NetworkDisk.Passive;

namespace GGTalk
{
    /// <summary>
    /// 网盘/远程磁盘 窗口。
    /// </summary>
    public partial class NDiskForm : BaseForm
    {        
        public NDiskForm(string ownerID,string nickName, IFileOutter fileOutter , INDiskOutter diskOutter)
        {
            InitializeComponent();
            this.TopMost = false;
            this.ShowInTaskbar = true;
            this.nDiskBrowser1.Initialize(ownerID, fileOutter, diskOutter);
            this.Text = ownerID == null ? "我的网盘" : string.Format("远程磁盘 - {0}", nickName);
        }

       
    }
}
