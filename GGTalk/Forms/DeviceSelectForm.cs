using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OMCS.Tools;
using OMCS.Server;
using OMCS.Passive;
using ESBasic;
using OMCS;
using OMCS.Communication.Basic;


namespace GGTalk
{
    /// <summary>
    /// 用于选择摄像头、麦克风、扬声器设备的选择窗口。
    /// </summary>
    public partial class DeviceSelectForm : Form
    {
        private IMultimediaManager multimediaManager = MultimediaManagerFactory.GetSingleton();
        private string userID;
        private AgileIPEndPoint ipe;
        public event CbGeneric<DeviceSelectForm> DeviceSelected;

        #region CameraIndex、MicrophoneIndex、SpeakerIndex
        public int CameraIndex
        {
            get
            {
                return this.comboBox_camera.SelectedIndex;
            }
        }

        public int MicrophoneIndex
        {
            get
            {
                return this.comboBox_mic.SelectedIndex;
            }
        }

        public int SpeakerIndex
        {
            get
            {
                return this.comboBox_speaker.SelectedIndex;
            }
        } 
        #endregion

        public DeviceSelectForm() : this(null) 
        {              
        }

        public DeviceSelectForm(IMultimediaManager mgr)
        {
            InitializeComponent();

            this.multimediaManager = mgr ?? MultimediaManagerFactory.GetSingleton();
            this.userID = this.multimediaManager.CurrentUserID;
            this.ipe = this.multimediaManager.ServerIPE;

            this.multimediaManager.AudioCaptured += new CbGeneric<byte[]>(multimediaManager_AudioCaptured);
            this.multimediaManager.DeviceErrorOccurred += new CbGeneric<MultimediaDeviceType, int, string>(multimediaManager_DeviceErrorOccurred);
            this.cameraConnector1.ConnectEnded += new CbGeneric<ConnectResult>(cameraConnector1_ConnectEnded);
            this.microphoneConnector1.ConnectEnded += new CbGeneric<ConnectResult>(microphoneConnector1_ConnectEnded);

            this.button_refresh_Click(this.button_refresh, new EventArgs());
            try
            {
                this.comboBox_camera.SelectedIndex = this.multimediaManager.CameraDeviceIndex;
                this.comboBox_mic.SelectedIndex = this.multimediaManager.MicrophoneDeviceIndex;
                this.comboBox_speaker.SelectedIndex = this.multimediaManager.SpeakerIndex;
            }
            catch { }
        }

        void multimediaManager_AudioCaptured(byte[] data)
        {
            if (!this.button_start.Enabled)
            {
                this.decibelDisplayer1.DisplayAudioData(data);
            }
        }

        void multimediaManager_DeviceErrorOccurred(MultimediaDeviceType deviceType, int deviceIndex, string error)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric<MultimediaDeviceType, int, string>(this.multimediaManager_DeviceErrorOccurred), deviceType, deviceIndex ,error);
            }
            else
            {
                MessageBox.Show(string.Format("Error : {0} - {1}" ,deviceType,error));
            }
        }

        void microphoneConnector1_ConnectEnded(ConnectResult res)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric<ConnectResult>(this.microphoneConnector1_ConnectEnded), res);
            }
            else
            {
                if (res != ConnectResult.Succeed)
                {
                    this.label_error2.Visible = true;
                    this.label_error2.Text = res.ToString();
                }
                else
                {
                    this.label_error2.Visible = false;
                }                
            }
        }

        void cameraConnector1_ConnectEnded(ConnectResult res)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric<ConnectResult>(this.cameraConnector1_ConnectEnded), res);
            }                
            else
            {
                if (res != ConnectResult.Succeed)
                {
                    this.label_error.Visible = true;
                    this.label_error.Text = res.ToString();
                }
                else
                {
                    this.label_error.Visible = false;                   
                }
                this.label_info.Visible = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.label_error.Visible = false;
                this.label_error2.Visible = false;
                this.label_error3.Visible = false;
                this.label_info.Visible = true;
                if (!SoundDevice.IsSoundCardInstalled())
                {
                    this.label_error3.Visible = true;
                    this.label_error3.Text = "声卡没有安装";
                }

                bool reIni = this.multimediaManager.CameraDeviceIndex != this.comboBox_camera.SelectedIndex ||
                             this.multimediaManager.MicrophoneDeviceIndex != this.comboBox_mic.SelectedIndex ||
                             this.multimediaManager.SpeakerIndex != this.comboBox_speaker.SelectedIndex;
                     
                this.multimediaManager.CameraDeviceIndex = this.comboBox_camera.SelectedIndex;
                this.multimediaManager.MicrophoneDeviceIndex = this.comboBox_mic.SelectedIndex;
                this.multimediaManager.SpeakerIndex = this.comboBox_speaker.SelectedIndex;
                if (this.DeviceSelected != null)
                {
                    this.DeviceSelected(this);
                }

                if (!this.multimediaManager.Available)
                {
                    this.multimediaManager.Initialize(this.userID, "", this.ipe.IPAddress, this.ipe.Port); //与OMCS服务器建立连接，并登录
                }

                //尝试连接设备
                this.cameraConnector1.BeginConnect(this.userID);
                this.microphoneConnector1.BeginConnect(this.userID);

                this.decibelDisplayer1.Working = true;
                this.button_start.Enabled = false;               
                this.button_stop.Enabled = true;
                this.button_refresh.Enabled = false;
                this.comboBox_camera.Enabled = false;
                this.comboBox_mic.Enabled = false;
                this.comboBox_speaker.Enabled = false;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            try
            {
                //断开到设备的连接
                this.cameraConnector1.Disconnect();
                this.microphoneConnector1.Disconnect();               

                this.button_start.Enabled = true;
                this.button_stop.Enabled = false;                
                this.button_refresh.Enabled = true;  
                this.comboBox_camera.Enabled = true;
                this.comboBox_mic.Enabled = true;
                this.comboBox_speaker.Enabled = true;
                this.decibelDisplayer1.Working = false;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            try
            {
                //获取摄像头列表
                IList<CameraInformation> cameras = Camera.GetCameras();
                this.comboBox_camera.DataSource = cameras;
                if (cameras.Count > 0)
                {
                    this.comboBox_camera.SelectedIndex = 0;
                }

                //获取麦克风列表
                IList<MicrophoneInformation> microphones = SoundDevice.GetMicrophones();
                this.comboBox_mic.DataSource = microphones;
                if (microphones.Count > 0)
                {
                    this.comboBox_mic.SelectedIndex = 0;
                }

                //获取扬声器列表
                IList<SpeakerInformation> speakers = SoundDevice.GetSpeakers();
                this.comboBox_speaker.DataSource = speakers;
                if (speakers.Count > 0)
                {
                    this.comboBox_speaker.SelectedIndex = 0;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void DeviceSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.button_stop_Click(this.button_stop, new EventArgs());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}
