using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESPlus.Rapid;
using ESPlus.Application.CustomizeInfo;
using CCWin;
using System.Drawing;
using JustLib.NetworkDisk.Passive;
using OMCS.Passive;
using System.Configuration;

namespace GGTalk
{
    static class Program
    {
        public static IMultimediaManager MultimediaManager;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                GlobalResourceManager.PreInitialize();

                ESPlus.GlobalUtil.SetMaxLengthOfUserID(20);
                ESPlus.GlobalUtil.SetMaxLengthOfMessage(1024 * 1024 * 10);
                OMCS.GlobalUtil.SetMaxLengthOfUserID(20);
                var mainForm = new MainForm();
                var passiveEngine = RapidEngineFactory.CreatePassiveEngine();

                var nDiskPassiveHandler = new NDiskPassiveHandler(); //V 2.0
                var complexHandler = new ComplexCustomizeHandler(nDiskPassiveHandler, mainForm);//V 2.0
                var loginForm = new LoginForm(passiveEngine, complexHandler);

                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                #region 初始化OMCS
                Program.MultimediaManager = MultimediaManagerFactory.GetSingleton();                
                Program.MultimediaManager.CameraDeviceIndex = SystemSettings.Singleton.WebcamIndex;
                Program.MultimediaManager.MicrophoneDeviceIndex = SystemSettings.Singleton.MicrophoneIndex;
                var okSize = OMCS.Tools.Camera.MatchCameraVideoSize(SystemSettings.Singleton.WebcamIndex, GlobalConsts.CommonQualityVideo);
                Program.MultimediaManager.CameraVideoSize = okSize == null ? new Size(320, 240) : okSize.Value;
                Program.MultimediaManager.OmcsLogger = GlobalResourceManager.Logger;
                Program.MultimediaManager.Advanced.VideoQualityEnhanced = true;
                Program.MultimediaManager.Initialize(passiveEngine.CurrentUserID, "", ConfigurationManager.AppSettings["ServerIP"], int.Parse(ConfigurationManager.AppSettings["OmcsServerPort"]));               
                #endregion

                nDiskPassiveHandler.Initialize(passiveEngine.FileOutter, null);
                mainForm.Initialize(passiveEngine, loginForm.LoginStatus, loginForm.StateImage);
                Application.Run(mainForm);
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show(ee.Message);
                ee = ee;
            }
        }
    }

}
