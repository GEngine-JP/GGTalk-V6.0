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
    /// 用于发送系统通知的窗口。
    /// </summary>
    internal partial class SystemNotifySendForm : BaseForm
    {      
        private IRapidPassiveEngine rapidPassiveEngine;

        public SystemNotifySendForm(IRapidPassiveEngine engine)
        {
            InitializeComponent();
            this.rapidPassiveEngine = engine;
            this.Icon = GlobalResourceManager.Icon64;           
        }               
       
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                SystemNotifyContract contract = new SystemNotifyContract(this.skinTextBox_id.SkinTxt.Text, this.richTextBox1.Text, this.rapidPassiveEngine.CurrentUserID, this.skinTextBox_groupID.SkinTxt.Text);
                byte[] data = CompactPropertySerializer.Default.Serialize(contract);
                int infoType = this.skinRadioButton_group.Checked ? InformationTypes.SystemNotify4Group : InformationTypes.SystemNotify4AllOnline;
                this.rapidPassiveEngine.CustomizeOutter.Send(infoType, data);
                MessageBox.Show("发送成功！");
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("发送失败！" + ee.Message);
            }
        }

        private void SystemNotifySendForm_Load(object sender, EventArgs e)
        {

        }

        private void skinRadioButton_group_CheckedChanged(object sender, EventArgs e)
        {
            this.skinLabel_gID.Visible = this.skinRadioButton_group.Checked;
            this.skinTextBox_groupID.Visible = this.skinRadioButton_group.Checked;
        }      
         
    }
}
