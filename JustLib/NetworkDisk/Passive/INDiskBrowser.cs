using System;
using ESPlus.Application.FileTransfering.Passive;

namespace JustLib.NetworkDisk.Passive
{
    public interface INDiskBrowser
    {
        /// <summary>
        /// 是否允许上传文件夹
        /// </summary>
        bool AllowUploadFolder { get; set; }

        /// <summary>
        /// 当前所在目录的路径。
        /// </summary>
        string CurrentDirectoryPath { get; }

        /// <summary>
        /// OwnerIsOnline 远程机器是否在线。
        /// </summary>
        bool Connected { get; set; }

        /// <summary>
        /// 是否有文件正在传送中。
        /// </summary>
        bool IsFileTransfering { get; }

        void Initialize(string _ownerID, IFileOutter _fileOutter, INDiskOutter _fileDirectoryOutter);

        /// <summary>
        /// 取消所有正在传送的项目。通常是在窗口被关闭时调用。
        /// </summary>
        void CancelAllTransfering();
        
    }
}
