using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ESBasic;

namespace GGTalk
{
    /// <summary>
    /// 显示远程磁盘请求的窗口。
    /// </summary>
    public partial class DiskRequestPanel : UserControl
    {
        /// <summary>
        /// 回复磁盘请求
        /// </summary>
        public event CbGeneric<bool> DiskRequestAnswerd;

        public DiskRequestPanel()
        {
            InitializeComponent();
        }

        private void skinButtomReject_Click(object sender, EventArgs e)
        {
            if (this.DiskRequestAnswerd != null)
            {
                this.DiskRequestAnswerd(false);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (this.DiskRequestAnswerd != null)
            {
                this.DiskRequestAnswerd(true);
            }
        }
    }
}
