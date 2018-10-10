using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ESBasic.ObjectManagement;
using ESBasic;
using System.Windows.Forms;
using System.Drawing;
using ESBasic.ObjectManagement.Forms;
using JustLib;


namespace GGTalk
{
    #region support
    public enum UnhandleMessageType
    {
        Friend = 0,
        Group
    }

    public interface ITwinkleNotifySupporter
    {
        IChatForm GetChatForm(string friendID);
        IChatForm GetExistedChatForm(string friendID);
        IGroupChatForm GetGroupChatForm(string groupID);
        IGroupChatForm GetExistedGroupChatForm(string groupID);
        string GetFriendName(string friendID);
        string GetGroupName(string groupID);

        void PlayAudioAsyn();
        Icon GetHeadIcon(string userID);
        Icon Icon64 { get; }
        Icon NoneIcon64 { get; }
        Icon GroupIcon { get; }
        Icon GetStatusIcon(UserStatus status);
    }


    public interface IChatForm
    {
        void HandleReceivedMessage(List<Parameter<int, byte[], object>> MessageList);
        void HandleReceivedMessage(int informationType, byte[] info, object tag);
    }

    public interface IGroupChatForm
    {
        void HandleReceivedMessage(List<Parameter<string, int, byte[]>> MessageList);
        void HandleReceivedMessage(string broadcasterID, int broadcastType, byte[] content);
    } 
    #endregion

    /// <summary>
    /// 会闪动的托盘图标。缓存未处理的好友或群 消息。
    /// </summary>
    public partial class TwinkleNotifyIcon : Component
    {
        private Control control;
        private object locker = new object();
        private List<UnhandleFriendMessageBox> friendQueue = new List<UnhandleFriendMessageBox>();
        private List<UnhandleGroupMessageBox> groupQueue = new List<UnhandleGroupMessageBox>();
        private ITwinkleNotifySupporter twinkleNotifySupporter;
        private System.Windows.Forms.Timer timer = new Timer();
        /// <summary>
        /// 当出现未处理的聊天消息时，在后台线程中触发此事件。
        /// </summary>
        public event CbGeneric<UnhandleMessageType, string> UnhandleMessageOccured;
        /// <summary>
        /// 当聊天消息已经被提取时，在后台线程中触发此事件。
        /// </summary>
        public event CbGeneric<UnhandleMessageType, string> UnhandleMessageGone;
        public event MouseEventHandler MouseClick;

        public TwinkleNotifyIcon(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        public void Initialize(ITwinkleNotifySupporter getter, Control ctrl)
        {
            this.control = ctrl;
            this.twinkleNotifySupporter = getter;
            this.timer.Tick += new EventHandler(timer_Tick);
            this.timer.Interval = 500;

            this.normalIcon = this.twinkleNotifySupporter.Icon64;
            this.notifyIcon1.Icon = this.normalIcon;
            this.notifyIcon1.MouseClick += new MouseEventHandler(notifyIcon1_MouseClick);
        }

        private void ControlTimer(bool start)
        {
            if (this.control.InvokeRequired)
            {
                this.control.BeginInvoke(new CbGeneric<bool>(this.ControlTimer), start);
            }
            else
            {
                if (start)
                {
                    this.timer.Start(); ;
                }
                else
                {
                    this.timer.Stop();
                    //2014.11.05
                    this.notifyIcon1.Icon = this.normalIcon;
                    this.notifyIcon1.Text = this.normalText;
                }
            }
        }


        private Icon twinkleIcon = null;
        private Icon normalIcon;
        public void ChangeMyStatus(UserStatus status)
        {
            this.normalIcon = this.twinkleNotifySupporter.GetStatusIcon(status);
            if (!this.timer.Enabled)
            {
                this.notifyIcon1.Icon = this.normalIcon;
            }
        }

        private string normalText = null;
        public void ChangeText(string text)
        {
            this.normalText = text;
            if (!this.timer.Enabled)
            {
                this.notifyIcon1.Text = this.normalText;
            }
        }

        void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Left)
                {
                    return;
                }

