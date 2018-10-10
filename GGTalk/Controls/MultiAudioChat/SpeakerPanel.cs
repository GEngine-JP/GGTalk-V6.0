using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OMCS.Passive.MultiChat;
using OMCS.Passive;
using ESBasic;

namespace GGTalk.Controls
{
    public partial class SpeakerPanel : UserControl ,IDisposable
    {
        private IChatUnit chatUnit;     

        public SpeakerPanel()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);// 禁止擦除背景.
            this.SetStyle(ControlStyles.UserPaint, true);//自行绘制            
            this.UpdateStyles();
        }

        public string MemberID
        {
            get
            {
                if (this.chatUnit == null)
                {
                    return null;
                }

                return this.chatUnit.MemberID;
            }
        }

        public void Initialize(IChatUnit unit)
        {
            this.chatUnit = unit;
            this.skinLabel_name.Text = unit.MemberID;
                   
            this.chatUnit.MicrophoneConnector.ConnectEnded += new ESBasic.CbGeneric<OMCS.Passive.ConnectResult>(MicrophoneConnector_ConnectEnded);
            this.chatUnit.MicrophoneConnector.OwnerOutputChanged += new CbGeneric(MicrophoneConnector_OwnerOutputChanged);
            this.chatUnit.MicrophoneConnector.AudioDataReceived += new CbGeneric<byte[]>(MicrophoneConnector_AudioDataReceived);
            this.chatUnit.MicrophoneConnector.BeginConnect(unit.MemberID);
        }

        public void Initialize(string curUserID)
        {
            this.skinLabel_name.Text = curUserID;
            this.pictureBox_Mic.Visible = false;
            this.decibelDisplayer1.Visible = false;
        }

        void MicrophoneConnector_AudioDataReceived(byte[] data)
        {
            this.decibelDisplayer1.DisplayAudioData(data);
        }

        void MicrophoneConnector_OwnerOutputChanged()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric(this.MicrophoneConnector_OwnerOutputChanged));
            }
            else
            {
                this.ShowMicState();
            }
        }

        private ConnectResult connectResult;
        void MicrophoneConnector_ConnectEnded(ConnectResult res)
        {            
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric<ConnectResult>(this.MicrophoneConnector_ConnectEnded), res);
            }
            else
            {
                this.connectResult = res;
                this.ShowMicState();
            }
        }        

        private void ShowMicState()
        {
            if (this.connectResult != OMCS.Passive.ConnectResult.Succeed)
            {
                this.pictureBox_Mic.BackgroundImage = this.imageList1.Images[2];
                this.toolTip1.SetToolTip(this.pictureBox_Mic, this.connectResult.ToString());
            }
            else
            {
                this.decibelDisplayer1.Working = false;
                if (!this.chatUnit.MicrophoneConnector.OwnerOutput)
                {
                    this.pictureBox_Mic.BackgroundImage = this.imageList1.Images[1];
                    this.toolTip1.SetToolTip(this.pictureBox_Mic, "好友禁用了麦克风");
                    return;
                }

                if (this.chatUnit.MicrophoneConnector.Mute)
                {
                    this.pictureBox_Mic.BackgroundImage = this.imageList1.Images[1];
                    this.toolTip1.SetToolTip(this.pictureBox_Mic, "静音");
                }
                else
                {
                    this.pictureBox_Mic.BackgroundImage = this.imageList1.Images[0];
                    this.toolTip1.SetToolTip(this.pictureBox_Mic, "正常");
                    this.decibelDisplayer1.Working = true;
                }
            }

        }

        private void pictureBox_Mic_Click(object sender, EventArgs e)
        {
            if (!this.chatUnit.MicrophoneConnector.OwnerOutput)
            {
                return;
            }

            this.chatUnit.MicrophoneConnector.Mute = !this.chatUnit.MicrophoneConnector.Mute;
            this.ShowMicState();
        }
    }
}
