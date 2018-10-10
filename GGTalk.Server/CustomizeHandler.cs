using System;
using System.Collections.Generic;
using System.Text;
using ESPlus.Application.CustomizeInfo.Server;
using ESPlus.Application.Basic.Server;
using ESPlus.Application.CustomizeInfo;
using ESFramework.Server.UserManagement;
using ESPlus.Rapid;
using ESPlus.Serialization;

using ESFramework;
using JustLib;

namespace GGTalk.Server
{
    /// <summary>
    /// 自定义信息处理器。
    /// （1）当用户上线时，在其LocalTag挂接资料信息。
    /// </summary>
    internal class CustomizeHandler : IIntegratedCustomizeHandler 
    {
        private GlobalCache globalCache;
        private IRapidServerEngine rapidServerEngine;
        private OfflineFileController offlineFileController;      
        public void Initialize(GlobalCache db, IRapidServerEngine engine, OfflineFileController fileCtr)
        {
            this.globalCache = db;
            this.rapidServerEngine = engine;           
            this.offlineFileController = fileCtr;

            this.rapidServerEngine.UserManager.SomeOneDisconnected += new ESBasic.CbGeneric<UserData, ESFramework.Server.DisconnectedType>(UserManager_SomeOneDisconnected);
            this.rapidServerEngine.ContactsController.BroadcastReceived += new ESBasic.CbGeneric<string, string, int, byte[] ,string>(ContactsController_BroadcastReceived);
            this.rapidServerEngine.MessageReceived += new ESBasic.CbGeneric<string, int, byte[], string>(rapidServerEngine_MessageReceived);
        }

        void rapidServerEngine_MessageReceived(string sourceUserID, int informationType, byte[] info, string tag)
        {
            if (informationType == InformationTypes.Chat)
            {             
                string destID = tag;
                if (this.rapidServerEngine.UserManager.IsUserOnLine(destID))
                {
                    this.rapidServerEngine.SendMessage(destID, informationType, info, sourceUserID, 2048);
                }
                else
                {
                    OfflineMessage msg = new OfflineMessage(sourceUserID, destID, informationType, info);
                    this.globalCache.StoreOfflineMessage(msg);
                }
                this.globalCache.StoreChatRecord(sourceUserID, destID, info);
                return;
            }

            if (informationType == InformationTypes.UpdateUserInfo)
            {
                GGUser user = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<GGUser>(info, 0);
                GGUser old = this.globalCache.GetUser(user.UserID);               
                this.globalCache.UpdateUser(user);
                List<string> friendIDs = this.globalCache.GetFriends(sourceUserID);
                byte[] subData = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize<GGUser>(user.PartialCopy); //0922   
                foreach (string friendID in friendIDs)
                {
                    if (friendID != sourceUserID)
                    {
                        //可能要分块发送
                        this.rapidServerEngine.CustomizeController.Send(friendID, InformationTypes.UserInforChanged, subData, true, ActionTypeOnChannelIsBusy.Continue);
                    }
                }
                return;
            }
        }

        void ContactsController_BroadcastReceived(string broadcasterID, string groupID, int broadcastType, byte[] broadcastContent ,string tag)
        {
            if (broadcastType == BroadcastTypes.BroadcastChat)
            {
                this.globalCache.StoreGroupChatRecord(groupID, broadcasterID, broadcastContent);
            }
        }

        void UserManager_SomeOneDisconnected(UserData data, ESFramework.Server.DisconnectedType obj2)
        {
            GGUser user = this.globalCache.GetUser(data.UserID);
            if (user != null)
            {
                user.UserStatus = UserStatus.OffLine;
            }
        }

        //发送离线消息给客户端
        public void SendOfflineMessage(string destUserID)
        {
            List<OfflineMessage> list = this.globalCache.PickupOfflineMessage(destUserID);
            if (list != null && list.Count > 0)
            {
                foreach (OfflineMessage msg in list)
                {
                    byte[] bMsg = CompactPropertySerializer.Default.Serialize<OfflineMessage>(msg);
                    this.rapidServerEngine.CustomizeController.SendBlob(msg.DestUserID, InformationTypes.OfflineMessage, bMsg, 2048);
                }
            }
        }

