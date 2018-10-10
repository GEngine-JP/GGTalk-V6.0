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
using CCWin.SkinControl;
using JustLib;

namespace GGTalk
{
    /// <summary>
    /// 查找用户 窗口。
    /// </summary>
    internal partial class SearchFriendForm : BaseForm
    {
        private MainForm mainForm;       
        private GGUser currentUser;

        public SearchFriendForm(MainForm form, GGUser mine)
        {
            InitializeComponent();
            this.mainForm = form;                    
            this.currentUser = mine;
        }   

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.chatListBox.Items.Clear();
            List<GGUser> users = GlobalResourceManager.RemotingService.SearchUser(this.skinTextBox_id.SkinTxt.Text.Trim());
            bool hasResult = users.Count > 0;
            this.skinLabel_noResult.Visible = !hasResult;
            if (hasResult)
            {
                this.chatListBox.Items.Add(new ChatListItem("查找结果"));
                this.chatListBox.Items[0].IsOpen = true;
                foreach (GGUser user in users)
                {                   
                    Image headImage = this.mainForm.GetHeadImage(user);
                    ChatListSubItem subItem = new ChatListSubItem(user.ID, user.ID, user.Name, user.Signature, ChatListSubItem.UserStatus.Online, headImage);
                    subItem.Tag = user;
                    this.chatListBox.Items[0].SubItems.Add(subItem);
                }
            }
        }

        private void chatListBox_DoubleClickSubItem(object sender, ChatListEventArgs e)
        {
            string userID = e.SelectSubItem.ID;
            if (userID == this.currentUser.ID)
            {
                return;
            }

            if (this.mainForm.IsFriend(userID))
            {
                ChatForm form = (ChatForm)this.mainForm.GetChatForm(userID);
                form.Show();
            }
            else
            {
                this.mainForm.AddFriend(userID);
            }
        }         
    }
}
