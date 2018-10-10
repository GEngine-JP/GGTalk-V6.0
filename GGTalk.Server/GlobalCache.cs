using System;
using System.Collections.Generic;
using System.Text;

using ESBasic.ObjectManagement.Managers;
using ESBasic.Security;
using System.Configuration;
using ESBasic;

using JustLib.Records;

namespace GGTalk.Server
{   
    /// <summary>
    /// 服务端全局 用户/群组/离线消息/离线文件 缓存。
    /// </summary>
    internal class GlobalCache 
    {
        private IDbPersister dbPersister ;
        private ObjectManager<string, GGUser> userCache = new ObjectManager<string, GGUser>(); // key:用户ID 。 Value：用户信息
        private ObjectManager<string, GGGroup> groupCache = new ObjectManager<string, GGGroup>();  // key:组ID 。 Value：Group信息
        private ObjectManager<string, List<OfflineMessage>> offlineMessageTable = new ObjectManager<string, List<OfflineMessage>>();//key:用户ID 。 
        private ObjectManager<string, List<OfflineFileItem>> offlineFileTable = new ObjectManager<string, List<OfflineFileItem>>();//key:用户ID 。 

        public GlobalCache(IDbPersister persister)
        {
            this.dbPersister = persister;

            foreach (var user in this.dbPersister.GetAllUser())
            {
                this.userCache.Add(user.UserID, user);
            }

            foreach (var group in this.dbPersister.GetAllGroup())
            {
                this.groupCache.Add(group.GroupID, group);
            }       
        }

        #region UserTable       

        /// <summary>
        /// 根据ID或Name搜索用户【完全匹配】。
        /// </summary>   
        public List<GGUser> SearchUser(string idOrName)
        {
            var list = new List<GGUser>();
            foreach (var user in this.userCache.GetAllReadonly())
            {
                if (user.ID == idOrName || user.Name == idOrName)
                {
                    list.Add(user);
                }
            }
            return list;
        }

        /// <summary>
        /// 插入一个新用户。
        /// </summary>      
        public void InsertUser(GGUser user)
        {
            this.userCache.Add(user.UserID, user);
            this.dbPersister.InsertUser(user);
        }

        public void UpdateUser(GGUser user)
        {
            var old = this.userCache.Get(user.UserID);
            if (old == null)
            {
                return;
            }

            user.Friends = old.Friends;       //0922 
            user.Groups = old.Groups;  //0922   
            user.Version = old.Version + 1;
            this.userCache.Add(user.UserID, user);
            this.dbPersister.UpdateUser(user);
        }       

        /// <summary>
        /// 目标帐号是否已经存在？
        /// </summary>    
        public bool IsUserExist(string userID)
        {
            return this.userCache.Contains(userID);
        }

        /// <summary>
        /// 根据ID获取用户信息。
        /// </summary>        
        public GGUser GetUser(string userID)
        {
            return this.userCache.Get(userID);
        } 

        public ChangePasswordResult ChangePassword(string userID, string oldPasswordMD5, string newPasswordMD5)
        {
            var user = this.userCache.Get(userID);
            if (user == null)
            {
                return ChangePasswordResult.UserNotExist;
            }

            if (user.PasswordMD5 != oldPasswordMD5)
            {
                return ChangePasswordResult.OldPasswordWrong;
            }
            
            user.PasswordMD5 = newPasswordMD5;

            this.dbPersister.ChangeUserPassword(userID, newPasswordMD5);
            return ChangePasswordResult.Succeed;
        }
      
        /// <summary>
        /// 获取某个用户的好友列表。
        /// </summary>      
        public List<string> GetFriends(string userID)
        {
            var user = this.userCache.Get(userID);
            if (user == null)
            {
                return new List<string>();
            }

            return user.GetAllFriendList(); 
        }

        /// <summary>
        /// 添加好友，建立双向关系
        /// </summary>  
        public void AddFriend(string ownerID, string friendID, string catalogName)
        {
            var user1 = this.userCache.Get(ownerID);
            var user2 = this.userCache.Get(friendID);
            if (user1 == null || user2 == null)
            {
                return;
            }

            user1.AddFriend(friendID, catalogName);
            user2.AddFriend(ownerID ,user2.DefaultFriendCatalog);
            this.dbPersister.UpdateUserFriends(user1);
            this.dbPersister.UpdateUserFriends(user2);
        }