                lock (this.locker)
                {
                    if (this.friendQueue.Count > 0)
                    {
                        UnhandleFriendMessageBox cache = this.friendQueue[0];
                        this.friendQueue.RemoveAt(0);
                        IChatForm form = this.twinkleNotifySupporter.GetChatForm(cache.User);
                        if (form != null) //如果为null,表示刚删除好友
                        {
                            form.HandleReceivedMessage(cache.MessageList);
                        }

                        this.DetectUnhandleMessage();

                        if (this.UnhandleMessageGone != null)
                        {
                            this.UnhandleMessageGone(UnhandleMessageType.Friend, cache.User);
                        }
                        return;
                    }

                    if (this.groupQueue.Count > 0)
                    {
                        UnhandleGroupMessageBox cache = this.groupQueue[0];
                        this.groupQueue.RemoveAt(0);
                        IGroupChatForm form = this.twinkleNotifySupporter.GetGroupChatForm(cache.Group);
                        form.HandleReceivedMessage(cache.MessageList);

                        this.DetectUnhandleMessage();

                        if (this.UnhandleMessageGone != null)
                        {
                            this.UnhandleMessageGone(UnhandleMessageType.Group, cache.Group);
                        }
                        return;
                    }
                }

                if (this.MouseClick != null)
                {
                    this.MouseClick(sender, e);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + " - " + ee.StackTrace);
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (this.friendQueue.Count == 0 && this.groupQueue.Count == 0)
            {
                this.ControlTimer(false);
                return;
            }

            if (this.notifyIcon1.Icon == this.twinkleNotifySupporter.NoneIcon64)
            {
                this.notifyIcon1.Icon = this.twinkleIcon;
            }
            else
            {
                this.notifyIcon1.Icon = this.twinkleNotifySupporter.NoneIcon64;
            }
        }

