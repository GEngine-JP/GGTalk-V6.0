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
    /// 视频请求面板。
    /// </summary>
    public partial class VideoRequestPanel : UserControl
    {
        /// <summary>
        /// 回复视频请求
        /// </summary>
        public event CbGeneric<bool> VideoRequestAnswerd;
       
        public VideoRequestPanel()
        {
            InitializeComponent();
        }

        private void skinButtomReject_Click(object sender, EventArgs e)
        {
            if (this.VideoRequestAnswerd != null)
            {
                this.VideoRequestAnswerd(false);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (this.VideoRequestAnswerd != null)
            {
                this.VideoRequestAnswerd(true);
            }
        }
    }
}
