using System;
using System.Collections.Generic;
using System.Text;
using ESBasic.ObjectManagement.Managers;
using ESPlus.Rapid;
using ESPlus.Serialization;
using ESBasic;
using System.IO;
using System.Threading;
using JustLib;
using ESBasic.Loggers;

namespace GGTalk
{
    /// <summary>
    /// 客户端管理所有本地联系人（好友、组友）、组的资料、状态。
    /// （1）当前登录用户的资料不缓存，每次登录从服务器获取最新。
    /// （2）当前登录用户修改个人资料时，服务器会将其Version增加1。如果仅仅是好友列表/所属组发生变化，则Version不会增加。
    /// </summary>
    public class GlobalUserCache :IUserNameGetter
    {
        private IRapidPassiveEngine rapidPassiveEngine;
        private string companyGroupID = "";
        private string persistenceFilePath = "";
        private int pageSize4LoadFriends = 20;
        private ObjectManager<string, GGUser> userManager = new ObjectManager<string, GGUser>(); //缓存用户资料
        private ObjectManager<string, GGGroup> groupManager = new ObjectManager<string, GGGroup>();
        private UserLocalPersistence originUserLocalPersistence;
        private IAgileLogger logger;

        public event CbGeneric<GGUser> FriendInfoChanged;
        public event CbGeneric<GGUser> FriendAdded;
        public event CbGeneric<GGGroup, GroupChangedType, string> GroupChanged; //group - type - userID      
        public event CbGeneric<GGUser> FriendStatusChanged;
        public event CbGeneric<string> FriendRemoved;
        public event CbGeneric FriendRTDataRefreshCompleted;

        #region CurrentUser
        protected GGUser currentUser;
        public GGUser CurrentUser
        {
            get { return currentUser; }
        }
        #endregion       

        #region Ctor
       public GlobalUserCache(IRapidPassiveEngine engine)
       {
           this.rapidPassiveEngine = engine;
           var persistenceFilePath = SystemSettings.SystemSettingsDir + engine.CurrentUserID + ".dat";
           this.Initialize(this.rapidPassiveEngine.CurrentUserID, persistenceFilePath, GlobalConsts.CompanyGroupID, GlobalResourceManager.Logger);
       }

       private void Initialize(string curUserID, string persistencePath, string _companyGroupID, IAgileLogger _logger)
       {
           this.GroupChanged += delegate { };
           this.FriendInfoChanged += delegate { };
           this.FriendStatusChanged += delegate { };
           this.FriendRemoved += delegate { };

           this.FriendRTDataRefreshCompleted += new CbGeneric(GlobalUserCache_FriendRTDataRefreshCompleted);

           this.companyGroupID = _companyGroupID;
           this.logger = _logger;

           //自己的信息始终加载最新的           
           this.currentUser = this.DoGetUser(curUserID);
           this.userManager.Add(this.currentUser.ID, this.currentUser);

           this.persistenceFilePath = persistencePath;
           this.originUserLocalPersistence = UserLocalPersistence.Load(this.persistenceFilePath);//返回null，表示该登录帐号还没有任何缓存
           if (this.originUserLocalPersistence != null && this.originUserLocalPersistence.FriendList != null)
           {
               foreach (var user in this.originUserLocalPersistence.FriendList)
               {
                   if (user.ID == null)
                   {
                       continue;
                   }
                   if (user.ID != this.currentUser.ID)
                   {
                       user.UserStatus = UserStatus.OffLine;
                       this.userManager.Add(user.ID, user);
                   }
               }

               foreach (var group in this.originUserLocalPersistence.GroupList)
               {
                   if (this.currentUser.GroupList.Contains(group.ID))
                   {
                       this.groupManager.Add(group.ID, group);
                   }

               }
           }
       }

       void GlobalUserCache_FriendRTDataRefreshCompleted()
       {
           this.SaveUserLocalCache(null);
       }
       #endregion       

        #region private methods
        private GGUser DoGetUser(string userID)
        {
            var bUser = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetUserInfo, System.Text.Encoding.UTF8.GetBytes(userID));
            if (bUser == null)
            {
                return null;
            }
            return ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<GGUser>(bUser, 0);
        }

