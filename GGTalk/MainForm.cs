using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using CCWin;
using CCWin.SkinControl;
using System.Runtime.InteropServices;
using CCWin.Win32;
using ESPlus.Application.CustomizeInfo;
using ESBasic;
using ESPlus.Rapid;
using ESBasic.ObjectManagement.Forms;
using ESPlus.Application.FileTransfering;
using ESPlus.FileTransceiver;
using ESPlus.Application.Basic;
using OMCS.Passive;
using System.Configuration;
using ESBasic.ObjectManagement.Managers;
using ESPlus.Serialization;
using GGTalk.Core;
using ESBasic.Helpers;
using JustLib;
using GGTalk.UnitViews;
using JustLib.NetworkDisk.Passive;
using JustLib.NetworkDisk;

namespace GGTalk
{
    /// <summary>
    /// 客户端主窗口。
    /// </summary>
    public partial class MainForm : BaseForm, IChatSupporter, ITwinkleNotifySupporter, IHeadImageGetter
    {
        private bool initialized = false;       
        private UserStatus myStatus = UserStatus.OffLine; //用于断线重连       
        private IRapidPassiveEngine rapidPassiveEngine;
        private GlobalUserCache globalUserCache; //缓存用户资料
        private FormManager<string, ChatForm> chatFormManager = new FormManager<string, ChatForm>();
        private FormManager<string, GroupChatForm> groupChatFormManager = new FormManager<string, GroupChatForm>();

        private INDiskOutter nDiskOutter; // V2.0    

        #region Ctor 
        public MainForm()
        {
            InitializeComponent();
            this.autoDocker1.Initialize(this);

            this.toolStripSplitButton_Friends.Visible = true;
            this.添加好友toolStripMenuItem.Visible = true;
            this.查找好友ToolStripMenuItem.Visible = true;
            this.头像显示ToolStripMenuItem.Visible = true;

            UiSafeInvoker invoker = new UiSafeInvoker(this, true,true, GlobalResourceManager.Logger);
            GlobalResourceManager.SetUiSafeInvoker(invoker);

            this.toolstripButton_mainMenu.Image = GlobalResourceManager.Png64;

            this.friendListBox1.AddCatalogClicked += new CbGeneric(friendListBox1_AddCatalogClicked);
            this.friendListBox1.ChangeCatalogNameClicked += new CbGeneric<string>(friendListBox1_ChangeCatalogNameClicked);
            this.friendListBox1.UserDoubleClicked +=new CbGeneric<GGUser>(friendListBox1_UserDoubleClicked);
            this.friendListBox1.RemoveUserClicked += new CbGeneric<GGUser>(friendListBox1_RemoveUserClicked);
            this.friendListBox1.ChatRecordClicked += new CbGeneric<GGUser>(friendListBox1_ChatRecordClicked);
            this.friendListBox1.CatalogAdded += new CbGeneric<string>(friendListBox1_CatalogAdded);
            this.friendListBox1.CatalogNameChanged += new CbGeneric<string, string, bool>(friendListBox1_CatalogNameChanged);
            this.friendListBox1.CatalogRemoved += new CbGeneric<string>(friendListBox1_CatalogRemoved);
            this.friendListBox1.FriendCatalogMoved += new CbGeneric<string, string ,string >(friendListBox1_FriendCatalogMoved);
            this.recentListBox1.UnitDoubleClicked += new CbGeneric<string, bool>(recentListBox1_UserDoubleClicked);
            this.recentListBox1.ChatRecordClicked += new CbGeneric<string, bool>(recentListBox1_ChatRecordClicked);            
            this.groupListBox.GroupDoubleClicked += new CbGeneric<IGroup>(groupListBox_GroupDoubleClicked);
            this.groupListBox.DismissGroupClicked += new CbGeneric<IGroup>(groupListBox_DismissGroupClicked);
            this.groupListBox.QuitGroupClicked += new CbGeneric<IGroup>(groupListBox_QuitGroupClicked);
            this.groupListBox.ChatRecordClicked += new CbGeneric<IGroup>(groupListBox_ChatRecordClicked);           
        }

        void friendListBox1_AddCatalogClicked()
        {
            EditCatelogNameForm form = new EditCatelogNameForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.friendListBox1.AddCatalog(form.NewName);
            }
        }

