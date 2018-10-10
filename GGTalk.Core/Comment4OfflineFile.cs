using System;
using System.Collections.Generic;
using System.Text;

namespace GGTalk.Core
{
    /// <summary>
    /// 在离线文件的环境中解析BeginSendFile方法的comment参数。
    /// </summary>
    public static class Comment4OfflineFile
    {
        private const string Prefix = "OfflineFile:";
        public static string ParseUserID(string comment)
        {
            if (comment == null || !comment.StartsWith(Comment4OfflineFile.Prefix))
            {
                return null;
            }

            return comment.Substring(Comment4OfflineFile.Prefix.Length);
        }
     
        public static string BuildComment(string accepterID)
        {
            return Comment4OfflineFile.Prefix + accepterID;
        }
    }
}
