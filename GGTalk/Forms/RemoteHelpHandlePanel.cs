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
    /// 远程桌面的主人控制面板
    /// </summary>
    public partial class RemoteHelpHandlePanel : UserControl
    {        

        /// <summary>
        /// 中止远程协助
        /// </summary>
        public event CbGeneric RemoteHelpTerminated;

        public RemoteHelpHandlePanel()
        {
            InitializeComponent();

            this.timerLabel1.Visible = false;
            this.skinLabel_msg.Visible = true;
        }

        public bool IsWorking
        {
            get
            {
                return this.timerLabel1.IsWorking;
            }
        }

        private void skinButtomReject_Click(object sender, EventArgs e)
        {
            if (this.RemoteHelpTerminated != null)
            {
                this.RemoteHelpTerminated();
            }

            this.OnTerminate();
        }

        public void OnAgree()
        {            
            this.timerLabel1.Visible = true;
            this.skinLabel_msg.Visible = false;            
            this.timerLabel1.Start();
            this.timerLabel1.Location = new Point(this.Width / 2 - this.timerLabel1.Width / 2, this.timerLabel1.Location.Y);
        }       
        
        public void OnTerminate()
        {             
            this.timerLabel1.Visible = false;
            this.timerLabel1.Stop();
            this.timerLabel1.Reset();
            this.skinLabel_msg.Visible = true;           
        }
    }
}
