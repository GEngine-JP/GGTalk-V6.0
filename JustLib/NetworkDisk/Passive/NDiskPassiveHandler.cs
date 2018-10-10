using System;
using System.Collections.Generic;
using System.Text;
using ESPlus.Application.CustomizeInfo.Passive;
using System.IO;
using ESPlus.Serialization;
using ESPlus.Application.FileTransfering.Passive;
using ESBasic.Helpers;
using ESPlus.Application.CustomizeInfo;
using ESPlus.FileTransceiver;
using ESPlus.Application.FileTransfering;
using ESBasic;

namespace JustLib.NetworkDisk.Passive
{
    /// <summary>
    /// 客户端网盘处理器
    /// </summary>
    public class NDiskPassiveHandler : IIntegratedCustomizeHandler, IDisposable
    {
        #region FileDirectoryInfoTypes
        private NDiskInfoTypes fileDirectoryInfoTypes = new NDiskInfoTypes();
        public NDiskInfoTypes FileDirectoryInfoTypes
        {
            get { return fileDirectoryInfoTypes; }
            set { fileDirectoryInfoTypes = value; }
        } 
        #endregion

        private IFileOutter fileOutter;
        private string sharedRootPath = null;//共享的根目录，如果为null，表示共享整个磁盘。       

        #region Initialize
        public void Initialize(IFileOutter outter, string rootPath)
        {           
            this.fileOutter = outter;
            this.sharedRootPath = rootPath;

            //要作为单例使用，否则，应该调用其Dispose方法，以释放预定的事件。
            this.fileOutter.FileRequestReceived += new CbFileRequestReceived(fileOutter_FileRequestReceived);
        }

        void fileOutter_FileRequestReceived(string projectID, string senderID, string fileName, ulong totalSize, ResumedProjectItem resumedFileItem, string comment)
        {
            NDiskParameters paras = Comment4NDisk.Parse(comment);
            if (paras == null)
            {
                return;
            }

            //string savePath = resumedFileItem != null ? resumedFileItem.LocalSavePath : comment; 
            //上述bug，2014.11.04修复
            string savePath = resumedFileItem != null ? resumedFileItem.LocalSavePath : paras.DirectoryPath;
            string fullPath = this.ConstructFullPath(savePath);
            if (savePath != null && savePath.Length >= 2 && savePath[1] == ':') //表示为含驱动器的绝对路径。
            {
                fullPath = savePath;
            }         
            this.fileOutter.BeginReceiveFile(projectID, fullPath);
        }

        public void Dispose()
        {
            this.fileOutter.FileRequestReceived -= new CbFileRequestReceived(fileOutter_FileRequestReceived); //要作为单例使用，否则，应该调用其Dispose方法        
        }
        #endregion

        private string ConstructFullPath(string relativePath)
        {
            if (string.IsNullOrEmpty( this.sharedRootPath))
            {
                return relativePath;
            }

            return this.sharedRootPath + relativePath;
        }

        public bool CanHandle(int informationType)
        {
            return this.fileDirectoryInfoTypes.Contains(informationType);
        }

        public void HandleInformation(string sourceUserID, int informationType, byte[] info)
        {
            if (informationType == this.fileDirectoryInfoTypes.CreateDirectory)
            {
                CreateDirectoryContract contract = CompactPropertySerializer.Default.Deserialize<CreateDirectoryContract>(info, 0);
                string fullPath = this.ConstructFullPath(contract.ParentDirectoryPath);
                DirectoryInfo dir = new DirectoryInfo(fullPath);
                Directory.CreateDirectory(fullPath + "\\" + contract.NewDirectoryName);
                return;
            }
        }

