using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESPlus.Application.FileTransfering.Server;
using ESBasic.Helpers;
using ESPlus.FileTransceiver;
using ESPlus.Application.FileTransfering;

namespace JustLib.NetworkDisk.Server
{
    /// <summary>
    /// 网络硬盘，为用户提供在线的网络硬盘服务。可通过INetworkDiskPathManager来将不同用户的目录分散到不同的文件服务器上。
    /// </summary>
    public class NetworkDisk 
    {  
        private INDiskPathManager networkDiskPathManager ;
        private IFileController fileController;
        public NetworkDisk(INDiskPathManager mgr , IFileController controller)
        {
            this.networkDiskPathManager = mgr;
            this.fileController = controller;
            this.fileController.FileRequestReceived += new CbFileRequestReceived(fileController_FileRequestReceived);
        }

        void fileController_FileRequestReceived(string fileID, string senderID, string fileName, ulong fileLength, ResumedProjectItem resumedFileItem, string comment)
        {
            var paras = Comment4NDisk.Parse(comment);
            if (paras == null)
            {
                return;
            }

            if (resumedFileItem != null)
            {
                this.fileController.BeginReceiveFile(fileID, resumedFileItem.LocalSavePath); //续传
                return;
            }

            var rootPath = this.networkDiskPathManager.GetNetworkDiskRootPath(senderID, paras.NetDiskID);
            this.fileController.BeginReceiveFile(fileID, rootPath + paras.DirectoryPath);
        }           

        #region Methods
        #region GetNetworkDiskRootPath
        public string GetNetworkDiskRootPath(string clientUserID, string netDiskID)
        {
            return this.networkDiskPathManager.GetNetworkDiskRootPath(clientUserID, netDiskID);
        }
        #endregion

        #region GetNetworkDisk
        public SharedDirectory GetNetworkDisk(string clientUserID, string netDiskID, string dirPath)
        {
            var rootPath = this.networkDiskPathManager.GetNetworkDiskRootPath(clientUserID, netDiskID);
            if (rootPath == null)
            {
                return null;
            }

            var iniDirName = this.networkDiskPathManager.GetNetworkDiskIniDirName(clientUserID, netDiskID);
            var diskRpotDir = rootPath + "\\" + iniDirName + "\\";
            if (!Directory.Exists(diskRpotDir))
            {
                Directory.CreateDirectory(diskRpotDir);
            }

            if (dirPath == null)
            {
                var dir = new SharedDirectory();
                var disk = new DiskDrive();
                disk.Name = iniDirName;
                disk.TotalSize = this.networkDiskPathManager.GetNetworkDiskTotalSize(clientUserID, netDiskID);
                disk.AvailableFreeSpace = disk.TotalSize - this.networkDiskPathManager.GetNetworkDiskSizeUsed(clientUserID, netDiskID);

                dir.DriveList.Add(disk);
                return dir;
            }

            return SharedDirectory.GetSharedDirectory(rootPath + dirPath);
        }
        #endregion

        #region GetNetworkDiskState
        public NetworkDiskState GetNetworkDiskState(string clientUserID, string netDiskID)
        {
            var total = this.networkDiskPathManager.GetNetworkDiskTotalSize(clientUserID, netDiskID);
            var used = this.networkDiskPathManager.GetNetworkDiskSizeUsed(clientUserID, netDiskID);

            return new NetworkDiskState(total, used);
        }
        #endregion

        #region CreateDirectory
        public void CreateDirectory(string clientUserID, string netDiskID, string parentDirectoryPath, string newDirName)
        {
            var rootPath = this.networkDiskPathManager.GetNetworkDiskRootPath(clientUserID, netDiskID);
            Directory.CreateDirectory(rootPath + parentDirectoryPath + newDirName);
        }
        #endregion

        #region DeleteFileOrDirectory
        public void DeleteFileOrDirectory(string clientUserID, string netDiskID, string sourceParentDirectoryPath, IList<string> filesBeDeleted, IList<string> directoriesBeDeleted)
        {
            var rootPath = this.networkDiskPathManager.GetNetworkDiskRootPath(clientUserID, netDiskID);
            if (filesBeDeleted != null)
            {
                foreach (var fileName in filesBeDeleted)
                {
                    var filePath = rootPath + sourceParentDirectoryPath + fileName;
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }

            if (directoriesBeDeleted != null)
            {
                foreach (var dirName in directoriesBeDeleted)
                {
                    var dirPath = rootPath + sourceParentDirectoryPath + dirName + "\\";
                    if (Directory.Exists(dirPath))
                    {
                        FileHelper.DeleteDirectory(dirPath);
                    }
                }
            }
        }
        #endregion

        #region Rename
        public void Rename(string clientUserID, string netDiskID, string parentDirectoryPath, bool isFile, string oldName, string newName)
        {
            var rootPath = this.networkDiskPathManager.GetNetworkDiskRootPath(clientUserID, netDiskID);
            if (isFile)
            {
                File.Move(rootPath + parentDirectoryPath + oldName, rootPath + parentDirectoryPath + newName);
            }
            else
            {
                Directory.Move(rootPath + parentDirectoryPath + oldName, rootPath + parentDirectoryPath + newName);
            }
        }
        #endregion

        #region Move
        public void Move(string clientUserID, string netDiskID, string oldParentDirectoryPath, IEnumerable<string> filesBeMoved, IEnumerable<string> directoriesBeMoved, string newParentDirectoryPath)
        {
            var rootPath = this.networkDiskPathManager.GetNetworkDiskRootPath(clientUserID, netDiskID);
            FileHelper.Move(rootPath + oldParentDirectoryPath, filesBeMoved, directoriesBeMoved, rootPath + newParentDirectoryPath);
        }
        #endregion

        #region Copy
        public void Copy(string clientUserID, string netDiskID, string sourceParentDirectoryPath, IEnumerable<string> filesBeCopyed, IEnumerable<string> directoriesCopyed, string destParentDirectoryPath)
        {
            var rootPath = this.networkDiskPathManager.GetNetworkDiskRootPath(clientUserID, netDiskID);
            FileHelper.Copy(rootPath + sourceParentDirectoryPath, filesBeCopyed, directoriesCopyed, rootPath + destParentDirectoryPath);
        }
        #endregion 
        #endregion       
    }
}
