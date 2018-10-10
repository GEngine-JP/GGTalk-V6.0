using System;
using System.Collections.Generic;
using System.Text;

namespace JustLib.NetworkDisk
{
    public class CopyContract
    {
        public CopyContract() { }
        public CopyContract(string _netDiskID, string _sourceParentDirectoryPath, IList<string> _filesBeCopyed, IList<string> _dirsBeCopyed, string _destParentDirectoryPath) 
        {
            this.netDiskID = _netDiskID;
            this.sourceParentDirectoryPath = _sourceParentDirectoryPath;
            this.filesBeCopyed = _filesBeCopyed;
            this.directoriesBeCopyed = _dirsBeCopyed;
            this.destParentDirectoryPath = _destParentDirectoryPath;
        }

        #region NetDiskID
        private string netDiskID;
        /// <summary>
        /// 要访问的网络磁盘的ID。
        /// </summary>
        public string NetDiskID
        {
            get { return netDiskID; }
            set { netDiskID = value; }
        }
        #endregion

        #region SourceParentDirectoryPath
        private string sourceParentDirectoryPath;
        public string SourceParentDirectoryPath
        {
            get { return sourceParentDirectoryPath; }
            set { sourceParentDirectoryPath = value; }
        } 
        #endregion

        #region DirectoriesBeCopyed
        private IList<string> directoriesBeCopyed;
        public IList<string> DirectoriesBeCopyed
        {
            get { return directoriesBeCopyed; }
            set { directoriesBeCopyed = value; }
        }

        #endregion

        #region FilesBeCopyed
        private IList<string> filesBeCopyed = new List<string>();
        public IList<string> FilesBeCopyed
        {
            get { return filesBeCopyed; }
            set { filesBeCopyed = value; }
        }
        #endregion

        #region DestParentDirectoryPath
        private string destParentDirectoryPath;
        public string DestParentDirectoryPath
        {
            get { return destParentDirectoryPath; }
            set { destParentDirectoryPath = value; }
        } 
        #endregion
    }

    public class CreateDirectoryContract
    {
        #region Ctor
        public CreateDirectoryContract() { }
        public CreateDirectoryContract(string _netDiskID, string parentDirPath, string newDirName)
        {
            this.netDiskID = _netDiskID;
            this.parentDirectoryPath = parentDirPath;
            this.newDirectoryName = newDirName;
        }
        #endregion

        #region NetDiskID
        private string netDiskID;
        /// <summary>
        /// 要访问的网络磁盘的ID。
        /// </summary>
        public string NetDiskID
        {
            get { return netDiskID; }
            set { netDiskID = value; }
        }
        #endregion

        #region ParentDirectoryPath
        private string parentDirectoryPath;
        public string ParentDirectoryPath
        {
            get { return parentDirectoryPath; }
            set { parentDirectoryPath = value; }
        }
        #endregion

        #region NewDirectoryName
        private string newDirectoryName;
        public string NewDirectoryName
        {
            get { return newDirectoryName; }
            set { newDirectoryName = value; }
        }
        #endregion
    }

    public class DeleteContract
    {
        #region Ctor
        public DeleteContract() { }
        public DeleteContract(string _netDiskID, string _sourceParentDirectoryPath, IList<string> _filesBeDeleted, IList<string> _directoriesBeDeleted)
        {
            this.netDiskID = _netDiskID;
            this.sourceParentDirectoryPath = _sourceParentDirectoryPath;
            this.filesBeDeleted = _filesBeDeleted;
            this.directoriesBeDeleted = _directoriesBeDeleted;
        }
        #endregion

        #region NetDiskID
        private string netDiskID;
        /// <summary>
        /// 要访问的网络磁盘的ID。
        /// </summary>
        public string NetDiskID
        {
            get { return netDiskID; }
            set { netDiskID = value; }
        }
        #endregion

        #region SourceParentDirectoryPath
        private string sourceParentDirectoryPath;
        public string SourceParentDirectoryPath
        {
            get { return sourceParentDirectoryPath; }
            set { sourceParentDirectoryPath = value; }
        }
        #endregion

        #region FilesBeDeleted
        private IList<string> filesBeDeleted;
        public IList<string> FilesBeDeleted
        {
            get { return filesBeDeleted; }
            set { filesBeDeleted = value; }
        }
        #endregion

        #region DirectoriesBeDeleted
        private IList<string> directoriesBeDeleted;
        public IList<string> DirectoriesBeDeleted
        {
            get { return directoriesBeDeleted; }
            set { directoriesBeDeleted = value; }
        }
        #endregion
    }

    public class DownloadContract
    {
        #region Ctor
        public DownloadContract() { }
        public DownloadContract(string _netDiskID, string sourcePath, string savePath, bool _isFile)
        {
            this.netDiskID = _netDiskID;
            this.sourceRemotePath = sourcePath;
            this.saveLocalPath = savePath;
            this.isFile = _isFile;
        }
        #endregion

        #region NetDiskID
        private string netDiskID;
        /// <summary>
        /// 要访问的网络磁盘的ID。
        /// </summary>
        public string NetDiskID
        {
            get { return netDiskID; }
            set { netDiskID = value; }
        }
        #endregion

        #region IsFile
        private bool isFile = true;
        public bool IsFile
        {
            get { return isFile; }
            set { isFile = value; }
        }
        #endregion

        #region SourceRemotePath
        private string sourceRemotePath;
        public string SourceRemotePath
        {
            get { return sourceRemotePath; }
            set { sourceRemotePath = value; }
        }
        #endregion

