using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CCWin;
using CCWin.SkinControl;
using JustLib;


namespace GGTalk
{
    /// <summary>
    /// 显示个人资料的窗口。
    /// </summary>
    public partial class UserInformationForm : BaseForm ,IUserInformationForm
    {
        private Point pt;
        public UserInformationForm(Point pt)
        {
            this.Location = pt;
            InitializeComponent();            
        }

        public void SetUser(GGUser user)
        {
            this.lblQm.Text = user.Signature;
            this.skinLabel_id.Text = user.ID;
            this.skinLabel_name.Text = user.Name;           
            this.pnlImgTx.BackgroundImage = GlobalResourceManager.GetHeadImageOnline((GGUser)user);
        }

        private void UserInformationForm_Load(object sender, EventArgs e)
        {
            this.Location = this.pt;
        }         

        //窗体重绘时
        private void FrmUserInformation_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush sb = new SolidBrush(Color.FromArgb(100, 255, 255, 255));
            g.FillRectangle(sb, new Rectangle(new Point(1, Height - 103), new Size(Width - 2, 80)));
        }

        //计时器
        private bool flag = false;
        private void timShow_Tick(object sender, EventArgs e)
        {
            //鼠标不在窗体内时
            if (!this.Bounds.Contains(Cursor.Position) && flag)
            {
                this.Hide();
                flag = false;
            }
            else if (this.Bounds.Contains(Cursor.Position))
            {
                flag = true;
            }
        }

       
    }
}
