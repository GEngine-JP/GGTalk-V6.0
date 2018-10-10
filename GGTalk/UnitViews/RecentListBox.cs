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
    public partial class RecentListBox : UserControl
    {
        public event CbGeneric<string,bool> UnitDoubleClicked;
        public event CbGeneric<string, bool> ChatRecordClicked;

        public RecentListBox()
        {
            InitializeComponent();
        }

        private IHeadImageGetter resourceGetter;
        public void Initialize(IHeadImageGetter getter)
        {
            this.resourceGetter = getter;
        }

        public void Clear()
        {
            this.chatListBox.Items[0].SubItems.Clear();
        }

        public void AddRecentUnit(IUnit unit, int insertIndex)
        {
            string recentID = RecentListBox.ConstructRecentID(unit);
            UserStatus status = unit.IsGroup ? UserStatus.Online : ((GGUser)unit).UserStatus;
            Image img = unit.IsGroup ? this.imageList1.Images[0] : this.resourceGetter.GetHeadImage((GGUser)unit);
            ChatListSubItem subItem = new ChatListSubItem(recentID, "", unit.Name, "", this.ConvertUserStatus(status), img);
            subItem.Tag = unit;
            subItem.LastWords = unit.LastWords;
            this.chatListBox.Items[0].SubItems.Insert(insertIndex, subItem);
            this.chatListBox.Invalidate();
        }

        public void LastWordChanged(IUnit unit)
        {
            string recentID = RecentListBox.ConstructRecentID(unit);
            ChatListSubItem[] items = this.chatListBox.GetSubItemsById(recentID);
            if (items != null && items.Length > 0)
            {
                ChatListSubItem item = items[0];
                item.LastWords = unit.LastWords;
                item.OwnerListItem.SubItems.Remove(item);
                item.OwnerListItem.SubItems.Insert(0, item);
            }
            else
            {
                this.AddRecentUnit(unit, 0);
            }
            this.chatListBox.Invalidate();
        }

        public void RemoveUser(string userID)
        {
            string recentID = RecentListBox.ConstructRecentID4User(userID);
            this.chatListBox.RemoveSubItemsById(recentID);
            this.chatListBox.Invalidate();
        }

        public void RemoveUnit(IUnit unit)
        {
            string recentID = RecentListBox.ConstructRecentID(unit);
            this.chatListBox.RemoveSubItemsById(recentID);
            this.chatListBox.Invalidate();
        }

        public void UserStatusChanged(GGUser user)
        {
            string recentID = RecentListBox.ConstructRecentID(user);
            ChatListSubItem[] items = this.chatListBox.GetSubItemsById(recentID);
            if (items == null || items.Length == 0)
            {
                return;
            }

            items[0].DisplayName = user.Name;
            items[0].HeadImage = this.resourceGetter.GetHeadImage(user);
            items[0].Status = this.ConvertUserStatus(user.UserStatus);           
            this.chatListBox.Invalidate();
        }

        public List<string> GetRecentUserList(int maxCount)
        {
            List<string> recentList = new List<string>();
            int count = 0;
            foreach (ChatListSubItem item in this.chatListBox.Items[0].SubItems)
            {
                recentList.Add(item.ID);
                ++count;
                if (count >= maxCount)
                {
                    break;
                }
            }
            return recentList;
        }

        public void SetTwinkleState(string id, bool isGroup, bool twinkle)
        {
            string recentID = isGroup ? RecentListBox.ConstructRecentID4Group(id) : RecentListBox.ConstructRecentID4User(id);
            ChatListSubItem[] items = this.chatListBox.GetSubItemsById(recentID);
            if (items == null || items.Length == 0)
            {
                return;
            }
            items[0].IsTwinkle = twinkle;
        }

        public void SetAllUserOffline()
        {
            foreach (ChatListItem item in this.chatListBox.Items)
            {
                foreach (ChatListSubItem sub in item.SubItems)
                {
                    IUnit unit = (IUnit)sub.Tag;
                    if (!unit.IsGroup)
                    {
                        sub.Status = ChatListSubItem.UserStatus.OffLine;
                    }
                }
            }

            this.chatListBox.Invalidate();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ChatListSubItem item = this.chatListBox.SelectSubItem;          
            item.IsTwinkle = false;

            Parameter<string, bool> para = RecentListBox.ParseIDFromRecentID(item.ID);
            if (this.UnitDoubleClicked != null)
            {
                this.UnitDoubleClicked(para.Arg1, para.Arg2);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ChatListSubItem item = this.chatListBox.SelectSubItem;
            Parameter<string, bool> para = RecentListBox.ParseIDFromRecentID(item.ID);

            if (this.ChatRecordClicked != null)
            {
                this.ChatRecordClicked(para.Arg1, para.Arg2);
            }
        }

        private void 从列表中移除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chatListBox.Items[0].SubItems.Remove(this.chatListBox.SelectSubItem);
        }

        private void chatListBox_DoubleClickSubItem(object sender, ChatListEventArgs e)
        {
            Parameter<string, bool> para = RecentListBox.ParseIDFromRecentID(e.SelectSubItem.ID);

            if (this.UnitDoubleClicked != null)
            {
                this.UnitDoubleClicked(para.Arg1, para.Arg2);
            }
        }

        #region RecentID
        public static string ConstructRecentID(IUnit unit)
        {
            return (unit.IsGroup ? "G_" : "U_") + unit.ID;
        }

        public static string ConstructRecentID4User(string userID)
        {
            return "U_" + userID;
        }

        public static string ConstructRecentID4Group(string groupID)
        {
            return "G_" + groupID;
        }

        public static Parameter<string, bool> ParseIDFromRecentID(string recentID)
        {
            string id = recentID.Substring(2);
            bool isGroup = recentID.StartsWith("G_");
            return new Parameter<string, bool>(id, isGroup);
        }
        #endregion

        #region ConvertUserStatus
        private ChatListSubItem.UserStatus ConvertUserStatus(UserStatus status)
        {
            if (status == UserStatus.Hide)
            {
                return ChatListSubItem.UserStatus.OffLine;
            }

            return (ChatListSubItem.UserStatus)((int)status);
        }
        #endregion
    }    
}
