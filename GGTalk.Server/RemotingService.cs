using System;
using System.Collections.Generic;
using System.Text;
using ESPlus.Rapid;
using ESBasic;
using JustLib.Records;

namespace GGTalk.Server
{
    /// <summary>
    /// 服务端发布的Remoting服务，供客户端调用。提供如下功能：
    /// （1）注册用户。
    /// （2）查询用户。
    /// （3）发送系统通知。
    /// （4）查询聊天记录。
    /// </summary>
    internal class RemotingService :MarshalByRefObject, IRemotingService
    {
        private GlobalCache globalCache;
        private IRapidServerEngine rapidServerEngine;
        public RemotingService(GlobalCache db ,IRapidServerEngine engine)
        {
            this.globalCache = db;
            this.rapidServerEngine = engine;
        }

        public void SendSystemNotify(string title, string content)
        {
            var contract = new SystemNotifyContract(title, content, "", null);
            var info = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize(contract);
            foreach (var userID in this.rapidServerEngine.UserManager.GetOnlineUserList())
            {
                this.rapidServerEngine.CustomizeController.Send(userID, InformationTypes.SystemNotify4AllOnline, info);
            }
        }

        public RegisterResult Register(GGUser user)
        {
            try
            {
                if (this.globalCache.IsUserExist(user.UserID))
                {
                    return RegisterResult.Existed;
                }

                this.globalCache.InsertUser(user);
                return RegisterResult.Succeed;
            }
            catch (Exception ee)
            {
                return RegisterResult.Error;
            }
        }

        public List<GGUser> SearchUser(string idOrName)
        {
            return this.globalCache.SearchUser(idOrName);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public ChatRecordPage GetChatRecordPage(ChatRecordTimeScope timeScope, string senderID, string accepterID, int pageSize, int pageIndex)
        {
            return this.globalCache.GetChatRecordPage(timeScope ,senderID, accepterID, pageSize, pageIndex);
        }

        public ChatRecordPage GetGroupChatRecordPage(ChatRecordTimeScope timeScope, string groupID, int pageSize, int pageIndex)
        {
            var page = this.globalCache.GetGroupChatRecordPage(timeScope ,groupID, pageSize, pageIndex);
            return page;
        }       

        public void InsertChatMessageRecord(ChatMessageRecord record)
        {
            //目前没有通过remoting插入数据库
        }
    }
}
