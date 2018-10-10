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
    /// 远程协助 请求面板。
    /// </summary>
    public partial class RemoteHelpRequestPanel : UserControl
    {
       
        /// <summary>
        /// 回复远程协助请求
        /// </summary>
        public event CbGeneric<bool, RemoteHelpStyle> RemoteHelpRequestAnswerd;

        public RemoteHelpRequestPanel()
        {
            InitializeComponent();
        }

        private void skinButtomReject_Click(object sender, EventArgs e)
        {
            if (this.RemoteHelpRequestAnswerd != null)
            {
                this.RemoteHelpRequestAnswerd(false, this.remoteDesktopStyle);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (this.RemoteHelpRequestAnswerd != null)
            {
                this.RemoteHelpRequestAnswerd(true ,this.remoteDesktopStyle);
            }
        }

        private RemoteHelpStyle remoteDesktopStyle;
        public void SetRemoteDesktopStyle(RemoteHelpStyle style)
        {
            this.remoteDesktopStyle = style;
            this.skinLabel1.Text = "对方向您请求远程协助 . . ." ;
        }

        
    }
}
