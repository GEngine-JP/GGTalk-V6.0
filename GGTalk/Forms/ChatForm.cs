using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CCWin;
using CCWin.SkinControl;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using ESPlus.Rapid;
using ESBasic.ObjectManagement.Forms;
using ESPlus.Application.P2PSession.Passive;
using ESPlus.FileTransceiver;
using ESBasic;
using OMCS.Passive;
using ESBasic.Helpers;
using ESPlus.Serialization;
using JustLib;
using GGTalk.Core;
using ESPlus.Application;
using GGTalk.Controls;
using JustLib.Records;
using JustLib.NetworkDisk.Passive;
using JustLib.NetworkDisk;


namespace GGTalk
{
    /// <summary>
    /// 与好友进行聊天的窗口。
    /// </summary>
    public partial class ChatForm : BaseForm, IManagedForm<string>, IChatForm
    {
        private Font messageFont = new Font("微软雅黑", 9);
        private INDiskOutter nDiskOutter; // V2.0         
        private VideoForm videoForm = null;
        private RemoteHelpForm remoteHelpForm = null;
        private string Title_FileTransfer = "文件";
        private string Title_Video = "视频";
        private string Title_Disk = "磁盘";
        private string Title_RemoteHelp = "远程协助";
        private string Title_RemoteHelpHandle = "远程协助中";
        private string Title_Audio = "语音对话";       

        private FileTransferingViewer fileTransferingViewer = new FileTransferingViewer();
        private VideoRequestPanel videoRequestPanel = new VideoRequestPanel();
        private DiskRequestPanel diskRequestPanel = new DiskRequestPanel();
        private RemoteHelpRequestPanel remoteHelpRequestPanel = new RemoteHelpRequestPanel();
        private RemoteHelpHandlePanel remoteHelpHandlePanel = new RemoteHelpHandlePanel();     
        private AudioHandlePanel audioHandlePanel = new AudioHandlePanel();
        private EmotionForm emotionForm;
        private IRapidPassiveEngine rapidPassiveEngine;
        private GlobalUserCache globalUserCache;
      
        private GGUser currentFriend;
        private GGUser mine;
        private System.Windows.Forms.Timer inputingTimer = new System.Windows.Forms.Timer();

        private LastWordsRecord lastWordsRecord = null;
        public event CbGeneric<bool ,string, LastWordsRecord> LastWordChanged; //isGroup - friendID/groupID - content 

        #region IManagedForm Member
        public string FormID
        {
            get { return this.currentFriend.UserID; }
        } 
        #endregion

        #region Ctor
        private bool FilterTransferingProject(TransferingProject pro)
        {
            var para = Comment4NDisk.Parse(pro.Comment);
            if (para != null)
            {
                return false;
            }

            if (ESFramework.NetServer.IsServerUser(pro.DestUserID))
            {
                var offlineFileSenderID = Comment4OfflineFile.ParseUserID(pro.Comment);
                return offlineFileSenderID == this.currentFriend.ID;
            }

            return pro.DestUserID == this.currentFriend.ID;
        }

        public ChatForm(IRapidPassiveEngine engine, INDiskOutter diskOutter, GlobalUserCache cache, GGUser myself, GGUser friend)
        {
            this.rapidPassiveEngine = engine;          
            this.nDiskOutter = diskOutter;
            this.globalUserCache = cache;
            this.currentFriend = friend;
            this.mine = myself;
           
            InitializeComponent();

            this.chatBoxSend.Initialize(GlobalResourceManager.EmotionDictionary);
            this.chatBox_history.Initialize(GlobalResourceManager.EmotionDictionary);
            this.chatBoxSend.Font = SystemSettings.Singleton.Font;
            this.chatBoxSend.ForeColor = SystemSettings.Singleton.FontColor;
            this.Size = SystemSettings.Singleton.ChatFormSize;
           
            this.chatBoxSend.EnableAutoDragDrop = false;
            this.chatBoxSend.AllowDrop = true;
            this.chatBoxSend.FileOrFolderDragDrop += new CbGeneric<string[]>(chatBoxSend_FileOrFolderDragDrop);                       

            this.OnFriendInfoChanged(this.currentFriend);
            this.OnMyInfoChanged(myself);
            this.chatBoxSend.Focus();          

            this.emotionForm = new EmotionForm();
            this.emotionForm.Load += new EventHandler(emotionForm_Load);
            this.emotionForm.Initialize(GlobalResourceManager.EmotionList);
            this.emotionForm.EmotionClicked += new CbGeneric<int, Image>(emotionForm_Clicked);
            this.emotionForm.Visible = false;
            this.emotionForm.LostFocus += new EventHandler(emotionForm_LostFocus);

            this.rapidPassiveEngine.P2PController.P2PChannelOpened += new CbGeneric<P2PChannelState>(P2PController_P2PChannelOpened);
            this.rapidPassiveEngine.P2PController.P2PChannelClosed += new CbGeneric<P2PChannelState>(P2PController_P2PChannelClosed);

            //文件传送
            this.fileTransferingViewer.Initialize(this.rapidPassiveEngine.FileOutter, this.FilterTransferingProject);
            this.fileTransferingViewer.FileTransDisruptted += new CbGeneric<string, bool, FileTransDisrupttedType>(fileTransferingViewer1_FileTransDisruptted);
            this.fileTransferingViewer.FileTransCompleted += new CbGeneric<string, bool ,string,bool>(fileTransferingViewer1_FileTransCompleted);
            this.fileTransferingViewer.FileResumedTransStarted += new CbGeneric<string, bool>(fileTransferingViewer1_FileResumedTransStarted);
            this.fileTransferingViewer.AllTaskFinished += new CbSimple(fileTransferingViewer1_AllTaskFinished);

            //语音
            this.audioHandlePanel.Initialize(this.currentFriend.UserID);
            this.audioHandlePanel.AudioRequestAnswerd += new CbGeneric<bool>(audioHandlePanel_AudioRequestAnswerd);
            this.audioHandlePanel.AudioTerminated += new CbGeneric(audioHandlePanel_AudioTerminated);
            //视频
            this.videoRequestPanel.VideoRequestAnswerd += new CbGeneric<bool>(videoRequestPanel_VideoRequestAnswerd);
            //磁盘 V2.0
            this.diskRequestPanel.DiskRequestAnswerd += new CbGeneric<bool>(diskRequestPanel_DiskRequestAnswerd);
            //远程协助 V2.2
            this.remoteHelpRequestPanel.RemoteHelpRequestAnswerd += new CbGeneric<bool ,RemoteHelpStyle>(remoteHelpRequestPanel_RemoteHelpRequestAnswerd);
            this.remoteHelpHandlePanel.RemoteHelpTerminated += new CbGeneric(remoteHelpHandlePanel_RemoteHelpTerminated);
            this.ShowP2PState();

            this.inputingTimer.Interval = 1000;
            this.inputingTimer.Tick += new EventHandler(timer_Tick);
            this.inputingTimer.Start();

            var icon = this.currentFriend.GetHeadIcon(GlobalResourceManager.HeadImages);
            if (icon != null)
            {
                this.UseCustomIcon = true;
                this.Icon = icon;
            }

            if (SystemSettings.Singleton.LoadLastWordsWhenChatFormOpened)
            {
                var record = this.currentFriend.Tag as LastWordsRecord ;
                if(record != null)
                {
                    this.AppendChatBoxContent(record.IsMe ? this.mine.Name : this.currentFriend.Name, record.SpeakTime, record.ChatBoxContent, record.IsMe ? Color.SeaGreen : Color.Blue, false,false);
                }
            }
        }
              

        void timer_Tick(object sender, EventArgs e)
        {
            this.CheckInptingVisiable();
        }

