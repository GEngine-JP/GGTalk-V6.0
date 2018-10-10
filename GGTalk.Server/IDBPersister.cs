using System;
using System.Collections.Generic;
using System.Text;
using JustLib.Records;

namespace GGTalk.Server
{
    /// <summary>
    /// 数据库持久化器接口。
    /// </summary>
    public interface IDBPersister : IChatRecordPersister
    {
        void InsertUser(GGUser t);
        void UpdateUserFriends(GGUser t);
        void InsertGroup(GGGroup t);       
        void UpdateUser(GGUser t);
        void UpdateGroup(GGGroup t);
        void DeleteGroup(string groupID);
        List<GGUser> GetAllUser();
        List<GGGroup> GetAllGroup();      

        void ChangeUserPassword(string userID, string newPasswordMD5);
        void ChangeUserGroups(string userID, string groups);        
    }    
}
