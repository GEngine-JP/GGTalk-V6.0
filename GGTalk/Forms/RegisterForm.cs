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
using System.Configuration;
using ESBasic.Security;

namespace GGTalk
{
    /// <summary>
    /// 注册窗口。
    /// </summary>
    public partial class RegisterForm : BaseForm
    {
        private int headImageIndex = 0;
        private IRemotingService ggService;

        public RegisterForm()
        {
            InitializeComponent();

            int registerPort = int.Parse(ConfigurationManager.AppSettings["RemotingPort"]);
            this.ggService = (IRemotingService)Activator.GetObject(typeof(IRemotingService), string.Format("tcp://{0}:{1}/RemotingService", ConfigurationManager.AppSettings["ServerIP"], registerPort)); ;
              
            Random ran = new Random();
            this.headImageIndex = ran.Next(0,GlobalResourceManager.HeadImages.Length);
            this.pnlImgTx.BackgroundImage = GlobalResourceManager.HeadImages[this.headImageIndex];//根据ID获取头像            
        }

        #region RegisteredUser
        private GGUser registeredUser = null;
        public GGUser RegisteredUser
        {
            get
            {
                return this.registeredUser;
            }
        } 
        #endregion       

        private void skinButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string userID = this.skinTextBox_id.SkinTxt.Text.Trim();
            if (userID.Length == 0)
            {
                this.skinTextBox_id.SkinTxt.Focus();
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                MessageBoxEx.Show("帐号不能为空！");
                return;
            }

            string pwd = this.skinTextBox_pwd.SkinTxt.Text ;
            if (pwd != this.skinTextBox_pwd2.SkinTxt.Text)
            {
                MessageBoxEx.Show("两次输入的密码不一致！");
                this.skinTextBox_pwd.SkinTxt.SelectAll() ;
                this.skinTextBox_pwd.SkinTxt.Focus();
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            try
            {

                GGUser user = new GGUser(userID, SecurityHelper.MD5String2(pwd), this.skinTextBox_nickName.SkinTxt.Text,"",this.skinTextBox_signature.SkinTxt.Text, this.headImageIndex, "");
                if (this.selfPhoto)
                {                   
                    user.HeadImageData = ESBasic.Helpers.ImageHelper.Convert(this.pnlImgTx.BackgroundImage);
                    user.HeadImageIndex = -1;
                }

                RegisterResult result = ggService.Register(user);
                if (result == RegisterResult.Existed)
                {
                    this.skinTextBox_id.SkinTxt.SelectAll();
                    this.skinTextBox_id.SkinTxt.Focus();
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    MessageBoxEx.Show("用户帐号已经存在！");                    
                    return;
                }

                if (result == RegisterResult.Error)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    MessageBoxEx.Show("注册出现错误！");                    
                    return;
                }

                this.registeredUser = user;                
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ee)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                MessageBoxEx.Show("注册失败！" + ee.Message);
            }

        }
       
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.headImageIndex = (++this.headImageIndex) % GlobalResourceManager.HeadImages.Length;
            this.pnlImgTx.BackgroundImage = GlobalResourceManager.HeadImages[this.headImageIndex];
            this.selfPhoto = false;
        }

        private bool selfPhoto = false;
        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PhotoForm form = new PhotoForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.pnlImgTx.BackgroundImage = form.CurrentImage;
                this.selfPhoto = true;
            }
        }     

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HeadImageForm form = new HeadImageForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.pnlImgTx.BackgroundImage = form.CurrentImage;
                this.selfPhoto = true;
            }
        }                   
    }
}
