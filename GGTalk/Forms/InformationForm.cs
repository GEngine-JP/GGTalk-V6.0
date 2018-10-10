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
    /// 启动时，右下托盘提示窗口。
    /// </summary>
    public partial class InformationForm : BaseForm
    {
        public InformationForm(string id,string name, Image title)
        {
            InitializeComponent();           

            this.Text = string.Format("您好，{0}（{1}）",name, id);
            this.pnlImgTx.BackgroundImage = title;
            this.skinLabel2.Text = string.Format("欢迎使用 GGTalk 2016 \n\n可在广域网运行的QQ高仿版" ,GlobalResourceManager.SoftwareName);
        }

        //窗口加载时
        private void FrmInformation_Load(object sender, EventArgs e)
        {
            //初始化窗口出现位置
            Point p = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.cnblogs.com/justnow");
        }

        private void skinButtom1_Click(object sender, EventArgs e)
        {

        }        
    }
}
