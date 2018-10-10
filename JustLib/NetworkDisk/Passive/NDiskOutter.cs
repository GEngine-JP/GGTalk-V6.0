using System;
using System.Collections.Generic;
using System.Text;
using ESFramework.Passive;
using ESFramework.Core;
using ESPlus.Core;
using ESBasic.Helpers;
using ESPlus.Application.FileTransfering.Passive;
using System.IO;
using ESPlus.Application.CustomizeInfo.Passive;
using ESPlus.Serialization;

namespace JustLib.NetworkDisk.Passive
{
    /// <summary>
    /// INDiskOutter接口的实现。
    /// </summary>
    public class NDiskOutter : INDiskOutter
    {
        #region FileDirectoryInfoTypes
        private NDiskInfoTypes fileDirectoryInfoTypes = new NDiskInfoTypes();
        public NDiskInfoTypes FileDirectoryInfoTypes
        {
            get { return fileDirectoryInfoTypes; }
            set { fileDirectoryInfoTypes = value; }
        } 
        #endregion

        private ICustomizeOutter customizeOutter = null;
        private IFileOutter fileOutter;

        #region Ctor
        public NDiskOutter(IFileOutter outter, ICustomizeOutter _customizeOutter)
        {           
            this.fileOutter = outter;
            this.customizeOutter = _customizeOutter;
        }
        #endregion
        
        #region IDirectoryOutter 成员
        public SharedDirectory GetSharedDirectory(string ownerID, string netDiskID ,string dirPath)
        {
            ReqDirectoryContract contract = new ReqDirectoryContract(netDiskID ,dirPath);
            byte[] res = this.customizeOutter.Query(ownerID, this.fileDirectoryInfoTypes.ReqDirectory, CompactPropertySerializer.Default.Serialize<ReqDirectoryContract>(contract));

            ResDirectoryContract resContract = CompactPropertySerializer.Default.Deserialize<ResDirectoryContract>(res, 0);
            return resContract.SharedDirectory;
        }

        public NetworkDiskState GetNetworkDiskState(string netDiskID)
        {
            byte[] res = this.customizeOutter.Query(this.fileDirectoryInfoTypes.ReqNetworkDiskState, netDiskID == null ? null : System.Text.Encoding.UTF8.GetBytes(netDiskID));
            ResNetworkDiskStateContract resContract = CompactPropertySerializer.Default.Deserialize<ResNetworkDiskStateContract>(res, 0);
            return resContract.NetworkDiskState;
        }

        public void CreateDirectory(string ownerID, string netDiskID, string parentDirectoryPath, string newDirName)
        {
            CreateDirectoryContract contract = new CreateDirectoryContract(netDiskID, parentDirectoryPath, newDirName);
            this.customizeOutter.Send(ownerID, this.fileDirectoryInfoTypes.CreateDirectory, CompactPropertySerializer.Default.Serialize<CreateDirectoryContract>(contract));
        }

        public OperationResult Rename(string ownerID, string netDiskID, string parentDirectoryPath, bool isFile, string oldName, string newName)
        {
            RenameContract contract = new RenameContract(netDiskID, parentDirectoryPath, isFile, oldName, newName);
            byte[] res = this.customizeOutter.Query(ownerID, this.fileDirectoryInfoTypes.Rename, CompactPropertySerializer.Default.Serialize<RenameContract>(contract));
            return CompactPropertySerializer.Default.Deserialize<OperationResult>(res, 0);
        }

        public OperationResult Download(string ownerID, string netDiskID, string sourceRemotePath, string saveLocalPath, bool isFile)
        {
            DownloadContract contract = new DownloadContract(netDiskID, sourceRemotePath, saveLocalPath, isFile);
            byte[] res = this.customizeOutter.Query(ownerID, this.fileDirectoryInfoTypes.Download, CompactPropertySerializer.Default.Serialize<DownloadContract>(contract));
            return CompactPropertySerializer.Default.Deserialize<OperationResult>(res, 0);
        }

        public void Upload(string ownerID, string netDiskID, string sourceLocalPath, string newDestPath)
        {
            if (File.Exists(sourceLocalPath))
            {
                FileStream stream = File.OpenRead(sourceLocalPath);
                stream.Close();
                stream.Dispose();
            }

            string fileID = null;
            //BeginSendFile的comment参数值使用存放文件的路径
            this.fileOutter.BeginSendFile(ownerID, sourceLocalPath, Comment4NDisk.BuildComment(newDestPath, netDiskID), out fileID);
        }

        public OperationResult Delete(string ownerID, string netDiskID, string sourceParentDirectoryPath, IList<string> filesBeDeleted, IList<string> directoriesBeDeleted)
        {
            DeleteContract contract = new DeleteContract(netDiskID, sourceParentDirectoryPath, filesBeDeleted, directoriesBeDeleted);
            byte[] res = this.customizeOutter.Query(ownerID, this.fileDirectoryInfoTypes.Delete, CompactPropertySerializer.Default.Serialize<DeleteContract>(contract));
            return CompactPropertySerializer.Default.Deserialize<OperationResultConatract>(res, 0);
        }

        public OperationResult Move(string ownerID, string netDiskID, string oldParentDirectoryPath, IList<string> filesBeMoved, IList<string> directoriesBeMoved, string newParentDirectoryPath)
        {
            MoveContract contract = new MoveContract(netDiskID, oldParentDirectoryPath, filesBeMoved, directoriesBeMoved, newParentDirectoryPath);
            byte[] res = this.customizeOutter.Query(ownerID, this.fileDirectoryInfoTypes.Move, CompactPropertySerializer.Default.Serialize<MoveContract>(contract));
            return CompactPropertySerializer.Default.Deserialize<OperationResult>(res, 0);
        }

        public OperationResult Copy(string ownerID, string netDiskID, string sourceParentDirectoryPath, IList<string> filesBeCopyed, IList<string> directoriesCopyed, string destParentDirectoryPath)
        {
            CopyContract contract = new CopyContract(netDiskID ,sourceParentDirectoryPath, filesBeCopyed, directoriesCopyed, destParentDirectoryPath);
            byte[] res = this.customizeOutter.Query(ownerID, this.fileDirectoryInfoTypes.Copy, CompactPropertySerializer.Default.Serialize<CopyContract>(contract));
            return CompactPropertySerializer.Default.Deserialize<OperationResult>(res, 0);
        }
        #endregion
    }
}
