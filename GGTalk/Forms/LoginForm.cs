using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CCWin;
using CCWin.SkinControl;
using CCWin.SkinClass;
using System.Runtime.InteropServices;
using System.Threading;
using GGTalk.Properties;
using System.Diagnostics;
using ESPlus.Rapid;
using ESPlus.Application.Basic;
using ESPlus.Application.CustomizeInfo;
using System.Configuration;
using ESBasic.Security;
using JustLib;

namespace GGTalk
{
    /// <summary>
    /// 登录窗口。
    /// </summary>
    public partial class LoginForm : BaseForm
    {
        private IRemotingService remotingService;
        private IRapidPassiveEngine rapidPassiveEngine;
        private ICustomizeHandler customizeHandler;
        private string pwdMD5;            

        public LoginForm(IRapidPassiveEngine engine ,ICustomizeHandler handler)
        {
            this.rapidPassiveEngine = engine;
            this.customizeHandler = handler;

            var registerPort = int.Parse(ConfigurationManager.AppSettings["RemotingPort"]);
            this.remotingService = (IRemotingService)Activator.GetObject(typeof(IRemotingService), string.Format("tcp://{0}:{1}/RemotingService", ConfigurationManager.AppSettings["ServerIP"], registerPort)); ;
             
            InitializeComponent();          

            var statusImageDictionary = new Dictionary<UserStatus, Image>();
            statusImageDictionary.Add(UserStatus.Online, this.imageList_state.Images[0]);
            statusImageDictionary.Add(UserStatus.Away, this.imageList_state.Images[1]);
            statusImageDictionary.Add(UserStatus.Busy, this.imageList_state.Images[2]);
            statusImageDictionary.Add(UserStatus.DontDisturb, this.imageList_state.Images[3]);
            statusImageDictionary.Add(UserStatus.Hide, this.imageList_state.Images[4]);
            statusImageDictionary.Add(UserStatus.OffLine, this.imageList_state.Images[5]);
            GlobalResourceManager.SetStatusImage(statusImageDictionary);

            this.skinLabel_SoftName.Text = GlobalResourceManager.SoftwareName;            

            this.checkBoxRememberPwd.Checked = SystemSettings.Singleton.RememberPwd;
            this.checkBoxAutoLogin.Checked = SystemSettings.Singleton.AutoLogin;   
            this.textBoxId.SkinTxt.Text = SystemSettings.Singleton.LastLoginUserID;
            if (SystemSettings.Singleton.RememberPwd)
            {
                this.textBoxPwd.SkinTxt.Text = "11111111";
                this.pwdMD5 = SystemSettings.Singleton.LastLoginPwdMD5;
                this.pwdIsMD5 = true;
            }
        }


        public CCWin.SkinControl.ChatListSubItem.UserStatus LoginStatus
        {
            get
            {
                return (ChatListSubItem.UserStatus)int.Parse(this.skinButton_State.Tag.ToString());
            }
        }

        public Image StateImage
        {
            get
            {
                return this.skinButton_State.Image;
            }
        }

