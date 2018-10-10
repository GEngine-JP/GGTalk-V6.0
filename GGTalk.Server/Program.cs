using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESPlus.Rapid;
using ESPlus.Application.CustomizeInfo.Server;
using ESFramework;
using ESFramework.Server.UserManagement;
using ESPlus.Core;
using ESPlus.Application.Friends.Server;
using System.Configuration;
using OMCS;
using OMCS.Server;
using ESPlus.Application.CustomizeInfo;
using System.Runtime.Remoting;
using JustLib.NetworkDisk.Server;
using DataRabbit.DBAccessing;

/*
 * 本demo采用的是ESFramework的免费版本，不需要再次授权。若想获取ESFramework其它版本，请联系 www.oraycn.com。
 * 
 */
namespace GGTalk.Server
{
    static class Program
    {
        private static IRapidServerEngine RapidServerEngine = RapidEngineFactory.CreateServerEngine();
        private static IMultimediaServer MultimediaServer;            

        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                IDbPersister persister;
                if (bool.Parse(ConfigurationManager.AppSettings["UseVirtualDB"]))
                {
                    persister = new VirtualDB();
                }
                else
                {
                    var dataBaseType = (DataBaseType)Enum.Parse(typeof(DataBaseType),ConfigurationManager.AppSettings["DBType"]) ;
                    if (dataBaseType == DataBaseType.SqlServer)
                    {
                        persister = new RealDb(ConfigurationManager.AppSettings["DBName"], ConfigurationManager.AppSettings["DBIP"], ConfigurationManager.AppSettings["SaPwd"]);
                    }
                    else //MySQL
                    {
                        persister = new RealDb(ConfigurationManager.AppSettings["DBName"], ConfigurationManager.AppSettings["DBIP"], int.Parse(ConfigurationManager.AppSettings["DBPort"]), ConfigurationManager.AppSettings["SaPwd"]);
                    }
                }

                var globalCache = new GlobalCache(persister);

                #region 初始化ESFramework服务端引擎
                ESPlus.GlobalUtil.SetAuthorizedUser("FreeUser", "");
                ESPlus.GlobalUtil.SetMaxLengthOfUserID(20);
                ESPlus.GlobalUtil.SetMaxLengthOfMessage(1024 * 1024 * 10);
              
                //自定义的联系人管理器
                var contactsManager = new ContactsManager(globalCache);
                Program.RapidServerEngine.ContactsManager = contactsManager;


                var nDiskHandler = new NDiskHandler(); //网盘处理器 V1.9
                var handler = new CustomizeHandler();
                var complexHandler = new ComplexCustomizeHandler(nDiskHandler, handler); 

                //初始化服务端引擎
                Program.RapidServerEngine.SecurityLogEnabled = false;
                Program.RapidServerEngine.Initialize(int.Parse(ConfigurationManager.AppSettings["Port"]), complexHandler, new BasicHandler(globalCache));
                Program.RapidServerEngine.ContactsController.ContactsConnectedNotifyEnabled = false;
                Program.RapidServerEngine.ContactsController.ContactsDisconnectedNotifyEnabled = true;                
                Program.RapidServerEngine.ContactsController.BroadcastBlobListened = true; //为群聊天记录

                //初始化网盘处理器 V1.9
                var networkDiskPathManager = new NetworkDiskPathManager() ;
                var networkDisk = new NetworkDisk(networkDiskPathManager, Program.RapidServerEngine.FileController);
                nDiskHandler.Initialize(Program.RapidServerEngine.FileController, networkDisk);

                //设置重登陆模式
                Program.RapidServerEngine.UserManager.RelogonMode = RelogonMode.ReplaceOld; 

                //离线消息控制器 V3.2
                var offlineFileController = new OfflineFileController(Program.RapidServerEngine, globalCache);

                handler.Initialize(globalCache, Program.RapidServerEngine, offlineFileController);
                #endregion            

                #region 初始化OMCS服务器
                OMCS.GlobalUtil.SetAuthorizedUser("FreeUser", "");
                OMCS.GlobalUtil.SetMaxLengthOfUserID(20);
                var config = new OMCSConfiguration();

                //用于验证登录用户的帐密
                var userVerifier = new DefaultUserVerifier();
                Program.MultimediaServer = MultimediaServerFactory.CreateMultimediaServer(int.Parse(ConfigurationManager.AppSettings["OmcsPort"]), userVerifier, config,false);                          
                
                #endregion

                #region 发布用于注册的Remoting服务
                RemotingConfiguration.Configure("GGTalk.Server.exe.config", false);
                var registerService = new Server.RemotingService(globalCache ,Program.RapidServerEngine);
                RemotingServices.Marshal(registerService, "RemotingService");      
                #endregion       
 
                //如果不需要默认的UI显示，可以替换下面这句为自己的Form
                var mainForm = new MainServerForm(Program.RapidServerEngine);
                mainForm.Text = ConfigurationManager.AppSettings["SoftwareName"] + " 服务器";
                Application.Run(mainForm);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }       
    }
}
