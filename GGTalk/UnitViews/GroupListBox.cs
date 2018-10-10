using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GGTalk.UnitViews;
using CCWin.SkinControl;
using ESBasic;
using JustLib;

namespace GGTalk.UnitViews
{
    public partial class GroupListBox : UserControl
    {
        private GGUser currentUser;
        private string companyGroupID;

        public event CbGeneric<IGroup> GroupDoubleClicked;
        public event CbGeneric<IGroup> ChatRecordClicked;
        public event CbGeneric<IGroup> DismissGroupClicked;
        public event CbGeneric<IGroup> QuitGroupClicked;

        public GroupListBox()
        {
            InitializeComponent();
        }

        public void Initialize(GGUser current, string _companyGroupID)
        {
            this.currentUser = current;
            this.companyGroupID = _companyGroupID;
        } 

        public void AddGroup(IGroup group)
        {
            ChatListSubItem subItem = new ChatListSubItem(group.ID, "", group.Name, string.Format("{0}人", group.MemberList.Count), ChatListSubItem.UserStatus.Online, this.imageList1.Images[0]);
            subItem.Tag = group;
#if !Org
            this.chatListBox_group.Items[0].SubItems.Add(subItem);
#else
            if (group.ID == this.companyGroupID)
            {
                this.chatListBox_group.Items[0].SubItems.Insert(0, subItem);
            }           
            else
            {
                this.chatListBox_group.Items[0].SubItems.Add(subItem);
            }
#endif
        }

        public void RemoveGroup(string groupID)
        {
            this.chatListBox_group.RemoveSubItemsById(groupID);
            this.chatListBox_group.Invalidate();
        }

        public void SetTwinkleState(string groupID, bool twinkle)
        {
            ChatListSubItem[] items = this.chatListBox_group.GetSubItemsById(groupID);
            if (items == null || items.Length == 0)
            {
                return;
            }
            items[0].IsTwinkle = twinkle;
        }

        public void GroupInfoChanged(IGroup group, GroupChangedType type, string userID)
        {
            ChatListSubItem[] subItems = this.chatListBox_group.GetSubItemsById(group.ID);
            if (type == GroupChangedType.GroupDeleted)
            {
                if (subItems == null || subItems.Length == 0)
                {
                    return;
                }

                this.chatListBox_group.Items[0].SubItems.Remove(subItems[0]);               
                return;
            }

            if (subItems == null || subItems.Length == 0)
            {
                ChatListSubItem subItem = new ChatListSubItem(group.ID, "", group.Name, string.Format("{0}人", group.MemberList.Count), ChatListSubItem.UserStatus.Online, this.imageList1.Images[0]);
                subItem.Tag = group;
#if !Org
                this.chatListBox_group.Items[0].SubItems.Add(subItem);
#else
                if (group.ID == this.companyGroupID)
                {
                    this.chatListBox_group.Items[0].SubItems.Insert(0, subItem);
                }                
                else
                {
                    this.chatListBox_group.Items[0].SubItems.Add(subItem);
                }
#endif

                return;
            }
            else
            {
                subItems[0].Tag = group;
                subItems[0].PersonalMsg = string.Format("{0}人", group.MemberList.Count);               
            }
        }

        private void 消息记录ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.currentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            IGroup group = (IGroup)this.chatListBox_group.SelectSubItem.Tag;
            if (this.ChatRecordClicked != null)
            {
                this.ChatRecordClicked(group);
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.currentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            IGroup group = (IGroup)this.chatListBox_group.SelectSubItem.Tag;
            if (this.QuitGroupClicked != null)
            {
                this.QuitGroupClicked(group);
            }
        }

        private void 解散该群ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            IGroup group = (IGroup)this.chatListBox_group.SelectSubItem.Tag;
            if (this.DismissGroupClicked != null)
            {
                this.DismissGroupClicked(group);
            }
        }

        private void chatListBox_group_DoubleClickSubItem(object sender, ChatListEventArgs e)
        {
            ChatListSubItem item = e.SelectSubItem;
            IGroup group = (IGroup)item.Tag;
            item.IsTwinkle = false;

            if (this.GroupDoubleClicked != null)
            {
                this.GroupDoubleClicked(group);
            }
        }
    }
}
