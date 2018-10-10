using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ESBasic;
using OMCS.Passive;

namespace GGTalk
{
    /// <summary>
    /// 语音聊天控制面板
    /// </summary>
    public partial class AudioHandlePanel : UserControl
    {
        /// <summary>
        /// 挂断语音
        /// </summary>
        public event CbGeneric AudioTerminated;

        /// <summary>
        /// 回复语音请求
        /// </summary>
        public event CbGeneric<bool> AudioRequestAnswerd;

        public AudioHandlePanel()
        {
            InitializeComponent();

            this.microphoneConnector1.ConnectEnded += new CbGeneric<OMCS.Passive.ConnectResult>(microphoneConnector1_ConnectEnded);
            this.microphoneConnector1.AudioDataReceived += new CbGeneric<byte[]>(microphoneConnector1_AudioDataReceived);
            this.timerLabel1.Visible = false;
            this.skinLabel_msg.Visible = true;
        }

        void microphoneConnector1_AudioDataReceived(byte[] data)
        {
            this.decibelDisplayer1.DisplayAudioData(data);
        }

        void microphoneConnector1_ConnectEnded(OMCS.Passive.ConnectResult res)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric<ConnectResult>(this.microphoneConnector1_ConnectEnded), res);
            }
            else
            {
                try
                {
                    this.timerLabel1.Start();
                    this.timerLabel1.Location = new Point(this.Width / 2 - this.timerLabel1.Width / 2, this.timerLabel1.Location.Y);
                    this.channelQualityDisplayer1.Initialize(this.friendID);
                    this.channelQualityDisplayer1.Visible = true;
                    this.decibelDisplayer1.Working = true;
                    this.decibelDisplayer2.Working = true;
                    this.panel_decibel.Visible = true;
                }
                catch (Exception ee)
                {
                    GlobalResourceManager.Logger.Log(ee, "microphoneConnector1_ConnectEnded", ESBasic.Loggers.ErrorLevel.Standard);
                    MessageBox.Show(ee.Message + " - " + ee.StackTrace);
                }
            }
        }

        private string friendID;
        public void Initialize(string destID )
        {
            this.friendID = destID;           
        }

        void mgr_AudioCaptured(byte[] obj)
        {
            this.decibelDisplayer2.DisplayAudioData(obj);
        }

        public bool IsWorking
        {
            get
            {
                return this.timerLabel1.IsWorking;
            }
        }

        private bool isSender = false;
        public bool IsSender
        {
            get
            {
                return this.isSender;
            }
            set
            {
                this.isSender = value;
                this.skinButtomReject.Visible = !value;
                this.btnAccept.Visible = !value;
                this.skinButton_HungUp.Visible = value;
                this.skinLabel_msg.Visible = value;
            }
        }

        private void skinButton_HungUp_Click(object sender, EventArgs e)
        {
            this.microphoneConnector1.Disconnect();
            if (this.AudioTerminated != null)
            {
                this.AudioTerminated();
            }

            this.OnTerminate();
        }

        private IMultimediaManager multimediaManager;
        public void OnAgree(IMultimediaManager mgr)
        {
            this.panel_decibel.Visible = false;
            this.multimediaManager = mgr;
            this.microphoneConnector1.BeginConnect(this.friendID);
            this.multimediaManager.AudioCaptured += new CbGeneric<byte[]>(mgr_AudioCaptured);            
            this.timerLabel1.Visible = true;
            this.skinLabel_msg.Visible = false;
            this.skinButton_HungUp.Visible = true;
            this.btnAccept.Visible = false;
            this.skinButtomReject.Visible = false;    
        }
        
        public void OnTerminate()
        {
            if (this.multimediaManager != null)
            {
                this.multimediaManager.AudioCaptured -= new CbGeneric<byte[]>(mgr_AudioCaptured);
            }

            this.microphoneConnector1.Disconnect();
            
            this.timerLabel1.Stop();
            this.timerLabel1.Reset();     
            this.timerLabel1.Visible = false;
            this.channelQualityDisplayer1.Visible = false;
            this.skinLabel_msg.Visible = true;

            this.decibelDisplayer1.Working = false;
            this.decibelDisplayer2.Working = false;
            this.panel_decibel.Visible = false;  
        }

        private void skinButtomReject_Click(object sender, EventArgs e)
        {
            if (this.AudioRequestAnswerd != null)
            {
                this.AudioRequestAnswerd(false);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (this.AudioRequestAnswerd != null)
            {
                this.AudioRequestAnswerd(true);
            }
        }
    }
}
