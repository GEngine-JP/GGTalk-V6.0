using System;
using System.IO;
using System.Collections ;
using System.Collections.Generic;
using ESPlus.FileTransceiver;

namespace JustLib.NetworkDisk
{
	/// <summary>
    /// 共享的目录内容。
	/// </summary>
    [Serializable]
    public class SharedDirectory
    {
        /// <summary>
        /// 获取某个目录下的所有文件以及所有子目录。
        /// </summary>
        /// <param name="dirPath">父目录路径，如果为null，表示获取根目录（如“我的电脑”）下的磁盘或目录</param>              
        public static SharedDirectory GetSharedDirectory(string dirPath)
        {
            SharedDirectory ftpDir = new SharedDirectory();
            ftpDir.directoryPath = dirPath;

            try
            {
                if (dirPath == null)
                {
                    DriveInfo[] drives = DriveInfo.GetDrives();
                    foreach (DriveInfo drive in drives)
                    {
                        ftpDir.SubDirectorys.Add(new DirectoryDetail(drive.Name ,DateTime.Now));
                        ftpDir.DriveList.Add(new DiskDrive(drive));
                    }
                }
                else
                {
                    DirectoryInfo info = new DirectoryInfo(dirPath);
                    foreach (FileInfo file in info.GetFiles())
                    {
                        if (file.Extension.ToLower() != ".tmpe$")
                        {
                            ftpDir.FileList.Add(new FileDetail(file.Name, file.Length, file.CreationTime));
                        }
                    }

                    foreach (DirectoryInfo subInfo in info.GetDirectories())
                    {
                        
                        ftpDir.SubDirectorys.Add(new DirectoryDetail(subInfo.Name ,subInfo.CreationTime));
                    }
                }
            }
            catch (Exception ee)
            {
                ftpDir.Valid = false;
                ftpDir.exception = ee.Message;
            }

            return ftpDir;
        }

        #region Valid
        private bool valid = true;
        /// <summary>
        /// Valid 表示获取目录信息是否成功。如果不成功，则Exception属性指出了失败的原因。
        /// </summary>
        public bool Valid
        {
            get { return valid; }
            set { valid = value; }
        }
        #endregion

        #region Exception
        private string exception;
        /// <summary>
        /// Exception 当获取目录信息失败时，该属性指出了失败的原因。
        /// </summary>
        public string Exception
        {
            get { return exception; }
            set { exception = value; }
        }
        #endregion

        #region DirectoryPath
        private string directoryPath = null;
        public string DirectoryPath
        {
            get { return directoryPath; }
            set { directoryPath = value; }
        }
        #endregion

        #region FileList
        private List<FileDetail> fileList = new List<FileDetail>(); //元素为FileDetail 
        public List<FileDetail> FileList
        {
            get
            {
                return this.fileList;
            }
            set
            {
                this.fileList = value;
            }
        }
        #endregion

        #region SubDirectorys
        private List<DirectoryDetail> subDirectorys = new List<DirectoryDetail>(); //元素为Directory的名称
        public List<DirectoryDetail> SubDirectorys
        {
            get
            {
                return this.subDirectorys;
            }
            set
            {
                this.subDirectorys = value;
            }
        }
        #endregion

        #region GetChildPath
        /// <summary>
        /// GetChildPath 获取当前目录下的某目录或文件的路径。
        /// </summary>       
        public string GetChildPath(string name)
        {
            return string.Format("{0}\\{1}", this.directoryPath, name);
        }
        #endregion

        #region DriveList
        private List<DiskDrive> driveList = new List<DiskDrive>();
        public List<DiskDrive> DriveList
        {
            get { return driveList; }
            set { driveList = value; }
        }
        #endregion
    }

    /// <summary>
    /// 文件的详细信息。
    /// </summary>
	[Serializable]
	public class FileDetail :IComparable<FileDetail>
	{
		public FileDetail(){}
		public FileDetail(string fileName ,long fileSize ,DateTime create)
		{
			this.name = fileName ;
			this.size = fileSize ;
            this.createTime = create;
		}

		#region Name
		private string name = "" ; 
		public string Name
		{
			get
			{
				return this.name ;
			}
			set
			{
				this.name = value ;
			}
		}
		#endregion
		
		#region Size
		private long size = 0 ; 
		public long Size
		{
			get
			{
				return this.size ;
			}
			set
			{
				this.size = value ;
			}
		}
		#endregion

        #region CreateTime
        private DateTime createTime = DateTime.Now;
        /// <summary>
        /// 文件的创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        } 
        #endregion

        #region IComparable<FileDetail> 成员

        public int CompareTo(FileDetail other)
        {
            return this.name.CompareTo(other.name);
        }

        #endregion
    }

    /// <summary>
    /// 目录的详细信息。
    /// </summary>
    [Serializable]
    public class DirectoryDetail : IComparable<DirectoryDetail>
    {
        public DirectoryDetail(){}
        public DirectoryDetail(string fileName, DateTime create)
		{
			this.name = fileName ;			
            this.createTime = create;
		}

        #region Name
        private string name = "";
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        #endregion       

        #region CreateTime
        private DateTime createTime = DateTime.Now;
        /// <summary>
        /// 目录的创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }
        #endregion

        #region IComparable<FileDetail> 成员

        public int CompareTo(DirectoryDetail other)
        {
            return this.name.CompareTo(other.name);
        }

        #endregion

    }

    /// <summary>
    /// 磁盘/光驱等设备的详细信息。
    /// </summary>
    public class DiskDrive :IComparable<DiskDrive>
    {
        public DiskDrive() { }
        public DiskDrive(DriveInfo info)
        {
            this.name = info.Name;
            this.isReady = info.IsReady;
            this.driveType = info.DriveType;
            
            if (info.IsReady)
            {
                this.volumeLabel = info.VolumeLabel;
                this.availableFreeSpace = (ulong)info.AvailableFreeSpace;
                this.totalSize = (ulong)info.TotalSize;               
            }
        }

        #region Name
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        } 
        #endregion

        #region IsReady
        private bool isReady = true;
        public bool IsReady
        {
            get { return isReady; }
            set { isReady = value; }
        } 
        #endregion

        #region DriveType
        private DriveType driveType = DriveType.Fixed;
        public DriveType DriveType
        {
            get { return driveType; }
            set { driveType = value; }
        } 
        #endregion

        #region AvailableFreeSpace
        private ulong availableFreeSpace;
        public ulong AvailableFreeSpace
        {
            get { return availableFreeSpace; }
            set { availableFreeSpace = value; }
        } 
        #endregion

        #region TotalSize
        private ulong totalSize;
        public ulong TotalSize
        {
            get { return totalSize; }
            set { totalSize = value; }
        } 
        #endregion

        #region VolumeLabel
        private string volumeLabel;
        public string VolumeLabel
        {
            get { return volumeLabel; }
            set { volumeLabel = value; }
        } 
        #endregion

        #region IComparable<DiskDrive> 成员

        public int CompareTo(DiskDrive other)
        {
            return this.name.CompareTo(other.name);
        }

        #endregion
    }
}
