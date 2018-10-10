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
    /// 加入群/组 窗口。
    /// </summary>
    internal partial class JoinGroupForm : BaseForm
    {       
        private IRapidPassiveEngine rapidPassiveEngine;
        private IChatSupporter ggSupporter;     

        public JoinGroupForm(IRapidPassiveEngine engine, IChatSupporter supporter)
        {
            InitializeComponent();
            this.rapidPassiveEngine = engine;
            this.ggSupporter = supporter;

            int registerPort = int.Parse(ConfigurationManager.AppSettings["RemotingPort"]);           
        }

        #region GroupID
        private string groupID = null;
        public string GroupID
        {
            get
            {
                return this.groupID;
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
            this.groupID = this.skinTextBox_id.SkinTxt.Text.Trim();
            if (groupID.Length == 0)
            {
                MessageBoxEx.Show("群帐号不能为空！");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }           

            try
            {
                if (this.ggSupporter.IsInGroup(this.groupID))
                {
                    MessageBoxEx.Show("已经是该群的成员了！");
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }

                byte[] bRes = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.JoinGroup, System.Text.Encoding.UTF8.GetBytes(groupID));
                JoinGroupResult res = (JoinGroupResult)BitConverter.ToInt32(bRes, 0);
                if (res == JoinGroupResult.GroupNotExist)
                {
                    MessageBoxEx.Show("群不存在！");
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show("加入群失败！" + ee.Message);
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }         
    }
}
