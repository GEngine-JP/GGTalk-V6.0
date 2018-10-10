using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using CCWin.SkinControl;
using System.Configuration;
using ESBasic.Loggers;
using System.IO;
using ESBasic;
using ESBasic.Helpers;
using JustLib.Records;
using JustLib;

namespace GGTalk
{
    /// <summary>
    /// 客户端全局资源管理器。
    /// </summary>
    internal class GlobalResourceManager
    {
        #region PreInitialize
        public static void PreInitialize()
        {
            try
            {
                #region Log
                string logFilePath = SystemSettings.SystemSettingsDir + "AppLog.txt";
                GlobalResourceManager.logger = new FileAgileLogger(logFilePath);
                #endregion

                GlobalResourceManager.softwareName = ConfigurationManager.AppSettings["softwareName"];
                string resourceDir = AppDomain.CurrentDomain.BaseDirectory + "Resource\\";
                GlobalResourceManager.noneIcon64 = global::GGTalk.Properties.Resources.None64;
                GlobalResourceManager.groupIcon = ImageHelper.ConvertToIcon(global::GGTalk.Properties.Resources.normal_group_40, 64);

                #region HeadImage
                List<string> list = ESBasic.Helpers.FileHelper.GetOffspringFiles(AppDomain.CurrentDomain.BaseDirectory + "Head\\");
                List<string> picList = new List<string>();
                foreach (string file in list)
                {
                    string name = file.ToLower();
                    if (name.EndsWith(".bmp") || name.EndsWith(".jpg") || name.EndsWith(".jpeg") || name.EndsWith(".png"))
                    {
                        picList.Add(name);
                    }
                }
                picList.Sort();
                GlobalResourceManager.headImages = new Image[picList.Count];
                for (int i = 0; i < picList.Count; i++)
                {
                    GlobalResourceManager.headImages[i] = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "head\\" + list[i]);
                }

                GlobalResourceManager.headImagesGrey = new Image[picList.Count];
                for (int i = 0; i < GlobalResourceManager.headImagesGrey.Length; i++)
                {
                    GlobalResourceManager.headImagesGrey[i] = ESBasic.Helpers.ImageHelper.ConvertToGrey(GlobalResourceManager.headImages[i]);
                }
                #endregion

                #region Emotion
                List<string> tempList = ESBasic.Helpers.FileHelper.GetOffspringFiles(AppDomain.CurrentDomain.BaseDirectory + "Emotion\\");
                List<string> emotionFileList = new List<string>();
                foreach (string file in tempList)
                {
                    string name = file.ToLower();
                    if (name.EndsWith(".bmp") || name.EndsWith(".jpg") || name.EndsWith(".jpeg") || name.EndsWith(".png") || name.EndsWith(".gif"))
                    {
                        emotionFileList.Add(name);
                    }
                }
                emotionFileList.Sort(new Comparison<string>(CompareEmotionName));
                List<Image> emotionList = new List<Image>();
                for (int i = 0; i < emotionFileList.Count; i++)
                {
                    emotionList.Add(Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Emotion\\" + emotionFileList[i]));
                }
                #endregion

                GlobalResourceManager.audioFilePath = resourceDir + "ring.wav";

                int registerPort = int.Parse(ConfigurationManager.AppSettings["RemotingPort"]);
                GlobalResourceManager.remotingService = (IRemotingService)Activator.GetObject(typeof(IRemotingService), string.Format("tcp://{0}:{1}/RemotingService", ConfigurationManager.AppSettings["ServerIP"], registerPort));

                GlobalResourceManager.png64 = Image.FromFile(resourceDir + "64.png");
                GlobalResourceManager.icon64 = ImageHelper.ConvertToIcon(GlobalResourceManager.png64, 64);
                GlobalResourceManager.icon64Grey = ImageHelper.ConvertToIcon(ImageHelper.ConvertToGrey(GlobalResourceManager.png64), 64);
                GlobalResourceManager.mainBackImage = Image.FromFile(resourceDir + "BackImage.png");
                GlobalResourceManager.emotionList = emotionList;
                GlobalResourceManager.loginBackImage = GlobalResourceManager.MainBackImage;
            }
            catch (Exception ee)
            {
                MessageBox.Show("加载系统资源时，出现错误。" + ee.Message);
            }
        }

        private static int CompareEmotionName(string a, string b)
        {
            if (a.Length != b.Length)
            {
                return a.Length - b.Length;
            }

            return a.CompareTo(b);
        } 
        #endregion

