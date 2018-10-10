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
using ESBasic.ObjectManagement.Managers;
using JustLib;
using ESBasic.Helpers;
using ESPlus.Application;
using ESPlus.Serialization;
using OMCS.Passive.Audio;
using GGTalk.Controls;
using JustLib.Records;

namespace GGTalk
{
    /// <summary>
    /// 群/组聊天窗口。
    /// </summary>
    public partial class GroupChatForm : BaseForm, IManagedForm<string>, IGroupChatForm
    {
        private IChatSupporter ggSupporter;
        private GGGroup currentGroup;
        private EmotionForm emotionForm;
        private IRapidPassiveEngine rapidPassiveEngine;
        private GlobalUserCache globalUserCache;
        private GGUser mine;
        public event CbGeneric<bool, string, LastWordsRecord> LastWordChanged; //group - content 

        #region IManagedForm Member
        public string FormID
        {
            get { return this.currentGroup.GroupID; }
        } 
        #endregion

        #region Ctor
        public GroupChatForm(IRapidPassiveEngine engine,string groupID, GlobalUserCache cache ,IChatSupporter supporter)
        {
            this.rapidPassiveEngine = engine;            
            this.globalUserCache = cache;
            this.mine = this.globalUserCache.GetUser(this.rapidPassiveEngine.CurrentUserID);
            this.ggSupporter = supporter;
            this.currentGroup = this.globalUserCache.GetGroup(groupID);

            InitializeComponent();
            this.chatBoxSend.Initialize(GlobalResourceManager.EmotionDictionary);
            this.chatBox_history.Initialize(GlobalResourceManager.EmotionDictionary);
            this.chatBoxSend.Font = SystemSettings.Singleton.Font;
            this.chatBoxSend.ForeColor = SystemSettings.Singleton.FontColor;
            this.Size = SystemSettings.Singleton.ChatFormSize;

            this.linkLabel_softName.Text = GlobalResourceManager.SoftwareName;

            this.toolShow.SetToolTip(this.panelFriendHeadImage, this.currentGroup.GroupID);
            this.Text = string.Format("{0}({1})",this.currentGroup.Name ,this.currentGroup.GroupID);
            this.labelGroupName.Text = this.currentGroup.Name;
            this.label_announce.Text = this.currentGroup.Announce;
            this.chatBoxSend.Focus();            

            this.emotionForm = new EmotionForm();
            this.emotionForm.Load += new EventHandler(emotionForm_Load);
            this.emotionForm.Initialize(GlobalResourceManager.EmotionList);
            this.emotionForm.EmotionClicked += new CbGeneric<int,Image>(emotionForm_Clicked);
            this.emotionForm.Visible = false;
            this.emotionForm.LostFocus += new EventHandler(emotionForm_LostFocus);

            foreach (string memberID in this.currentGroup.MemberList)
            {
                GGUser friend = this.globalUserCache.GetUser(memberID);
                this.AddUserItem(friend);              
            }

            if (SystemSettings.Singleton.LoadLastWordsWhenChatFormOpened)
            {
                LastWordsRecord record = this.currentGroup.Tag as LastWordsRecord;
                if (record != null)
                {
                    string talker = string.Format("{0}({1})", record.SpeakerName, record.SpeakerID);
                    this.AppendChatBoxContent(talker, record.SpeakTime, record.ChatBoxContent, Color.Blue);
                }
            }
        }

        void emotionForm_LostFocus(object sender, EventArgs e)
        {
            this.emotionForm.Visible = false;
        }

        private void AddUserItem(GGUser friend)
        {
            ChatListSubItem subItem = new ChatListSubItem(friend.UserID, friend.UserID, friend.Name, friend.Signature, GlobalResourceManager.ConvertUserStatus(friend.UserStatus), GlobalResourceManager.GetHeadImage(friend));
            subItem.Tag = friend;           
            this.chatListBox1.Items[0].SubItems.AddAccordingToStatus(subItem);
        }

        void emotionForm_Load(object sender, EventArgs e)
        {
            this.emotionForm.Location = new Point((this.Left + 30) - (this.emotionForm.Width / 2), this.Top +  skToolMenu.Top - this.emotionForm.Height);
        }

