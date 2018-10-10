using System;
using System.Collections.Generic;
using System.Text;
using CCWin.Win32;
using System.Windows.Forms;
using CCWin.SkinControl;
using ESBasic;
using ESPlus.Serialization;
using System.Runtime.InteropServices;
using OMCS.Passive;
using JustLib;
using GGTalk.Controls;
using JustLib.Records;


namespace GGTalk
{
    public partial class MainForm : ESPlus.Application.CustomizeInfo.IIntegratedCustomizeHandler
    {
        void rapidPassiveEngine_MessageReceived(string sourceUserID, int informationType, byte[] info, string tag)
        {
            if (!this.initialized)
            {
                return;
            }

            if (informationType == InformationTypes.Chat)
            {
                sourceUserID = tag;
                var bChatBoxContent = info;
                if (bChatBoxContent != null)
                {
                    var record = new ChatMessageRecord(sourceUserID, this.rapidPassiveEngine.CurrentUserID, bChatBoxContent, false);
                    GlobalResourceManager.ChatMessageRecordPersister.InsertChatMessageRecord(record);
                }

                var decrypted = info;
                if (GlobalResourceManager.Des3Encryption != null)
                {
                    decrypted = GlobalResourceManager.Des3Encryption.Decrypt(info);
                }

                var content = CompactPropertySerializer.Default.Deserialize<ChatBoxContent>(decrypted, 0);
                var user = this.globalUserCache.GetUser(sourceUserID);
                this.notifyIcon.PushFriendMessage(sourceUserID, informationType, info, content);
                return;
            }
        }     