        #region PostInitialize
        public static void PostInitialize(string currentUserID)
        {
            #region ChatMessageRecordPersister
            GlobalResourceManager.sqliteFilePath = SystemSettings.SystemSettingsDir + currentUserID + ".db";
            GlobalResourceManager.chatMessageRecordPersister = new SqliteChatRecordPersister(GlobalResourceManager.sqliteFilePath);
            #endregion
        } 
        #endregion        

        #region Icon64
        private static Icon icon64;
        public static Icon Icon64
        {
            get { return icon64; }
        }
        #endregion

        #region Icon64Grey
        private static Icon icon64Grey;
        public static Icon Icon64Grey
        {
            get { return icon64Grey; }
        }
        #endregion

        #region MainBackImage
        private static Image mainBackImage;
        public static Image MainBackImage
        {
            get { return mainBackImage; }
        }
        #endregion

        #region EmotionList、EmotionDictionary
        private static List<Image> emotionList;
        public static List<Image> EmotionList
        {
            get { return emotionList; }
        }
        private static Dictionary<uint, Image> emotionDictionary;
        public static Dictionary<uint, Image> EmotionDictionary
        {
            get
            {
                if (emotionDictionary == null)
                {
                    emotionDictionary = new Dictionary<uint, Image>();
                    for (uint i = 0; i < emotionList.Count; i++)
                    {
                        emotionDictionary.Add(i, emotionList[(int)i]);
                    }
                }
                return emotionDictionary;
            }
        }
        #endregion

        #region Png64
        private static Image png64;
        public static Image Png64
        {
            get { return png64; }
        }
        #endregion

        #region SetStatusImage、GetStatusImage
        public static void SetStatusImage(Dictionary<UserStatus, Image> _statusImageDictionary)
        {
            GlobalResourceManager.do_SetStatusImage(_statusImageDictionary);
        }

        private static Dictionary<UserStatus, Icon> statusIconDictionary = new Dictionary<UserStatus, Icon>();
        private static Dictionary<UserStatus, Image> statusImageDictionary = new Dictionary<UserStatus, Image>();
        private static void do_SetStatusImage(Dictionary<UserStatus, Image> _statusImageDictionary)
        {
            GlobalResourceManager.statusImageDictionary = _statusImageDictionary;

            GlobalResourceManager.statusIconDictionary.Add(UserStatus.Online, GlobalResourceManager.Icon64);
            GlobalResourceManager.statusIconDictionary.Add(UserStatus.Busy, CombineStateImage(GlobalResourceManager.png64, statusImageDictionary[UserStatus.Busy]));
            GlobalResourceManager.statusIconDictionary.Add(UserStatus.Away, CombineStateImage(GlobalResourceManager.png64, statusImageDictionary[UserStatus.Away]));
            GlobalResourceManager.statusIconDictionary.Add(UserStatus.DontDisturb, CombineStateImage(GlobalResourceManager.png64, statusImageDictionary[UserStatus.DontDisturb]));
            GlobalResourceManager.statusIconDictionary.Add(UserStatus.Hide, CombineStateImage(GlobalResourceManager.png64, statusImageDictionary[UserStatus.Hide]));
            GlobalResourceManager.statusIconDictionary.Add(UserStatus.OffLine, GlobalResourceManager.Icon64Grey);

        }

        private static Icon CombineStateImage(Image img, Image stateImage)
        {
            Bitmap bm = new Bitmap(img);
            using (Graphics g = Graphics.FromImage(bm))
            {
                int len = (int)(img.Width * 0.45);
                g.DrawImage(stateImage, new Rectangle(len, len, img.Width - len, img.Height - len), new Rectangle(0, 0, stateImage.Width, stateImage.Height), GraphicsUnit.Pixel);
            }

            return ImageHelper.ConvertToIcon(bm, 64);
        }

        public static Image GetStatusImage(UserStatus status)
        {
            return GlobalResourceManager.statusImageDictionary[status];
        }

        public static Icon GetStatusIcon(UserStatus status)
        {
            return GlobalResourceManager.statusIconDictionary[status];
        }

        public static Dictionary<UserStatus, Image> GetStatusImageDictionary()
        {
            return statusImageDictionary;
        }
        #endregion

        #region ConvertUserStatus
        public static ChatListSubItem.UserStatus ConvertUserStatus(UserStatus status)
        {
            if (status == UserStatus.Hide)
            {
                return ChatListSubItem.UserStatus.OffLine;
            }

            return (ChatListSubItem.UserStatus)((int)status);
        }
        #endregion

