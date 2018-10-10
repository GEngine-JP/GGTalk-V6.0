using System;
using System.Collections.Generic;
using System.Text;
using ESBasic.Serialization;

namespace JustLib.NetworkDisk
{
    /// <summary>
    /// 在网盘的环境中解析BeginSendFile方法的comment参数。
    /// </summary>
    public static class Comment4NDisk
    {
        private const string Prefix = "NDisk:";
        public static NDiskParameters Parse(string comment)
        {
            if (comment == null || !comment.StartsWith(Comment4NDisk.Prefix))
            {
                return null;
            }

            return (NDiskParameters)SpringFox.ObjectXml(comment.Substring(Comment4NDisk.Prefix.Length));
        }

        public static string BuildComment(string directoryPath ,string netDiskID)
        {
            NDiskParameters para = new NDiskParameters(directoryPath, netDiskID);
            string xml = SpringFox.XmlObject(para);
            return Comment4NDisk.Prefix + xml;
        }
    }

    [Serializable]
    public class NDiskParameters
    {
        public NDiskParameters() { }
        public NDiskParameters(string path, string id)
        {
            this.DirectoryPath = path;
            this.NetDiskID = id;
        }

        #region DirectoryPath
        private string directoryPath;
        public string DirectoryPath
        {
            get { return directoryPath; }
            set { directoryPath = value; }
        } 
        #endregion

        #region NetDiskID
        private string netDiskID = "";
        public string NetDiskID
        {
            get { return netDiskID; }
            set { netDiskID = value ?? ""; }
        } 
        #endregion
    }
}
