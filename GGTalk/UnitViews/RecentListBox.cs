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
            var recentID = RecentListBox.ConstructRecentID(unit);
            var status = unit.IsGroup ? UserStatus.Online : ((GGUser)unit).UserStatus;
            var img = unit.IsGroup ? this.imageList1.Images[0] : this.resourceGetter.GetHeadImage((GGUser)unit);
            var subItem = new ChatListSubItem(recentID, "", unit.Name, "", this.ConvertUserStatus(status), img);
            subItem.Tag = unit;
            subItem.LastWords = unit.LastWords;
            this.chatListBox.Items[0].SubItems.Insert(insertIndex, subItem);
            this.chatListBox.Invalidate();
        }

        public void LastWordChanged(IUnit unit)
        {
            var recentID = RecentListBox.ConstructRecentID(unit);
            var items = this.chatListBox.GetSubItemsById(recentID);
            if (items != null && items.Length > 0)
            {
                var item = items[0];
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
            var recentID = RecentListBox.ConstructRecentID4User(userID);
            this.chatListBox.RemoveSubItemsById(recentID);
            this.chatListBox.Invalidate();
        }

        public void RemoveUnit(IUnit unit)
        {
            var recentID = RecentListBox.ConstructRecentID(unit);
            this.chatListBox.RemoveSubItemsById(recentID);
            this.chatListBox.Invalidate();
        }

        public void UserStatusChanged(GGUser user)
        {
            var recentID = RecentListBox.ConstructRecentID(user);
            var items = this.chatListBox.GetSubItemsById(recentID);
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
            var recentList = new List<string>();
            var count = 0;
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
            var recentID = isGroup ? RecentListBox.ConstructRecentID4Group(id) : RecentListBox.ConstructRecentID4User(id);
            var items = this.chatListBox.GetSubItemsById(recentID);
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
                    var unit = (IUnit)sub.Tag;
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
            var item = this.chatListBox.SelectSubItem;          
            item.IsTwinkle = false;

            var para = RecentListBox.ParseIDFromRecentID(item.ID);
            if (this.UnitDoubleClicked != null)
            {
                this.UnitDoubleClicked(para.Arg1, para.Arg2);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            var item = this.chatListBox.SelectSubItem;
            var para = RecentListBox.ParseIDFromRecentID(item.ID);

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
            var para = RecentListBox.ParseIDFromRecentID(e.SelectSubItem.ID);

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
            var id = recentID.Substring(2);
            var isGroup = recentID.StartsWith("G_");
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