        #region GetUserStatusName
        public static string GetUserStatusName(UserStatus status)
        {
            if (status == UserStatus.Online)
            {
                return "在线";
            }
            if (status == UserStatus.Away)
            {
                return "离开";
            }
            if (status == UserStatus.Busy)
            {
                return "忙碌";
            }
            if (status == UserStatus.DontDisturb)
            {
                return "请勿打扰";
            }
            return "隐身或离线";
        }
        #endregion

        #region RemotingService
        private static IRemotingService remotingService;
        public static IRemotingService RemotingService
        {
            get { return GlobalResourceManager.remotingService; }
        }
        #endregion

        #region SqliteFilePath
        private static string sqliteFilePath = "";
        private static SqliteChatRecordPersister chatMessageRecordPersister;
        public static SqliteChatRecordPersister ChatMessageRecordPersister
        {
            get { return chatMessageRecordPersister; }
        }
        #endregion

        #region Logger
        private static IAgileLogger logger = null;
        public static IAgileLogger Logger
        {
            get { return GlobalResourceManager.logger; }
        }
        #endregion

        #region UiSafeInvoker
        private static UiSafeInvoker uiSafeInvoker;
        public static UiSafeInvoker UiSafeInvoker
        {
            get { return GlobalResourceManager.uiSafeInvoker; }
        }

        public static void SetUiSafeInvoker(UiSafeInvoker invoker)
        {
            GlobalResourceManager.uiSafeInvoker = invoker;
        }
        #endregion

        #region SoftwareName
        private static string softwareName = "GGTalk";
        public static string SoftwareName
        {
            get { return GlobalResourceManager.softwareName; }
        }
        #endregion

        #region NoneIcon64
        private static Icon noneIcon64;
        public static Icon NoneIcon64
        {
            get { return noneIcon64; }
        }
        #endregion

        #region GroupIcon
        private static Icon groupIcon;
        public static Icon GroupIcon
        {
            get { return GlobalResourceManager.groupIcon; }
        }
        #endregion

        #region LoginBackImage
        private static Image loginBackImage;
        public static Image LoginBackImage
        {
            get { return loginBackImage; }
        }
        #endregion

        #region HeadImages
        private static Image[] headImages;
        public static Image[] HeadImages
        {
            get
            {
                return headImages;
            }
        }
        #endregion

        #region HeadImagesGrey
        private static Image[] headImagesGrey;
        public static Image[] HeadImagesGrey
        {
            get
            {
                return headImagesGrey;
            }
        }
        #endregion

        #region 播放声音
        public static void PlayAudioAsyn()
        {
            if (!SystemSettings.Singleton.PlayAudio4Message)
            {
                return;
            }

            CbGeneric<string, int> cbPlayAudio = new CbGeneric<string, int>(PlayAudio);
            cbPlayAudio.BeginInvoke(audioFilePath, 1, null, null);
        }

        private static string audioFilePath = "";
        private static void PlayAudio(string audioPath, int playTimes)
        {
            System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(audioPath);
            sndPlayer.Play();
            if (playTimes > 1)
            {
                System.Threading.Thread.Sleep(2000);
                sndPlayer.Play();
            }
        }
        #endregion

        #region GetHeadImage ,GetHeadImageOnline
        public static Image GetHeadImage(GGUser user)
        {
            return GlobalResourceManager.GetHeadImage(user, false);
        }

        public static Image GetHeadImageOnline(GGUser user)
        {
            if (user.HeadImageIndex >= 0)
            {
                if (user.HeadImageIndex < GlobalResourceManager.headImages.Length)
                {
                    return GlobalResourceManager.headImages[user.HeadImageIndex];
                }

                return GlobalResourceManager.headImages[0];
            }
            else
            {
                return user.HeadImage;
            }
        }

        public static Image GetHeadImage(GGUser user, bool mine)
        {
            if (user.HeadImageIndex >= 0)
            {
                Image[] ary = (mine ? !user.OnlineOrHide : user.OfflineOrHide) ? GlobalResourceManager.headImagesGrey : GlobalResourceManager.headImages;
                if (user.HeadImageIndex < GlobalResourceManager.headImages.Length)
                {
                    return ary[user.HeadImageIndex];
                }

                return ary[0];
            }
            else
            {
                return (mine ? !user.OnlineOrHide : user.OfflineOrHide) ? user.HeadImageGrey : user.HeadImage;
            }
        } 
        #endregion

        #region 2016.01 聊天消息加密、解密
        private static Des3Encryption des3Encryption = null ;//new Des3Encryption("abcd1234"); // null;        
        /// <summary>
        /// 3DES加密。如果消息不需要加密，则返回null。
        /// </summary>
        public static Des3Encryption Des3Encryption
        {
            get { return des3Encryption; }
        } 

        #endregion

    }
}
