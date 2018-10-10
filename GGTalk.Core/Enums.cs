using System;
using System.Collections.Generic;
using System.Text;

namespace GGTalk
{
    public enum AddFriendResult
    {
        Succeed = 0,
        FriendNotExist,        
    }

    public enum JoinGroupResult
    {
        Succeed = 0,
        GroupNotExist,   
    }

    public enum CreateGroupResult
    {
        Succeed = 0,
        GroupExisted,
    }

    public enum ChangePasswordResult
    {
        Succeed = 0,
        OldPasswordWrong,
        UserNotExist
    }
}
