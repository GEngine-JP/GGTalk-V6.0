using System;
using System.Collections.Generic;
using System.Text;
using ESPlus.Application.CustomizeInfo.Server;
using ESPlus.Serialization;
using System.IO;
using ESPlus.Application.FileTransfering;
using ESPlus.Application.CustomizeInfo;

namespace JustLib.NetworkDisk.Server
{
    public class NDiskHandler : IIntegratedCustomizeHandler
    {
        #region FileDirectoryInfoTypes
        private NDiskInfoTypes fileDirectoryInfoTypes = new NDiskInfoTypes();
        public NDiskInfoTypes FileDirectoryInfoTypes
        {
            get { return fileDirectoryInfoTypes; }
            set { fileDirectoryInfoTypes = value; }
        } 
        #endregion

        private NetworkDisk networkDisk;
        private IBaseFileController fileController;
        
        #region Initialize
        public void Initialize(IBaseFileController controller, NetworkDisk disk)
        {           
            this.fileController = controller;
            this.networkDisk = disk;
        }
        #endregion

        public bool CanHandle(int informationType)
        {
            return this.fileDirectoryInfoTypes.Contains(informationType);
        }       

        public void HandleInformation(string sourceUserID, int informationType, byte[] info)
        {
            if (informationType == this.fileDirectoryInfoTypes.CreateDirectory)
            {
                CreateDirectoryContract contract = CompactPropertySerializer.Default.Deserialize<CreateDirectoryContract>(info,0);
                this.networkDisk.CreateDirectory(sourceUserID, contract.NetDiskID, contract.ParentDirectoryPath, contract.NewDirectoryName);
                return;
            }    
        }

