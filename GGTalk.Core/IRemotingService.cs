using System;
using System.Collections.Generic;
using System.Text;
using ESBasic;
using JustLib.Records;

namespace GGTalk
{
    /// <summary>
    /// 服务端要发布的Remoting服务的接口，供客户端调用。提供如下功能：
    /// （1）注册用户。
    /// （2）查询用户。
    /// （3）发送系统通知。
    /// （4）查询聊天记录。
    /// </summary>
    public interface IRemotingService :IChatRecordPersister
    {
        RegisterResult Register(GGUser user); 

        /// <summary>
        /// 根据ID或Name搜索用户【完全匹配】。
        /// </summary>   
        List<GGUser> SearchUser(string idOrName);

        /// <summary>
        /// 发送系统通知给所有在线用户。
        /// </summary>      
        void SendSystemNotify(string title, string content);
    }

    public enum RegisterResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        Succeed = 0 ,

        /// <summary>
        /// 帐号已经存在
        /// </summary>
        Existed,

        /// <summary>
        /// 过程中出现错误
        /// </summary>
        Error
    }
}
