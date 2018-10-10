using System;
using ESBasic;
using ESPlus.FileTransceiver;
using System.Collections.Generic;

namespace GGTalk.Controls
{
    /// <summary>
    /// 文件传送查看器。用于查看与某个好友的所有文件传送的进度状态信息。
    /// 注意，FileTransferingViewer的所有事件都是在UI线程中触发的。
    /// </summary>
    public interface IFileTransferingViewer
    {
        /// <summary>
        /// 当某个文件开始续传时，触发该事件。参数为FileName - isSending
        /// </summary>
        event CbGeneric<string, bool> FileResumedTransStarted;

        /// <summary>
        /// 当某个文件传送完毕时，触发该事件。参数为FileName - isSending - comment - isFolder
        /// </summary>
        event CbGeneric<string, bool, string, bool> FileTransCompleted;

        /// <summary>
        /// 当某个文件传送中断时，触发该事件。参数为FileName - isSending - FileTransDisrupttedType
        /// </summary>
        event CbGeneric<string, bool, FileTransDisrupttedType> FileTransDisruptted;

        /// <summary>
        /// 当某个文件传送开始时，触发该事件。参数为FileName - isSending
        /// </summary>
        event CbGeneric<string, bool> FileTransStarted;

        /// <summary>
        /// 当所有文件都传送完成时，触发该事件。
        /// </summary>
        event CbSimple AllTaskFinished;      

        /// <summary>
        /// 当前是否有文件正在传送中。
        /// </summary>        
        bool IsFileTransfering { get; }

        /// <summary>
        /// 当收到发送文件的请求时,接收方调用此方法显示fileTransferItem。
        /// </summary>
        /// <param name="projectID">传送项目的ID</param>
        /// <param name="offlineFile">是否为离线文件</param>
        /// <param name="doneAccepted">如果当前是接收方，是否已经同意接收了？（不显示“接收”、“拒绝”按钮？）</param>   
        void NewFileTransferItem(string projectID, bool offlineFile, bool doneAgreed);

        /// <summary>
        /// 获取所有正在传送的项目的ID集合。
        /// </summary>        
        List<string> GetTransferingProjectIDsInCurrentViewer();

        /// <summary>
        /// 在该控件释放之前（比如在其宿主窗体的FormClosing事件中），一定要调用此方法！
        /// </summary>
        void BeforeDispose();
    }
}
