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
using System.Configuration;
using ESPlus.Rapid;
using OMCS.Tools;
using OMCS.Passive;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace GGTalk
{
    /// <summary>
    /// 系统设置窗口。
    /// </summary>
    public partial class SystemSettingForm : BaseForm
    {
        public SystemSettingForm()
        {
            InitializeComponent();
            SendMessage(this.btnClose.Handle, BCM_SETSHIELD, 0, (IntPtr)1);

            this.skinRadioButton_hide.Checked = !SystemSettings.Singleton.ExitWhenCloseMainForm;
            this.skinCheckBox_autoRun.Checked = SystemSettings.Singleton.AutoRun;
            this.skinCheckBox_autoLogin.Checked = SystemSettings.Singleton.AutoLogin;
            this.skinCheckBox_ring.Checked = SystemSettings.Singleton.PlayAudio4Message;
            this.skinCheckBox_lastWords.Checked = SystemSettings.Singleton.LoadLastWordsWhenChatFormOpened;

            SystemSettings.Singleton.Save();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                bool autoStartChanged = (this.skinCheckBox_autoRun.Checked != SystemSettings.Singleton.AutoRun);
                SystemSettings.Singleton.AutoRun = this.skinCheckBox_autoRun.Checked;               
                SystemSettings.Singleton.ExitWhenCloseMainForm = !this.skinRadioButton_hide.Checked;
                SystemSettings.Singleton.AutoLogin = this.skinCheckBox_autoLogin.Checked;
                SystemSettings.Singleton.PlayAudio4Message = this.skinCheckBox_ring.Checked;
                SystemSettings.Singleton.LoadLastWordsWhenChatFormOpened = this.skinCheckBox_lastWords.Checked;
                MultimediaManagerFactory.GetSingleton().CameraDeviceIndex = SystemSettings.Singleton.WebcamIndex;
                SystemSettings.Singleton.Save();

                if (autoStartChanged)
                {
                    string name = "GGTalk.exe";
                    string args = string.Format("{0} {1}", name, SystemSettings.Singleton.AutoRun.ToString());
                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + "AutoStart.exe", args);
                }

                //操作注册表，需要使用管理员身份启动程序。
                //ESBasic.Helpers.WindowsHelper.RunWhenStart_usingReg(this.skinCheckBox_autoRun.Checked, "GG2013", Application.ExecutablePath);
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show(ee.Message);
            }
        }

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, IntPtr lParam);
        public const UInt32 BCM_SETSHIELD = 0x160C;

    }
}