        /// <summary>
        /// 处理来自客户端的消息。
        /// </summary> 
        public void HandleInformation(string sourceUserID, int informationType, byte[] info)
        {
            if (informationType == InformationTypes.AddFriendCatalog)
            {
                string catalogName = System.Text.Encoding.UTF8.GetString(info) ;
                this.globalCache.AddFriendCatalog(sourceUserID, catalogName);
                return;
            }

            if (informationType == InformationTypes.RemoveFriendCatalog)
            {
                string catalogName = System.Text.Encoding.UTF8.GetString(info);
                this.globalCache.RemoveFriendCatalog(sourceUserID, catalogName);
                return;
            }

            if (informationType == InformationTypes.ChangeFriendCatalogName)
            {
                ChangeCatalogContract contract = CompactPropertySerializer.Default.Deserialize<ChangeCatalogContract>(info, 0);
                this.globalCache.ChangeFriendCatalogName(sourceUserID, contract.OldName, contract.NewName);
                return;
            }

            if (informationType == InformationTypes.MoveFriendToOtherCatalog)
            {
                MoveFriendToOtherCatalogContract contract = CompactPropertySerializer.Default.Deserialize<MoveFriendToOtherCatalogContract>(info, 0);
                this.globalCache.MoveFriend(sourceUserID,contract.FriendID, contract.OldCatalog, contract.NewCatalog);
                return;
            }

            if (informationType == InformationTypes.GetOfflineMessage)
            {
                this.SendOfflineMessage(sourceUserID);
                return;
            }

            if (informationType == InformationTypes.GetOfflineFile)
            {
                this.offlineFileController.SendOfflineFile(sourceUserID);
                return;
            }

            if (informationType == InformationTypes.QuitGroup)
            {
                string groupID = System.Text.Encoding.UTF8.GetString(info) ;
                this.globalCache.QuitGroup(sourceUserID, groupID);
                //通知其它组成员
                this.rapidServerEngine.ContactsController.Broadcast(groupID, BroadcastTypes.SomeoneQuitGroup, System.Text.Encoding.UTF8.GetBytes(sourceUserID),null, ESFramework.ActionTypeOnChannelIsBusy.Continue);
              
                return;
            }

            if (informationType == InformationTypes.DeleteGroup)
            {
                string groupID = System.Text.Encoding.UTF8.GetString(info);               
                //通知其它组成员
                this.rapidServerEngine.ContactsController.Broadcast(groupID, BroadcastTypes.GroupDeleted, System.Text.Encoding.UTF8.GetBytes(sourceUserID),null, ESFramework.ActionTypeOnChannelIsBusy.Continue);
                this.globalCache.DeleteGroup(groupID);
                return;
            }

            if (informationType == InformationTypes.RemoveFriend)
            {
                string friendID = System.Text.Encoding.UTF8.GetString(info);
                this.globalCache.RemoveFriend(sourceUserID, friendID);
                //通知好友
                this.rapidServerEngine.CustomizeController.Send(friendID, InformationTypes.FriendRemovedNotify, System.Text.Encoding.UTF8.GetBytes(sourceUserID));
                return;
            }        

            if (informationType == InformationTypes.ChangeStatus)
            {
                GGUser user = this.globalCache.GetUser(sourceUserID);
                int newStatus = BitConverter.ToInt32(info, 0);
                user.UserStatus = (UserStatus)newStatus;
                List<string> contacts = this.globalCache.GetAllContacts(sourceUserID);                          
                UserStatusChangedContract contract = new UserStatusChangedContract(sourceUserID, newStatus);
                byte[] msg = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize(contract);
                foreach (string friendID in contacts)
                {
                    this.rapidServerEngine.CustomizeController.Send(friendID, InformationTypes.UserStatusChanged, msg);
                }                
                return;
            }

            if (informationType == InformationTypes.SystemNotify4AllOnline)
            {                
                foreach (string userID in this.rapidServerEngine.UserManager.GetOnlineUserList())
                {
                    this.rapidServerEngine.CustomizeController.Send(userID, InformationTypes.SystemNotify4AllOnline, info);
                }
                return;
            }

            if (informationType == InformationTypes.SystemNotify4Group)
            {
                SystemNotifyContract contract = CompactPropertySerializer.Default.Deserialize<SystemNotifyContract>(info, 0);
                GGGroup group = this.globalCache.GetGroup(contract.GroupID);
                if (group != null)
                {
                    foreach (string userID in group.MemberList)
                    {
                        this.rapidServerEngine.CustomizeController.Send(userID, InformationTypes.SystemNotify4Group, info);
                    }
                }
                return;
            }
        }