        #region buttonLogin_Click        
        private void buttonLogin_Click(object sender, EventArgs e)
        { 
            var id = this.textBoxId.SkinTxt.Text;
            var pwd = this.textBoxPwd.SkinTxt.Text ;
            if (id.Length == 0) { return; }

            this.Cursor = Cursors.WaitCursor;
            this.buttonLogin.Enabled = false;
            try
            {
                this.rapidPassiveEngine.SecurityLogEnabled = false;
                
                if (!this.pwdIsMD5)
                { 
                    pwdMD5 = SecurityHelper.MD5String2(pwd);
                }
                var response = this.rapidPassiveEngine.Initialize(id, pwdMD5, ConfigurationManager.AppSettings["ServerIP"], int.Parse(ConfigurationManager.AppSettings["ServerPort"]), this.customizeHandler);
                if (response.LogonResult == LogonResult.Failed)
                {
                    MessageBoxEx.Show(response.FailureCause);
                    return;
                }

                //0923
                if (response.LogonResult == LogonResult.VersionMismatched)
                {
                    MessageBoxEx.Show("客户端与服务器的ESFramework版本不一致！");
                    return;
                }

                if (response.LogonResult == LogonResult.HadLoggedOn)
                {
                    MessageBoxEx.Show("该帐号已经在其它地方登录！");
                    return;
                }

                SystemSettings.Singleton.LastLoginUserID = id;
                SystemSettings.Singleton.RememberPwd =this.checkBoxRememberPwd.Checked ;
                SystemSettings.Singleton.LastLoginPwdMD5 = pwdMD5;
                SystemSettings.Singleton.AutoLogin = this.checkBoxAutoLogin.Checked;
                SystemSettings.Singleton.Save();
            }
            catch (Exception ee)
            {
                this.toolShow.Show(ee.Message, this.buttonLogin, new Point(this.buttonLogin.Width/2,-this.buttonLogin.Height), 3000);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.buttonLogin.Enabled = true;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        } 
        #endregion

        //选择状态
        private void btnState_Click(object sender, EventArgs e)
        {
            this.menuStripState.Show(this.Left + pnlTx.Left + panelHeadImage.Left + skinButton_State.Left, this.Top + pnlTx.Top + panelHeadImage.Top + skinButton_State.Top + skinButton_State.Height + 5);
        }

        //状态选择项
        private void Item_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            this.skinButton_State.Image = item.Image;
            this.skinButton_State.Tag = item.Tag;
        }
      

        //点击 软键盘
        private void textBoxPwd_IconClick(object sender, EventArgs e)
        {
            var pass = new PassKey(this.Left + this.textBoxPwd.Left - 25, this.Top + this.textBoxPwd.Bottom, this.textBoxPwd.SkinTxt);
            pass.Show(this);
        }

        //点击 自动登录CheckBox
        private void checkBoxAutoLogin_CheckedChanged(object sender, EventArgs e)
        {
            this.checkBoxRememberPwd.Checked = this.checkBoxAutoLogin.Checked ? true : this.checkBoxRememberPwd.Checked;
        }

        //点击 记住密码CheckBox
        private void checkBoxRememberPwd_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.checkBoxRememberPwd.Checked && this.checkBoxAutoLogin.Checked)
            {
                this.checkBoxAutoLogin.Checked = false;
            }
        }       
        
        //关闭
        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool pwdIsMD5 = false;
        private void btnRegister_Click(object sender, EventArgs e)
        {
            var registerForm = new RegisterForm();
            var res = registerForm.ShowDialog() ;
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                MessageBoxEx.Show("注册成功！接下来将使用此帐号自动登录！" ,"快速登录");
                this.textBoxId.SkinTxt.Text = registerForm.RegisteredUser.UserID;               
                this.textBoxPwd.SkinTxt.Text = "11111111";
                this.pwdMD5 = registerForm.RegisteredUser.PasswordMD5;
                this.pwdIsMD5 = true;
                this.buttonLogin.PerformClick();
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.Back = GlobalResourceManager.LoginBackImage;
                this.panelHeadImage.BackgroundImage = GlobalResourceManager.Png64;
            }

            if (SystemSettings.Singleton.AutoLogin)
            {
                this.buttonLogin.PerformClick();                
            }
        }

      
        private void textBoxPwd_SkinTxt_KeyUp(object sender, KeyEventArgs e)
        {
            this.pwdIsMD5 = false;
        }

        private void textBoxId_SkinTxt_TextChanged(object sender, EventArgs e)
        {
            this.textBoxPwd.SkinTxt.Clear();
            this.pwdIsMD5 = false;
        }

        private void skinButtom1_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.cnblogs.com/justnow");
        }

        private void skinLabel1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("2027224508");
            MessageBox.Show("已经将作者QQ号：2027224508复制到粘贴板！");
        }
    }
}