        /// <summary>
        /// 删除好友，并删除双向关系
        /// </summary>  
        public void RemoveFriend(string ownerID, string friendID)
        {
            var user1 = this.userCache.Get(ownerID);
            if (user1 != null)
            {
                user1.RemoveFriend(friendID);
                this.dbPersister.UpdateUserFriends(user1);
            }

            var user2 = this.userCache.Get(friendID);
            if (user2 != null)
            {
                user2.RemoveFriend(ownerID);
                this.dbPersister.UpdateUserFriends(user2);
            }
        }

        public void ChangeFriendCatalogName(string ownerID, string oldName, string newName)
        {
            var user = this.userCache.Get(ownerID);
            if (user == null)
            {
                return ;
            }

            user.ChangeFriendCatalogName(oldName, newName);
            this.dbPersister.UpdateUserFriends(user);
        }

        public void AddFriendCatalog(string ownerID, string catalogName)
        {
            var user = this.userCache.Get(ownerID);
            if (user == null)
            {
                return;
            }

            user.AddFriendCatalog(catalogName);
            this.dbPersister.UpdateUserFriends(user);
        }

        public void RemoveFriendCatalog(string ownerID, string catalogName)
        {
            var user = this.userCache.Get(ownerID);
            if (user == null)
            {
                return;
            }
            user.RemvoeFriendCatalog(catalogName);
            this.dbPersister.UpdateUserFriends(user);
        }

        public void MoveFriend(string ownerID, string friendID, string oldCatalog, string newCatalog)
        {
            var user = this.userCache.Get(ownerID);
            if (user == null)
            {
                return;
            }
            user.MoveFriend(friendID, oldCatalog ,newCatalog);
            this.dbPersister.UpdateUserFriends(user);
        }
        #endregion

        #region GroupTable       

        /// <summary>
        /// 获取某用户所在的所有组列表。
        /// 建议：可将某个用户所在的组ID列表挂接在用户资料的某个字段上，以避免遍历计算。
        /// </summary>       
        public List<GGGroup> GetMyGroups(string userID)
        {
            var groups = new List<GGGroup>();
            var user = this.userCache.Get(userID);
            if (user == null)
            {
                return groups;
            }

            foreach (var groupID in user.GroupList)
            {
                var g = this.groupCache.Get(groupID);
                if (g != null)
                {
                    groups.Add(g);
                }
            }           
            return groups;
        }

        public Dictionary<string, int> GetMyGroupVersions(string userID)
        {
            var dic = new Dictionary<string, int>();
            var user = this.userCache.Get(userID);
            if (user == null)
            {
                return dic;
            }
           
            foreach (var groupID in user.GroupList)
            {
                var g = this.groupCache.Get(groupID);
                if (g != null)
                {
                    dic.Add(groupID,g.Version);
                }
            }
            return dic;
        }

        /// <summary>
        /// 获取某个组
        /// </summary>       
        public GGGroup GetGroup(string groupID)
        {
            return this.groupCache.Get(groupID);     
        }

        /// <summary>
        /// 创建组
        /// </summary>       
        public CreateGroupResult CreateGroup(string creatorID, string groupID, string groupName, string announce)
        {
            if (this.groupCache.Contains(groupID))
            {
                return CreateGroupResult.GroupExisted;
            }

            var group = new GGGroup(groupID, groupName, creatorID, announce, creatorID);            
            this.groupCache.Add(groupID, group);
            this.dbPersister.InsertGroup(group);

            var user = this.userCache.Get(creatorID);          
            user.JoinGroup(groupID);
            this.dbPersister.ChangeUserGroups(user.UserID, user.Groups);
            return CreateGroupResult.Succeed;
        }

        /// <summary>
        /// 退出组
        /// </summary>       
        public void QuitGroup(string userID, string groupID)
        {
            var group = this.groupCache.Get(groupID);
            if (group != null)
            {
                group.RemoveMember(userID);                
            }
            this.dbPersister.UpdateGroup(group);

            var user = this.userCache.Get(userID);           
            user.QuitGroup(groupID);           
            this.dbPersister.ChangeUserGroups(user.UserID, user.Groups);         
        }

        public void DeleteGroup(string groupID)
        {
            var group = this.groupCache.Get(groupID);
            if (group == null)
            {
                return;
            }
            foreach (var userID in group.MemberList)
            {
                var user = this.userCache.Get(userID);
                if (user != null)
                {
                    user.QuitGroup(groupID);
                    this.dbPersister.ChangeUserGroups(user.UserID, user.Groups);
                }
            }
            this.dbPersister.DeleteGroup(groupID);
        }

