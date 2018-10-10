using System;
using System.Collections.Generic;
using System.Text;
using ESPlus.Rapid;

using System.IO;
using GGTalk.Core;
using ESPlus.Serialization;

namespace GGTalk.Server
{
    /// <summary>
    /// 离线文件控制器 V3.2。
    /// </summary>
    internal class OfflineFileController
    {
        private IRapidServerEngine rapidServerEngine;
        private GlobalCache globalCache;
        public OfflineFileController(IRapidServerEngine engine, GlobalCache db)
        {
            this.rapidServerEngine = engine;
            this.globalCache = db;
            this.rapidServerEngine.FileController.FileRequestReceived += new ESPlus.Application.FileTransfering.CbFileRequestReceived(FileController_FileRequestReceived);
            this.rapidServerEngine.FileController.FileReceivingEvents.FileTransCompleted += new ESBasic.CbGeneric<ESPlus.FileTransceiver.TransferingProject>(FileReceivingEvents_FileTransCompleted);
            this.rapidServerEngine.FileController.FileSendingEvents.FileTransCompleted += new ESBasic.CbGeneric<ESPlus.FileTransceiver.TransferingProject>(FileSendingEvents_FileTransCompleted);
            this.rapidServerEngine.FileController.FileResponseReceived += new ESBasic.CbGeneric<ESPlus.FileTransceiver.TransferingProject, bool>(FileController_FileResponseReceived);
        }

        void FileController_FileResponseReceived(ESPlus.FileTransceiver.TransferingProject project, bool agree)
        {
            string senderID = Comment4OfflineFile.ParseUserID(project.Comment);
            if (senderID == null)
            {
                return;
            }

            if (!agree) //客户端拒绝接收离线文件
            {
                File.Delete(project.OriginPath); //删除在服务端保存的离线文件

                //通知发送方
                OfflineFileResultNotifyContract contract = new OfflineFileResultNotifyContract(project.AccepterID, project.ProjectName, false);
                this.rapidServerEngine.CustomizeController.Send(senderID, InformationTypes.OfflineFileResultNotify, CompactPropertySerializer.Default.Serialize<OfflineFileResultNotifyContract>(contract));
       
            }
        }

        //离线文件发送给接收者完成
        void FileSendingEvents_FileTransCompleted(ESPlus.FileTransceiver.TransferingProject project)
        {
            string senderID = Comment4OfflineFile.ParseUserID(project.Comment);
            if (senderID == null)
            {
                return;
            }
            File.Delete(project.OriginPath);

            //通知发送方
            OfflineFileResultNotifyContract contract = new OfflineFileResultNotifyContract(project.AccepterID, project.ProjectName, true);
            this.rapidServerEngine.CustomizeController.Send(senderID, InformationTypes.OfflineFileResultNotify, CompactPropertySerializer.Default.Serialize<OfflineFileResultNotifyContract>(contract));
        }

        //发送者上传离线文件完成
        void FileReceivingEvents_FileTransCompleted(ESPlus.FileTransceiver.TransferingProject project)
        {
            string accepterID = Comment4OfflineFile.ParseUserID(project.Comment);
            if (accepterID == null)
            {
                return;
            }

            OfflineFileItem item = new OfflineFileItem();
            item.AccepterID = accepterID;
            item.FileLength = project.TotalSize;
            item.FileName = project.ProjectName;
            item.SenderID = project.SenderID;
            item.RelayFilePath = project.LocalSavePath;

            if (this.rapidServerEngine.UserManager.IsUserOnLine(accepterID)) //如果接收者在线，则直接转发离线文件
            {
                string newProjectID = null;
                this.rapidServerEngine.FileController.BeginSendFile(item.AccepterID, item.RelayFilePath, Comment4OfflineFile.BuildComment(item.SenderID), out newProjectID);                  
            }
            else
            {                
                this.globalCache.StoreOfflineFileItem(item);
            }
        }

        //发送者请求上传离线文件
        void FileController_FileRequestReceived(string projectID, string senderID, string fileName, ulong totalSize, ESPlus.FileTransceiver.ResumedProjectItem resumedFileItem, string comment)
        {
            string accepterID = Comment4OfflineFile.ParseUserID(comment);
            if (accepterID == null)
            {
                return;
            }

            string saveFileDir = AppDomain.CurrentDomain.BaseDirectory + "\\OfflineFiles\\";//根据某种策略得到存放文件的路径
            if (!Directory.Exists(saveFileDir))
            {
                Directory.CreateDirectory(saveFileDir);
            }

            saveFileDir = string.Format("{0}\\{1}\\", saveFileDir, accepterID);
            if (!Directory.Exists(saveFileDir))
            {
                Directory.CreateDirectory(saveFileDir);
            }
            string saveFilePath = saveFileDir + fileName;            

            //给客户端回复同意，并开始准备接收文件。
            this.rapidServerEngine.FileController.BeginReceiveFile(projectID, saveFilePath);
        }

        /// <summary>
        /// 向目标用户发送离线文件。
        /// </summary>       
        public void SendOfflineFile(string accepterID)
        {
            List<OfflineFileItem> list = this.globalCache.PickupOfflineFileItem(accepterID);
            if (list != null)
            {
                foreach (OfflineFileItem item in list)
                {
                    string projectID = null;
                    this.rapidServerEngine.FileController.BeginSendFile(item.AccepterID, item.RelayFilePath, Comment4OfflineFile.BuildComment(item.SenderID), out projectID);   
                }
            }

        }
    }
}
