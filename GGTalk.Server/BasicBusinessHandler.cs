using System;
using System.Collections.Generic;
using System.Text;
using ESPlus.Application.Basic.Server;


namespace GGTalk.Server
{
    /// <summary>
    /// 基础处理器，用于验证登陆的用户。
    /// </summary>
    internal class BasicHandler : IBasicHandler
    {
        private GlobalCache globalCache;
        public BasicHandler(GlobalCache db)
        {
            this.globalCache = db;
        }

        /// <summary>
        /// 此处验证用户的账号和密码。返回true表示通过验证。
        /// </summary>  
        public bool VerifyUser(string systemToken, string userID, string password, out string failureCause)
        {
            failureCause = "";          
            GGUser user = this.globalCache.GetUser(userID);
            if (user == null)
            {
                failureCause = "用户不存在！";
                return false;
            }

            if (user.PasswordMD5 != password)
            {
                failureCause = "密码错误！";
                return false;
            }

            return true;
        }
    }
}