        public byte[] HandleQuery(string sourceUserID, int informationType, byte[] info)
        {
            #region ReqDirectory
            if (informationType == this.fileDirectoryInfoTypes.ReqDirectory)
            {
                ReqDirectoryContract contract = CompactPropertySerializer.Default.Deserialize<ReqDirectoryContract>(info,0);
                SharedDirectory dir = this.networkDisk.GetNetworkDisk(sourceUserID, contract.NetDiskID, contract.DirectoryPath);
                return CompactPropertySerializer.Default.Serialize<ResDirectoryContract>(new ResDirectoryContract(dir));
            }
            #endregion

            #region ReqNetworkDiskState
            if (informationType == this.fileDirectoryInfoTypes.ReqNetworkDiskState)
            {
                string netDiskID = null;
                if (info != null)
                {
                    netDiskID = System.Text.Encoding.UTF8.GetString(info);
                }
                NetworkDiskState state = this.networkDisk.GetNetworkDiskState(sourceUserID, netDiskID);
                return CompactPropertySerializer.Default.Serialize<ResNetworkDiskStateContract>(new ResNetworkDiskStateContract(state));
            }
            #endregion            

            if (informationType == this.fileDirectoryInfoTypes.Rename)
            {
                RenameContract contract = CompactPropertySerializer.Default.Deserialize<RenameContract>(info, 0);
                try
                {
                    this.networkDisk.Rename(sourceUserID, contract.NetDiskID, contract.ParentDirectoryPath, contract.IsFile, contract.OldName, contract.NewName);
                    return CompactPropertySerializer.Default.Serialize<OperationResultConatract>(new OperationResultConatract()); 
                }
                catch (Exception ee)
                {
                    string error = "";
                    if (ee is IOException)
                    {
                        error = string.Format("{0} 正在被使用！", Path.GetFileName(contract.OldName));
                    }
                    else
                    {
                        error = ee.Message;
                    }
                    return CompactPropertySerializer.Default.Serialize<OperationResultConatract>(new OperationResultConatract(error)); ;
                }
            }

            #region DownloadFile
            if (informationType == this.fileDirectoryInfoTypes.Download)
            {
                DownloadContract contract = CompactPropertySerializer.Default.Deserialize<DownloadContract>(info, 0);
                string fileOrDirPath = this.networkDisk.GetNetworkDiskRootPath(sourceUserID, contract.NetDiskID) + contract.SourceRemotePath;
                string error = "";
                try
                {
                    if (File.Exists(fileOrDirPath))
                    {
                        FileStream stream = File.OpenRead(fileOrDirPath);
                        stream.Close();
                        stream.Dispose();
                    }
                    else
                    {
                        if (!Directory.Exists(fileOrDirPath))
                        {
                            error = string.Format("{0} 不存在或已经被删除！", Path.GetFileName(fileOrDirPath));
                            return CompactPropertySerializer.Default.Serialize<OperationResultConatract>(new OperationResultConatract(error));
                        }
                    }
                }
                catch (Exception ee)
                {                    
                    if (ee is FileNotFoundException)
                    {
                        error = string.Format("{0} 不存在或已经被删除！", Path.GetFileName(fileOrDirPath));
                    }
                    else if (ee is IOException)
                    {
                        error = string.Format("{0} 正在被其它进程占用！", Path.GetFileName(fileOrDirPath));
                    }
                    else
                    {
                        error = ee.Message;
                    }

                    return CompactPropertySerializer.Default.Serialize<OperationResultConatract>(new OperationResultConatract(error));
                }


                string projectID = null;
                this.fileController.BeginSendFile(sourceUserID, fileOrDirPath, Comment4NDisk.BuildComment(contract.SaveLocalPath, contract.NetDiskID), out projectID);

                return CompactPropertySerializer.Default.Serialize<OperationResultConatract>(new OperationResultConatract());
            }
            #endregion
           
            #region DeleteFileOrDirectory
            if (informationType == this.fileDirectoryInfoTypes.Delete)
            {
                OperationResultConatract resultContract = new OperationResultConatract();
                try
                {
                    DeleteContract contract = CompactPropertySerializer.Default.Deserialize<DeleteContract>(info,0);
                    this.networkDisk.DeleteFileOrDirectory(sourceUserID, contract.NetDiskID, contract.SourceParentDirectoryPath, contract.FilesBeDeleted, contract.DirectoriesBeDeleted);
                }
                catch (Exception ee)
                {
                    resultContract = new OperationResultConatract(ee.Message);
                }
                return CompactPropertySerializer.Default.Serialize<OperationResultConatract>(resultContract);
            }
            #endregion

            #region CopyFileOrDirectory
            if (informationType == this.fileDirectoryInfoTypes.Copy)
            {
                OperationResultConatract resultContract = new OperationResultConatract();
                try
                {
                    CopyContract contract = CompactPropertySerializer.Default.Deserialize<CopyContract>(info,0);
                    this.networkDisk.Copy(sourceUserID, contract.NetDiskID, contract.SourceParentDirectoryPath, contract.FilesBeCopyed, contract.DirectoriesBeCopyed, contract.DestParentDirectoryPath);
                }
                catch (Exception ee)
                {
                    resultContract = new OperationResultConatract(ee.Message);
                }
                return CompactPropertySerializer.Default.Serialize<OperationResultConatract>(resultContract);
            }
            #endregion

            #region MoveFileOrDirectory
            if (informationType == this.fileDirectoryInfoTypes.Move)
            {
                OperationResultConatract resultContract = new OperationResultConatract();
                try
                {
                    MoveContract contract = CompactPropertySerializer.Default.Deserialize<MoveContract>(info,0);
                    this.networkDisk.Move(sourceUserID, contract.NetDiskID, contract.OldParentDirectoryPath, contract.FilesBeMoved, contract.DirectoriesBeMoved, contract.NewParentDirectoryPath);
                }
                catch (Exception ee)
                {
                    resultContract = new OperationResultConatract(ee.Message);
                }
                return CompactPropertySerializer.Default.Serialize<OperationResultConatract>(resultContract);
            }
            #endregion
            return null;
        }
    }
}
