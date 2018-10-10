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
    /// 上传头像/选择头像 窗口。
    /// </summary>
    public partial class HeadImageForm : BaseForm
    {
        public HeadImageForm()
        {
            InitializeComponent();            
          
            try
            {
                this.imagePartSelecter1.ImagePartSelected += new ESBasic.CbGeneric<Bitmap>(imagePartSelecter1_ImagePartSelected);
                this.imagePartSelecter1.Initialize(150);               
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show(ee.Message);
            }          
        }

        void imagePartSelecter1_ImagePartSelected(Bitmap obj)
        {
            this.currentImage = obj;
        }
        

        private Image currentImage = null;
        public Image CurrentImage
        {
            get { return currentImage; }
        }       
        
        
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (this.currentImage == null)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void skinButton1_Click_1(object sender, EventArgs e)
        {          
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            string file = ESBasic.Helpers.FileHelper.GetFileToOpen("请选择要使用的图片");
            if (file == null)
            {
                return;
            }

            Image img = Image.FromFile(file) ;
            this.imagePartSelecter1.SetSourceImage(img);
            this.DialogResult = System.Windows.Forms.DialogResult.None;
        }
    }
}