        private DateTime dtLastSendInptingNotify = DateTime.Now;
        private void textBoxSend_TextChanged(object sender, EventArgs e)
        {
            if (this.chatBoxSend.TextLength == 0)
            {
                return;
            }

            if ((DateTime.Now - this.dtLastSendInptingNotify).TotalSeconds <= 5)
            {
                return;
            }

            //0923
            this.dtLastSendInptingNotify = DateTime.Now; //20150316
            this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.InputingNotify, null, true, ESFramework.ActionTypeOnChannelIsBusy.Discard);
        }

        private DateTime dtInptingVisiableShowTime = DateTime.Now;
        public void OnInptingNotify()
        {
            this.skinLabel_inputing.Visible = true;
            this.dtInptingVisiableShowTime = DateTime.Now;
        }

        private void CheckInptingVisiable()
        {
            if (!this.skinLabel_inputing.Visible)
            {
                return;
            }

            if ((DateTime.Now - this.dtInptingVisiableShowTime).TotalSeconds >= 10)
            {
                this.skinLabel_inputing.Visible = false;
            }
        }
        void chatBoxSend_FileOrFolderDragDrop(string[] fileOrDirs)
        {
            foreach (var fileOrDirPath in fileOrDirs)
            {
                string projectID;
                var sendingFileParas = new SendingFileParas(2048, 0);//文件数据包大小，可以根据网络状况设定，局网内可以设为204800，传输速度可以达到30M/s以上；公网建议设定为2048或4096或8192
                this.rapidPassiveEngine.FileOutter.BeginSendFile(this.currentFriend.UserID, fileOrDirPath, null, sendingFileParas, out projectID);
                this.FileRequestReceived(projectID);             
            }             
        }        

        void emotionForm_LostFocus(object sender, EventArgs e)
        {
            this.emotionForm.Visible = false;
        }

        public void OnFriendInfoChanged(GGUser friend)
        {
            this.currentFriend = friend;
            this.Text = "与 " + this.currentFriend.Name + " 对话中";
            this.labelFriendName.Text = this.currentFriend.Name;
            this.skinLabel_inputing.Location = new Point(this.labelFriendName.Location.X + this.labelFriendName.Width -5, this.skinLabel_inputing.Location.Y);
            this.labelFriendSignature.Text = this.currentFriend.Signature;
            this.panelFriendHeadImage.BackgroundImage = GlobalResourceManager.GetHeadImage(this.currentFriend);           
            this.skinLabel_FriendID.Text = this.currentFriend.UserID;
            this.skinLabel_FriendName.Text = this.currentFriend.Name;
            this.skinPanel_friend.BackgroundImage = GlobalResourceManager.GetHeadImageOnline(this.currentFriend);
        
            this.skinPanel_status.BackgroundImage = GlobalResourceManager.GetStatusImage(friend.UserStatus);
            this.skinPanel_status.Visible = !(friend.OfflineOrHide || friend.UserStatus == UserStatus.Online);
            this.toolShow.SetToolTip(this.panelFriendHeadImage, "状态：" + GlobalResourceManager.GetUserStatusName(friend.UserStatus));
        }

        public void OnRemovedFromFriend()
        {
            MessageBoxEx.Show("对方已经将您从好友列表中移除。");
            this.Close();
        }

        public void OnMyInfoChanged(GGUser my)
        {
            //this.skinLabel_myID.Text = my.UserID;
            //this.skinLabel_myName.Text = my.Name;     
            this.skinPanel_mine.BackgroundImage = GlobalResourceManager.GetHeadImage(my);
        }

        void emotionForm_Load(object sender, EventArgs e)
        {
            this.emotionForm.Location = new Point((this.Left + 30) - (this.emotionForm.Width / 2), this.Top + skToolMenu.Top - this.emotionForm.Height);
        }

        void emotionForm_Clicked(int imgIndex, Image img)
        {
            this.chatBoxSend.InsertDefaultEmotion((uint)imgIndex);
            this.emotionForm.Visible = false;
        } 
        #endregion

        #region HandleReceivedMessage
        public void HandleReceivedMessage(List<Parameter<int, byte[], object>> messageList)
        {
            foreach (var para in messageList)
            {
                this.HandleReceivedMessage2(para.Arg1, para.Arg2, para.Arg3, false);
            }
        }

        public void HandleReceivedMessage(int informationType, byte[] info, object tag)
        {
            this.HandleReceivedMessage2(informationType, info, tag, true);
        }

        public void FlashChatWindow(bool flash)
        {
            if (flash)
            {
                JustHelper.FlashChatWindow(this);
            }
        }

        private void HandleReceivedMessage2(int informationType, byte[] info, object tag, bool flash)
        {
            GlobalResourceManager.UiSafeInvoker.ActionOnUI<int, byte[], object, bool>(this.DoHandleReceivedMessage, informationType, info, tag, flash);
        }

        private void DoHandleReceivedMessage(int informationType, byte[] info, object tag, bool flash)
        {
            if (informationType == InformationTypes.Chat)
            {
                var content = (ChatBoxContent)tag;
                this.OnReceivedMsg(content, null);
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.FriendAddedNotify)
            {
                this.AppendSysMessage("对方添加您为好友，可以开始对话了...");
                return;
            } 

            if (informationType == InformationTypes.OfflineMessage)
            {
                var para = (Parameter<ChatBoxContent, DateTime>)tag;
                this.OnReceivedMsg(para.Arg1, para.Arg2);
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.OfflineFileResultNotify)
            {
                var contract = CompactPropertySerializer.Default.Deserialize<OfflineFileResultNotifyContract>(info, 0);
                this.OnReceivedOfflineFileResultNotify(contract.FileName, contract.Accept);
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.Vibration)
            {
                this.OnViberation();
                return;
            }

            if (informationType == InformationTypes.VideoRequest)
            {
                this.OnVideoRequestReceived();
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.AgreeVideo)
            {
                this.OnVideoAnswerReceived(true);
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.RejectVideo)
            {
                this.OnVideoAnswerReceived(false);
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.HungUpVideo)
            {
                this.OnVideoHungUpReceived();
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.DiskRequest)
            {
                this.OnDiskRequestReceived();
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.AgreeDisk)
            {
                this.OnDiskAnswerReceived(true);
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.RejectDisk)
            {
                this.OnDiskAnswerReceived(false);
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.RemoteHelpRequest)
            {
                var style = (RemoteHelpStyle)BitConverter.ToInt32(info, 0);
                this.OnRemoteHelpRequestReceived(style);
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.AgreeRemoteHelp)
            {
                this.OnRemoteHelpAnswerReceived(true);
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.RejectRemoteHelp)
            {
                this.OnRemoteHelpAnswerReceived(false);
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.CloseRemoteHelp)
            {
                this.OnHelperCloseRemoteHelp();
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.TerminateRemoteHelp)
            {
                this.OnOwnerTerminateRemoteHelp();
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.AudioRequest)
            {
                this.OnAudioRequestReceived();
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.AgreeAudio)
            {
                this.OnAudioAnswerReceived(true);
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.RejectAudio)
            {
                this.OnAudioAnswerReceived(false);
                this.FlashChatWindow(flash);
                return;
            }

            if (informationType == InformationTypes.HungupAudio)
            {
                this.OnAudioHungUpReceived();
                this.FlashChatWindow(flash);
                return;
            }
        }
        #endregion

        #region P2P 通道状态
        void P2PController_P2PChannelClosed(P2PChannelState state)
        {
            this.ShowP2PState();
        }

        void P2PController_P2PChannelOpened(P2PChannelState state)
        {
            this.ShowP2PState();
        }

        public void ShowP2PState()
        {
            GlobalResourceManager.UiSafeInvoker.ActionOnUI(this.do_ShowP2PState);
        }

