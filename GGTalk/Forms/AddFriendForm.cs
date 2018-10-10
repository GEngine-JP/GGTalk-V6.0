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
using ESPlus.Rapid;


namespace GGTalk
{
    /// <summary>
    /// 添加好友。
    /// </summary>
    internal partial class AddFriendForm : BaseForm
    {      
        private IRapidPassiveEngine rapidPassiveEngine;
        private IChatSupporter ggSupporter;

        public AddFriendForm(IRapidPassiveEngine engine, IChatSupporter supporter, GGUser currentUser)
            :this(engine,supporter,currentUser,"")
        {
        }

        public AddFriendForm(IRapidPassiveEngine engine ,IChatSupporter supporter ,GGUser currentUser ,string friendID)
        {
            InitializeComponent();
            this.Icon = GlobalResourceManager.Icon64;
            this.rapidPassiveEngine = engine;
            this.ggSupporter = supporter;
            this.skinComboBox1.DataSource = currentUser.GetFriendCatalogList();
            this.skinComboBox1.SelectedIndex = 0;
            this.skinTextBox_id.SkinTxt.Text = friendID??"";
        }

        #region FriendID
        private string friendID = "";
        public string FriendID
        {
            get
            {
                return this.friendID;
            }
        } 
        #endregion        

        #region CatalogName
        private string catalogName = "";
        public string CatalogName
        {
            get { return catalogName; }
        } 
        #endregion

        private void skinButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.friendID = this.skinTextBox_id.SkinTxt.Text.Trim();
            if (this.friendID.Length == 0)
            {
                MessageBoxEx.Show("帐号不能为空！");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }           

            try
            {
                if (this.ggSupporter.IsFriend(this.friendID))
                {
                    MessageBoxEx.Show("该用户已经是好友！");
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }

                this.catalogName = this.skinComboBox1.SelectedItem.ToString();
                var contract = new AddFriendContract(this.friendID,this.catalogName);
                var info = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize(contract);
                var bRes = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.AddFriend, info);
                var res = (AddFriendResult)BitConverter.ToInt32(bRes,0);
                if (res == AddFriendResult.FriendNotExist)
                {
                    MessageBoxEx.Show("帐号不存在！");
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show("添加好友失败！" + ee.Message);
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }      
         
    }
}