        #region HandleInformation
        public void HandleInformation(string sourceUserID, int informationType, byte[] info)
        {
            if (!this.initialized)
            {
                return;
            }

            #region 需要twinkle的消息
            if (informationType == InformationTypes.Chat || informationType == InformationTypes.OfflineMessage || informationType == InformationTypes.OfflineFileResultNotify
                         || informationType == InformationTypes.Vibration || informationType == InformationTypes.VideoRequest || informationType == InformationTypes.AgreeVideo
                         || informationType == InformationTypes.RejectVideo || informationType == InformationTypes.HungUpVideo || informationType == InformationTypes.DiskRequest
                         || informationType == InformationTypes.AgreeDisk || informationType == InformationTypes.RejectDisk || informationType == InformationTypes.RemoteHelpRequest
                         || informationType == InformationTypes.AgreeRemoteHelp || informationType == InformationTypes.RejectRemoteHelp || informationType == InformationTypes.CloseRemoteHelp
                         || informationType == InformationTypes.TerminateRemoteHelp
                         || informationType == InformationTypes.AudioRequest || informationType == InformationTypes.RejectAudio || informationType == InformationTypes.AgreeAudio
                         || informationType == InformationTypes.HungupAudio
                         || informationType == InformationTypes.FriendAddedNotify)
            {                
                if (informationType == InformationTypes.FriendAddedNotify)
                { 
                    var owner = CompactPropertySerializer.Default.Deserialize<GGUser>(info ,0); // 0922
                    this.globalUserCache.CurrentUser.AddFriend(owner.ID, this.globalUserCache.CurrentUser.DefaultFriendCatalog);
                    this.globalUserCache.OnFriendAdded(owner); //自然会添加 好友条目
                    sourceUserID = owner.UserID;
                }

                object tag = null;
                if (informationType == InformationTypes.OfflineMessage)
                {
                    byte[] bChatBoxContent = null;
                    var msg = CompactPropertySerializer.Default.Deserialize<OfflineMessage>(info, 0);
                    if (msg.InformationType == InformationTypes.Chat) //目前只处理离线的聊天消息
                    {
                        sourceUserID = msg.SourceUserID;
                        bChatBoxContent = msg.Information;
                        var decrypted = bChatBoxContent;
                        if (GlobalResourceManager.Des3Encryption != null)
                        {
                            decrypted = GlobalResourceManager.Des3Encryption.Decrypt(bChatBoxContent);
                        }
                        
                        var record = new ChatMessageRecord(sourceUserID, this.rapidPassiveEngine.CurrentUserID, decrypted, false);
                        GlobalResourceManager.ChatMessageRecordPersister.InsertChatMessageRecord(record);
                        var content = CompactPropertySerializer.Default.Deserialize<ChatBoxContent>(decrypted, 0);
                        tag = new Parameter<ChatBoxContent, DateTime>(content, msg.Time);
                    }
                }

                if (informationType == InformationTypes.OfflineFileResultNotify)
                {
                    var contract = CompactPropertySerializer.Default.Deserialize<OfflineFileResultNotifyContract>(info, 0);
                    sourceUserID = contract.AccepterID;
                }

                var user = this.globalUserCache.GetUser(sourceUserID);
                this.notifyIcon.PushFriendMessage(sourceUserID, informationType, info, tag);
                return;
            }
            #endregion

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric<string, int, byte[]>(this.HandleInformation), sourceUserID, informationType, info);
            }
            else
            {
                try
                {
                    if (informationType == InformationTypes.InputingNotify)
                    {
                        var form = this.chatFormManager.GetForm(sourceUserID);
                        if (form != null)
                        {
                            form.OnInptingNotify();
                        }
                        return;
                    }

                    if (informationType == InformationTypes.FriendRemovedNotify)
                    {
                        var friendID = System.Text.Encoding.UTF8.GetString(info);
                        this.globalUserCache.RemovedFriend(friendID);
                        return;
                    }

                    if (informationType == InformationTypes.UserInforChanged)
                    {
                        var user = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<GGUser>(info, 0);
                        this.globalUserCache.AddOrUpdateUser(user);
                        return;
                    }

                    if (informationType == InformationTypes.UserStatusChanged)
                    {
                        var contract = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<UserStatusChangedContract>(info, 0);
                        this.globalUserCache.ChangeUserStatus(contract.UserID, (UserStatus)contract.NewStatus);
                    }

                    if (informationType == InformationTypes.SystemNotify4AllOnline)
                    {
                        var contract = CompactPropertySerializer.Default.Deserialize<SystemNotifyContract>(info, 0);
                        var form = new SystemNotifyForm(contract.Title, contract.Content);
                        form.Show();
                        return;
                    }

                    if (informationType == InformationTypes.SystemNotify4Group)
                    {
                        var contract = CompactPropertySerializer.Default.Deserialize<SystemNotifyContract>(info, 0);
                        var form = new SystemNotifyForm(contract.Title, contract.Content);
                        form.Show();
                        return;
                    }
                }
                catch (Exception ee)
                {
                    GlobalResourceManager.Logger.Log(ee, "MainForm.HandleInformation", ESBasic.Loggers.ErrorLevel.Standard);
                    MessageBox.Show(ee.Message);
                }
            }
        }

        #endregion

        public byte[] HandleQuery(string sourceUserID, int informationType, byte[] info)
        {
            return null;
        }

        public bool CanHandle(int informationType)
        {
            return InformationTypes.ContainsInformationType(informationType);
        }

        void ContactsOutter_BroadcastReceived(string broadcasterID, string groupID, int broadcastType, byte[] content ,string tag)
        {
            if (!this.initialized)
            {
                return;
            }

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric<string, string, int, byte[] ,string>(this.ContactsOutter_BroadcastReceived), broadcasterID, groupID, broadcastType, content, tag);
            }
            else
            {
                try
                {
                    if (broadcastType == BroadcastTypes.BroadcastChat)
                    {
                        var group = this.globalUserCache.GetGroup(groupID);

                        var decrypted = content;
                        if (GlobalResourceManager.Des3Encryption != null)
                        {
                            decrypted = GlobalResourceManager.Des3Encryption.Decrypt(content);
                        }

                        this.notifyIcon.PushGroupMessage(broadcasterID, groupID, broadcastType, decrypted);
                        var record = new ChatMessageRecord(broadcasterID, groupID, decrypted, true);
                        GlobalResourceManager.ChatMessageRecordPersister.InsertChatMessageRecord(record);
                        return;
                    }

                    if (broadcastType == BroadcastTypes.SomeoneJoinGroup)
                    {
                        var userID = System.Text.Encoding.UTF8.GetString(content);
                        this.globalUserCache.OnSomeoneJoinGroup(groupID, userID);
                        return;
                    }

                    if (broadcastType == BroadcastTypes.SomeoneQuitGroup)
                    {
                        var userID = System.Text.Encoding.UTF8.GetString(content);
                        this.globalUserCache.OnSomeoneQuitGroup(groupID, userID);
                        return;
                    }

                    if (broadcastType == BroadcastTypes.GroupDeleted)
                    {
                        var userID = System.Text.Encoding.UTF8.GetString(content);
                        this.globalUserCache.OnGroupDeleted(groupID, userID);
                        return;
                    }
                }
                catch (Exception ee)
                {
                    GlobalResourceManager.Logger.Log(ee, "MainForm.GroupOutter_BroadcastReceived", ESBasic.Loggers.ErrorLevel.Standard);
                    MessageBox.Show(ee.Message);
                }

            }
        }
    }
}
