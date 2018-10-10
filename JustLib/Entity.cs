using System;
using System.Collections.Generic;
using System.Text;
using ESBasic;
using System.Drawing;

namespace JustLib
{
    #region IUnit
    public interface IUnit
    {
        string ID { get; }
        string Name { get; }
        int Version { get; set; }
        string LastWords { get; }

        bool IsGroup { get; }

        object Tag { get; set; }
        Parameter<string, string> GetIDName();
    } 
    #endregion

    #region IUser
    public interface IUser : IUnit
    {
        List<string> GroupList { get; }
        UserStatus UserStatus { get; set; }
        object Tag { get; set; }
        Dictionary<string, List<string>> FriendDicationary { get; }
        string Signature { get; }
        string DefaultFriendCatalog { get; }

        /// <summary>
        /// 如果为空字符串，则表示位于组织外。
        /// </summary>
        string Department { get; }
        string[] OrgPath { get; }
        bool IsInOrg { get; } 

        List<string> GetAllFriendList();
        List<string> GetFriendCatalogList();       
    }
    #endregion

    #region IGroup
    public interface IGroup : IUnit
    {
        string CreatorID { get; }

        List<string> MemberList { get; }

        void AddMember(string userID);
        void RemoveMember(string userID);

    }
    #endregion   

    //在线状态
    public enum UserStatus
    {
        Online = 2,
        Away = 3,
        Busy = 4,
        DontDisturb = 5,
        OffLine = 6,
        Hide = 7
    }

    public enum GroupChangedType
    {
        /// <summary>
        /// 成员的资料发生变化
        /// </summary>
        MemberInfoChanged = 0,
        /// <summary>
        /// 组的资料（如组名称、公告等）发生变化
        /// </summary>
        GroupInfoChanged,
        SomeoneJoin,
        SomeoneQuit,
        GroupDeleted
    }

    #region ContactRTDatas
    public class UserRTData
    {
        public UserRTData() { }
        public UserRTData(UserStatus status, int ver)
        {
            this.UserStatus = status;
            this.Version = ver;
        }

        public UserStatus UserStatus { get; set; }
        public int Version { get; set; }
    }

    public class ContactRTDatas
    {
        public Dictionary<string, UserRTData> UserStatusDictionary { get; set; }
        public Dictionary<string, int> GroupVersionDictionary { get; set; }
    } 
    #endregion
}