        #region SaveLocalPath
        private string saveLocalPath;
        public string SaveLocalPath
        {
            get { return saveLocalPath; }
            set { saveLocalPath = value; }
        }
        #endregion
    }

    public class MoveContract
    {
        public MoveContract() { }
        public MoveContract(string _netDiskID, string _oldParentDirectoryPath, IList<string> _filesBeMoved, IList<string> _directoriesBeMoved, string _newParentDirectoryPath)
        {
            this.netDiskID = _netDiskID;
            this.oldParentDirectoryPath = _oldParentDirectoryPath;
            this.directoriesBeMoved = _directoriesBeMoved;
            this.filesBeMoved = _filesBeMoved;
            this.newParentDirectoryPath = _newParentDirectoryPath;
        }

        #region NetDiskID
        private string netDiskID;
        /// <summary>
        /// 要访问的网络磁盘的ID。
        /// </summary>
        public string NetDiskID
        {
            get { return netDiskID; }
            set { netDiskID = value; }
        }
        #endregion

        #region OldParentDirectoryPath
        private string oldParentDirectoryPath;
        public string OldParentDirectoryPath
        {
            get { return oldParentDirectoryPath; }
            set { oldParentDirectoryPath = value; }
        }
        #endregion

        #region DirectoriesBeMoved
        private IList<string> directoriesBeMoved;
        public IList<string> DirectoriesBeMoved
        {
            get { return directoriesBeMoved; }
            set { directoriesBeMoved = value; }
        }

        #endregion

        #region FilesBeMoved
        private IList<string> filesBeMoved = new List<string>();
        public IList<string> FilesBeMoved
        {
            get { return filesBeMoved; }
            set { filesBeMoved = value; }
        }
        #endregion

        #region NewParentDirectoryPath
        private string newParentDirectoryPath;
        public string NewParentDirectoryPath
        {
            get { return newParentDirectoryPath; }
            set { newParentDirectoryPath = value; }
        }
        #endregion
    }

    /// <summary>
    /// 针对Delete/Move/Copy的操作回复。
    /// </summary>
    public class OperationResultConatract : OperationResult
    {
        public OperationResultConatract() { }
        public OperationResultConatract(string error)
            : base(error)
        {

        }
    }

    public class RenameContract
    {
        #region Ctor
        public RenameContract() { }
        public RenameContract(string _netDiskID, string parentPath, bool _isFile, string _oldName, string _newName)
        {
            this.netDiskID = _netDiskID;
            this.parentDirectoryPath = parentPath;
            this.isFile = _isFile;
            this.oldName = _oldName;
            this.newName = _newName;
        }
        #endregion

        #region NetDiskID
        private string netDiskID;
        /// <summary>
        /// 要访问的网络磁盘的ID。
        /// </summary>
        public string NetDiskID
        {
            get { return netDiskID; }
            set { netDiskID = value; }
        }

        #endregion
        #region ParentDirectoryPath
        private string parentDirectoryPath;
        /// <summary>
        /// ParentDirectoryPath 要重命名的文件或文件夹在哪个目录下。
        /// </summary>
        public string ParentDirectoryPath
        {
            get { return parentDirectoryPath; }
            set { parentDirectoryPath = value; }
        }
        #endregion

        #region IsFile
        private bool isFile = true;
        public bool IsFile
        {
            get { return isFile; }
            set { isFile = value; }
        }
        #endregion

        #region OldName
        private string oldName;
        public string OldName
        {
            get { return oldName; }
            set { oldName = value; }
        }
        #endregion

        #region NewName
        private string newName;
        public string NewName
        {
            get { return newName; }
            set { newName = value; }
        }
        #endregion
    }

    /// <summary>
    /// 获取某个目录的信息。
    /// </summary>	
    public class ReqDirectoryContract
    {
        public ReqDirectoryContract()
        {
        }

        public ReqDirectoryContract(string _netDiskID ,string _directoryPath)
        {
            this.directoryPath = _directoryPath;
            this.netDiskID = _netDiskID;
        }

        #region NetDiskID
        private string netDiskID;
        /// <summary>
        /// 要访问的网络磁盘的ID。
        /// </summary>
        public string NetDiskID
        {
            get { return netDiskID; }
            set { netDiskID = value; }
        } 
        #endregion

        #region FtpDirectory
        private string directoryPath = null;
        /// <summary>
        /// DirectoryPath 如果值为null，表示获取所有的磁盘驱动器列表。
        /// </summary>
        public string DirectoryPath
        {
            get
            {
                return this.directoryPath;
            }
            set
            {
                this.directoryPath = value;
            }
        }
        #endregion
    }

    /// <summary>
    /// 回复某个目录信息。
    /// </summary>	
    public class ResDirectoryContract
    {
        public ResDirectoryContract()
        {
        }

        public ResDirectoryContract(SharedDirectory directory)
        {
            this.sharedDirectory = directory;
        }        

        #region SharedDirectory
        private SharedDirectory sharedDirectory = null;
        public SharedDirectory SharedDirectory
        {
            get
            {
                return this.sharedDirectory;
            }
            set
            {
                this.sharedDirectory = value;
            }
        }
        #endregion
    }

    public class ResNetworkDiskStateContract
    {
        #region Ctor
        public ResNetworkDiskStateContract() { }
        public ResNetworkDiskStateContract(NetworkDiskState state)
        {
            this.networkDiskState = state;
        }
        #endregion

        #region NetworkDiskState
        private NetworkDiskState networkDiskState;
        public NetworkDiskState NetworkDiskState
        {
            get { return networkDiskState; }
            set { networkDiskState = value; }
        }
        #endregion
    }
}
