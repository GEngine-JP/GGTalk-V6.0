using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GGTalk
{
    /// <summary>
    /// 编辑好友分组名称的窗口。
    /// </summary>
    public partial class EditCatelogNameForm : BaseForm
    {
        private bool isNew = true;
        private string oldName;
        public EditCatelogNameForm(string _oldName)
        {
            InitializeComponent();
            this.isNew = false;
            this.oldName = _oldName;
            this.skinTextBox1.SkinTxt.Text = oldName;
            this.skinTextBox1.Focus();
        }

        public EditCatelogNameForm() : this("")
        {
        }

        private string newName;
        public string NewName
        {
            get
            {
                return this.newName;
            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            this.newName = this.skinTextBox1.SkinTxt.Text.Trim();
            if (string.IsNullOrEmpty(this.newName))
            {
                MessageBox.Show("名称不能为空！");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            
            if (this.newName == this.oldName)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                return;
            }

           

            if (this.newName.Contains(":") || this.newName.Contains(";"))
            {
                MessageBox.Show("名称中不能包含特殊字符！");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