        void friendListBox1_ChangeCatalogNameClicked(string catalogName)
        {
            EditCatelogNameForm form = new EditCatelogNameForm(catalogName);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.friendListBox1.ChangeCatelogName(catalogName, form.NewName);
            }
        }


        void groupListBox_ChatRecordClicked(IGroup group)
        {
            ChatRecordForm form = new ChatRecordForm(GlobalResourceManager.RemotingService, GlobalResourceManager.ChatMessageRecordPersister, group.GetIDName(), this.globalUserCache.CurrentUser.GetIDName(), this.globalUserCache);
            form.Show();
        }

        void groupListBox_QuitGroupClicked(IGroup group)
        {
            try
            {  
                if (!ESBasic.Helpers.WindowsHelper.ShowQuery(string.Format("您确定要退出群{0}({1})吗？", group.ID, group.Name)))
                {
                    return;
                }

                //SendCertainly 发送请求，并等待Ack回复
                this.rapidPassiveEngine.CustomizeOutter.SendCertainly(null, InformationTypes.QuitGroup, System.Text.Encoding.UTF8.GetBytes(group.ID));
                this.groupListBox.RemoveGroup(group.ID);
                this.recentListBox1.RemoveUnit(group);
                GroupChatForm form = this.groupChatFormManager.GetForm(group.ID);
                if (form != null)
                {
                    form.Close();
                }
                this.globalUserCache.RemoveGroup(group.ID);
                this.globalUserCache.CurrentUser.QuitGroup(group.ID);
                MessageBoxEx.Show(string.Format("您已经退出群{0}({1})。", group.ID, group.Name));
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show("请求超时！" + ee.Message, GlobalResourceManager.SoftwareName);
            }
        }

        void groupListBox_DismissGroupClicked(IGroup group)
        {   
            try
            {
                if (group.CreatorID != this.globalUserCache.CurrentUser.UserID)
                {
                    MessageBoxEx.Show("只有群的创始人才能解散群。");
                    return;
                }

                if (!ESBasic.Helpers.WindowsHelper.ShowQuery(string.Format("您确定要解散群{0}({1})吗？", group.ID, group.Name)))
                {
                    return;
                }

                this.rapidPassiveEngine.CustomizeOutter.SendCertainly(null, InformationTypes.DeleteGroup, System.Text.Encoding.UTF8.GetBytes(group.ID));
                this.groupListBox.RemoveGroup(group.ID) ;
                this.recentListBox1.RemoveUnit(group);
                GroupChatForm form = this.groupChatFormManager.GetForm(group.ID);
                if (form != null)
                {
                    form.Close();
                }
                this.globalUserCache.RemoveGroup(group.ID);
                this.globalUserCache.CurrentUser.QuitGroup(group.ID);
                MessageBoxEx.Show(string.Format("您已经解散群{0}({1})。", group.ID, group.Name));               
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show("请求超时！" + ee.Message, GlobalResourceManager.SoftwareName);
            }
        }

        void groupListBox_GroupDoubleClicked(IGroup group)
        {
            try
            {               
                GroupChatForm form = this.GetGroupChatForm(group.ID);
                form.Show();
                form.Focus();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        void recentListBox1_ChatRecordClicked(string unitID ,bool isGroup)
        {
            ChatRecordForm form = null;
            if (isGroup)
            {
                GGGroup group = this.globalUserCache.GetGroup(unitID);
                form = new ChatRecordForm(GlobalResourceManager.RemotingService, GlobalResourceManager.ChatMessageRecordPersister, group.GetIDName(), this.globalUserCache.CurrentUser.GetIDName(), this.globalUserCache);
                form.Show();
            }
            else
            {
                GGUser friend = this.globalUserCache.GetUser(unitID);
                form = new ChatRecordForm(GlobalResourceManager.RemotingService, GlobalResourceManager.ChatMessageRecordPersister, this.globalUserCache.CurrentUser.GetIDName(), friend.GetIDName());
            }
            form.Show();
        }

        void recentListBox1_UserDoubleClicked(string unitID, bool isGroup)
        {
            Form form = null;
            if (isGroup)
            {
                form = this.GetGroupChatForm(unitID);
            }
            else
            {
                form = this.GetChatForm(unitID);
            }
            form.Show();
            form.Focus();
        }

        void friendListBox1_FriendCatalogMoved(string friendID, string oldCatalog, string newCatalog)
        {
            this.globalUserCache.CurrentUser.MoveFriend(friendID, oldCatalog, newCatalog);
            MoveFriendToOtherCatalogContract contract = new MoveFriendToOtherCatalogContract(friendID, oldCatalog, newCatalog);
            byte[] info = CompactPropertySerializer.Default.Serialize(contract);
            this.rapidPassiveEngine.CustomizeOutter.Send(InformationTypes.MoveFriendToOtherCatalog, info);
        }

        void friendListBox1_CatalogRemoved(string catalog)
        {
            this.globalUserCache.CurrentUser.RemvoeFriendCatalog(catalog);
            this.rapidPassiveEngine.CustomizeOutter.Send(InformationTypes.RemoveFriendCatalog, System.Text.Encoding.UTF8.GetBytes(catalog));
        }

        void friendListBox1_CatalogNameChanged(string oldName, string newName, bool isMerge)
        {
            this.globalUserCache.CurrentUser.ChangeFriendCatalogName(oldName, newName);

            ChangeCatalogContract contract = new ChangeCatalogContract(oldName, newName);
            byte[] info = CompactPropertySerializer.Default.Serialize(contract);            
            this.rapidPassiveEngine.CustomizeOutter.Send(InformationTypes.ChangeFriendCatalogName, info);
        }

        void friendListBox1_CatalogAdded(string catalog)
        {
            this.globalUserCache.CurrentUser.AddFriendCatalog(catalog);
            this.rapidPassiveEngine.CustomizeOutter.Send(InformationTypes.AddFriendCatalog, System.Text.Encoding.UTF8.GetBytes(catalog));
        }

        void friendListBox1_ChatRecordClicked(GGUser friend)
        {
            ChatRecordForm form = new ChatRecordForm(GlobalResourceManager.RemotingService, GlobalResourceManager.ChatMessageRecordPersister, this.globalUserCache.CurrentUser.GetIDName(), friend.GetIDName());
            form.Show();
        }

        void friendListBox1_UserDoubleClicked(GGUser friend)
        {
            Form form = this.GetChatForm(friend.ID);
            form.Show();
            form.Focus();
        }

        void friendListBox1_RemoveUserClicked(GGUser friend)
        {
            if (this.globalUserCache.CurrentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            try
            {
                if (friend.ID == this.rapidPassiveEngine.CurrentUserID)
                {
                    return;
                }

                if (!ESBasic.Helpers.WindowsHelper.ShowQuery(string.Format("您确定要删除好友 {0}({1}) 吗？", friend.Name, friend.ID)))
                {
                    return;
                }

                //SendCertainly 发送请求，并等待Ack回复
                this.rapidPassiveEngine.CustomizeOutter.SendCertainly(null, InformationTypes.RemoveFriend, System.Text.Encoding.UTF8.GetBytes(friend.ID));
                this.globalUserCache.CurrentUser.RemoveFriend(friend.ID);
                this.friendListBox1.RemoveUser(friend.ID);

                // 从recent中删除
                this.recentListBox1.RemoveUnit(friend);
                ChatForm chatForm = this.chatFormManager.GetForm(friend.ID);
                if (chatForm != null)
                {
                    chatForm.Close();
                }

                this.globalUserCache.RemovedFriend(friend.ID);
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show("请求超时！" + ee.Message, GlobalResourceManager.SoftwareName);
            } 
        }
        #endregion

        #region Initialize
        /// <summary>
        /// 初始化流程：
        /// （1）Initialize时，从服务器加载自己的全部信息，从本地缓存文件加载好友列表。
        /// （2）MainForm_Load，填充ChatListBox
        /// （3）MainForm_Shown，调用globalUserCache在后台刷新：若是第一次登录，则分批从服务器加载好友资料。否则，从服务器获取好友最新状态和版本，并刷新本地缓存。
        /// （4）globalUserCache.FriendRTDataRefreshCompleted 事件，请求离线消息、离线文件、正式通知好友自己上线
        /// </summary>
        public void Initialize(IRapidPassiveEngine engine, ChatListSubItem.UserStatus userStatus, Image stateImage)//, Image headImage, string nickName, ChatListSubItem.UserStatus userStatus, Image stateImage)
        {
            GlobalResourceManager.PostInitialize(engine.CurrentUserID);            
            this.Cursor = Cursors.WaitCursor;

            this.toolTip1.SetToolTip(this.skinButton_headImage,"帐号：" + engine.CurrentUserID);
            this.rapidPassiveEngine = engine;
          
            this.globalUserCache = new GlobalUserCache(this.rapidPassiveEngine);
            this.globalUserCache.FriendInfoChanged += new CbGeneric<GGUser>(globalUserCache_FriendInfoChanged);
            this.globalUserCache.FriendStatusChanged += new CbGeneric<GGUser>(globalUserCache_FriendStatusChanged);
            this.globalUserCache.GroupChanged += new CbGeneric<GGGroup ,GroupChangedType, string>(globalUserCache_GroupInfoChanged);
            this.globalUserCache.FriendRTDataRefreshCompleted += new CbGeneric(globalUserCache_FriendRTDataRefreshCompleted);
            this.globalUserCache.FriendRemoved += new CbGeneric<string>(globalUserCache_FriendRemoved);
            this.globalUserCache.FriendAdded += new CbGeneric<GGUser>(globalUserCache_FriendAdded);

            this.globalUserCache.CurrentUser.UserStatus = (UserStatus)((int)userStatus);
            this.myStatus = this.globalUserCache.CurrentUser.UserStatus;
            this.labelSignature.Text = this.globalUserCache.CurrentUser.Signature;
            this.skinButton_headImage.Image = GlobalResourceManager.GetHeadImage(this.globalUserCache.CurrentUser);
            this.labelName.Text = this.globalUserCache.CurrentUser.Name;           

            skinButton_State.Image = stateImage;
            skinButton_State.Tag = userStatus;
            this.skinLabel_softName.Text = GlobalResourceManager.SoftwareName;
            this.notifyIcon.ChangeText(String.Format("{0}：{1}（{2}）\n状态：{3}", GlobalResourceManager.SoftwareName, this.globalUserCache.CurrentUser.Name, this.globalUserCache.CurrentUser.UserID, GlobalResourceManager.GetUserStatusName(this.globalUserCache.CurrentUser.UserStatus)));

            this.MaximumSize = new Size(543, Screen.GetWorkingArea(this).Height);
            this.Size = SystemSettings.Singleton.MainFormSize;
            this.Location = SystemSettings.Singleton.MainFormLocation;//new Point(Screen.PrimaryScreen.Bounds.Width - this.Width - 20, 40);

            this.friendListBox1.Initialize(this.globalUserCache.CurrentUser, this, new UserInformationForm(new Point(this.Location.X-284 ,this.friendListBox1.Location.Y)));
            this.groupListBox.Initialize(this.globalUserCache.CurrentUser ,GlobalConsts.CompanyGroupID);
            this.recentListBox1.Initialize(this);            

            if (!SystemSettings.Singleton.ShowLargeIcon)
            {
                this.friendListBox1.IconSizeMode = ChatListItemIcon.Small;
                this.大头像ToolStripMenuItem.Checked = false;
                this.小头像ToolStripMenuItem.Checked = true;
            }

            //文件传送
            this.rapidPassiveEngine.FileOutter.FileRequestReceived += new CbFileRequestReceived(fileOutter_FileRequestReceived);
            this.rapidPassiveEngine.FileOutter.FileResponseReceived += new CbGeneric<TransferingProject, bool>(fileOutter_FileResponseReceived);
            
            this.rapidPassiveEngine.ConnectionInterrupted += new CbGeneric(rapidPassiveEngine_ConnectionInterrupted);//预订断线事件
            this.rapidPassiveEngine.BasicOutter.BeingPushedOut += new CbGeneric(BasicOutter_BeingPushedOut);
            this.rapidPassiveEngine.RelogonCompleted += new CbGeneric<LogonResponse>(rapidPassiveEngine_RelogonCompleted);//预订重连成功事件  
            this.rapidPassiveEngine.MessageReceived += new CbGeneric<string, int, byte[], string>(rapidPassiveEngine_MessageReceived);

            //群、组
            this.rapidPassiveEngine.ContactsOutter.BroadcastReceived += new CbGeneric<string, string, int, byte[] ,string>(ContactsOutter_BroadcastReceived);
            this.rapidPassiveEngine.ContactsOutter.ContactsOffline += new CbGeneric<string>(ContactsOutter_ContactsOffline); //所有联系人的下线事件

            //网盘访问器 V2.0
            this.nDiskOutter = new NDiskOutter(this.rapidPassiveEngine.FileOutter, this.rapidPassiveEngine.CustomizeOutter);              
           
            this.notifyIcon.UnhandleMessageOccured += new CbGeneric<UnhandleMessageType ,string>(notifyIcon_UnhandleMessageOccured);
            this.notifyIcon.UnhandleMessageGone += new CbGeneric<UnhandleMessageType ,string>(notifyIcon_UnhandleMessageGone);
            this.notifyIcon.Initialize(this ,this);
        }

        void notifyIcon_UnhandleMessageGone(UnhandleMessageType type, string friendOrGroupID)
        {
            if (type == UnhandleMessageType.Friend)
            {
                this.friendListBox1.SetTwinkleState(friendOrGroupID, false); 
                this.recentListBox1.SetTwinkleState(friendOrGroupID, false, false);
                return;
            }

            if (type == UnhandleMessageType.Group)
            {
                this.groupListBox.SetTwinkleState(friendOrGroupID, false);
                this.recentListBox1.SetTwinkleState(friendOrGroupID, true, false);
                return;
            }
        }

        void notifyIcon_UnhandleMessageOccured(UnhandleMessageType type, string friendOrGroupID)
        {
            if (type == UnhandleMessageType.Friend)
            {
                this.friendListBox1.SetTwinkleState(friendOrGroupID, true); 
                this.recentListBox1.SetTwinkleState(friendOrGroupID, false, true);
                return;
            }

            if (type == UnhandleMessageType.Group)
            {
                this.groupListBox.SetTwinkleState(friendOrGroupID, true);
                this.recentListBox1.SetTwinkleState(friendOrGroupID, true, true);
                return;
            }
        }

        void BasicOutter_BeingPushedOut()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CbGeneric(this.BasicOutter_BeingPushedOut));
            }
            else
            {
                MessageBox.Show("已经在其它地方登录！",GlobalResourceManager.SoftwareName);
            }
            
        }

        #region globalUserCache_FriendAdded
        void globalUserCache_FriendAdded(GGUser friend)
        {
            this.friendListBox1.AddUser(friend); 
        } 
        #endregion

        #region globalUserCache_FriendRemoved
        /// <summary>
        /// 删除好友，或被别人从好友中删除
        /// </summary>       
        void globalUserCache_FriendRemoved(string friendID)
        {
            GlobalResourceManager.UiSafeInvoker.ActionOnUI<string>(this.do_globalUserCache_FriendRemoved, friendID);            
        }

        private void do_globalUserCache_FriendRemoved(string friendID)
        {
            this.friendListBox1.RemoveUser(friendID); 
            this.recentListBox1.RemoveUser(friendID);   

            ChatForm chatForm = this.chatFormManager.GetForm(friendID);
            if (chatForm != null)
            {
                chatForm.OnRemovedFromFriend();
            }
        }
        #endregion       

        #region globalUserCache_GroupInfoChanged
        void globalUserCache_GroupInfoChanged(GGGroup group, GroupChangedType type, string userID)
        {
            GlobalResourceManager.UiSafeInvoker.ActionOnUI<GGGroup, GroupChangedType, string>(this.do_globalUserCache_GroupInfoChanged, group, type, userID);
        }

        void do_globalUserCache_GroupInfoChanged(GGGroup group, GroupChangedType type, string userID)
        {
            this.groupListBox.GroupInfoChanged(group, type, userID);
            
            if (type == GroupChangedType.GroupDeleted)
            {
                GroupChatForm form = this.groupChatFormManager.GetForm(group.ID);
                if (form != null)
                {
                    form.Close();
                }

                if (userID != null) //为null 表示更改了自己的部门资料
                {
                    MessageBoxEx.Show(string.Format("群{0}({1})已经被解散。", group.ID, group.Name));
                }
                return;
            }

            GroupChatForm form2 = this.groupChatFormManager.GetForm(group.ID);
            if (form2 != null)
            {
                form2.OnGroupInfoChanged(type, userID);
            }
        }
        #endregion

        #region globalUserCache_FriendRTDataRefreshCompleted
        void globalUserCache_FriendRTDataRefreshCompleted()
        {
            GlobalResourceManager.UiSafeInvoker.ActionOnUI( this.do_globalUserCache_FriendRTDataRefreshCompleted);
        }

        void do_globalUserCache_FriendRTDataRefreshCompleted()
        {
            //请求离线消息 
            this.rapidPassiveEngine.CustomizeOutter.Send(InformationTypes.GetOfflineMessage, null);
            //请求离线文件
            this.rapidPassiveEngine.CustomizeOutter.Send(InformationTypes.GetOfflineFile, null);

            //正式通知好友，自己上线
            this.rapidPassiveEngine.CustomizeOutter.Send(InformationTypes.ChangeStatus, BitConverter.GetBytes((int)this.globalUserCache.CurrentUser.UserStatus));

            GGUser mine = this.globalUserCache.GetUser(this.rapidPassiveEngine.CurrentUserID);            
            this.InitializeFinished();
        }

        private void InitializeFinished()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CbGeneric(this.InitializeFinished));
            }
            else
            {
                this.friendListBox1.SortAllUser();
                this.Cursor = Cursors.Default;
            }
        } 
        #endregion

        #region globalUserCache_FriendStatusChanged
        void globalUserCache_FriendStatusChanged(GGUser friend)
        {
            GlobalResourceManager.UiSafeInvoker.ActionOnUI<GGUser>(this.do_globalUserCache_FriendStatusChanged, friend);               
        }

        private void do_globalUserCache_FriendStatusChanged(GGUser friend)
        {
            this.friendListBox1.UserStatusChanged(friend);
            this.recentListBox1.UserStatusChanged(friend);      

            ChatForm form = this.chatFormManager.GetForm(friend.UserID);
            if (form != null)
            {
                form.FriendStateChanged(friend.UserStatus);
            }

            foreach (GroupChatForm groupForm in this.groupChatFormManager.GetAllForms())
            {
                if (groupForm != null)
                {
                    groupForm.GroupmateStateChanged(friend.UserID, friend.UserStatus);
                    return;
                }
            }
        }
        #endregion

        #region globalUserCache_FriendInfoChanged
        void globalUserCache_FriendInfoChanged(GGUser user)
        {
            GlobalResourceManager.UiSafeInvoker.ActionOnUI<GGUser>(this.do_globalUserCache_FriendInfoChanged, user);          
        }

        void do_globalUserCache_FriendInfoChanged(GGUser user)
        {
            this.friendListBox1.UserInfoChanged(user);
            this.recentListBox1.UserStatusChanged(user);           

            ChatForm form = this.chatFormManager.GetForm(user.UserID);
            if (form != null)
            {
                form.OnFriendInfoChanged(user);
            }

            foreach (GroupChatForm groupForm in this.groupChatFormManager.GetAllForms())
            {
                groupForm.OnUserInfoChanged(user);
            }
        }
        #endregion
        #endregion

        #region MainForm_Load
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {                   
                    this.notifyIcon.Visible = true;
                }

                //我的好友
                foreach (string friendID in this.globalUserCache.CurrentUser.GetAllFriendList())
                {
                    if (friendID == this.globalUserCache.CurrentUser.UserID)
                    {
                        continue;
                    }

                    GGUser friend = this.globalUserCache.GetUser(friendID);
                    if (friend != null)
                    {
                        this.friendListBox1.AddUser(friend);                       
                    }
                }
                this.friendListBox1.SortAllUser();
                this.friendListBox1.ExpandRoot();

                foreach (GGGroup group in this.globalUserCache.GetAllGroups()) //初期不包含 固定群
                {
                    this.groupListBox.AddGroup(group);
                }

                //加载最近联系人
                int insertIndex = 0;
                foreach (string recentID in this.globalUserCache.GetRecentList())
                {
                    Parameter<string, bool> para = RecentListBox.ParseIDFromRecentID(recentID);
                    IUnit unit = this.globalUserCache.GetUnit(para.Arg1,para.Arg2);
                    if (unit == null)
                    {
                        continue;
                    }
                    this.recentListBox1.AddRecentUnit(unit, insertIndex);                                
                    ++insertIndex;
                }

                this.initialized = true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }           
        #endregion

        #region MainForm_Shown
        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.globalUserCache.StartRefreshFriendInfo();
            InformationForm frm = new InformationForm(this.rapidPassiveEngine.CurrentUserID, this.globalUserCache.CurrentUser.Name, GlobalResourceManager.GetHeadImage(this.globalUserCache.CurrentUser));
            frm.Show();
        } 
        #endregion

        #region IChatSupporter
        public bool IsFriend(string friendID)
        {
            return this.friendListBox1.ContainsUser(friendID);
        }

        public bool IsInGroup(string groupID)
        {
            foreach (string gid in this.globalUserCache.CurrentUser.GroupList)
            {
                if (groupID == gid)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region 网络状态变化事件       
        #region rapidPassiveEngine_RelogonCompleted
        void rapidPassiveEngine_RelogonCompleted(LogonResponse logonResponse)
        {
            GlobalResourceManager.UiSafeInvoker.ActionOnUI<LogonResponse>(this.do_rapidPassiveEngine_RelogonCompleted, logonResponse);
        }

        void do_rapidPassiveEngine_RelogonCompleted(LogonResponse logonResponse)
        {
            if (logonResponse.LogonResult != LogonResult.Succeed)
            {
                this.notifyIcon.ChangeText(String.Format("{0}：{1}（{2}）\n状态：离线，请重新登录。", GlobalResourceManager.SoftwareName, this.globalUserCache.CurrentUser.Name, this.globalUserCache.CurrentUser.UserID));
                MessageBoxEx.Show("自动重登录失败，可能是密码已经被修改。请重启程序，手动登录！", GlobalResourceManager.SoftwareName);
                return;
            }

            this.globalUserCache.CurrentUser.UserStatus = this.myStatus;
            this.skinButton_headImage.Image = GlobalResourceManager.GetHeadImage(this.globalUserCache.CurrentUser);
            this.skinButton_State.Enabled = true;
            this.skinButton_State.Image = GlobalResourceManager.GetStatusImage(this.globalUserCache.CurrentUser.UserStatus);
            this.skinButton_State.Tag = (int)this.globalUserCache.CurrentUser.UserStatus;            
            this.globalUserCache.StartRefreshFriendInfo();
            this.notifyIcon.ChangeText(String.Format("{0}：{1}（{2}）\n状态：{3}", GlobalResourceManager.SoftwareName, this.globalUserCache.CurrentUser.Name, this.globalUserCache.CurrentUser.UserID, GlobalResourceManager.GetUserStatusName(this.globalUserCache.CurrentUser.UserStatus)));
            this.notifyIcon.ChangeMyStatus(this.globalUserCache.CurrentUser.UserStatus);
        }       
        #endregion

        #region rapidPassiveEngine_ConnectionInterrupted
        void rapidPassiveEngine_ConnectionInterrupted()
        {
            GlobalResourceManager.UiSafeInvoker.ActionOnUI( this.do_rapidPassiveEngine_ConnectionInterrupted);
        }

        void do_rapidPassiveEngine_ConnectionInterrupted()
        {
            this.globalUserCache.CurrentUser.UserStatus = UserStatus.OffLine;
            this.skinButton_headImage.Image = GlobalResourceManager.GetHeadImage(this.globalUserCache.CurrentUser);
            this.skinButton_State.Image = Properties.Resources.imoffline__2_;
            this.skinButton_State.Tag = ChatListSubItem.UserStatus.OffLine;
            this.skinButton_State.Enabled = false;            
            this.notifyIcon.ChangeText(String.Format("{0}：{1}（{2}）\n状态：离线，正在重连 . . .", GlobalResourceManager.SoftwareName, this.globalUserCache.CurrentUser.Name, this.globalUserCache.CurrentUser.UserID));
            this.notifyIcon.ChangeMyStatus(UserStatus.OffLine);

            foreach (GGUser friend in this.globalUserCache.GetAllUser())
            {
                friend.UserStatus = UserStatus.OffLine;               
            }
            this.friendListBox1.SetAllUserOffline();            
            this.recentListBox1.SetAllUserOffline();          

            foreach (ChatForm form in this.chatFormManager.GetAllForms())
            {
                form.MyselfOffline();
            }

            foreach (GroupChatForm form in this.groupChatFormManager.GetAllForms())
            {
                form.MyselfOffline();
            }
        }
        #endregion
        #endregion

        #region ContactsOutter_ContactsOffline
        void ContactsOutter_ContactsOffline(string userID)
        {
            this.globalUserCache.ChangeUserStatus(userID, UserStatus.OffLine);           
        }       
        #endregion

        #region 文件传输
        #region //接收方收到文件发送 请求时 的 处理
        void fileOutter_FileRequestReceived(string projectID, string senderID, string projectName, ulong totalSize, ResumedProjectItem resumedFileItem, string comment)
        {
            NDiskParameters para = Comment4NDisk.Parse(comment);
            if (para != null) //表明为网盘或远程磁盘
            {
                return;
            } 

            GlobalResourceManager.UiSafeInvoker.ActionOnUI<string, string, string, ulong, ResumedProjectItem, string>(this.do_fileOutter_FileRequestReceived, projectID, senderID, projectName, totalSize, resumedFileItem, comment);
        }

        void do_fileOutter_FileRequestReceived(string projectID, string senderID, string projectName, ulong totalSize, ResumedProjectItem resumedFileItem, string comment)
        {
            string offlineFileSenderID = Comment4OfflineFile.ParseUserID(comment);
            bool offlineFile = (offlineFileSenderID != null);
            if (offlineFile)
            {
                senderID = offlineFileSenderID;
            }
            ChatForm form = this.GetChatForm(senderID);
            form.FileRequestReceived(projectID, offlineFile);
            form.FlashChatWindow(true);
        }
        #endregion

        #region 发送方收到 接收方（同意或者拒绝 接收文件）的回应时的 
        void fileOutter_FileResponseReceived(TransferingProject project, bool agreeReceive)
        {
            if (project.Comment != null) //表示为网盘或远程磁盘
            {
                return;
            }

            GlobalResourceManager.UiSafeInvoker.ActionOnUI<TransferingProject, bool>(this.do_fileOutter_FileResponseReceived, project, agreeReceive);
        }
        void do_fileOutter_FileResponseReceived(TransferingProject project, bool agreeReceive)
        {
            ChatForm form = this.GetChatForm(project.DestUserID);
            form.FlashChatWindow(true);
        }
        #endregion
        #endregion      
       
        #region GetChatForm
        public ChatForm GetChatForm(string friendID)
        {
            ChatForm form = this.chatFormManager.GetForm(friendID);
            if (form == null)
            {   
                this.rapidPassiveEngine.P2PController.P2PConnectAsyn(friendID);//尝试P2P连接。
                form = new ChatForm(this.rapidPassiveEngine, this.nDiskOutter, this.globalUserCache, this.globalUserCache.CurrentUser, this.globalUserCache.GetUser(friendID));
                form.LastWordChanged += new CbGeneric<bool,string,LastWordsRecord>(form_LastWordChanged);
                this.chatFormManager.Add(form);
                form.TopMost = true;
                form.Show();
                form.TopMost = SystemSettings.Singleton.ChatFormTopMost;

                UnhandleFriendMessageBox cache = this.notifyIcon.PickoutFriendMessageCache(friendID);
                if (cache != null)
                {
                    form.HandleReceivedMessage(cache.MessageList);
                }
            }
            else
            {
                if (form.WindowState == FormWindowState.Minimized)
                {
                    form.WindowState = FormWindowState.Normal;
                }
                form.Focus();
            }
            return form;
        }        

        void form_LastWordChanged(bool isGroup ,string friendOrGroup, LastWordsRecord record)
        {
            IUnit unit = isGroup ? (IUnit)this.globalUserCache.GetGroup(friendOrGroup) : this.globalUserCache.GetUser(friendOrGroup);
            unit.Tag = record;
            this.recentListBox1.LastWordChanged(unit);
        }

        public ChatForm GetExistedChatForm(string friendID)
        {
            return this.chatFormManager.GetForm(friendID);
        }
        #endregion

        #region GetGroupChatForm
        public GroupChatForm GetGroupChatForm(string groupID)
        {
            GroupChatForm form = this.groupChatFormManager.GetForm(groupID);
            if (form == null)
            {
                form = new GroupChatForm(this.rapidPassiveEngine, groupID, this.globalUserCache, this);
                form.LastWordChanged +=new CbGeneric<bool,string,LastWordsRecord>(form_LastWordChanged);
                this.groupChatFormManager.Add(form);
                form.Show();
                UnhandleGroupMessageBox cache = this.notifyIcon.PickoutGroupMessageCache(groupID);
                if (cache != null)
                {
                    form.HandleReceivedMessage(cache.MessageList);
                }
            }
            else
            {
                if (form.WindowState == FormWindowState.Minimized)
                {
                    form.WindowState = FormWindowState.Normal;
                }
                form.Focus();
            }
            return form;
        }

        public GroupChatForm GetExistedGroupChatForm(string groupID)
        {
            return this.groupChatFormManager.GetForm(groupID);
        }        
        #endregion      

        //打开QQ主菜单
        private void toolQQMenu_Click(object sender, EventArgs e)
        {
            this.skinContextMenuStrip_main.Show(skinToolStrip1, new Point(3, -2), ToolStripDropDownDirection.AboveRight);    
        }        

        //选择状态
        private void skinButton_State_Click(object sender, EventArgs e)
        {
            this.skinContextMenuStrip_State.Show(skinButton_State, new Point(0, skinButton_State.Height), ToolStripDropDownDirection.Right);
        }

        
        //状态选择项
        private void Item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem Item = (ToolStripMenuItem)sender;
            skinButton_State.Image = Item.Image;
            skinButton_State.Tag = Item.Tag;
            this.globalUserCache.CurrentUser.UserStatus = (UserStatus)Convert.ToInt32(skinButton_State.Tag);      
            this.myStatus = this.globalUserCache.CurrentUser.UserStatus;

            this.skinButton_headImage.Image = GlobalResourceManager.GetHeadImage(this.globalUserCache.CurrentUser ,true);
            this.notifyIcon.ChangeMyStatus(this.myStatus);
            this.rapidPassiveEngine.CustomizeOutter.Send(InformationTypes.ChangeStatus, BitConverter.GetBytes((int)this.globalUserCache.CurrentUser.UserStatus));

            this.notifyIcon.ChangeText( String.Format("{0}：{1}（{2}）\n状态：{3}", GlobalResourceManager.SoftwareName, this.globalUserCache.CurrentUser.Name, this.globalUserCache.CurrentUser.UserID, GlobalResourceManager.GetUserStatusName(this.globalUserCache.CurrentUser.UserStatus)));
        } 

        private void toolQQShow_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.Focus();
        }

        private bool gotoExit = false;
        private void toolExit_Click(object sender, EventArgs e)
        {
            this.gotoExit = true;
            this.Close();
        }

        #region MainForm_FormClosing
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SystemSettings.Singleton.ExitWhenCloseMainForm && !this.gotoExit)
            {
                this.Visible = false;
                e.Cancel = true;
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            this.globalUserCache.SaveUserLocalCache(this.recentListBox1.GetRecentUserList(20));

            SystemSettings.Singleton.MainFormSize = this.Size;
            SystemSettings.Singleton.MainFormLocation = this.Location;
            SystemSettings.Singleton.Save();

            foreach (ChatForm form in this.chatFormManager.GetAllForms())
            {
                form.Close();
            }

            foreach (GroupChatForm form in this.groupChatFormManager.GetAllForms())
            {
                form.Close();
            }

            this.Visible = false;
            this.notifyIcon.Visible = false;
            this.rapidPassiveEngine.Close();
            Program.MultimediaManager.Dispose();
            this.Cursor = Cursors.Default;
        } 
        #endregion

        //V2.0
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            NDiskForm form = new NDiskForm(null, null, this.rapidPassiveEngine.FileOutter, this.nDiskOutter);            
            form.Show();
        }              
               
        private void toolStripSplitButton3_ButtonClick(object sender, EventArgs e)
        {
            this.recentListBox1.Visible = false;
            this.friendListBox1.Visible = false;
            this.groupListBox.Visible = true;           
        }

        private void toolStripSplitButton4_ButtonClick(object sender, EventArgs e)
        {
            this.recentListBox1.Visible = false;
            this.friendListBox1.Visible = true;
            this.groupListBox.Visible = false;            
        }

        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e)
        {
            this.recentListBox1.Visible = true;
            this.friendListBox1.Visible = false;
            this.groupListBox.Visible = false;            
        }       

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SystemSettingForm form = new SystemSettingForm();
            form.Show();
        }       

        private void ToJoinGroup()
        {
            JoinGroupForm form = new JoinGroupForm(this.rapidPassiveEngine,this);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.globalUserCache.CurrentUser.JoinGroup(form.GroupID);
                GGGroup group = this.globalUserCache.GetGroup(form.GroupID);
                this.groupListBox.AddGroup(group);            

                GroupChatForm groupChatForm = this.GetGroupChatForm(group.ID);
                groupChatForm.AppendSysMessage("您已经成功加入群，可以开始聊天了...");
                groupChatForm.Show();
                groupChatForm.Focus();
            }
        }      

        private void 清空会话列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.recentListBox1.Clear();
        }       

        private void UpdateMyInfo()
        {
            UpdateUserInfoForm form = new UpdateUserInfoForm(this.rapidPassiveEngine, this.globalUserCache, this.globalUserCache.CurrentUser);
            form.UserInfoChanged += new CbGeneric<GGUser>(form_UserInfoChanged);
            form.TopMost = true;
            form.Show();
            form.TopMost = false;
        }

        void form_UserInfoChanged(GGUser user)
        {
            this.labelSignature.Text = this.globalUserCache.CurrentUser.Signature;
            this.skinButton_headImage.Image = GlobalResourceManager.GetHeadImage(this.globalUserCache.CurrentUser);
            this.labelName.Text = this.globalUserCache.CurrentUser.Name;
            this.globalUserCache.AddOrUpdateUser(this.globalUserCache.CurrentUser);

            foreach (ChatForm chatForm in this.chatFormManager.GetAllForms())
            {
                chatForm.OnMyInfoChanged(this.globalUserCache.CurrentUser);
            }           
        }

        private void skinPanel_HeadImage_MouseEnter(object sender, EventArgs e)
        {
            this.pnlTx.BorderStyle = BorderStyle.Fixed3D;
        }

        private void skinPanel_HeadImage_MouseLeave(object sender, EventArgs e)
        {
            this.pnlTx.BorderStyle = BorderStyle.None;
        }

        private void notyfyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!this.initialized)
            {
                return;
            }

            this.Visible = true;
            this.Focus();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            SystemSettingForm form = new SystemSettingForm();
            form.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.gotoExit = true;
            this.Close();
        }

        private void 个人资料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.globalUserCache.CurrentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            this.UpdateMyInfo();
        }       

        private void 加入群ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.globalUserCache.CurrentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            this.ToJoinGroup();
        }

        private void 创建群ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.globalUserCache.CurrentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            CreateGroupForm form = new CreateGroupForm(this.rapidPassiveEngine);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {               
                GGGroup group = form.Group;
                this.globalUserCache.CurrentUser.JoinGroup(group.ID);
                this.globalUserCache.OnCreateGroup(group);
                this.groupListBox.AddGroup(group);

                GroupChatForm groupChatForm = this.GetGroupChatForm(group.ID);
                groupChatForm.AppendSysMessage("您已经成功创建群...");
                groupChatForm.Show();
                groupChatForm.Focus();
            }
        }      

        private void skinButton_headImage_Click(object sender, EventArgs e)
        {
            if (this.globalUserCache.CurrentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            this.UpdateMyInfo();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.globalUserCache.CurrentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            ChangePasswordForm form = new ChangePasswordForm(this.rapidPassiveEngine);
            form.ShowDialog();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.initialized)
            {
                return;
            }

            this.Visible = true;
            this.Focus();
        }

        private void 小头像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.friendListBox1.IconSizeMode = ChatListItemIcon.Small;
            this.大头像ToolStripMenuItem.Checked = false;           
            SystemSettings.Singleton.ShowLargeIcon = false;
            SystemSettings.Singleton.Save();
        }

        private void 大头像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.friendListBox1.IconSizeMode = ChatListItemIcon.Large;
            this.小头像ToolStripMenuItem.Checked = false;          
            SystemSettings.Singleton.ShowLargeIcon = true;
            SystemSettings.Singleton.Save();
        }       

        private void FocusCurrent(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void 查找好友ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchFriendForm form = new SearchFriendForm(this, this.globalUserCache.CurrentUser);
            form.Show();
        }

        //添加好友
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (this.globalUserCache.CurrentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            this.AddFriend(null);
        }

        public void AddFriend(string friendID )
        {
            AddFriendForm form = new AddFriendForm(this.rapidPassiveEngine, this, this.globalUserCache.CurrentUser ,friendID);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.globalUserCache.CurrentUser.AddFriend(form.FriendID, form.CatalogName);
                GGUser user = this.globalUserCache.GetUser(form.FriendID);
                this.friendListBox1.AddUser(user);
                ChatForm chatForm = this.GetChatForm(form.FriendID);
                chatForm.AppendSysMessage("您已经成功将对方添加为好友，可以开始对话了...");
                chatForm.Show();
                chatForm.Focus();
            } 
        }

        public List<ChatListSubItem> SearchChatListSubItem(string idOrName)
        {
            return this.friendListBox1.SearchChatListSubItem(idOrName);
        }        

        #region ITwinkleNotifySupporter
        public string GetFriendName(string friendID)
        {
            GGUser user = this.globalUserCache.GetUser(friendID);
            return user.Name;
        }

        public string GetGroupName(string groupID)
        {
            GGGroup group = this.globalUserCache.GetGroup(groupID);
            return group.Name;
        }

        public void PlayAudioAsyn()
        {
            GlobalResourceManager.PlayAudioAsyn();
        }

        public Icon GetHeadIcon(string userID)
        {
            GGUser user = this.globalUserCache.GetUser(userID);
            return user.GetHeadIcon(GlobalResourceManager.HeadImages);
        }

        public Icon Icon64
        {
            get { return GlobalResourceManager.Icon64; }
        }

        public Icon NoneIcon64
        {
            get { return GlobalResourceManager.NoneIcon64; }
        }

        public Icon GroupIcon
        {
            get { return GlobalResourceManager.GroupIcon; }
        }

        public Icon GetStatusIcon(UserStatus status)
        {
            return GlobalResourceManager.GetStatusIcon(status);
        }

        IChatForm ITwinkleNotifySupporter.GetChatForm(string friendID)
        {
            return this.GetChatForm(friendID);
        }

        IChatForm ITwinkleNotifySupporter.GetExistedChatForm(string friendID)
        {
            return this.GetExistedChatForm(friendID);
        }

        IGroupChatForm ITwinkleNotifySupporter.GetGroupChatForm(string groupID)
        {
            return this.GetGroupChatForm(groupID);
        }

        IGroupChatForm ITwinkleNotifySupporter.GetExistedGroupChatForm(string groupID)
        {
            return this.GetExistedGroupChatForm(groupID);
        } 
        #endregion  
        
    
        public Image GetHeadImage(GGUser user)
        {
            return GlobalResourceManager.GetHeadImage((GGUser)user);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.cnblogs.com/justnow");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            SystemNotifySendForm form = new SystemNotifySendForm(this.rapidPassiveEngine);
            form.Show();
        }    
    }

    public interface IChatSupporter
    {
        bool IsFriend(string friendID);
        bool IsInGroup(string groupID);

        GroupChatForm GetExistedGroupChatForm(string groupID);
        GroupChatForm GetGroupChatForm(string groupID);
        ChatForm GetChatForm(string friendID);
        ChatForm GetExistedChatForm(string friendID);
        List<ChatListSubItem> SearchChatListSubItem(string idOrName);
    }
}