        /// <summary>
        /// 处理来自客户端的同步调用请求。
        /// </summary>       
        public byte[] HandleQuery(string sourceUserID, int informationType, byte[] info)
        {
            if (informationType == InformationTypes.GetFriendIDList)
            {
                List<string> friendIDs = this.globalCache.GetFriends(sourceUserID);
                return CompactPropertySerializer.Default.Serialize<List<string>>(friendIDs);
            }

            if (informationType == InformationTypes.AddFriend)
            {
                AddFriendContract contract = CompactPropertySerializer.Default.Deserialize<AddFriendContract>(info ,0);
                bool isExist = this.globalCache.IsUserExist(contract.FriendID);
                if (!isExist)
                {
                    return BitConverter.GetBytes((int)AddFriendResult.FriendNotExist);
                }
                this.globalCache.AddFriend(sourceUserID, contract.FriendID ,contract.CatalogName);

                //0922
                GGUser owner = this.globalCache.GetUser(sourceUserID);
                byte[] ownerBuff = CompactPropertySerializer.Default.Serialize<GGUser>(owner);

                //通知对方
                this.rapidServerEngine.CustomizeController.Send(contract.FriendID, InformationTypes.FriendAddedNotify, ownerBuff, true, ESFramework.ActionTypeOnChannelIsBusy.Continue);
                return BitConverter.GetBytes((int)AddFriendResult.Succeed);
            }

            if (informationType == InformationTypes.GetAllContacts)
            {
                List<string> contacts = this.globalCache.GetAllContacts(sourceUserID);
                Dictionary<string, GGUser> contactDic = new Dictionary<string, GGUser>();
                foreach (string friendID in contacts)
                {
                    if (!contactDic.ContainsKey(friendID))
                    {
                        GGUser friend = this.globalCache.GetUser(friendID);
                        if (friend != null)
                        {
                            contactDic.Add(friendID, friend);
                        }
                    }
                }

                return CompactPropertySerializer.Default.Serialize<List<GGUser>>(new List<GGUser>(contactDic.Values));
            }

            if (informationType == InformationTypes.GetSomeUsers)
            {
                List<string> friendIDs = CompactPropertySerializer.Default.Deserialize<List<string>>(info, 0);
                List<GGUser> friends = new List<GGUser>();
                foreach (string friendID in friendIDs)
                {
                    GGUser friend = this.globalCache.GetUser(friendID);
                    if (friend != null)
                    {
                        friends.Add(friend);
                    }
                }

                return CompactPropertySerializer.Default.Serialize<List<GGUser>>(friends);
            }

            if (informationType == InformationTypes.GetContactsRTData)
            {
                List<string> contacts = this.globalCache.GetAllContacts(sourceUserID);               
                Dictionary<string, UserRTData> dic = new Dictionary<string, UserRTData>();
                foreach (string friendID in contacts)
                {
                    if (!dic.ContainsKey(friendID))
                    {
                        GGUser data = this.globalCache.GetUser(friendID);
                        if (data != null)
                        {
                            UserRTData rtData = new UserRTData(data.UserStatus ,data.Version) ;
                            dic.Add(friendID, rtData);
                        }
                    }
                }     
                Dictionary<string, int> groupVerDic = this.globalCache.GetMyGroupVersions(sourceUserID);
                ContactsRTDataContract contract = new ContactsRTDataContract(dic, groupVerDic);
                return CompactPropertySerializer.Default.Serialize(contract);
            }

            if (informationType == InformationTypes.GetUserInfo)
            {
                string target = System.Text.Encoding.UTF8.GetString(info);
                GGUser user = this.globalCache.GetUser(target);
                if (user == null)
                {
                    return null;
                }
                if (sourceUserID != target)  //0922   
                {
                    user = user.PartialCopy;
                }
                return CompactPropertySerializer.Default.Serialize<GGUser>(user);
            }     

            if (informationType == InformationTypes.GetMyGroups)
            {
                List<GGGroup> myGroups = this.globalCache.GetMyGroups(sourceUserID);
                return CompactPropertySerializer.Default.Serialize(myGroups);
            }

            if (informationType == InformationTypes.GetSomeGroups)
            {
                List<string> groups = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<List<string>>(info, 0);
                List<GGGroup> myGroups = new List<GGGroup>();
                foreach (string groupID in groups)
                {
                    GGGroup group = this.globalCache.GetGroup(groupID);
                    if (group != null)
                    {
                        myGroups.Add(group);
                    }
                }
                
                return CompactPropertySerializer.Default.Serialize(myGroups);
            }

            if (informationType == InformationTypes.JoinGroup)
            {
                string groupID = System.Text.Encoding.UTF8.GetString(info);
                JoinGroupResult res = this.globalCache.JoinGroup(sourceUserID, groupID);
                if (res == JoinGroupResult.Succeed)
                {
                    //通知其它组成员
                    this.rapidServerEngine.ContactsController.Broadcast(groupID, BroadcastTypes.SomeoneJoinGroup, System.Text.Encoding.UTF8.GetBytes(sourceUserID),null, ESFramework.ActionTypeOnChannelIsBusy.Continue);
                }
                return BitConverter.GetBytes((int)res);
            }

            if (informationType == InformationTypes.CreateGroup)
            {
                CreateGroupContract contract = CompactPropertySerializer.Default.Deserialize<CreateGroupContract>(info, 0);
                CreateGroupResult res = this.globalCache.CreateGroup(sourceUserID, contract.ID, contract.Name, contract.Announce);               
                return BitConverter.GetBytes((int)res);
            }

            if (informationType == InformationTypes.GetGroup)
            {
                string groupID = System.Text.Encoding.UTF8.GetString(info);
                GGGroup group = this.globalCache.GetGroup(groupID);
                return CompactPropertySerializer.Default.Serialize(group);
            }

            if (informationType == InformationTypes.ChangePassword)
            {
                ChangePasswordContract contract = CompactPropertySerializer.Default.Deserialize<ChangePasswordContract>(info, 0);
                ChangePasswordResult res = this.globalCache.ChangePassword(sourceUserID, contract.OldPasswordMD5, contract.NewPasswordMD5);
                return BitConverter.GetBytes((int)res);
            }
            return null;
        }

        public bool CanHandle(int informationType)
        {
            return InformationTypes.ContainsInformationType(informationType);
        }
    }
}