        private void do_ShowP2PState()
        {
            var state = this.rapidPassiveEngine.P2PController.GetP2PChannelState(this.currentFriend.UserID);
            if (state != null)
            {
                this.Text = string.Format("与 {0} 对话中...【P2P直连/{1}】", this.currentFriend.Name, state.ProtocolType);
                this.skinLabel_p2PState.Text = string.Format("P2P直连/{0}", state.ProtocolType);
                this.pictureBox_state.Visible = true;
            }
            else
            {
                this.Text = string.Format("与 {0} 对话中...", this.currentFriend.Name);
                this.skinLabel_p2PState.Text = "";
                this.pictureBox_state.Visible = false;
            }
        }
        #endregion

        #region 自己掉线，好友状态改变

        public void FriendStateChanged(UserStatus newStatus)
        {
            this.OnFriendInfoChanged(this.currentFriend);
            if (newStatus == UserStatus.OffLine)
            {
                GlobalResourceManager.UiSafeInvoker.ActionOnUI(this.FriendOffline);
            }
        }

        /// <summary>
        /// 自己掉线
        /// </summary>
        public void MyselfOffline()
        {
            this.OnOffline(true);
        }       

        /// <summary>
        /// 好友掉线
        /// </summary>
        private void FriendOffline()
        {
            this.OnFriendInfoChanged(this.currentFriend);
            this.OnOffline(false);
        }

        private void OnOffline(bool myself)
        {
            if (this.videoForm != null)
            {
                this.videoForm.OnHungUpVideo();
                var msg = string.Format("{0}已经掉线，与对方的视频会话中止。" ,myself? "自己" : "对方");
                this.AppendSysMessage(msg);
            }

            if (this.TabControlContains(this.Title_Disk))
            {
                this.skinTabControl1.TabPages.RemoveAt(this.GetSelectIndex(this.Title_Disk));
                this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
            }

            if (this.TabControlContains(this.Title_RemoteHelpHandle))
            {
                this.skinTabControl1.TabPages.RemoveAt(this.GetSelectIndex(this.Title_RemoteHelpHandle));
                this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;                
            }

            if (this.TabControlContains(this.Title_RemoteHelp))
            {
                this.skinTabControl1.TabPages.RemoveAt(this.GetSelectIndex(this.Title_RemoteHelp));
                this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
            }

            if (this.TabControlContains(this.Title_Audio))
            {
                if (this.audioHandlePanel.IsWorking)
                {
                    var msg = string.Format("{0}已经掉线，与对方的语音对话中止。", myself ? "自己" : "对方");
                    this.AppendSysMessage(msg);
                }
                else
                {
                    var msg = string.Format("{0}已经掉线!", myself ? "自己" : "对方");
                    this.AppendSysMessage(msg);
                }

                this.skinTabControl1.TabPages.RemoveAt(this.GetSelectIndex(this.Title_Audio));
                this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
            }

            this.ResetTabControVisible();
        }
        #endregion        

