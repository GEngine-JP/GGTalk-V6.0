using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace GGTalk
{
    /// <summary>
    /// 系统设置。保存用户设置的数据。
    /// </summary>
    [Serializable]
    public class SystemSettings
    {
        public static string SystemSettingsDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\GGTalk5.5\\" ;
        private static string SystemSettingsFilePath = SystemSettingsDir + "SystemSettings.dat";

        private static SystemSettings singleton;
        /// <summary>
        /// 单例模式。
        /// </summary>
        public static SystemSettings Singleton
        {
            get
            {
                if (SystemSettings.singleton == null)
                {
                    SystemSettings.singleton = SystemSettings.Load();
                    if (SystemSettings.singleton == null)
                    {
                        SystemSettings.singleton = new SystemSettings();
                    }
                }

                return SystemSettings.singleton;
            }           
        }

        private SystemSettings() { }

        public static void UserChanged()
        {
            SystemSettings.singleton = new SystemSettings();
        }

        #region AutoRun
        private bool autoRun = false;
        /// <summary>
        /// 是否开机自动 运行
        /// </summary>
        public bool AutoRun
        {
            get { return autoRun; }
            set { autoRun = value; }
        }
        #endregion

        #region WebcamIndex
        private int webcamIndex = 0;
        /// <summary>
        /// 摄像头索引
        /// </summary>
        public int WebcamIndex
        {
            get { return webcamIndex; }
            set { webcamIndex = value; }
        }
        #endregion

        #region MicrophoneIndex
        private int microphoneIndex = 0;
        /// <summary>
        /// 麦克风索引。
        /// </summary>
        public int MicrophoneIndex
        {
            get { return microphoneIndex; }
            set { microphoneIndex = value; }
        } 
        #endregion

        #region SpeakerIndex
        private int speakerIndex = 0;
        /// <summary>
        /// 扬声器索引。
        /// </summary>
        public int SpeakerIndex
        {
            get { return speakerIndex; }
            set { speakerIndex = value; }
        }
        #endregion

        #region ExitWhenCloseMainForm
        private bool exitWhenCloseMainForm = false;
        /// <summary>
        /// 点击关闭按钮的时候，是否退出程序。
        /// </summary>
        public bool ExitWhenCloseMainForm
        {
            get { return exitWhenCloseMainForm; }
            set { exitWhenCloseMainForm = value; }
        }
        #endregion

        #region PlayAudio4Message
        private bool playAudio4Message = true;
        /// <summary>
        /// 是否播放消息提示音。
        /// </summary>
        public bool PlayAudio4Message
        {
            get { return playAudio4Message; }
            set { playAudio4Message = value; }
        } 
        #endregion

        #region LastLoginUserID
        private string lastLoginUserID = "";
        /// <summary>
        /// 最后一次登录的帐号
        /// </summary>
        public string LastLoginUserID
        {
            get { return lastLoginUserID; }
            set { lastLoginUserID = value; }
        } 
        #endregion

        #region LastLoginPwdMD5
        private string lastLoginPwdMD5 = "";
        /// <summary>
        /// 最后一次登录的密码
        /// </summary
        public string LastLoginPwdMD5
        {
            get { return lastLoginPwdMD5; }
            set { lastLoginPwdMD5 = value; }
        } 
        #endregion

        #region RememberPwd
        private bool rememberPwd = false;
        /// <summary>
        /// 记住密码
        /// </summary>
        public bool RememberPwd
        {
            get { return rememberPwd; }
            set { rememberPwd = value; }
        } 
        #endregion

        #region AutoLogin
        private bool autoLogin = false;
        /// <summary>
        /// 自动登录
        /// </summary>
        public bool AutoLogin
        {
            get { return autoLogin; }
            set { autoLogin = value; }
        } 
        #endregion

        #region ShowLargeIcon
        private bool showLargeIcon = true;
        /// <summary>
        /// 显示大头像
        /// </summary>
        public bool ShowLargeIcon
        {
            get { return showLargeIcon; }
            set { showLargeIcon = value; }
        } 
        #endregion   
     
        #region Font
        private Font font = new Font("宋体", 9);
        /// <summary>
        /// 聊天采用的字体。
        /// </summary>
        public Font Font
        {
            get
            {
                if (this.font == null)
                {
                    this.font = new Font("宋体", 9);
                }
                return font;
            }
            set { font = value; }
        }
        #endregion

        #region FontColor
        private Color fontColor = Color.Black;
        /// <summary>
        /// 文字聊天所使用的字体颜色。
        /// </summary>
        public Color FontColor
        {
            get
            {
                return fontColor;
            }
            set { fontColor = value; }
        }
        #endregion

        #region ChatFormSize
        private Size chatFormSize = new Size(630, 510);
        /// <summary>
        /// 聊天窗口的大小
        /// </summary>
        public Size ChatFormSize
        {
            get
            {
                if (this.chatFormSize == new Size(0, 0))
                {
                    this.chatFormSize = new Size(630, 510);
                }
                return chatFormSize;
            }
            set { chatFormSize = value; }
        }
        #endregion

        #region MainFormSize
        private Size mainFormSize = new Size(291, 756);
        /// <summary>
        /// 主窗体的大小
        /// </summary>
        public Size MainFormSize
        {
            get 
            {
                if (this.mainFormSize == new Size(0, 0))
                {
                    this.mainFormSize = new Size(291, 756);
                }
                return mainFormSize; 
            }
            set { mainFormSize = value; }
        } 
        #endregion

        #region MainFormLocation
        private Point mainFormLocation = new Point(0, 0);
        /// <summary>
        /// 主窗体的位置
        /// </summary>
        public Point MainFormLocation
        {
            get
            {
                if (this.mainFormLocation == new Point(0, 0))
                {
                    this.mainFormLocation = new Point(Screen.PrimaryScreen.Bounds.Width - 291 - 20, 40);
                }
                return mainFormLocation;
            }
            set { mainFormLocation = value; }
        } 
        #endregion

        #region LoadLastWordsWhenChatFormOpened
        private bool loadLastWordsWhenChatFormOpened = true;
        /// <summary>
        /// 打开聊天窗口时，是否加载上次交谈的最后一句话。
        /// </summary>
        public bool LoadLastWordsWhenChatFormOpened
        {
            get { return loadLastWordsWhenChatFormOpened; }
            set { loadLastWordsWhenChatFormOpened = value; }
        }   
        #endregion

        #region ChatFormTopMost
        private bool chatFormTopMost = false;
        public bool ChatFormTopMost
        {
            get { return chatFormTopMost; }
            set { chatFormTopMost = value; }
        } 
        #endregion

        public void Save()
        {
            byte[] data = ESBasic.Helpers.SerializeHelper.SerializeObject(this);
            ESBasic.Helpers.FileHelper.WriteBuffToFile(data, SystemSettingsFilePath);
        }

        private static SystemSettings Load()
        {
            try
            {                
                if (!File.Exists(SystemSettingsFilePath))
                {
                    return null;
                }

                byte[] data = ESBasic.Helpers.FileHelper.ReadFileReturnBytes(SystemSettingsFilePath);
                return (SystemSettings)ESBasic.Helpers.SerializeHelper.DeserializeBytes(data,0,data.Length);
            }
            catch(Exception ee)
            {
                System.Windows.Forms.MessageBox.Show(ee.Message);
                return null;
            }
        }
    }

   
}
