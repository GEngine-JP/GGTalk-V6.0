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
using ESPlus.Serialization;

namespace GGTalk
{
    /// <summary>
    /// 创建群/组的窗口。
    /// </summary>
    public partial class CreateGroupForm : BaseForm
    {       
        private IRapidPassiveEngine rapidPassiveEngine;

        public CreateGroupForm(IRapidPassiveEngine engine)
        {
            InitializeComponent();
            this.rapidPassiveEngine = engine;
        }

        #region Group
        private GGGroup group = null;
        public GGGroup Group
        {
            get
            {
                return this.group;
            }
        } 
        #endregion        

        private void skinButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            string groupID = this.skinTextBox_id.SkinTxt.Text.Trim();
            if (groupID.Length == 0)
            {
                MessageBoxEx.Show("群帐号不能为空！");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            try
            {
                CreateGroupContract contract = new CreateGroupContract(groupID, this.skinTextBox_name.SkinTxt.Text.Trim() ,this.skinTextBox_announce.SkinTxt.Text);
                byte[] bRes = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.CreateGroup, CompactPropertySerializer.Default.Serialize(contract));
                CreateGroupResult res = (CreateGroupResult)BitConverter.ToInt32(bRes, 0);
                if (res == CreateGroupResult.GroupExisted)
                {
                    MessageBoxEx.Show("同ID的群已经存在！");
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
               
                this.group = new GGGroup(groupID, contract.Name,this.rapidPassiveEngine.CurrentUserID,"",this.rapidPassiveEngine.CurrentUserID);               
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show("创建群失败！" + ee.Message);
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }      
         
    }
}
