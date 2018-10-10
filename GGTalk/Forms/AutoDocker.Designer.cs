using System.ComponentModel;
using System;
using System.Windows.Forms;
namespace GGTalk
{
    partial class AutoDocker
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.CheckPosTimer = new Timer(this.components);
            this.CheckPosTimer.Enabled = true;
            this.CheckPosTimer.Interval = 300;
            this.CheckPosTimer.Tick += new EventHandler(this.CheckPosTimer_Tick);
        }
    }
}
