using System.Collections.Generic;
using JustLib.Records;

namespace GGTalk.Server
{
    /// <summary>
    /// 数据库持久化器接口。
    /// </summary>
    public interface IDbPersister : IChatRecordPersister
    {
        void InsertUser(GGUser t);
        void UpdateUserFriends(GGUser t);
        void InsertGroup(GGGroup t);
        void UpdateUser(GGUser t);
        void UpdateGroup(GGGroup t);
        void DeleteGroup(string groupID);
        IEnumerable<GGUser> GetAllUser();
        IEnumerable<GGGroup> GetAllGroup();

        void ChangeUserPassword(string userID, string newPasswordMD5);
        void ChangeUserGroups(string userID, string groups);
    }
}