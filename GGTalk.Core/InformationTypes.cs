using System;
using System.Collections.Generic;
using System.Text;

namespace GGTalk
{
    /// <summary>
    /// 自定义信息的类型，取值0-100
    /// </summary>
    public static class InformationTypes
    {
        #region Function
        /// <summary>
        /// 聊天信息 0
        /// </summary>
        public const int Chat = 0;

        /// <summary>
        /// 窗口抖动 1
        /// </summary>
        public const int Vibration = 1;

        /// <summary>
        /// 请求视频 2
        /// </summary>
        public const int VideoRequest = 2;

        /// <summary>
        /// 同意视频 3 （C->C）
        /// </summary>
        public const int AgreeVideo = 3;

        /// <summary>
        /// 拒绝视频 4 （C->C）
        /// </summary>
        public const int RejectVideo = 4;

        /// <summary>
        /// 挂断视频 5 （C->C）
        /// </summary>
        public const int HungUpVideo = 5;


        /// <summary>
        /// 请求访问对方的磁盘 6 （C->C）
        /// </summary>
        public const int DiskRequest = 6;

        /// <summary>
        /// 同意磁盘访问 7 （C->C）
        /// </summary>
        public const int AgreeDisk = 7;

        /// <summary>
        /// 拒绝磁盘访问 8 （C->C）
        /// </summary>
        public const int RejectDisk = 8;

        /// <summary>
        /// 请求对方远程协助自己（访问自己的桌面） 9 （C->C）
        /// </summary>
        public const int RemoteHelpRequest = 9;

        /// <summary>
        /// 同意远程协助对方 10 （C->C）
        /// </summary>
        public const int AgreeRemoteHelp = 10;

        /// <summary>
        /// 拒绝远程协助对方 （C->C）
        /// </summary>
        public const int RejectRemoteHelp = 11;

        /// <summary>
        /// 请求方终止了协助 （C->C）
        /// </summary>
        public const int TerminateRemoteHelp = 12;

        /// <summary>
        /// 协助方终止了协助 （C->C）
        /// </summary>
        public const int CloseRemoteHelp = 13;

        /// <summary>
        /// 请求离线消息 （C->S）
        /// </summary>
        public const int GetOfflineMessage = 14;

        /// <summary>
        /// 服务端转发离线消息给客户端 （S->C）
        /// </summary>
        public const int OfflineMessage = 15;

        /// <summary>
        /// 请求离线文件 （C->S）
        /// </summary>
        public const int GetOfflineFile = 16;

        /// <summary>
        /// 通知发送方，对方是接收了离线文件，还是拒绝了离线文件。（S->C）
        /// </summary>
        public const int OfflineFileResultNotify = 17;

        /// <summary>
        /// 通知对方自己正在输入 （C->C）
        /// </summary>
        public const int InputingNotify = 18;

        /// <summary>
        /// 请求对语音对话 （C->C）
        /// </summary>
        public const int AudioRequest = 19;

        /// <summary>
        /// 同意语音对话 （C->C）
        /// </summary>
        public const int AgreeAudio = 20;

        /// <summary>
        /// 拒绝语音对话 （C->C）
        /// </summary>
        public const int RejectAudio = 21;

        /// <summary>
        /// 挂断语音对话 （C->C）
        /// </summary>
        public const int HungupAudio = 22;       
        #endregion

        #region 个人资料、状态       
        /// <summary>
        /// 获取用户资料（C->S）
        /// </summary>
        public const int GetUserInfo = 32;

        /// <summary>
        /// 修改自己的个人资料（C->S）
        /// </summary>
        public const int UpdateUserInfo = 33;

        /// <summary>
        /// 获取指定某些用户的资料（C->S）
        /// </summary>
        public const int GetSomeUsers = 34;

        /// <summary>
        /// 通知某人资料发生了变化（S->C）
        /// </summary>
        public const int UserInforChanged = 35;

        /// <summary>
        /// 修改状态（C->S）
        /// </summary>
        public const int ChangeStatus = 36;

        /// <summary>
        /// 通知某人状态发生了变化（S->C）
        /// </summary>
        public const int UserStatusChanged = 37;

        /// <summary>
        /// 修改密码（C->S）
        /// </summary>
        public const int ChangePassword = 38;
        
        #endregion

        #region contacts
        /// <summary>
        /// 获取我的所有联系人的在线状态、版本号，以及我的所有组的版本号（C->S）
        /// </summary>
        public const int GetContactsRTData = 40;
        
        /// <summary>
        /// 获取我的所有好友ID（C->S）
        /// </summary>        
        public const int GetFriendIDList = 41;

        /// <summary>
        /// 获取我的所有联系人资料（C->S）
        /// </summary>
        public const int GetAllContacts = 42;   
 

        /// <summary>
        /// 添加好友（C->S）
        /// </summary>
        public const int AddFriend = 43;

        /// <summary>
        /// 删除好友（C->S）
        /// </summary>
        public const int RemoveFriend = 44;

        /// <summary>
        /// 通知客户端其被对方从好友中删除（S->C）
        /// </summary>
        public const int FriendRemovedNotify = 45;

        /// <summary>
        /// 通知客户端其被对方添加为好友（S->C）
        /// </summary>
        public const int FriendAddedNotify = 46; 
        #endregion

        #region Group
        /// <summary>
        /// 获取我的所有组资料（C->S）
        /// </summary>
        public const int GetMyGroups = 50;

        /// <summary>
        /// 获取指定的某些组的资料（C->S）
        /// </summary>
        public const int GetSomeGroups = 51;

        /// <summary>
        /// 加入组（C->S）
        /// </summary>
        public const int JoinGroup = 52;

        /// <summary>
        /// 获取组资料（C->S）
        /// </summary>
        public const int GetGroup = 53;

        /// <summary>
        /// 创建组（C->S）
        /// </summary>
        public const int CreateGroup = 54;

        /// <summary>
        /// 退出组（C->S）
        /// </summary>
        public const int QuitGroup = 55;

        /// <summary>
        /// 解散组（C->S）
        /// </summary>
        public const int DeleteGroup = 56; 
        #endregion

        #region FriendCatalog
        /// <summary>
        /// 添加好友分组（C->S）
        /// </summary>
        public const int AddFriendCatalog = 70;

        /// <summary>
        /// 修改好友分组名称（C->S）
        /// </summary>
        public const int ChangeFriendCatalogName = 71;

        /// <summary>
        /// 删除好友分组（C->S）
        /// </summary>
        public const int RemoveFriendCatalog = 72;

        /// <summary>
        /// 移动好友到别的分组（C->S）
        /// </summary>
        public const int MoveFriendToOtherCatalog = 73;
        #endregion

        #region SystemNotify 系统消息 V5.0
        /// <summary>
        /// 发送给所有在线用户的系统消息
        /// </summary>
        public const int SystemNotify4AllOnline = 80;

        /// <summary>
        /// 发送给某个组的系统消息
        /// </summary>
        public const int SystemNotify4Group = 81;        

        #endregion

        //取值0-100。
        public static bool ContainsInformationType(int infoType)
        {
            return infoType >= 0 && infoType <= 100;
        }
    }


    public static class BroadcastTypes
    {
        /// <summary>
        /// 群聊天
        /// </summary>
        public const int BroadcastChat = 0;

        /// <summary>
        /// 某用户加入组
        /// </summary>
        public const int SomeoneJoinGroup = 1;

        /// <summary>
        /// 某用户退出组
        /// </summary>
        public const int SomeoneQuitGroup = 2;

        /// <summary>
        /// 组被解散
        /// </summary>
        public const int GroupDeleted = 3;
    }
}
