using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESPlus.FileTransceiver;

namespace JustLib.NetworkDisk.Server
{
    /// <summary>
    /// 网络硬盘的目录路径管理。可以按照计划的策略将不同用户的虚拟硬盘分散到不同的文件服务器上；也可以采用分布式文件存储系统，如FastDFS。
    /// 各方法的 netDiskID 参数 ： 如果为null或“”，则表示访问的是个人网盘；否则，表示访问的是一个群组的共享网盘，此时netDiskID即是groupID。
    /// </summary>
    public interface INDiskPathManager
    {
        /// <summary>
        /// 获取某网盘对应的文件夹所在父目录的路径。返回null表示没有网络硬盘服务。  
        /// </summary>       
        string GetNetworkDiskRootPath(string clientUserID ,string netDiskID);

        /// <summary>
        /// 获取某网盘的根目录的名称。
        /// </summary>        
        string GetNetworkDiskIniDirName(string clientUserID, string netDiskID);

        /// <summary>
        /// 获取指定网络硬盘的总大小。
        /// </summary>        
        ulong GetNetworkDiskTotalSize(string clientUserID, string netDiskID);

        /// <summary>
        /// 获取指定网络硬盘已经使用的大小。
        /// </summary>        
        ulong GetNetworkDiskSizeUsed(string clientUserID, string netDiskID);
    }

    /// <summary>
    /// 提供INDiskPathManager接口的基础实现（简化版）。以某个文件目录作为整个网络硬盘的总目录。
    /// </summary>
    public class NetworkDiskPathManager : INDiskPathManager
    {
        #region RootPath4PersonalDisk
        private string rootPath4PersonalDisk = AppDomain.CurrentDomain.BaseDirectory + "\\PersonalNetworkDisk\\";
        /// <summary>
        /// 提供个人网络硬盘的根目录，可以为网络路径，如 \\192.168.0.11\FTP\
        /// </summary>
        public string RootPath4PersonalDisk 
        {
            get { return rootPath4PersonalDisk; }
            set
            {
                rootPath4PersonalDisk = value;
                if (!rootPath4PersonalDisk.EndsWith("\\"))
                {
                    rootPath4PersonalDisk += "\\";
                }
            }
        }
        #endregion        

        #region RootPath4GroupDisk
        private string rootPath4GroupDisk = AppDomain.CurrentDomain.BaseDirectory + "\\GroupSharedNetworkDisk\\";
        /// <summary>
        /// 提供群组共享的网络硬盘的根目录，可以为网络路径，如 \\192.168.0.11\FTP\
        /// </summary>
        public string RootPath4GroupDisk
        {
            get { return rootPath4GroupDisk; }
            set
            {
                rootPath4GroupDisk = value;
                if (!rootPath4GroupDisk.EndsWith("\\"))
                {
                    rootPath4GroupDisk += "\\";
                }
            }
        }
        #endregion        

        #region TotalSizeOfOneUser
        private ulong totalSizeOfOneUser = 1024 * 1024 * 1024;
        /// <summary>
        /// 每个用户的空间大小
        /// </summary>
        public ulong TotalSizeOfOneUser
        {
            get { return totalSizeOfOneUser; }
            set { totalSizeOfOneUser = value; }
        } 
        #endregion

        #region INetworkDiskPathManager 成员

        public virtual string GetNetworkDiskRootPath(string clientUserID, string netDiskID)
        {
            if (string.IsNullOrEmpty(netDiskID))
            {
                return this.rootPath4PersonalDisk;
            }

            return this.rootPath4GroupDisk;
        }

        public virtual string GetNetworkDiskIniDirName(string clientUserID, string netDiskID)
        {
            if (string.IsNullOrEmpty(netDiskID))
            {
                return clientUserID;
            }

            return netDiskID;
        }

        public virtual ulong GetNetworkDiskTotalSize(string clientUserID, string netDiskID)
        {
            return this.totalSizeOfOneUser;
        }

        public virtual ulong GetNetworkDiskSizeUsed(string clientUserID, string netDiskID)
        {
            ulong size = 0;
            try
            {
                string path = string.Format("{0}{1}\\", this.GetNetworkDiskRootPath(clientUserID, netDiskID), this.GetNetworkDiskIniDirName(clientUserID, netDiskID));
                this.GetDirectorySize(path, ref size);
            }
            catch{ }

            return size;
        }

        private void GetDirectorySize(string dirPath ,ref ulong size)
        {
            string[] entries = System.IO.Directory.GetFileSystemEntries(dirPath);
            foreach (string entry in entries)
            {
                if (Directory.Exists(entry))
                {
                    this.GetDirectorySize(entry, ref size);
                }
                else
                {
                    if (!entry.EndsWith(".tmpe$"))
                    {
                        FileInfo fileInfo = new FileInfo(entry);
                        size += (ulong)fileInfo.Length;
                    }
                }
            }
        }
        #endregion
    }
}
