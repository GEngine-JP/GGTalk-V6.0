using System;
using System.Collections.Generic;
using System.Text;

namespace JustLib.NetworkDisk.Passive
{
    /// <summary>
    /// 统合了好友远程磁盘以及虚拟网络硬盘的访问。当ownerID为null，表示访问自己在服务器上的网盘。
    /// </summary>
    public interface INDiskOutter
    {
        /// <summary>
        /// GetSharedDirectory 获取ownerID机器上的路径为dirPath的目录信息。
        /// </summary>
        /// <param name="ownerID">目标机器的主人ID。当ownerID为NetServer.SystemUserID，表示访问自己的网络硬盘。</param>
        /// <param name="ownerID">网络磁盘的唯一标志。</param>
        /// <param name="dirPath">父目录路径，如果为null，表示获取根目录（如“我的电脑”）下的磁盘或目录</param>       
        SharedDirectory GetSharedDirectory(string ownerID, string netDiskID, string dirPath);

        //bool DirOrFileIsExist(string ownerID, string dirOrFilePath ,bool isFile);

        /// <summary>
        /// 获取自己的网络硬盘的状态。
        /// </summary>        
        NetworkDiskState GetNetworkDiskState(string netDiskID);

        void CreateDirectory(string ownerID, string netDiskID, string parentDirectoryPath, string newDirName);

        OperationResult Rename(string ownerID, string netDiskID, string parentDirectoryPath, bool isFile, string oldName, string newName);

        OperationResult Delete(string ownerID, string netDiskID, string sourceParentDirectoryPath, IList<string> filesBeDeleted, IList<string> directoriesBeDeleted);

        /// <summary>
        /// 移动多个文件或文件夹。当远程目录完成操作时，该方法才返回。
        /// </summary>        
        /// <param name="ownerID">网络磁盘的唯一标志。</param>
        /// <param name="oldParentDirectoryPath">被移动的物件所在目录的路径,以"\"结尾</param>     
        /// <param name="filesBeMoved">被移动的文件名称的集合</param>
        /// <param name="directoriesBeMoved">被移动的文件夹名称的集合</param>
        /// <param name="newParentDirectoryPath">目标文件夹的路径,以"\"结尾</param>
        OperationResult Move(string ownerID, string netDiskID, string oldParentDirectoryPath, IList<string> filesBeMoved, IList<string> directoriesBeMoved, string newParentDirectoryPath);

        /// <summary>
        /// 复制多个文件（夹）。当远程目录完成操作时，该方法才返回。
        /// </summary>        
        /// <param name="ownerID">网络磁盘的唯一标志。</param>
        /// <param name="sourceParentDirectoryPath">被复制的父目录路径,以"\"结尾</param>        
        /// <param name="filesBeCopyed">被复制的文件名称的集合</param>
        /// <param name="directoriesCopyed">被复制的文件夹名称的集合</param>
        /// <param name="destParentDirectoryPath">复制品放到哪个目录下,以"\"结尾</param>       
        OperationResult Copy(string ownerID, string netDiskID, string sourceParentDirectoryPath, IList<string> filesBeCopyed, IList<string> directoriesCopyed, string destParentDirectoryPath);

        /// <summary>
        /// UploadFile 上传文件（夹），借助于IFileOutter发送文件（夹）。如果要上传的文件（夹）不存在或被占用，则将抛出对应的异常。
        /// </summary>       
        void Upload(string ownerID, string netDiskID, string sourceLocalPath, string newDestPath);

        /// <summary>
        /// 请求下载文件（夹）。
        /// </summary>
        /// <param name="ownerID">目标机器的主人ID。当ownerID为null，表示访问自己的网络硬盘。</param>
        /// <param name="ownerID">网络磁盘的唯一标志。</param>
        /// <param name="sourceRemotePath">被下载文件（夹）的路径</param>
        /// <param name="saveLocalPath">保存本地文件（夹）的路径</param>
        /// <param name="isFile">下载的是文件还是文件夹</param>
        /// <returns>如果因目标文件（夹）不存在或者被占用而不能下载，会由返回的对象的ErrorMessage表明</returns>
        OperationResult Download(string ownerID, string netDiskID, string sourceRemotePath, string saveLocalPath, bool isFile);

    }
}
