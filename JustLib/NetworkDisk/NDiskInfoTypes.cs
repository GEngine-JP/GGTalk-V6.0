using System;
using System.Collections.Generic;
using ESPlus.Application.CustomizeInfo;

namespace JustLib.NetworkDisk
{
	/// <summary>
    /// 所有与网盘相关的消息类型的空间。默认取值 -100 ~ 0
	/// </summary>	
    public class NDiskInfoTypes : BaseInformationTypes
	{
        #region Ctor
        public NDiskInfoTypes()
            : this(-100)
        {
        }
        public NDiskInfoTypes(int _startKey)
        {
            base.StartKey = _startKey;
            base.Initialize();
        } 
        #endregion

        #region ReqDirectory 
        private int reqDirectory = 1;
        /// <summary>
        /// 请求目录信息。请求协议为ReqDirectoryContract，回复为ResDirectoryContract
        /// </summary>
        public int ReqDirectory
		{
			get
			{
                return this.reqDirectory;
			}
			set
			{
                this.reqDirectory = value;
			}
		}
		#endregion        

        #region CreateDirectory
        private int createDirectory = 2;
        /// <summary>
        /// 创建目录。
        /// </summary>
        public int CreateDirectory
		{
			get
			{
                return this.createDirectory;
			}
			set
			{
                this.createDirectory = value;
			}
		}
		#endregion

        #region Delete
        private int delete = 3;
        /// <summary>
        /// 删除文件或文件夹。请求协议DeleteContract，回复协议OperationResultConatract。
        /// </summary>
        public int Delete
		{
			get
			{
                return this.delete;
			}
			set
			{
                this.delete = value;
			}
		}
		#endregion        

        #region Rename
        private int rename = 4;
        /// <summary>
        /// 为文件或文件夹重命名。请求协议RenameContract，回复协议OperationResultConatract。
        /// </summary>
        public int Rename
        {
            get { return rename; }
            set { rename = value; }
        } 
        #endregion

        #region ReqNetworkDiskState
        private int reqNetworkDiskState = 5;
        /// <summary>
        /// 获取网络硬盘的状态，请求协议为null，回复协议为ResNetworkDiskStateContract。
        /// </summary>
        public int ReqNetworkDiskState
        {
            get { return reqNetworkDiskState; }
            set { reqNetworkDiskState = value; }
        } 
        #endregion        

        #region Move
        private int move = 6;
        /// <summary>
        /// 移动多个文件或文件夹。请求协议为MoveContract，回复协议OperationResultConatract。
        /// </summary>
        public int Move
        {
            get { return move; }
            set { move = value; }
        } 
        #endregion        

        #region Copy
        private int copy = 7;
        /// <summary>
        /// 复制多个文件或文件夹。请求协议为CopyContract，回复协议OperationResultConatract。
        /// </summary>
        public int Copy
        {
            get { return copy; }
            set { copy = value; }
        } 
        #endregion       

        #region Download
        private int download = 8;
        /// <summary>
        /// 请求下载文件（夹）。请求协议为DownloadContract，回复协议OperationResultConatract。
        /// </summary>
        public int Download
        {
            get
            {
                return this.download;
            }
            set
            {
                this.download = value;
            }
        }
        #endregion        
	}
}
