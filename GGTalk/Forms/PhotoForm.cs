using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CCWin;

namespace GGTalk
{
    /// <summary>
    /// 拍照（自拍头像）窗口。
    /// </summary>
    public partial class PhotoForm : BaseForm
    {     
        public PhotoForm()
        {
            InitializeComponent();            
          
            try
            {
                this.headImagePanel1.Start(SystemSettings.Singleton.WebcamIndex, new Size(320, 240), 150);                             
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show(ee.Message);
            }          
        }
        

        private Image currentImage = null;
        public Image CurrentImage
        {
            get { return currentImage; }
        }
       
        private void PhotoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.headImagePanel1.Stop(); 
        }
        
        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.currentImage = this.headImagePanel1.GetHeadImage();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void skinButton1_Click_1(object sender, EventArgs e)
        {
            this.headImagePanel1.Stop();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
