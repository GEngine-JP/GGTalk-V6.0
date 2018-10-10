using System;
using System.Collections.Generic;
using System.Text;

using JustLib;

namespace GGTalk
{
    /// <summary>
    /// 针对InformationTypes.OfflineFileResultNotify的消息协议
    /// </summary>
    public class OfflineFileResultNotifyContract
    {
        public OfflineFileResultNotifyContract() { }
        public OfflineFileResultNotifyContract(string accepterID, string file, bool accept)
        {
            this.AccepterID = accepterID;
            this.FileName = file;
            this.Accept = accept;
        }

        public string AccepterID { get; set; }

        public string FileName { get; set; }

        public bool Accept { get; set; }
    }

    public class CreateGroupContract
    {
        public CreateGroupContract() { }
        public CreateGroupContract(string id, string name, string announce)
        {
            this.ID = id;
            this.Name = name;
            this.Announce = announce;
        }

        public string ID { get; set; }

        public string Name { get; set; }

        public string Announce { get; set; }
    }

    public class ChangePasswordContract
    {
        public ChangePasswordContract() { }
        public ChangePasswordContract(string oldPasswordMD5, string newPasswordMD5)
        {
            this.OldPasswordMD5 = oldPasswordMD5;
            this.NewPasswordMD5 = newPasswordMD5;
        }

        public string OldPasswordMD5 { get; set; }

        public string NewPasswordMD5 { get; set; }
    }

    public class UserStatusChangedContract
    {
        public UserStatusChangedContract() { }
        public UserStatusChangedContract(string userID, int newStatus)
        {
            this.UserID = userID;
            this.NewStatus = newStatus;
        }

        public string UserID { get; set; }

        public int NewStatus { get; set; }
    }

    public class ContactsRTDataContract : ContactRTDatas
    {
        public ContactsRTDataContract() { }
        public ContactsRTDataContract(Dictionary<string, UserRTData> dic ,Dictionary<string, int> gVersion)
        {
            this.UserStatusDictionary = dic;
            this.GroupVersionDictionary = gVersion;
        }
    }

    public class AddFriendContract
    {
        public AddFriendContract() { }
        public AddFriendContract(string friendID, string catalog)
        {
            this.FriendID = friendID;
            this.CatalogName = catalog;
        }

        public string FriendID { get; set; }
        public string CatalogName { get; set; }
    }

    public class ChangeCatalogContract
    {
        public ChangeCatalogContract() { }
        public ChangeCatalogContract(string oldName, string newName)
        {
            this.OldName = oldName;
            this.NewName = newName;
        }

        public string OldName { get; set; }
        public string NewName { get; set; }
    }

    public class MoveFriendToOtherCatalogContract
    {
        public MoveFriendToOtherCatalogContract() { }
        public MoveFriendToOtherCatalogContract(string friendID, string oldCatalog, string newCatalog)
        {
            this.FriendID = friendID;
            this.OldCatalog = oldCatalog;
            this.NewCatalog = newCatalog;
        }

        public string FriendID{ get; set; }
        public string OldCatalog { get; set; }
        public string NewCatalog { get; set; }
    }

    /// <summary>
    /// 系统通知的协议类。
    /// </summary>
    public class SystemNotifyContract
    {
        public SystemNotifyContract()
        {
        }

        public SystemNotifyContract(string title, string content, string senderID ,string groupID)
        {
            this.Title = title;
            this.Content = content;
            this.SenderID = senderID;
            this.GroupID = groupID;
        }
        
        public string Title { get; set; }
        public string Content { get; set; }
        public string SenderID { get; set; }
        public string GroupID { get; set; }        
    }
}