        public byte[] HandleQuery(string sourceUserID, int informationType, byte[] info)
        {
            #region ReqDirectory
            if (informationType == this.fileDirectoryInfoTypes.ReqDirectory)
            {
                ReqDirectoryContract contract = CompactPropertySerializer.Default.Deserialize<ReqDirectoryContract>(info, 0);
                string fullPath = this.ConstructFullPath(contract.DirectoryPath);
                SharedDirectory dir = SharedDirectory.GetSharedDirectory(fullPath);
                return CompactPropertySerializer.Default.Serialize<ResDirectoryContract>(new ResDirectoryContract(dir));
            }
            #endregion

            #region Rename
            if (informationType == this.fileDirectoryInfoTypes.Rename)
            {
                RenameContract contract = CompactPropertySerializer.Default.Deserialize<RenameContract>(info, 0);
                string fullPath = this.ConstructFullPath(contract.ParentDirectoryPath);
                try
                {
                    if (contract.IsFile)
                    {
                        File.Move(fullPath + contract.OldName, fullPath + contract.NewName);
                    }
                    else
                    {
                        Directory.Move(fullPath + contract.OldName, fullPath + contract.NewName);
                    }

                    return CompactPropertySerializer.Default.Serialize<OperationResultConatract>(new OperationResultConatract()); ;
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
            #endregion

            #region DownloadFile
            if (informationType == this.fileDirectoryInfoTypes.Download)
            {
                DownloadContract contract = CompactPropertySerializer.Default.Deserialize<DownloadContract>(info, 0);
                string fullPath = this.ConstructFullPath(contract.SourceRemotePath);
                if (contract.IsFile)
                {
                    try
                    {
                        FileStream stream = File.OpenRead(fullPath);
                        stream.Close();
                        stream.Dispose();
                    }
                    catch (Exception ee)
                    {
                        string error = "";
                        if (ee is FileNotFoundException)
                        {
                            error = string.Format("{0} 不存在或已经被删除！", Path.GetFileName(fullPath));
                        }
                        else if (ee is IOException)
                        {
                            error = string.Format("{0} 正在被其它进程占用！", Path.GetFileName(fullPath));
                        }
                        else
                        {
                            error = ee.Message;
                        }

                        return CompactPropertySerializer.Default.Serialize<OperationResultConatract>(new OperationResultConatract(error));
                    }
                }
                else
                {
                    if (!Directory.Exists(fullPath))
                    {
                        string error = string.Format("{0} 不存在或已经被删除！", Path.GetFileName(fullPath));
                        return CompactPropertySerializer.Default.Serialize<OperationResultConatract>(new OperationResultConatract(error));
                    }
                }

                string fileID = null;
                this.fileOutter.BeginSendFile(sourceUserID, fullPath, Comment4NDisk.BuildComment(contract.SaveLocalPath, contract.NetDiskID), out fileID);
                return CompactPropertySerializer.Default.Serialize<OperationResultConatract>(new OperationResultConatract()); ;
            }
            #endregion

            #region DeleteFileOrDirectory
            if (informationType == this.fileDirectoryInfoTypes.Delete)
            {
                OperationResultConatract resultContract = new OperationResultConatract();
                try
                {
                    DeleteContract contract = CompactPropertySerializer.Default.Deserialize<DeleteContract>(info, 0);
                    string fullPath = this.ConstructFullPath(contract.SourceParentDirectoryPath);
                    if (contract.FilesBeDeleted != null)
                    {
                        foreach (string fileName in contract.FilesBeDeleted)
                        {
                            string filePath = fullPath + fileName;
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                        }
                    }

                    if (contract.DirectoriesBeDeleted != null)
                    {
                        foreach (string dirName in contract.DirectoriesBeDeleted)
                        {
                            string dirPath = fullPath + dirName + "\\";
                            if (Directory.Exists(dirPath))
                            {
                                FileHelper.DeleteDirectory(dirPath);
                            }
                        }
                    }
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
                    CopyContract contract = CompactPropertySerializer.Default.Deserialize<CopyContract>(info, 0);
                    FileHelper.Copy(this.ConstructFullPath(contract.SourceParentDirectoryPath), contract.FilesBeCopyed, contract.DirectoriesBeCopyed, this.ConstructFullPath(contract.DestParentDirectoryPath));
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
                    MoveContract contract = CompactPropertySerializer.Default.Deserialize<MoveContract>(info, 0);
                    FileHelper.Move(this.ConstructFullPath(contract.OldParentDirectoryPath), contract.FilesBeMoved, contract.DirectoriesBeMoved, this.ConstructFullPath(contract.NewParentDirectoryPath));
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

        public void HandleBroadcast(string broadcasterID, string groupID, int informationType, byte[] info)
        {

        }

        public void HandleInformationFromServer(int informationType, byte[] info)
        {

        }

        public byte[] HandleQueryFromServer(int informationType, byte[] info)
        {
            return null;
        }

        public void HandleBroadcastFromServer(string groupID, int informationType, byte[] info)
        {

        }
    }
}