        void emotionForm_Clicked(int index, Image img)
        {
            this.chatBoxSend.InsertDefaultEmotion((uint)index);
            this.emotionForm.Visible = false;
        } 
        #endregion                    

        /// <summary>
        /// 自己掉线
        /// </summary>
        public void MyselfOffline()
        {
            foreach (ChatListSubItem item in this.chatListBox1.Items[0].SubItems)
            {
                item.Status = ChatListSubItem.UserStatus.OffLine;
            }
            this.chatListBox1.Invalidate();
        }       

        public void GroupmateStateChanged(string userID, UserStatus newStatus)
        {     
            ChatListSubItem[] items = this.chatListBox1.GetSubItemsByNicName(userID);
            if (items == null || items.Length == 0)
            {
                return;
            }

            items[0].Status = GlobalResourceManager.ConvertUserStatus(newStatus);
            this.chatListBox1.Invalidate();
        }

        public void OnGroupInfoChanged(GroupChangedType type, string userID)
        {
            if (type == GroupChangedType.GroupInfoChanged)
            {
                this.Text = string.Format("{0}({1})", this.currentGroup.Name, this.currentGroup.GroupID);
                this.labelGroupName.Text = this.currentGroup.Name;
                this.label_announce.Text = this.currentGroup.Announce;
                return;
            }

            if (type == GroupChangedType.MemberInfoChanged)
            {
                GGUser user = this.globalUserCache.GetUser(userID);
                this.OnUserInfoChanged(user);
                return;
            }

            if (type == GroupChangedType.SomeoneJoin)
            {
                GGUser user = this.globalUserCache.GetUser(userID);
                this.AddUserItem(user);
                this.AppendSysMessage(string.Format("{0}({1})加入了该群！", user.Name, user.UserID));
                return;
            }

            if (type == GroupChangedType.SomeoneQuit)
            {
                GGUser user = this.globalUserCache.GetUser(userID);
                ChatListSubItem[] items = this.chatListBox1.GetSubItemsByNicName(userID);
                if (items == null || items.Length == 0)
                {
                    return;
                }
                ChatListSubItem item = items[0];
                this.chatListBox1.Items[0].SubItems.Remove(item);
                this.chatListBox1.Invalidate();
                this.AppendSysMessage(string.Format("{0}({1})退出了该群！", user.Name, user.UserID));
                return;
            }
        }      

        #region OnUserInfoChanged
        public void OnUserInfoChanged(GGUser user)
        {
            ChatListSubItem[] items = this.chatListBox1.GetSubItemsByNicName(user.UserID);
            if (items == null || items.Length == 0)
            {
                return;
            }

            items[0].HeadImage = GlobalResourceManager.GetHeadImage(user);
            items[0].DisplayName = user.Name;
            items[0].PersonalMsg = user.Signature;
            items[0].Tag = user;
            this.chatListBox1.Invalidate();
        } 
        #endregion