        /// <summary>
        /// 加入某个组。
        /// </summary>        
        public JoinGroupResult JoinGroup(string userID, string groupID)
        {
            var group = this.groupCache.Get(groupID);
            if (group == null)
            {
                return JoinGroupResult.GroupNotExist;
            }

            var user = this.userCache.Get(userID);
            if (!user.GroupList.Contains(groupID))
            {
                user.JoinGroup(groupID);
                this.dbPersister.ChangeUserGroups(user.UserID, user.Groups);
            }            

            if (!group.MemberList.Contains(userID))
            {
                group.AddMember(userID);
                this.dbPersister.UpdateGroup(group);      
            }

            return JoinGroupResult.Succeed;
        }

        /// <summary>
        /// 获取某个用户的所有联系人（组友，好友）。
        /// 建议：由于该方法经常被调用，可将组友关系缓存在内存中，而非每次都遍历计算一遍。
        /// </summary>        
        public List<string> GetAllContacts(string userID)
        {
            var contacts = new List<string>();
            var user = this.userCache.Get(userID);
            if (user == null)
            {
                return contacts;
            }

            contacts = user.GetAllFriendList();
            foreach (var groupID in user.GroupList)
            {
                var g = this.groupCache.Get(groupID);
                if (g != null)
                {
                    foreach (var memberID in g.MemberList)
                    {
                        if (memberID != userID && !contacts.Contains(memberID))
                        {
                            contacts.Add(memberID);
                        }
                    }
                }
            }

            return contacts;  
        }
        #endregion

        #region OfflineMessage
         /// <summary>
        /// 存储离线消息。
        /// </summary>       
        /// <param name="msg">要存储的离线消息</param>
        public void StoreOfflineMessage(OfflineMessage msg)
        {
            if (!this.offlineMessageTable.Contains(msg.DestUserID))
            {
                this.offlineMessageTable.Add(msg.DestUserID, new List<OfflineMessage>());
            }

            this.offlineMessageTable.Get(msg.DestUserID).Add(msg);
        }

        /// <summary>
        /// 提取目标用户的所有离线消息。
        /// </summary>       
        /// <param name="destUserID">接收离线消息用户的ID</param>
        /// <returns>属于目标用户的离线消息列表，按时间升序排列</returns>
        public List<OfflineMessage> PickupOfflineMessage(string destUserID)
        {
            if (!this.offlineMessageTable.Contains(destUserID))
            {
                return new List<OfflineMessage>();
            }

            var list = this.offlineMessageTable.Get(destUserID);
            this.offlineMessageTable.Remove(destUserID);
            return list;
        }
        #endregion

        #region OfflineFile
        /// <summary>
        /// 将一个离线文件条目保存到数据库中。
        /// </summary>     
        public void StoreOfflineFileItem(OfflineFileItem item)
        {
            if (!this.offlineFileTable.Contains(item.AccepterID))
            {
                this.offlineFileTable.Add(item.AccepterID, new List<OfflineFileItem>());
            }

            this.offlineFileTable.Get(item.AccepterID).Add(item);
        }

        /// <summary>
        /// 从数据库中提取接收者为指定用户的所有离线文件条目。
        /// </summary>       
        public List<OfflineFileItem> PickupOfflineFileItem(string accepterID)
        {
            if (!this.offlineFileTable.Contains(accepterID))
            {
                return new List<OfflineFileItem>();
            }

            var list = this.offlineFileTable.Get(accepterID);
            this.offlineFileTable.Remove(accepterID);
            return list;
        }

        #endregion

        #region ChatRecord
        public void StoreChatRecord(string senderID, string accepterID, byte[] content)
        {
            this.dbPersister.InsertChatMessageRecord(new ChatMessageRecord(senderID, accepterID, content, false));
        }


        public ChatRecordPage GetChatRecordPage(ChatRecordTimeScope timeScope, string senderID, string accepterID, int pageSize, int pageIndex)
        {
            return this.dbPersister.GetChatRecordPage(timeScope, senderID, accepterID, pageSize, pageIndex);
        }

        public void StoreGroupChatRecord(string groupID, string senderID, byte[] content)
        {
            this.dbPersister.InsertChatMessageRecord(new ChatMessageRecord(senderID, groupID, content, true));
        }

        public ChatRecordPage GetGroupChatRecordPage(ChatRecordTimeScope timeScope, string groupID, int pageSize, int pageIndex)
        {
            return this.dbPersister.GetGroupChatRecordPage(timeScope, groupID, pageSize, pageIndex);
        }
        #endregion               
    }    
}