        private GGGroup DoGetGroup(string groupID)
        {
            var bGroup = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetGroup, System.Text.Encoding.UTF8.GetBytes(groupID));
            return CompactPropertySerializer.Default.Deserialize<GGGroup>(bGroup, 0);
        }
        private List<GGGroup> DoGetMyGroups()
        {
            var bMyGroups = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetMyGroups, null);
            return CompactPropertySerializer.Default.Deserialize<List<GGGroup>>(bMyGroups, 0);
        }
        private List<GGGroup> DoGetSomeGroups(List<string> groupIDList)
        {
            var bMyGroups = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetSomeGroups, CompactPropertySerializer.Default.Serialize(groupIDList));
            return CompactPropertySerializer.Default.Deserialize<List<GGGroup>>(bMyGroups, 0);
        }
        private ContactRTDatas DoGetContactsRTDatas()
        {
            var res = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetContactsRTData, null);
            return ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<ContactsRTDataContract>(res, 0);
        }
        private List<GGUser> DoGetSomeUsers(List<string> userIDList)
        {
            var bFriends = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetSomeUsers, CompactPropertySerializer.Default.Serialize(userIDList));
            return CompactPropertySerializer.Default.Deserialize<List<GGUser>>(bFriends, 0);
        }

        private List<GGUser> DoGetAllContacts() //好友，包括组友 
        {
            var bFriends = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetAllContacts, null);
            return CompactPropertySerializer.Default.Deserialize<List<GGUser>>(bFriends, 0);
        } 
        #endregion

        public IUnit GetUnit(string id ,bool isGroup)
        {            
            if (isGroup)
            {
                return this.GetGroup(id);
            }

            return this.GetUser(id);
        }       


        

        #region UserLocalCache
        public List<string> GetRecentList()
        {
            if (this.originUserLocalPersistence == null || this.originUserLocalPersistence.RecentList == null)
            {
                return new List<string>();
            }

            return this.originUserLocalPersistence.RecentList;
        }

        public void SaveUserLocalCache(List<string> recentList) //recentID的列表，recentID以“G_”或“U_”开头，以区分用户或组。
        {
            if (recentList == null)
            {
                if (this.originUserLocalPersistence != null)
                {
                    recentList = this.originUserLocalPersistence.RecentList;
                }
            }

            var cache = new UserLocalPersistence(this.userManager.GetAllReadonly(), this.groupManager.GetAll(), recentList);
            cache.Save(this.persistenceFilePath);
        }
        #endregion

        #region StartRefreshFriendInfo
        private Thread updateThread;
        /// <summary>
        /// 当登录后窗体显示时，或断线重连成功时，调用此方法。
        /// </summary>
        public void StartRefreshFriendInfo()
        {
            //直接使用线程，可以快速启动。后台线程池初始化需要10秒左右，太慢了。
            this.updateThread = (this.userManager.Count > 1) ? new Thread(new ParameterizedThreadStart(this.RefreshContactRTData)) : new Thread(new ParameterizedThreadStart(this.LoadContactsFromServer));
            this.updateThread.Start();

            //if (this.userManager.Count > 0)
            //{

            //    CbGeneric cb = new CbGeneric(this.RefreshContactRTData);
            //    cb.BeginInvoke(null, null);
            //}
            //else
            //{
            //    CbGeneric cb = new CbGeneric(this.LoadContactsFromServer);
            //    cb.BeginInvoke(null, null);
            //}
        }

        #region LoadContactsFromServer
        private void LoadContactsFromServer(object state)
        {
            try
            {
                var tmpFriendList = this.currentUser.GetAllFriendList();
                var allContacts = new ESBasic.Collections.SortedArray<string>(tmpFriendList);
                var myGroups = this.DoGetMyGroups(); //如果开启的Org，则会有公司群，其包含了所有组织内用户
                foreach (var group in myGroups)
                {
                    this.groupManager.Add(group.ID, group);
                    allContacts.Add(group.MemberList, true); //加入组友
                }

                var allContactList = allContacts.GetAllReadonly(); //所有联系人，包括好友、组友
                var pageCount = allContactList.Count / this.pageSize4LoadFriends;
                var lastPageSize = allContactList.Count % this.pageSize4LoadFriends;
                if (lastPageSize > 0)
                {
                    pageCount += 1;
                }
                else
                {
                    lastPageSize = this.pageSize4LoadFriends;
                }

                if (pageCount == 1)
                {
                    //好友，包括组友                   
                    var friends = this.DoGetAllContacts();
                    foreach (var friend in friends)
                    {
                        if (friend.ID != this.currentUser.ID)
                        {
                            this.userManager.Add(friend.ID, friend);
                            this.FriendInfoChanged(friend);
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < pageCount; i++)
                    {
                        var ary = (i == pageCount - 1) ? new string[lastPageSize] : new string[this.pageSize4LoadFriends];
                        allContactList.CopyTo(i * this.pageSize4LoadFriends, ary, 0, ary.Length);
                        var tmp = new List<string>(ary);
                        var friends = this.DoGetSomeUsers(tmp);
                        foreach (var friend in friends)
                        {
                            if (friend.ID != this.currentUser.ID)
                            {
                                this.userManager.Add(friend.ID, friend);
                                this.FriendInfoChanged(friend);
                            }
                        }
                    }
                }

                foreach (var group in myGroups)
                {
                    this.GroupChanged(group, GroupChangedType.MemberInfoChanged, null);
                }

                this.FriendRTDataRefreshCompleted();
            }
            catch (Exception ee)
            {
                this.logger.Log(ee, "GlobalUserCache.LoadContactsFromServer", ESBasic.Loggers.ErrorLevel.Standard);
            }
        }
        #endregion

        #region RefreshContactRTData
        private void RefreshContactRTData(object state)
        {
            try
            {
                var contract = this.DoGetContactsRTDatas();
                foreach (var userID in this.userManager.GetKeyList())
                {
                    if (userID != this.currentUser.ID && !contract.UserStatusDictionary.ContainsKey(userID)) //最新的联系人中不包含缓存用户，则将之从缓存中删除。
                    {
                        this.userManager.Remove(userID);
                        if (this.FriendRemoved != null)
                        {
                            this.FriendRemoved(userID);
                        }
                    }
                }

                foreach (var pair in contract.UserStatusDictionary)
                {
                    if (pair.Key == this.currentUser.ID)
                    {
                        continue;
                    }

                    var origin = this.userManager.Get(pair.Key);
                    if (origin == null) //不存在于本地缓存中
                    {
                        var user = this.DoGetUser(pair.Key);
                        this.userManager.Add(user.ID, user);
                        if (this.FriendInfoChanged != null)
                        {
                            this.FriendInfoChanged(user);
                        }
                    }
                    else
                    {
                        //资料变化
                        if (pair.Value.Version != origin.Version)
                        {
                            var user = this.DoGetUser(pair.Key);
                            if (this.FriendInfoChanged != null)
                            {
                                this.FriendInfoChanged(user);
                            }
                            user.Tag = origin.Tag;
                            this.userManager.Add(user.ID, user);
                        }
                        else
                        {
                            //状态变化
                            if (origin.UserStatus != pair.Value.UserStatus)
                            {
                                origin.UserStatus = pair.Value.UserStatus;
                                if (this.FriendStatusChanged != null)
                                {
                                    this.FriendStatusChanged(origin);
                                }
                            }
                        }
                    }
                }

                var updateGroupList = new List<string>();
                foreach (var groupID in this.currentUser.GroupList)
                {
                    var group = this.groupManager.Get(groupID);
                    if (group == null)
                    {
                        updateGroupList.Add(groupID);
                        continue;
                    }

                    if (contract.GroupVersionDictionary.ContainsKey(groupID))
                    {
                        if (contract.GroupVersionDictionary[groupID] != group.Version)
                        {
                            updateGroupList.Add(groupID);
                            continue;
                        }
                    }
                }

                if (updateGroupList.Count > 0)
                {
                    //加载组
                    var newGroups = this.DoGetSomeGroups(updateGroupList);
                    foreach (var group in newGroups)
                    {
                        this.groupManager.Add(group.ID, group);
                        if (this.GroupChanged != null)
                        {
                            this.GroupChanged(group, GroupChangedType.MemberInfoChanged, null);
                        }
                    }
                }

                if (this.FriendRTDataRefreshCompleted != null)
                {
                    this.FriendRTDataRefreshCompleted();
                }
            }
            catch (Exception ee)
            {
                this.logger.Log(ee, "GlobalUserCache.RefreshContactRTData", ESBasic.Loggers.ErrorLevel.Standard);
            }
        }
        #endregion
        #endregion

        #region Methods
        public List<GGUser> GetAllUser()
        {
            return this.userManager.GetAll();
        }

        public List<string> GetAllUserID()
        {
            return this.userManager.GetKeyList();
        }

        #region GetUserName
        public string GetUserName(string userID)
        {
            var user = this.GetUser(userID);
            if (user == null)
            {
                return null;
            }

            return user.Name;
        }
        #endregion

        public GGUser GetUser(string userID)
        {
            var user = this.userManager.Get(userID);
            if (user == null)
            {
                user = this.DoGetUser(userID);
                if (user != null)
                {
                    this.userManager.Add(userID, user);
                }
            }
            return user;
        }

        public void ChangeUserStatus(string userID, UserStatus status)
        {
            var user = this.userManager.Get(userID);
            if (user != null)
            {
                user.UserStatus = status;
                if (this.FriendStatusChanged != null)
                {
                    this.FriendStatusChanged(user);
                }
            }
        }

        /// <summary>
        /// 被别人添加为好友。
        /// </summary>        
        public void OnFriendAdded(GGUser user)
        {
            this.userManager.Add(user.ID, user);
            if (this.FriendAdded != null)
            {
                this.FriendAdded(user);
            }
        }

        public void AddOrUpdateUser(GGUser user)
        {
            var isNew = !this.userManager.Contains(user.ID);
            this.userManager.Add(user.ID, user);
            if (this.FriendInfoChanged != null)
            {
                this.FriendInfoChanged(user);
            }

#if Org            
            GGGroup group = this.groupManager.Get(this.companyGroupID);
            if (this.currentUser.IsInOrg && group != null)//资料发生变化的可能是自己
            {
                if (user.IsInOrg)
                {
                    group.MemberList.Add(user.ID);
                }
                else
                {
                    group.MemberList.Remove(user.ID);
                }
                this.GroupChanged(group, GroupChangedType.SomeoneJoin, user.ID);
            }    
#endif

        }

        public void RemovedFriend(string friendID)
        {
            var inGroup = false;
            foreach (var group in this.groupManager.GetAll())
            {
                if (group.MemberList.Contains(friendID))
                {
                    inGroup = true;
                    break;
                }
            }

            if (!inGroup)
            {
                this.userManager.Remove(friendID);
            }

            if (this.FriendRemoved != null)
            {
                this.FriendRemoved(friendID);
            }
        }
        #endregion

        #region Group
        /// <summary>
        /// 当自己创建群时，调用此方法添加到缓存中。
        /// </summary>       
        public void OnCreateGroup(GGGroup group)
        {
            this.groupManager.Add(group.ID, group);
        }

        public List<GGGroup> GetAllGroups()
        {
            return this.groupManager.GetAll();
        }

        public GGGroup GetGroup(string groupID)
        {
            var group = this.groupManager.Get(groupID);
            if (group == null)
            {
                group = this.DoGetGroup(groupID);
                this.groupManager.Add(group.ID, group);
            }

            return group;
        }

        /// <summary>
        /// 当退出群时调用。
        /// </summary>        
        public void RemoveGroup(string groupID)
        {
            this.groupManager.Remove(groupID);
        }

        public void OnGroupInfoChanged(string groupID)
        {
            var group = this.groupManager.Get(groupID);
            if (group == null)
            {
                return;
            }

            this.GroupChanged(group, GroupChangedType.GroupInfoChanged, null);
        }

        public void OnGroupDeleted(string groupID, string operateUserID)
        {
            if (this.currentUser.ID == operateUserID)
            {
                return;
            }

            var group = this.groupManager.Get(groupID);
            if (group == null)
            {
                return;
            }
            this.GroupChanged(group, GroupChangedType.GroupDeleted, operateUserID);
            this.groupManager.Remove(groupID);
        }

        public void OnSomeoneJoinGroup(string groupID, string userID)
        {
            var group = this.groupManager.Get(groupID);
            if (group == null || group.MemberList.Contains(userID))
            {
                return;
            }
            group.MemberList.Add(userID);

            if (this.GroupChanged != null)
            {
                this.GroupChanged(group, GroupChangedType.SomeoneJoin, userID);
            }
        }

        public void OnSomeoneQuitGroup(string groupID, string userID)
        {
            var group = this.groupManager.Get(groupID);
            if (group == null || !group.MemberList.Contains(userID))
            {
                return;
            }
            group.MemberList.Remove(userID);

            if (this.GroupChanged != null)
            {
                this.GroupChanged(group, GroupChangedType.SomeoneQuit, userID);
            }
        }
        #endregion
    }  
}