        #region TabControlContains
        private bool TabControlContains(string text)
        {
            for (var i = 0; i < this.skinTabControl1.TabPages.Count; i++)
            {
                if (this.skinTabControl1.TabPages[i].Text == text)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region GetSelectIndex
        private int GetSelectIndex(string text)
        {
            for (var i = 0; i < this.skinTabControl1.TabPages.Count; i++)
            {
                if (this.skinTabControl1.TabPages[i].Text == text)
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        #region 文件传输

        #region 发送 传输文件的请求
        private void SendFileOrFolder(bool isFolder)
        {
            if (this.mine.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            try
            {
                string fileOrFolderPath = null;
                if (isFolder)
                {
                    fileOrFolderPath = ESBasic.Helpers.FileHelper.GetFolderToOpen(false);
                }
                else
                {
                    fileOrFolderPath = ESBasic.Helpers.FileHelper.GetFileToOpen("请选择要发送的文件");
                }
                if (fileOrFolderPath == null)
                {
                    return;
                }

                var filePackageSize = 2048;
                var state = this.rapidPassiveEngine.P2PController.GetP2PChannelState(this.currentFriend.UserID);
                if (state != null && state.InSameLAN)
                {
                    filePackageSize = 20480;
                }
                string projectID;
                var sendingFileParas = new SendingFileParas(filePackageSize, 0);//文件数据包大小，可以根据网络状况设定，局网内可以设为204800，传输速度可以达到30M/s以上；公网建议设定为2048或4096或8192

                this.rapidPassiveEngine.FileOutter.BeginSendFile(this.currentFriend.UserID, fileOrFolderPath, null, sendingFileParas, out projectID);
                this.FileRequestReceived(projectID);
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show(ee.Message, "GGTalk");
            }
        }

        private void toolStripButton_fileTransfer_Click(object sender, EventArgs e)
        {
            this.SendFileOrFolder(false);
        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {
            this.SendFileOrFolder(true);
        }  
        #endregion

        #region 收到文件传输请求
        /// <summary>
        /// 当收到文件传输请求的时候 ，展开fileTransferingViewer,如果 本来就是展开 状态，直接添加
        /// 自己发送 文件请求的时候，也调用这里
        /// </summary>        
        internal void FileRequestReceived(string projectID ,bool offlineFile)
        {
            if (!this.TabControlContains(this.Title_FileTransfer))
            {               
                var page = new TabPage(this.Title_FileTransfer);
                page.BackColor = System.Drawing.Color.White;
                var pannel = new Panel();               
                page.Controls.Add(pannel);
                pannel.BackColor = Color.Transparent;
                pannel.Dock = DockStyle.Fill;
                pannel.Controls.Add(this.fileTransferingViewer);
                this.fileTransferingViewer.Dock = System.Windows.Forms.DockStyle.Fill;
                this.skinTabControl1.TabPages.Add(page);
                this.skinTabControl1.SelectedIndex = this.GetSelectIndex(this.Title_FileTransfer);
                this.ResetTabControVisible();
            }
            var pro = this.rapidPassiveEngine.FileOutter.GetTransferingProject(projectID);
            if (offlineFile)
            {
                var strFile = pro.IsFolder ? "离线文件夹" : "离线文件";
                this.AppendSysMessage(string.Format("对方给您发送了{0}'{1}'，大小：{2}", strFile, pro.ProjectName, ESBasic.Helpers.PublicHelper.GetSizeString(pro.TotalSize)));
            }
             
            this.fileTransferingViewer.NewFileTransferItem(projectID, offlineFile,false);            
        }

        internal void FileRequestReceived(string projectID)
        {
            this.FileRequestReceived(projectID, false);
        }
        #endregion     

        #region fileTransferingViewer1_FileTransDisruptted
        /// <summary>
        /// 文件传输失败
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="isSender">是接收者，还是发送者</param>
        /// <param name="fileTransDisrupttedType">失败原因</param>
        private void fileTransferingViewer1_FileTransDisruptted(string projectName, bool isSender, FileTransDisrupttedType fileTransDisrupttedType)
        {          
            var showText = "";
            switch (fileTransDisrupttedType)
            {
                case FileTransDisrupttedType.RejectAccepting:
                    {
                        if (isSender)
                        {
                            showText += string.Format("'{0}'传送失败！{1}", projectName, "对方拒绝接收文件。");
                        }
                        else
                        {
                            showText += string.Format("'{0}'传送失败！{1}", projectName, "您拒绝接收文件。");
                        }
                        break;
                    }
                case FileTransDisrupttedType.ActiveCancel:
                    {
                        if (isSender)
                        {
                            showText += string.Format("'{0}'传送失败！{1}", projectName, "您取消发送文件。");
                        }
                        else
                        {
                            showText += string.Format("'{0}'传送失败！{1}", projectName, "您取消接收文件。");
                        }
                        break;
                    }
                case FileTransDisrupttedType.DestCancel:
                    {
                        if (isSender)
                        {
                            showText += string.Format("'{0}'传送失败！{1}", projectName, "对方取消接收文件。");
                        }
                        else
                        {
                            showText += string.Format("'{0}'传送失败！{1}", projectName, "对方取消发送文件。");
                        }

                        break;
                    }
                case FileTransDisrupttedType.DestOffline:
                    {
                        showText += string.Format("'{0}'传送失败！{1}", projectName, "对方掉线！");
                        break;
                    }
                case FileTransDisrupttedType.SelfOffline:
                    {
                        showText += string.Format("'{0}'传送失败！{1}", projectName, "您已经掉线！");
                        break;
                    }
                case FileTransDisrupttedType.DestInnerError:
                    {
                        showText += string.Format("'{0}'传送失败！{1}", projectName, "对方系统内部错误。");
                        break;
                    }
                case FileTransDisrupttedType.InnerError:
                    {
                        showText += string.Format("'{0}'传送失败！{1}", projectName, "本地系统内部错误。");
                        break;
                    }
                case FileTransDisrupttedType.ReliableP2PChannelClosed:
                    {
                        showText += string.Format("'{0}'传送失败！{1}", projectName, "与对方可靠的P2P通道已经关闭。");
                        break;
                    }

            }
            this.AppendSysMessage(showText);
            //this.AppendMessage("系统", Color.Gray, showText, false);      
        }
        #endregion

        #region fileTransferingViewer1_FileResumedTransStarted
        /// <summary>
        /// 文件续传
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="isSender">接收者，还是发送者</param>
        private void fileTransferingViewer1_FileResumedTransStarted(string projectName, bool isSender)
        {
            var showText = string.Format("正在续传文件 '{0}'...", projectName);
            this.AppendSysMessage(showText);            
        }
        #endregion

        #region fileTransferingViewer1_FileTransCompleted
        /// <summary>
        /// 文件传输成功
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="isSender">接收者，还是发送者</param>
        private void fileTransferingViewer1_FileTransCompleted(string projectName, bool isSender, string comment ,bool isFolder)
        {
            var offlineFile = (Comment4OfflineFile.ParseUserID(comment) == null) ? "" : "离线文件";
            if (isFolder && !string.IsNullOrEmpty(offlineFile))
            {
                offlineFile += "夹";
            }
            var showText = offlineFile + string.Format("'{0}' {1}完成！", projectName, isSender ? "发送" :"接收");
            this.AppendSysMessage(showText);                   
        }
        #endregion

        #region AppendMessage
        private void AppendChatBoxContent(string userName, DateTime? originTime, ChatBoxContent content, Color color, bool followingWords)
        {
            this.AppendChatBoxContent(userName, originTime, content, color,followingWords, originTime != null);
        }

        private void AppendChatBoxContent(string userName, DateTime? originTime, ChatBoxContent content, Color color ,bool followingWords ,bool offlineMessage)
        {
            if (!followingWords)
            {
                var showTime = DateTime.Now.ToLongTimeString();
                if (!offlineMessage && originTime != null)
                {
                    showTime = originTime.Value.ToString();
                }
                this.chatBox_history.AppendRichText(string.Format("{0}  {1}\n", userName, showTime), new Font(this.messageFont, FontStyle.Regular), color);
                if (originTime != null && offlineMessage)
                {
                    this.chatBox_history.AppendText(string.Format("    [{0} 离线消息] ", originTime.Value.ToString()));
                }
                else
                {
                    this.chatBox_history.AppendText("    ");
                }
            }
            else
            {
                this.chatBox_history.AppendText("   .");
            }

            this.chatBox_history.AppendChatBoxContent(content);
            this.chatBox_history.AppendText("\n");
            this.chatBox_history.Select(this.chatBox_history.Text.Length, 0);
            this.chatBox_history.ScrollToCaret();
        }

       
        private void AppendMessage(string userName, Color color, string msg)
        {
            var showTime = DateTime.Now;
            this.chatBox_history.AppendRichText(string.Format("{0}  {1}\n", userName, showTime.ToLongTimeString()), new Font(this.messageFont, FontStyle.Regular), color);
            this.chatBox_history.AppendText("    ");

            this.chatBox_history.AppendText(msg);      
            this.chatBox_history.Select(this.chatBox_history.Text.Length, 0);
            this.chatBox_history.ScrollToCaret();
        }

        public void AppendSysMessage(string msg)
        {
            this.AppendMessage("系统", Color.Gray,msg);
            this.chatBox_history.AppendText("\n");
        }
        #endregion

        #region fileTransferingViewer1_AllTaskFinished
        private void fileTransferingViewer1_AllTaskFinished()
        {
            this.skinTabControl1.TabPages.RemoveAt(this.GetSelectIndex(this.Title_FileTransfer));
            this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
            this.ResetTabControVisible();
        }
        #endregion
        #endregion                

        #region 文字聊天       

        #region 发送
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.rapidPassiveEngine.Connected)
                {
                    this.toolShow.Show("已经掉线。", this.skinButton_send, new Point(this.skinButton_send.Width / 2, -this.skinButton_send.Height), 3000);
                    return;
                }

                var content = this.chatBoxSend.GetContent();
                if (content.IsEmpty())
                {
                    return;
                }

                var buff = CompactPropertySerializer.Default.Serialize(content);
                var encrypted = buff;
                if (GlobalResourceManager.Des3Encryption != null)
                {
                    encrypted = GlobalResourceManager.Des3Encryption.Encrypt(buff);
                }

                ++this.sendingCount;
                this.gifBox_wait.Visible = true;
                var handler = new UIResultHandler(this, this.HandleSentResult);
                this.rapidPassiveEngine.SendMessage(null, InformationTypes.Chat, encrypted, this.currentFriend.UserID, 2048, handler.Create(), null);

                var followingWords = false;
                if (this.lastWordsRecord != null && this.lastWordsRecord.IsMe)
                {
                    followingWords = (DateTime.Now - this.lastWordsRecord.SpeakTime).TotalSeconds <= 30;
                }

                this.AppendChatBoxContent(this.mine.Name, null, content, Color.SeaGreen, followingWords);
                var record = new ChatMessageRecord(this.mine.UserID, this.currentFriend.UserID, buff, false);
                GlobalResourceManager.ChatMessageRecordPersister.InsertChatMessageRecord(record);

                //清空输入框
                this.chatBoxSend.Text = string.Empty;
                this.chatBoxSend.Focus();

                if (this.LastWordChanged != null)
                {
                    this.lastWordsRecord = new LastWordsRecord(this.mine.ID, this.mine.Name, true ,content);
                    this.LastWordChanged(false, this.currentFriend.UserID, this.lastWordsRecord);
                }
            }
            catch(Exception ee)
            {
                this.AppendSysMessage("发送消息失败！" + ee.Message);
            }
        }

        //0923
        private int sendingCount = 0;
        private void HandleSentResult(bool succeed, object tag)
        {
            --this.sendingCount;
            if (this.sendingCount <= 0)
            {
                this.sendingCount = 0;
                this.gifBox_wait.Visible = false;
            }

            if (!succeed)
            {
                this.toolShow.Show("因为网络原因，刚才的消息尚未发送成功！", this.skinButton_send, new Point(this.skinButton_send.Width / 2, -this.skinButton_send.Height), 3000);
            }
        }      
        #endregion

        #region 字体
        //显示字体对话框
        private void toolFont_Click(object sender, EventArgs e)
        {
            this.fontDialog1.Font = this.chatBoxSend.Font;
            this.fontDialog1.Color = this.chatBoxSend.ForeColor;
            if (DialogResult.OK == this.fontDialog1.ShowDialog())
            {
                this.chatBoxSend.Font = this.fontDialog1.Font;
                this.chatBoxSend.ForeColor = this.fontDialog1.Color;

                SystemSettings.Singleton.FontColor = this.fontDialog1.Color;
                SystemSettings.Singleton.Font = this.fontDialog1.Font;
                SystemSettings.Singleton.Save();
            }
        }
        #endregion

        #region Vibration 震动
        //震动方法
        private void Vibration()
        {
            var pOld = this.Location;//原来的位置
            var radius = 3;//半径
            for (var n = 0; n < 3; n++) //旋转圈数
            {
                //右半圆逆时针
                for (var i = -radius; i <= radius; i++)
                {
                    var x = Convert.ToInt32(Math.Sqrt(radius * radius - i * i));
                    var y = -i;

                    this.Location = new Point(pOld.X + x, pOld.Y + y);
                    Thread.Sleep(10);
                }
                //左半圆逆时针
                for (var j = radius; j >= -radius; j--)
                {
                    var x = -Convert.ToInt32(Math.Sqrt(radius * radius - j * j));
                    var y = -j;

                    this.Location = new Point(pOld.X + x, pOld.Y + y);
                    Thread.Sleep(10);
                }
            }
            //抖动完成，恢复原来位置
            this.Location = pOld;
        }

        //震动
        private void toolVibration_Click(object sender, EventArgs e)
        {
            if (this.mine.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.Vibration, null);
            this.AppendMessage(this.mine.Name, Color.Green, "您发送了一个抖动提醒。\n");

            this.chatBoxSend.Text = string.Empty;
            this.chatBoxSend.Focus();
            Vibration();
        }
        #endregion               

        private void OnReceivedMsg(ChatBoxContent content, DateTime? originTime)
        {
            this.skinLabel_inputing.Visible = false;
            var followingWords = false;
            if (this.lastWordsRecord != null && !this.lastWordsRecord.IsMe)
            {
                followingWords = (DateTime.Now - this.lastWordsRecord.SpeakTime).TotalSeconds <= 30;
            }
            this.AppendChatBoxContent(this.currentFriend.Name, originTime, content, Color.Blue, followingWords);
            if (this.LastWordChanged != null)
            {
                this.lastWordsRecord = new LastWordsRecord(this.currentFriend.ID, this.currentFriend.Name, false, content);
                this.LastWordChanged(false, this.currentFriend.UserID, this.lastWordsRecord);
            }
        }      

        private void OnReceivedOfflineFileResultNotify(string fileName, bool accept)
        {
            var msg = string.Format("对方{0}了您发送的离线文件'{1}'", accept ? "已成功接收" : "拒绝", fileName);
            this.AppendSysMessage(msg);
        }

        private void OnViberation()
        {
            var msg = this.currentFriend.Name + "给您发送了抖动提醒。\n";
            this.AppendMessage(this.currentFriend.Name, Color.Blue, msg);

            if (this.TopMost)
            {
                this.Focus();
            }
            else
            {
                this.TopMost = true;
                Vibration();
                this.TopMost = false;
            }         
        } 
        #endregion

        #region 窗体事件      

        //渐变层
        private void FrmChat_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var sb = new SolidBrush(Color.FromArgb(100, 255, 255, 255));
            g.FillRectangle(sb, new Rectangle(new Point(1, 91), new Size(Width - 2, Height - 91)));
        }        
        #endregion      

        #region 关闭窗体
        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {           
            this.Close();
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.videoForm != null)
            {
                this.videoForm.Close();
            }

            if (this.remoteHelpForm != null)
            {
                this.remoteHelpForm.Close();
            }

            if (this.TabControlContains(this.Title_Audio))
            {
                if (this.audioHandlePanel.IsWorking || this.audioHandlePanel.IsSender)
                {
                    this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.HungupAudio, null);
                }
                else
                {
                    this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.RejectAudio, null);
                }
            }