        #region 文字聊天
        #region FlashChatWindow
        [DllImport("User32")]
        public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);
        public void FlashChatWindow()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                int MyTimes = 4;
                int MyTime = 500;
                for (int MyCount = 0; MyCount < MyTimes; MyCount++)
                {
                    FlashWindow(this.Handle, true);
                    System.Threading.Thread.Sleep(MyTime);
                }
            }
            else
            {
                this.Show();
                this.Focus();
            }
        }
        #endregion

        #region 发送
        //发送
        private void btnSend_Click(object sender, EventArgs e)
        {
            ChatBoxContent content = this.chatBoxSend.GetContent();
            if (content.IsEmpty())
            {
                return;
            }

            try
            {
                byte[] buff = CompactPropertySerializer.Default.Serialize(content);
                byte[] encrypted = buff;
                if (GlobalResourceManager.Des3Encryption != null)
                {
                    encrypted = GlobalResourceManager.Des3Encryption.Encrypt(buff);
                }

                ++this.sendingCount;
                this.gifBox_wait.Visible = true;
                UIResultHandler handler = new UIResultHandler(this, this.HandleSentResult);
                this.rapidPassiveEngine.ContactsOutter.BroadcastBlob(this.currentGroup.GroupID, BroadcastTypes.BroadcastChat, encrypted, null, 2048, handler.Create(), null );
               
                this.AppendChatBoxContent(string.Format("{0}({1})", this.mine.Name, this.mine.UserID),null, content, Color.Green);
                ChatMessageRecord record = new ChatMessageRecord(this.mine.UserID, this.currentGroup.GroupID, buff, true);
                GlobalResourceManager.ChatMessageRecordPersister.InsertChatMessageRecord(record);

                //清空输入框
                this.chatBoxSend.Text = string.Empty;
                this.chatBoxSend.Focus();

                if (this.LastWordChanged != null)
                {
                    LastWordsRecord lastWordsRecord = new LastWordsRecord(this.mine.ID, this.mine.Name, true, content);
                    this.LastWordChanged(true, this.currentGroup.GroupID, lastWordsRecord);
                }
            }
            catch
            {
                this.AppendSysMessage("发送消息失败！");
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

        #region AppendMessage
        private void AppendChatBoxContent(string userName,DateTime? speakTime , ChatBoxContent content, Color color)
        {
            string showTime = speakTime == null ? DateTime.Now.ToLongTimeString() : speakTime.ToString();
            this.chatBox_history.AppendRichText(string.Format("{0}  {1}\n", userName, showTime), new Font(this.Font, FontStyle.Regular), color);
            this.chatBox_history.AppendText("    ");
            this.chatBox_history.AppendChatBoxContent(content);
            this.chatBox_history.AppendText("\n");
            this.chatBox_history.Select(this.chatBox_history.Text.Length, 0);
            this.chatBox_history.ScrollToCaret();
        }


        private void AppendMessage(string userName, Color color, string msg)
        {
            DateTime showTime = DateTime.Now;
            this.chatBox_history.AppendRichText(string.Format("{0}  {1}\n", userName, showTime.ToLongTimeString()), new Font(this.Font, FontStyle.Regular), color);
            this.chatBox_history.AppendText("    ");

            this.chatBox_history.AppendText(msg);
            this.chatBox_history.Select(this.chatBox_history.Text.Length, 0);
            this.chatBox_history.ScrollToCaret();
        }

        public void AppendSysMessage(string msg)
        {
            this.AppendMessage("系统", Color.Gray, msg);
            this.chatBox_history.AppendText("\n");
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
            }
        }
        #endregion        


        public void HandleReceivedMessage(List<Parameter<string, int, byte[]>> messageList)
        {
            foreach (Parameter<string, int, byte[]> para in messageList)
            {
                this.HandleReceivedMessage2(para.Arg1 ,para.Arg2, para.Arg3 ,false);
            }
        }

        public void HandleReceivedMessage(string broadcasterID, int broadcastType, byte[] content)
        {
            this.HandleReceivedMessage2(broadcasterID, broadcastType, content, true);
        }

        public void FlashChatWindow(bool flash)
        {
            if (flash)
            {
                JustHelper.FlashChatWindow(this);
            }
        }

        private void HandleReceivedMessage2(string broadcasterID, int broadcastType, byte[] content, bool flash)
        {
            GlobalResourceManager.UiSafeInvoker.ActionOnUI<string,int, byte[], bool>(this.DoHandleReceivedMessage, broadcasterID, broadcastType, content, flash);
        }

        private void DoHandleReceivedMessage(string broadcasterID, int broadcastType, byte[] content, bool flash)
        {
            if (broadcastType == BroadcastTypes.BroadcastChat)
            {
                ChatBoxContent chatBoxContent = CompactPropertySerializer.Default.Deserialize<ChatBoxContent>(content, 0);
                GGUser user = this.globalUserCache.GetUser(broadcasterID);
                string talker = string.Format("{0}({1})", broadcasterID, broadcasterID);
                if (user != null)
                {
                    talker = string.Format("{0}({1})", user.Name, user.UserID);
                }
                this.AppendChatBoxContent(talker, null, chatBoxContent, Color.Blue);
                this.FlashChatWindow(flash);
                if (this.LastWordChanged != null)
                {
                    LastWordsRecord lastWordsRecord = new LastWordsRecord(broadcasterID ,user == null ? broadcasterID : user.Name, false, chatBoxContent);
                    this.LastWordChanged(true, this.currentGroup.GroupID, lastWordsRecord);
                }
                return;
            }   
        }
        
        #endregion

        #region 窗体事件
        //悬浮至好友Q名时
        private void labelFriendName_MouseEnter(object sender, EventArgs e)
        {
            this.labelGroupName.Font = new Font("微软雅黑", 14F, FontStyle.Underline);
        }

        //离开好友Q名时
        private void labelFriendName_MouseLeave(object sender, EventArgs e)
        {
            this.labelGroupName.Font = new Font("微软雅黑", 14F);
        }

        //渐变层
        private void FrmChat_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush sb = new SolidBrush(Color.FromArgb(100, 255, 255, 255));
            g.FillRectangle(sb, new Rectangle(new Point(1, this.chatListBox1.Location.Y), new Size(Width - 2, Height - this.chatListBox1.Location.Y))); //91
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
            this.emotionForm.Visible = false;
            this.emotionForm.Close();           
            e.Cancel = false;
        } 
        #endregion

        #region 截图
        private void buttonCapture_Click(object sender, EventArgs e)
        {
            ScreenCapturer imageCapturer = new ScreenCapturer();
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
            PaintForm form = new PaintForm();
            form.Location = new Point(this.Left + 20, this.Top + skToolMenu.Top - form.Height);
            if (DialogResult.OK == form.ShowDialog())
            {
                Bitmap bitmap = form.CurrentImage;
                if (bitmap != null)
                {
                    this.chatBoxSend.InsertImage(bitmap);
                    this.chatBoxSend.Focus();
                    this.chatBoxSend.ScrollToCaret();
                }
            }
        } 
        #endregion        
       
        private void toolStripButtonEmotion_MouseUp(object sender, MouseEventArgs e)
        {
            Point pos = new Point((this.Left + 30) - (this.emotionForm.Width / 2), this.Top + skToolMenu.Top - this.emotionForm.Height);
            if (pos.X < 10)
            {
                pos = new Point(10, pos.Y);
            }
            this.emotionForm.Location = pos;
            this.emotionForm.Visible = !this.emotionForm.Visible;     
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ChatRecordForm form = new ChatRecordForm(GlobalResourceManager.RemotingService,GlobalResourceManager.ChatMessageRecordPersister, this.currentGroup.GetIDName() ,this.mine.GetIDName() ,this.globalUserCache);
            form.Show();
        }

        private void chatListBox1_DoubleClickSubItem(object sender, ChatListEventArgs e)
        {
            ChatListSubItem item = e.SelectSubItem;
            item.IsTwinkle = false;

            string friendID = item.ID;
            if (friendID == this.rapidPassiveEngine.CurrentUserID)
            {
                return;
            }           

            ChatForm form = this.ggSupporter.GetChatForm(friendID);
            form.Show();
            form.Focus();
        }

        private void 发送本地图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string file = ESBasic.Helpers.FileHelper.GetFileToOpen2("请选择图片", null, ".jpg", ".bmp", ".png", ".gif");
                if (file == null)
                {
                    return;
                }

                Image img = Image.FromFile(file);
                this.chatBoxSend.InsertImage(img);
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show(ee.Message, GlobalResourceManager.SoftwareName);
            }
        }

        private void 发送屏幕截屏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap capturedBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics graphics4CapturedBitmap = Graphics.FromImage(capturedBitmap);
                graphics4CapturedBitmap.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.PrimaryScreen.Bounds.Size);
                graphics4CapturedBitmap.Dispose();
                this.chatBoxSend.InsertImage(capturedBitmap);
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show(ee.Message, "GGTalk");
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            try
            {
                string file = ESBasic.Helpers.FileHelper.GetFileToOpen2("请选择图片", null, ".jpg", ".bmp", ".png", ".gif");
                if (file == null)
                {
                    return;
                }

                Image img = Image.FromFile(file);
                this.chatBoxSend.InsertImage(img);
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show(ee.Message, "GGTalk");
            }
        }        
    }
}