        #region Property
        public ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return this.notifyIcon1.ContextMenuStrip;
            }
            set
            {
                this.notifyIcon1.ContextMenuStrip = value;
            }
        }

        public bool Visible
        {
            get
            {
                return this.notifyIcon1.Visible;
            }
            set
            {
                this.notifyIcon1.Visible = value;
            }
        }
        #endregion

        #region PushFriendMessage
        public void PushFriendMessage(string userID, int informationType, byte[] info, object tag)
        {           
            lock (this.locker)
            {
                try
                {
                    this.twinkleNotifySupporter.PlayAudioAsyn(); //播放消息提示音
                    //首先查看是否已经存在对应的聊天窗口
                    IChatForm form = this.twinkleNotifySupporter.GetExistedChatForm(userID); 
                    if (form != null)
                    {
                        form.HandleReceivedMessage(informationType, info, tag);
                        return;
                    }

                    //接下来准备将消息压入queue
                    UnhandleFriendMessageBox cache = null;
                    lock (this.locker)
                    {
                        //先查看queue中目标好友对应的Cache是否存在
                        for (int i = 0; i < this.friendQueue.Count; i++) 
                        {
                            if (this.friendQueue[i].User == userID)
                            {
                                cache = this.friendQueue[i];
                                break;
                            }
                        }

                        if (cache == null) //如果不存在，则为好友新建一个Cache
                        {
                            cache = new UnhandleFriendMessageBox(userID);
                            this.friendQueue.Add(cache);
                            //触发UnhandleMessageOccured事件
                            if (this.UnhandleMessageOccured != null)
                            {
                                this.UnhandleMessageOccured(UnhandleMessageType.Friend, userID); 
                            }
                        }

                        cache.MessageList.Add(new Parameter<int, byte[], object>(informationType, info, tag));
                    }

                    string userName = this.twinkleNotifySupporter.GetFriendName(userID);
                    this.notifyIcon1.Text = string.Format("{0}({1})  {2}条消息", userName, userID, cache.MessageList.Count);
                    //获取好友的头像，将其作为托盘图标
                    this.twinkleIcon = this.twinkleNotifySupporter.GetHeadIcon(userID);
                    this.ControlTimer(true); //启动闪烁
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }
        #endregion

        #region PushGroupMessage
        public void PushGroupMessage(string broadcasterID, string groupID, int broadcastType, byte[] content)
        {           
            lock (this.locker)
            {
                this.twinkleNotifySupporter.PlayAudioAsyn(); 
                IGroupChatForm form = this.twinkleNotifySupporter.GetExistedGroupChatForm(groupID);
                if (form != null)
                {
                    form.HandleReceivedMessage(broadcasterID, broadcastType, content);
                    return;
                }

                UnhandleGroupMessageBox cache = null;
                lock (this.locker)
                {
                    for (int i = 0; i < this.groupQueue.Count; i++)
                    {
                        if (this.groupQueue[i].Group == groupID)
                        {
                            cache = this.groupQueue[i];
                            break;
                        }
                    }

                    if (cache == null)
                    {
                        cache = new UnhandleGroupMessageBox(groupID);
                        this.groupQueue.Add(cache);
                        if (this.UnhandleMessageOccured != null)
                        {
                            this.UnhandleMessageOccured(UnhandleMessageType.Group, groupID);
                        }
                    }

                    cache.MessageList.Add(new Parameter<string, int, byte[]>(broadcasterID, broadcastType, content));
                }
                string groupName = this.twinkleNotifySupporter.GetGroupName(groupID);
                this.notifyIcon1.Text = string.Format("{0}({1})  {2}条消息", groupName, groupID, cache.MessageList.Count);
                this.twinkleIcon = this.twinkleNotifySupporter.GroupIcon;
                this.ControlTimer(true);
            }
        }
        #endregion

        #region PickoutFriendMessageCache
        public UnhandleFriendMessageBox PickoutFriendMessageCache(string userID)
        {
            lock (this.locker)
            {
                for (int i = 0; i < this.friendQueue.Count; i++)
                {
                    UnhandleFriendMessageBox tmp = this.friendQueue[i];
                    if (tmp.User == userID)
                    {
                        this.friendQueue.RemoveAt(i);
                        this.DetectUnhandleMessage();
                        if (this.UnhandleMessageGone != null)
                        {
                            this.UnhandleMessageGone(UnhandleMessageType.Friend, userID);
                        }
                        return tmp;
                    }
                }
            }

            return null;
        }
        #endregion

        #region PickoutGroupMessageCache
        public UnhandleGroupMessageBox PickoutGroupMessageCache(string groupID)
        {
            lock (this.locker)
            {
                for (int i = 0; i < this.groupQueue.Count; i++)
                {
                    UnhandleGroupMessageBox tmp = this.groupQueue[i];
                    if (tmp.Group == groupID)
                    {
                        this.groupQueue.RemoveAt(i);
                        this.DetectUnhandleMessage();
                        if (this.UnhandleMessageGone != null)
                        {
                            this.UnhandleMessageGone(UnhandleMessageType.Group, groupID);
                        }
                        return tmp;
                    }
                }
            }

            return null;
        }
        #endregion

        #region DetectUnhandleMessage
        private void DetectUnhandleMessage()
        {
            if (this.friendQueue.Count == 0 && this.groupQueue.Count == 0)
            {
                this.ControlTimer(false);
            }
            else if (this.friendQueue.Count > 0)
            {
                UnhandleFriendMessageBox cache = this.friendQueue[0];
                string userName = this.twinkleNotifySupporter.GetFriendName(cache.User);
                this.notifyIcon1.Text = string.Format("{0}({1})  {2}条消息", cache.User, userName, cache.MessageList.Count);
                this.twinkleIcon = this.twinkleNotifySupporter.GetHeadIcon(cache.User);
            }
            else
            {
                UnhandleGroupMessageBox cache = this.groupQueue[0];
                string groupName = this.twinkleNotifySupporter.GetGroupName(cache.Group);
                this.notifyIcon1.Text = string.Format("{0}({1})  {2}条消息", groupName, cache.Group, cache.MessageList.Count);
                this.twinkleIcon = this.twinkleNotifySupporter.GroupIcon;
            }
        }
        #endregion
    }

    #region UnhandleFriendMessageBox
    public class UnhandleFriendMessageBox
    {
        public UnhandleFriendMessageBox(string user)
        {
            this.User = user;
        }

        public string User { get; set; }
        //object 用于存放解析后的数据
        public List<Parameter<int, byte[], object>> MessageList = new List<Parameter<int, byte[], object>>();
    } 
    #endregion

    #region UnhandleGroupMessageBox
    public class UnhandleGroupMessageBox
    {
        public UnhandleGroupMessageBox(string group)
        {
            this.Group = group;
        }

        public string Group { get; set; }
        public List<Parameter<string, int, byte[]>> MessageList = new List<Parameter<string, int, byte[]>>();
    }
    #endregion
}
