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


namespace GGTalk
{
    /// <summary>
    /// 系统通知显示窗口。
    /// </summary>
    public partial class SystemNotifyForm : BaseForm
    {
        public SystemNotifyForm(string title, string content)
        {
            InitializeComponent();

            this.skinLabel_title.Text = title;
            this.skinLabel_content.Text = content;            
        }

        //窗口加载时
        private void FrmInformation_Load(object sender, EventArgs e)
        {
            //初始化窗口出现位置
            var p = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            this.PointToScreen(p);
            this.Location = p;
            NativeMethods.AnimateWindow(this.Handle, 130, AW.AW_SLIDE + AW.AW_VER_NEGATIVE);//开始窗体动画
        }

        //倒计时6秒关闭弹出窗
        private void timShow_Tick(object sender, EventArgs e)
        {
            //鼠标不在窗体内时
            if (!this.Bounds.Contains(Cursor.Position))
            {
                this.Close();
            }
        }

        private void skinButtom1_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.cnblogs.com/justnow");
        }        
    }
}
