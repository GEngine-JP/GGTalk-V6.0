using System;
using System.Collections.Generic;
using System.Text;
using OMCS.Passive.Video;
using OMCS.Passive;
using Oraycn.MFile;
using System.Drawing;
using ESBasic;
using OMCS.Passive.Audio;

namespace GGTalk
{
    /// <summary>
    /// 视频聊天录制器。将视频聊天的完整过程录制成标准的MP4文件。
    /// </summary>
    class VideoChatRecorder : IDisposable
    {
        private DynamicCameraConnector dynamicCameraConnector2Friend ; //连接到好友摄像头的连接器。
        private CameraConnector cameraConnector2Myself; //连接到自己摄像头的连接器。
        private IMultimediaManager multimediaManager;
        private VideoFileMaker videoFileMaker;
        private Size videoSize;
        private Rectangle myVideoRect;
        private volatile bool isRecording = false;
        private AudioInOutMixer audioInOutMixer;

        public VideoChatRecorder(IMultimediaManager mgr ,DynamicCameraConnector friend, CameraConnector myself)
        {
            this.multimediaManager = mgr;
            this.dynamicCameraConnector2Friend = friend;
            this.cameraConnector2Myself = myself;           

            //混音器。将自己和对方的声音混成一路。
            this.audioInOutMixer = new AudioInOutMixer();
            this.audioInOutMixer.AudioMixed += new CbGeneric<byte[]>(audioInOutMixer_AudioMixed);
        }

        //得到混音数据，将其录制到文件。
        void audioInOutMixer_AudioMixed(byte[] data)
        {
            if (this.isRecording)
            {
                this.videoFileMaker.AddAudioFrame(data);
            }
        }        

        //初始化录像设备，并开始录制。
        public void Initialize(string filePath)
        {
            if (!this.dynamicCameraConnector2Friend.Connected)
            {
                throw new Exception("连接器尚未连接到对方的摄像头！");
            }
            this.videoSize = this.dynamicCameraConnector2Friend.VideoSize;
            Size myVideoSize = new Size(this.videoSize.Width / 3, this.videoSize.Height / 3);
            this.myVideoRect = new Rectangle(this.videoSize.Width - myVideoSize.Width, this.videoSize.Height - myVideoSize.Height, myVideoSize.Width, myVideoSize.Height);

            this.videoFileMaker = new VideoFileMaker();
            this.videoFileMaker.AutoDisposeVideoFrame = true;
            this.videoFileMaker.Initialize(filePath, VideoCodecType.H264, this.videoSize.Width, this.videoSize.Height, 10, VideoQuality.High, AudioCodecType.AAC, 16000, 1, true);

            this.audioInOutMixer.Initialize(this.multimediaManager);
            this.isRecording = true;

            CbGeneric cb = new CbGeneric(this.RecordThread);
            cb.BeginInvoke(null, null);
        }        
        
        //录制线程。每隔100ms（对应VideoFileMaker的帧频为10fps）就合成一张图片，并录制它。
        private void RecordThread()
        {
            while (this.isRecording)
            {
                Bitmap bmFriend = this.dynamicCameraConnector2Friend.GetCurrentImage();
                if (bmFriend != null)
                {
                    Bitmap bmMyself = this.cameraConnector2Myself.GetCurrentImage();
                    //合成图像
                    if (bmMyself != null)
                    {
                        Graphics g = Graphics.FromImage(bmFriend);
                        g.DrawImage(bmMyself ,this.myVideoRect);   
                        g.Dispose();
                    }

                    //录制图像
                    this.videoFileMaker.AddVideoFrame(bmFriend);
                }

                System.Threading.Thread.Sleep(100);
            }

        }

        /// <summary>
        /// 停止录制，并释放录制设备。
        /// </summary>
        public void Dispose()
        {   
            if (!this.isRecording)
            {
                return;
            }

            this.isRecording = false;
            this.audioInOutMixer.Dispose();
            this.videoFileMaker.Close(true);
        }
    }
}
