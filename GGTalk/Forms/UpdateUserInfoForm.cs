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
using ESPlus.Rapid;
using ESBasic;

using ESPlus.Application.CustomizeInfo;
using ESPlus.Application;
using GGTalk;

namespace GGTalk
{
    /// <summary>
    /// 修改个人资料窗口。
    /// </summary>
    public partial class UpdateUserInfoForm : BaseForm
    {
        private int headImageIndex = 0;
        private IRemotingService ggService;
        private GGUser currentUser;
        private IRapidPassiveEngine rapidPassiveEngine;
        public event CbGeneric<GGUser> UserInfoChanged;

        public UpdateUserInfoForm(IRapidPassiveEngine engine,GlobalUserCache globalUserCache, GGUser user)
        {
            InitializeComponent();

            this.rapidPassiveEngine = engine;
            this.currentUser = user ;
            int registerPort = int.Parse(ConfigurationManager.AppSettings["RemotingPort"]);
            this.ggService = (IRemotingService)Activator.GetObject(typeof(IRemotingService), string.Format("tcp://{0}:{1}/RemotingService", ConfigurationManager.AppSettings["ServerIP"], registerPort)); ;
           
            this.skinLabel_ID.Text = user.UserID;
            this.skinTextBox_nickName.SkinTxt.Text = user.Name;
            this.skinTextBox_signature.SkinTxt.Text = user.Signature;           
            
            if (user.HeadImageIndex >= 0)
            {
                this.headImageIndex = user.HeadImageIndex;
                this.pnlImgTx.BackgroundImage = GlobalResourceManager.GetHeadImage(user); //根据ID获取头像   
            }
            else
            {                
                this.pnlImgTx.BackgroundImage = user.HeadImage;
                this.selfPhoto = true;
            }
        }        

        private void skinButton1_Click(object sender, EventArgs e)
        {
            this.Close();       
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                //0923
                if (!this.rapidPassiveEngine.Connected)
                {
                    this.toolTip1.Show("离线状态，无法修改资料。", this.btnRegister, new Point(this.btnRegister.Width / 2, -this.btnRegister.Height), 3000);
                    return;
                }

                this.currentUser.HeadImageIndex = this.headImageIndex;
                this.currentUser.Name = this.skinTextBox_nickName.SkinTxt.Text;
                this.currentUser.Signature = this.skinTextBox_signature.SkinTxt.Text;               
                if (this.selfPhoto)
                {
                    this.currentUser.HeadImageData = ESBasic.Helpers.ImageHelper.Convert(this.pnlImgTx.BackgroundImage);
                    this.currentUser.HeadImageIndex = -1;
                }
                else
                {
                    this.currentUser.HeadImageData = null;
                }

                //0923
                this.Cursor = Cursors.WaitCursor;
                UIResultHandler handler = new UIResultHandler(this, this.UpdateCallback);
                byte[] data = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize(this.currentUser);
                //回复异步调用，避免阻塞UI线程
                this.rapidPassiveEngine.SendMessage(null, InformationTypes.UpdateUserInfo, data, null, 2048, handler.Create(), null); //0924               
            }
            catch (Exception ee)
            {
                this.Cursor = Cursors.Default;
                this.toolTip1.Show("修改失败！" + ee.Message, this.btnRegister, new Point(this.btnRegister.Width / 2, -this.btnRegister.Height), 3000);   
            }

        }

        //0923
        private void UpdateCallback(bool acked, object tag)
        {
            this.Cursor = Cursors.Default;
            if (acked)
            {
                MessageBox.Show("修改成功！");
                if (this.UserInfoChanged != null)
                {
                    this.UserInfoChanged(this.currentUser);
                }
                this.Close();
            }
            else
            {
                this.toolTip1.Show("提交超时，修改失败！", this.btnRegister, new Point(this.btnRegister.Width / 2, -this.btnRegister.Height), 3000);
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