            if (this.TabControlContains(this.Title_RemoteHelp))
            {
                this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.RejectRemoteHelp, null);
            }

            if (this.TabControlContains(this.Title_RemoteHelpHandle))
            {
                this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.TerminateRemoteHelp, null);
            }

            if (this.TabControlContains(this.Title_Video))
            {
                this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.RejectVideo, null);
            }

            this.inputingTimer.Stop();
            this.inputingTimer.Dispose();
            this.rapidPassiveEngine.P2PController.P2PChannelOpened -= new CbGeneric<P2PChannelState>(P2PController_P2PChannelOpened);
            this.rapidPassiveEngine.P2PController.P2PChannelClosed -= new CbGeneric<P2PChannelState>(P2PController_P2PChannelClosed);

            this.emotionForm.Visible = false;
            this.emotionForm.Close();

            this.fileTransferingViewer.BeforeDispose();
            e.Cancel = false;
        } 
        #endregion

        #region 截图
        private void buttonCapture_Click(object sender, EventArgs e)
        {
            var imageCapturer = new ScreenCapturer();
            if (imageCapturer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.chatBoxSend.InsertImage(imageCapturer.Image);
                this.chatBoxSend.Focus();
                this.chatBoxSend.ScrollToCaret();
            }
        } 
        #endregion              

        #region 手写板
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var form = new PaintForm();
            form.Location = new Point(this.Left + 20, this.Top +skToolMenu.Top - form.Height);
            if (DialogResult.OK == form.ShowDialog())
            {
                var bitmap = form.CurrentImage;
                if (bitmap != null)
                {
                    this.chatBoxSend.InsertImage(bitmap);
                    this.chatBoxSend.Focus();
                    this.chatBoxSend.ScrollToCaret();
                }
            }
        } 
        #endregion

        #region 语音聊天
        private void 语音聊天ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.toolStripSplitButton3_ButtonClick(sender, e);
        }

        //发出语音邀请
        private void toolStripSplitButton3_ButtonClick(object sender, EventArgs e)
        {
        
            if (this.mine.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            if (this.videoForm != null)
            {
                return;
            }

            if (this.TabControlContains(this.Title_Audio) )
            {
                return;
            }

          
           
            if (!Program.MultimediaManager.Available)
            {
                MessageBox.Show("多媒体设备管理器不可用！", GlobalResourceManager.SoftwareName);
                return;
            }

            Program.MultimediaManager.OutputAudio = true;
            var msg = "请求与对方进行语音对话，正在等待对方回应...";
            this.AppendSysMessage(msg);

            this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.AudioRequest,null);

            this.audioHandlePanel.IsSender = true;
            var page = new TabPage(this.Title_Audio);
            page.BackColor = Color.White;// System.Drawing.Color.White;
            var pannel = new Panel();
            page.Controls.Add(pannel);
            pannel.BackColor = Color.Transparent;
            pannel.Dock = DockStyle.Fill;
            pannel.Controls.Add(this.audioHandlePanel);
            this.fileTransferingViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabControl1.TabPages.Add(page);
            this.skinTabControl1.SelectedIndex = this.GetSelectIndex(this.Title_Audio);
            this.ResetTabControVisible();
        }

        /// <summary>
        /// 收到语音对话的请求
        /// </summary>  
        private void OnAudioRequestReceived()
        {
            if (!this.TabControlContains(this.Title_Audio))
            {
                this.audioHandlePanel.IsSender = false;

                var page = new TabPage(this.Title_Audio);
                page.BackColor = System.Drawing.Color.White;
                var pannel = new Panel();
                page.Controls.Add(pannel);
                pannel.BackColor = Color.Transparent;
                pannel.Dock = DockStyle.Fill;
                pannel.Controls.Add(this.audioHandlePanel);
                this.fileTransferingViewer.Dock = System.Windows.Forms.DockStyle.Fill;
                this.skinTabControl1.TabPages.Add(page);
                this.skinTabControl1.SelectedIndex = this.GetSelectIndex(this.Title_Audio);
                this.ResetTabControVisible();
            }
        }

        /// <summary>
        /// 自己回复语音邀请
        /// </summary>  
        void audioHandlePanel_AudioRequestAnswerd(bool agree)
        {
            if (!agree)
            {
                this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.RejectAudio, null);
                this.AppendSysMessage("您拒绝了对方的语音对话请求。");
                this.skinTabControl1.TabPages.RemoveAt(this.GetSelectIndex(this.Title_Audio));
                this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
                this.ResetTabControVisible();
                return;
            }

            this.AppendSysMessage("您同意了对方的语音对话请求，正在启动多媒体设备...");
            if (!Program.MultimediaManager.Available)
            {
                this.AppendSysMessage("多媒体设备管理器不可用！");
                MessageBox.Show("多媒体设备管理器不可用！", GlobalResourceManager.SoftwareName);
                this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.RejectAudio, null);
                return;
            }
            this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.AgreeAudio, null);
            Program.MultimediaManager.OutputAudio = true;
            this.audioHandlePanel.OnAgree(Program.MultimediaManager);
        }

        /// <summary>
        /// 对方回复语音邀请
        /// </summary>        
        private void OnAudioAnswerReceived(bool agree)
        {
            if (!agree)
            {
                this.AppendSysMessage("对方拒绝了您的语音对话请求。");
                this.skinTabControl1.TabPages.RemoveAt(this.GetSelectIndex(this.Title_Audio));
                this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
                this.ResetTabControVisible();
                return;
            }

            this.AppendSysMessage("对方同意了您的语音对话请求。");           
            this.audioHandlePanel.OnAgree(Program.MultimediaManager);
        }

        //自己挂断
        void audioHandlePanel_AudioTerminated()
        {
            var showText = this.audioHandlePanel.IsWorking ? "您挂断了语音对话。" : "您取消了语音对话请求。";

            if (this.audioHandlePanel.IsWorking)
            {
                this.audioHandlePanel.OnTerminate();
            }
            this.skinTabControl1.TabPages.RemoveAt(this.GetSelectIndex(this.Title_Audio));
            this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
            this.ResetTabControVisible();
            this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.HungupAudio, null);           
            this.AppendSysMessage(showText);
        }

        /// <summary>
        /// 对方挂断了语音
        /// </summary>       
        private void OnAudioHungUpReceived()
        {   
            var tabIndex = this.GetSelectIndex(this.Title_Audio);
            if (tabIndex >= 0)
            {
                var showText = this.audioHandlePanel.IsWorking ? "对方挂断了语音对话。" : "对方取消了语音对话请求。";
                this.AppendSysMessage(showText);
                this.audioHandlePanel.OnTerminate();
                this.skinTabControl1.TabPages.RemoveAt(tabIndex);
                this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
                this.ResetTabControVisible();               
            }
        }        
        #endregion

        #region 视频聊天
        private void 视频聊天ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.toolStripSplitButton1_ButtonClick(sender, e);
        }

        //发出视频邀请
        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            if (this.mine.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            if (this.videoForm != null)
            {
                return;
            }

            this.videoForm = new VideoForm(this.rapidPassiveEngine.CurrentUserID, this.currentFriend.UserID, this.currentFriend.Name, true);
            this.videoForm.FormClosed += new FormClosedEventHandler(videoForm_FormClosed);
            this.videoForm.HungUpVideo += new CbGeneric<HungUpCauseType>(videoForm_ActiveHungUpVideo);    
            this.videoForm.Show();
            
            if (!Program.MultimediaManager.Available)
            {
                MessageBox.Show("多媒体设备管理器不可用！", GlobalResourceManager.SoftwareName);              
                if (this.videoForm != null)
                {
                    this.videoForm.Close();
                }
                return;
            }

            this.videoForm.Initialize(this.rapidPassiveEngine, Program.MultimediaManager);
            this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.VideoRequest, null);            
        }

        /// <summary>
        /// 收到视频聊天的请求
        /// </summary>  
        private void OnVideoRequestReceived()
        {
            if (!this.TabControlContains(this.Title_Video))
            {
                var page = new TabPage(this.Title_Video);
                page.BackColor = System.Drawing.Color.White;
                var pannel = new Panel();
                page.Controls.Add(pannel);
                pannel.BackColor = Color.Transparent;
                pannel.Dock = DockStyle.Fill;
                pannel.Controls.Add(this.videoRequestPanel);
                this.fileTransferingViewer.Dock = System.Windows.Forms.DockStyle.Fill;
                this.skinTabControl1.TabPages.Add(page);
                this.skinTabControl1.SelectedIndex = this.GetSelectIndex(this.Title_Video);
                this.ResetTabControVisible();
            }            
        }        

        /// <summary>
        /// 对方回复视频邀请
        /// </summary>        
        private void OnVideoAnswerReceived(bool agree)
        {
            if (this.videoForm == null)
            {
                return;
            }

            if (agree)
            {
                System.Threading.Thread.Sleep(1000);
                this.videoForm.OnAgree();
            }
            else
            {
                this.videoForm.Close();
                this.videoForm = null;
                this.AppendSysMessage("对方拒绝了您的视频邀请。");
            }
        }

        /// <summary>
        /// 对方挂断了视频
        /// </summary>       
        private void OnVideoHungUpReceived()
        {
            if (this.videoForm != null)
            {
                this.AppendSysMessage("对方中止了视频通话。");
                this.videoForm.OnHungUpVideo();
                return;
            }

            var tabIndex = this.GetSelectIndex(this.Title_Video);
            if (tabIndex >= 0)
            {
                this.skinTabControl1.TabPages.RemoveAt(tabIndex);
                this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
                this.ResetTabControVisible();
                this.AppendSysMessage("对方结束了视频会话邀请。");
            }
        }        

        /// <summary>
        /// 作为视频接收方，对视频会话请求的应答。（点击控件的按钮）
        /// </summary>
        private void videoRequestPanel_VideoRequestAnswerd(bool agree)
        {
            this.skinTabControl1.TabPages.RemoveAt(this.GetSelectIndex(this.Title_Video));
            this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
            this.ResetTabControVisible();
                        
            if (agree)
            {
                this.videoForm = new VideoForm(this.rapidPassiveEngine.CurrentUserID, this.currentFriend.UserID, this.currentFriend.Name, false);
                this.videoForm.FormClosed += new FormClosedEventHandler(videoForm_FormClosed);
                this.videoForm.HungUpVideo += new CbGeneric<HungUpCauseType>(videoForm_ActiveHungUpVideo);
                this.videoForm.Show();

                this.AppendSysMessage("您同意了对方的视频会话请求，正在启动多媒体设备...");
                if (!Program.MultimediaManager.Available)
                {
                   
                    this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.RejectVideo, null);
                    this.AppendSysMessage("多媒体设备管理器不可用！");
                    MessageBox.Show("多媒体设备管理器不可用！", GlobalResourceManager.SoftwareName);
                    if (this.videoForm != null)
                    {
                        this.videoForm.Close();
                    }
                    return;
                }

                this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.AgreeVideo, null);
                this.videoForm.Initialize(this.rapidPassiveEngine, Program.MultimediaManager);
            }
            else
            {
                this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.RejectVideo, null);
                this.AppendSysMessage("您拒绝了对方的视频会话请求。");
            }
        }

        private void videoForm_ActiveHungUpVideo(HungUpCauseType cause)
        {
            this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.HungUpVideo, null);
            if (cause == HungUpCauseType.ActiveHungUp)
            {
                this.AppendSysMessage("您中止了视频通话。");
            }
            else
            {
                this.AppendSysMessage("与对方的网络连接中断，视频通话结束。");
            }
        }

        private void videoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.videoForm = null;
        }
        #endregion        

        #region 远程磁盘 V2.0
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (!this.currentFriend.OnlineOrHide || !this.mine.OnlineOrHide)
            {
                return;
            }

            this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.DiskRequest, null);
            this.AppendSysMessage("请求访问对方磁盘，正在等待对方回应...");
        }

        /// <summary>
        /// 作为远程磁盘的访问者，收到主人对磁盘访问请求的回应
        /// </summary>        
        internal void OnDiskAnswerReceived(bool agree)
        {
            if (!agree)
            {
                this.AppendSysMessage("对方拒绝了您的磁盘访问请求.");
                return;
            }

            this.AppendSysMessage("对方同意了您的磁盘访问请求.");
            var form = new NDiskForm(this.currentFriend.UserID, this.currentFriend.Name, this.rapidPassiveEngine.FileOutter, this.nDiskOutter);
            form.Show();
        }
       
        /// <summary>
        /// 作为磁盘的主人，收到磁盘访问的请求
        /// </summary>
        private void OnDiskRequestReceived()
        {
            if (!this.TabControlContains(this.Title_Disk))
            {
                var page = new TabPage(this.Title_Disk);
                page.BackColor = System.Drawing.Color.White;
                var pannel = new Panel();
                page.Controls.Add(pannel);
                pannel.BackColor = Color.Transparent;
                pannel.Dock = DockStyle.Fill;
                pannel.Controls.Add(this.diskRequestPanel);
                this.fileTransferingViewer.Dock = System.Windows.Forms.DockStyle.Fill;
                this.skinTabControl1.TabPages.Add(page);
                this.skinTabControl1.SelectedIndex = this.GetSelectIndex(this.Title_Disk);
                this.ResetTabControVisible();
            }
        }

        /// <summary>
        /// 作为磁盘的主人，对远程磁盘请求的应答。（点击控件的按钮）
        /// </summary>        
        private void diskRequestPanel_DiskRequestAnswerd(bool agree)
        {
            this.skinTabControl1.TabPages.RemoveAt(this.GetSelectIndex(this.Title_Disk));
            this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
            this.ResetTabControVisible();

            this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, agree ? InformationTypes.AgreeDisk : InformationTypes.RejectDisk, null);

            var showText = string.Format("您{0}了对方的磁盘访问请求。", agree ? "同意" : "拒绝");
            this.AppendSysMessage(showText);
        }
        #endregion    

        #region 远程协助 V2.2
        private void 请求远程协助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mine.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            if (this.TabControlContains(this.Title_RemoteHelpHandle))
            {
                return;
            }
            
            this.PrepairRemoteHelp(null , RemoteHelpStyle.AllScreen);
        }

        private void 桌面共享指定区域ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mine.UserStatus == UserStatus.OffLine)
            {
                return;
            }
         
            var form = new ESBasic.Widget.CaptureScreenForm("按住左键选择桌面共享区域");          
            if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {                
                return;
            }
          
            this.PrepairRemoteHelp(form.CaptureRegion , RemoteHelpStyle.PartScreen);
        }

        private void PrepairRemoteHelp(Rectangle? regionSelected , RemoteHelpStyle style)
        {            
            var msg = "请求对方远程协助自己，正在等待对方回应...";
            if (regionSelected != null)
            {
                msg = string.Format("已经指定桌面区域{0}，", regionSelected.Value) + msg;
            }
            this.AppendSysMessage(msg);

            if (!Program.MultimediaManager.Available)
            {
                MessageBox.Show("多媒体设备管理器不可用！", GlobalResourceManager.SoftwareName);
                return;
            }
            Program.MultimediaManager.DesktopRegion = regionSelected; //设为null，表示共享整个屏幕
            this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.RemoteHelpRequest, BitConverter.GetBytes((int)style));            
          
            var page = new TabPage(this.Title_RemoteHelpHandle);
            page.BackColor = Color.White;// System.Drawing.Color.White;
            var pannel = new Panel();
            page.Controls.Add(pannel);
            pannel.BackColor = Color.Transparent;
            pannel.Dock = DockStyle.Fill;
            pannel.Controls.Add(this.remoteHelpHandlePanel);
            this.fileTransferingViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabControl1.TabPages.Add(page);
            this.skinTabControl1.SelectedIndex = this.GetSelectIndex(this.Title_RemoteHelpHandle);
            this.ResetTabControVisible();
        }  

        /// <summary>
        /// 【站在请求方角度】对方回复远程协助请求
        /// </summary>        
        private void OnRemoteHelpAnswerReceived(bool agree)
        {
            if (!agree)
            {               
                this.AppendSysMessage("对方拒绝了您的远程协助请求.");
                this.skinTabControl1.TabPages.RemoveAt(this.GetSelectIndex(this.Title_RemoteHelpHandle));
                this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
                this.ResetTabControVisible();                
                return;
            }

            this.AppendSysMessage("对方同意了您的远程协助请求.");
            this.remoteHelpHandlePanel.OnAgree();
        }

        /// <summary>
        /// 【站在协助方角度】收到远程协助请求
        /// </summary>
        private void OnRemoteHelpRequestReceived(RemoteHelpStyle style)
        {
            this.remoteHelpRequestPanel.SetRemoteDesktopStyle(style);
            if (!this.TabControlContains(this.Title_RemoteHelp))
            {
                var page = new TabPage(this.Title_RemoteHelp);
                page.BackColor = System.Drawing.Color.White;
                var pannel = new Panel();
                page.Controls.Add(pannel);
                pannel.BackColor = Color.Transparent;
                pannel.Dock = DockStyle.Fill;
                pannel.Controls.Add(this.remoteHelpRequestPanel);
                this.fileTransferingViewer.Dock = System.Windows.Forms.DockStyle.Fill;
                this.skinTabControl1.TabPages.Add(page);
                this.skinTabControl1.SelectedIndex = this.GetSelectIndex(this.Title_RemoteHelp);
                this.ResetTabControVisible();
            }
        }

        /// <summary>
        /// 【站在协助方角度】请求方终止了桌面共享
        /// </summary>
        private void OnOwnerTerminateRemoteHelp()
        {
            if (this.remoteHelpForm != null)
            {
                this.remoteHelpForm.OwnerTeminateHelp();
            }
            else
            {
                this.AppendSysMessage("对方取消了远程协助请求。");               
                this.skinTabControl1.TabPages.RemoveAt(this.GetSelectIndex(this.Title_RemoteHelp));
                this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
                this.ResetTabControVisible();
            }           
        }

        /// <summary>
        /// 【站在请求方角度】协助方关闭了远程桌面
        /// </summary>
        private void OnHelperCloseRemoteHelp()
        {
            this.AppendSysMessage("对方终止了对您的远程协助。");
            this.remoteHelpHandlePanel.OnTerminate();
            this.skinTabControl1.TabPages.RemoveAt(this.GetSelectIndex(this.Title_RemoteHelpHandle));
            this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
            this.ResetTabControVisible();
        }       

        /// <summary>
        /// 远程桌面被关闭。可能原因：1.协助方主动叉掉窗口； 2.请求方终止桌面共享
        /// </summary>       
        void remoteHelpForm_RemoteHelpEnded(bool ownerTerminateClose ,RemoteHelpStyle style)
        {
            if (!ownerTerminateClose)
            {
                this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.CloseRemoteHelp, null);
            }
            var showText = ownerTerminateClose ?  "对方终止了远程协助。" : "您终止了给对方的远程协助。";            
            this.AppendSysMessage(showText);
            this.remoteHelpForm = null;
        }

        /// <summary>
        /// 作为请求方，结束桌面共享。（点击控件的按钮）
        /// </summary>  
        void remoteHelpHandlePanel_RemoteHelpTerminated()
        {
            this.skinTabControl1.TabPages.RemoveAt(this.GetSelectIndex(this.Title_RemoteHelpHandle));
            this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
            this.ResetTabControVisible();
            this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.TerminateRemoteHelp, null);
            var showText = this.remoteHelpHandlePanel.IsWorking ? "您终止了远程协助。" : "您取消了远程协助请求。";
            this.AppendSysMessage(showText);
        }

        /// <summary>
        /// 作为协助方，对远程协助请求的应答。（点击控件的按钮）
        /// </summary>       
        void remoteHelpRequestPanel_RemoteHelpRequestAnswerd(bool agree, RemoteHelpStyle style)
        {
            this.skinTabControl1.TabPages.RemoveAt(this.GetSelectIndex(this.Title_RemoteHelp));
            this.skinTabControl1.SelectedIndex = this.skinTabControl1.TabPages.Count - 1;
            this.ResetTabControVisible();            
            if (agree)
            {
                if (!Program.MultimediaManager.Available)
                {

                    this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, InformationTypes.RejectRemoteHelp, null);
                    MessageBox.Show("多媒体设备管理器不可用！", GlobalResourceManager.SoftwareName);
                    return;
                }
            }
            this.rapidPassiveEngine.CustomizeOutter.Send(this.currentFriend.UserID, agree ? InformationTypes.AgreeRemoteHelp : InformationTypes.RejectRemoteHelp, null);

            var showText = string.Format("您{0}了对方的远程协助请求。", agree ? "同意" : "拒绝");
            this.AppendSysMessage(showText);

            if (agree)
            {
                this.remoteHelpForm = new RemoteHelpForm(this.currentFriend.UserID, this.currentFriend.Name, RemoteHelpStyle.AllScreen);
                this.remoteHelpForm.RemoteHelpEnded += new CbGeneric<bool, RemoteHelpStyle>(remoteHelpForm_RemoteHelpEnded);
                this.remoteHelpForm.Show();
            }
        }
        #endregion

        #region 离线文件 V3.2
        private void toolStripMenuItem34_Click(object sender, EventArgs e)
        {
            if (this.mine.UserStatus == UserStatus.OffLine)
            {
                return; 
            }

            try
            {
                var filePath = ESBasic.Helpers.FileHelper.GetFileToOpen("请选择要发送的离线文件");
                if (filePath == null)
                {
                    return;
                }
                string projectID;
                var sendingFileParas = new SendingFileParas(2048, 0);//文件数据包大小，可以根据网络状况设定，局网内可以设为204800，传输速度可以达到30M/s以上；公网建议设定为2048或4096或8192

                // BeginSendFile方法
                //（1）accepterID传入null，表示文件的接收者就是服务端
                //（2）巧用comment参数，参见Comment4OfflineFile类
                this.rapidPassiveEngine.FileOutter.BeginSendFile(null, filePath, Comment4OfflineFile.BuildComment(this.currentFriend.UserID), sendingFileParas, out projectID);
                this.FileRequestReceived(projectID);
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show(ee.Message, "GGTalk");
            }
        }

        private void 发送离线文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mine.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            try
            {
                var folderPath = ESBasic.Helpers.FileHelper.GetFolderToOpen(false);
                if (folderPath == null)
                {
                    return;
                }
                string projectID;
                var sendingFileParas = new SendingFileParas(2048, 0);//文件数据包大小，可以根据网络状况设定，局网内可以设为204800，传输速度可以达到30M/s以上；公网建议设定为2048或4096或8192

                // BeginSendFile方法
                //（1）accepterID传入null，表示文件的接收者就是服务端
                //（2）巧用comment参数，参见Comment4OfflineFile类
                this.rapidPassiveEngine.FileOutter.BeginSendFile(null, folderPath, Comment4OfflineFile.BuildComment(this.currentFriend.UserID), sendingFileParas, out projectID);
                this.FileRequestReceived(projectID);
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show(ee.Message, "GGTalk");
            }
        }
        #endregion

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.mine.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            var form = new ChatRecordForm(GlobalResourceManager.RemotingService, GlobalResourceManager.ChatMessageRecordPersister, this.mine.GetIDName(), this.currentFriend.GetIDName());
            form.Show();
        }

        private void ChatForm_Shown(object sender, EventArgs e)
        {
            this.chatBoxSend.Focus();
        }
       
        private void toolStripButtonEmotion_MouseUp(object sender, MouseEventArgs e)
        {
            var pos = new Point((this.Left + 30) - (this.emotionForm.Width / 2), this.Top + skToolMenu.Top - this.emotionForm.Height);
            if (pos.X < 10)
            {
                pos = new Point(10, pos.Y);
            }
            this.emotionForm.Location = pos;           
            this.emotionForm.Visible = !this.emotionForm.Visible;
        }

        private void FocusCurrent(object sender, EventArgs e)
        {
            this.Focus();
        }       

        private void ResetTabControVisible()
        {
            if (this.skinTabControl1.TabPages.Count == 0)
            {
                this.skinTabControl1.Visible = false;
                this.skinPanel_right.Visible = true;

                this.skinTabControl1.Location = new Point(this.btnClose.Location.X - 8, this.btnClose.Location.Y);
                this.skinTabControl1.Size = new Size(2, 2);
                this.skinTabControl1.Anchor = AnchorStyles.None;

                this.skinPanel_right.Invalidate();
            }
            else
            {
                this.skinTabControl1.Location = this.skinPanel_right.Location;
                this.skinTabControl1.Size = this.skinPanel_right.Size;
                this.skinTabControl1.Anchor = this.skinPanel_right.Anchor;
                this.skinTabControl1.Visible = true;
                this.skinPanel_right.Visible = false;
            }         

        }       

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            try
            {
                var file = ESBasic.Helpers.FileHelper.GetFileToOpen2("请选择图片", null, ".jpg", ".bmp", ".png", ".gif");
                if (file == null)
                {
                    return;
                }

                var img = Image.FromFile(file);
                this.chatBoxSend.InsertImage(img);
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show(ee.Message, "GGTalk");
            }
        }

        private void ChatForm_SizeChanged(object sender, EventArgs e)
        {
            //0920
            if (this.WindowState == FormWindowState.Maximized || this.WindowState == FormWindowState.Minimized)
            {
                return;
            }

            SystemSettings.Singleton.ChatFormSize = this.Size;
            SystemSettings.Singleton.Save();
        }

        private void 语音视频设备测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new DeviceSelectForm();
            form.DeviceSelected += new CbGeneric<DeviceSelectForm>(form_DeviceSelected);
            form.ShowDialog();
        }

        void form_DeviceSelected(DeviceSelectForm form)
        {
            SystemSettings.Singleton.WebcamIndex = form.CameraIndex;
            SystemSettings.Singleton.MicrophoneIndex = form.MicrophoneIndex;
            SystemSettings.Singleton.SpeakerIndex = form.SpeakerIndex;
            SystemSettings.Singleton.Save();
        }

        private void 语音视频设备测试ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.语音视频设备测试ToolStripMenuItem_Click(sender, e);            
        }     

        //private void textBoxSend_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        //{
        //    if (e.KeyData == Keys.Enter)
        //    {
        //        this.btnSend_Click(this.skinButton_send,new EventArgs()) ;
        //    }          
        //}

        //private void textBoxSend_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyData == Keys.Enter && !e.Control)
        //    {
        //        this.textBoxSend.Text = string.Empty;
        //    }            
        //}       
    }
}
