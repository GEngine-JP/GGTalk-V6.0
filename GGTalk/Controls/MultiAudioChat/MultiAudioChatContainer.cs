using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OMCS.Passive;
using OMCS.Passive.MultiChat;
using ESBasic;
using OMCS.Contracts;

namespace GGTalk.Controls
{
    public partial class MultiAudioChatContainer : UserControl
    {       
        private IMultimediaManager multimediaManager;
        private IChatGroup chatGroup;

        /// <summary>
        /// 当点击邀请好友的Button时，触发此事件。
        /// </summary>
        public event CbGeneric InviteFriendClick;
        
        public MultiAudioChatContainer()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);// 禁止擦除背景.
            this.SetStyle(ControlStyles.UserPaint, true);//自行绘制            
            this.UpdateStyles();
        }

        public void Close()
        {
            if (this.multimediaManager != null)
            {
                this.multimediaManager.AudioCaptured -= new ESBasic.CbGeneric<byte[]>(multimediaManager_AudioCaptured);
                this.multimediaManager.AudioPlayed -= new ESBasic.CbGeneric<byte[]>(multimediaManager_AudioPlayed);                
                this.multimediaManager.ChatGroupEntrance.Exit(ChatType.Audio, this.chatGroup.GroupID);
            }
        }

        public void Initialize(IMultimediaManager mgr, string chatGroupID)
        {             
            this.multimediaManager = mgr;
            this.chatGroup = this.multimediaManager.ChatGroupEntrance.Join(ChatType.Audio, chatGroupID);

            this.decibelDisplayer_mic.Working = true;
            this.decibelDisplayer_speaker.Working = true;
            this.multimediaManager.AudioCaptured += new ESBasic.CbGeneric<byte[]>(multimediaManager_AudioCaptured);
            this.multimediaManager.AudioPlayed += new ESBasic.CbGeneric<byte[]>(multimediaManager_AudioPlayed);

            this.chatGroup.SomeoneJoin += new ESBasic.CbGeneric<IChatUnit>(chatGroup_SomeoneJoin);
            this.chatGroup.SomeoneExit += new CbGeneric<string>(chatGroup_SomeoneExit);

            var myselfPanel = new SpeakerPanel();
            myselfPanel.Initialize(this.multimediaManager.CurrentUserID);
            this.flowLayoutPanel1.Controls.Add(myselfPanel);
            foreach (var unit in this.chatGroup.GetOtherMembers())
            {
                var panel = new SpeakerPanel();
                panel.Initialize(unit);
                this.flowLayoutPanel1.Controls.Add(panel);
            }

            this.groupBox_members.Text = string.Format("成员列表（{0}人）" ,this.flowLayoutPanel1.Controls.Count);

            this.flowLayoutPanel1_SizeChanged(this.flowLayoutPanel1, new EventArgs());
        }        

        void chatGroup_SomeoneExit(string memberID)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric<string>(this.chatGroup_SomeoneExit), memberID);
            }
            else
            {
                SpeakerPanel target = null;
                foreach (SpeakerPanel panel in this.flowLayoutPanel1.Controls)
                {
                    if (panel.MemberID == memberID)
                    {
                        target = panel;
                        break;
                    }
                }

                if (target == null)
                {
                    return;
                }

                this.flowLayoutPanel1.Controls.Remove(target);
                this.groupBox_members.Text = string.Format("成员列表 （{0}人）", this.flowLayoutPanel1.Controls.Count);
                this.toolStripLabel1.Text = string.Format("{0} 退出了组!", memberID);
            }
        }

        void chatGroup_SomeoneJoin(IChatUnit unit)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric<IChatUnit>(this.chatGroup_SomeoneJoin), unit);
            }
            else
            {
                var panel = new SpeakerPanel();
                panel.Width = this.flowLayoutPanel1.Width - 2;
                panel.Initialize(unit);
                this.flowLayoutPanel1.Controls.Add(panel);
                this.groupBox_members.Text = string.Format("成员列表 （{0}人）", this.flowLayoutPanel1.Controls.Count);
                this.toolStripLabel1.Text = string.Format("{0} 加入了组!" ,unit.MemberID);
            }
        }              

        void multimediaManager_AudioPlayed(byte[] data)
        {
            this.decibelDisplayer_speaker.DisplayAudioData(data);
        }

        void multimediaManager_AudioCaptured(byte[] data)
        {
            this.decibelDisplayer_mic.DisplayAudioData(data);
        }
       
        private void skinCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.decibelDisplayer_mic.Visible = this.skinCheckBox_mic.Checked;
            this.multimediaManager.OutputAudio = this.skinCheckBox_mic.Checked;
            this.decibelDisplayer_mic.Working = this.skinCheckBox_mic.Checked;
        }

        private void skinCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.decibelDisplayer_speaker.Visible = this.skinCheckBox_speaker.Checked;
            this.decibelDisplayer_speaker.Working = this.skinCheckBox_speaker.Checked;
            this.multimediaManager.Mute = !this.skinCheckBox_speaker.Checked;    
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.InviteFriendClick != null)
            {
                this.InviteFriendClick();
            }
        }

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            foreach (SpeakerPanel panel in this.flowLayoutPanel1.Controls)
            {
                panel.Width = this.flowLayoutPanel1.Width - 2;
            }
        }        
    }
}
