using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ESBasic;
using OMCS.Engine.Audio;
using OMCS.Passive;

namespace GGTalk.Controls
{
    /// <summary>
    /// 通道质量（信号强度）显示器。
    /// 根据JitterBuffer的缓冲区尺寸转换为信号强度。
    /// </summary>
    public partial class ChannelQualityDisplayer : UserControl
    {
        private int minBufferSize = 1;
        private int maxBufferSize = 1; 
        public ChannelQualityDisplayer()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);// 禁止擦除背景.
            this.SetStyle(ControlStyles.UserPaint, true);//自行绘制            
            this.UpdateStyles();
        }

        private string currentOwnerID;      
        public void Initialize(string _ownerID)
        {
            this.currentOwnerID = _ownerID;
            MultimediaManagerFactory.GetSingleton().JitterBufferDepthChanged += new ESBasic.CbGeneric<string, int,int, int>(multimediaMonitor_BufferDepthChanged);
            this.DisplaySignal(1,1,-1);
        }       

        void multimediaMonitor_BufferDepthChanged(string ownerID, int  minSize ,int maxSize ,int currentSize)
        {
            this.minBufferSize = minSize;
            this.maxBufferSize = maxSize;

            //newSize 30 - 300
            if (ownerID == this.currentOwnerID)
            {
                this.DisplaySignal(currentSize);
            }
        }

        #region DisplaySignal
        private int lastValue = -1;
        private void DisplaySignal(int current)
        {
            this.DisplaySignal(this.minBufferSize, this.maxBufferSize, current);
        }
        private void DisplaySignal(int minDepth, int maxDepth, int current)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric<int, int, int>(this.DisplaySignal), minDepth, maxDepth, current);
            }
            else
            {
                this.lastValue = current;
                this.label1.BackColor = this.colorNoSignal;
                this.label2.BackColor = this.colorNoSignal;
                this.label3.BackColor = this.colorNoSignal;
                this.label4.BackColor = this.colorNoSignal;
                this.label5.BackColor = this.colorNoSignal;

                if (current < 0)
                {
                    return;
                }

                int delt = (maxDepth - minDepth) / 5;

                if (maxDepth - delt <= current)
                {
                    this.label1.BackColor = this.colorBadSignal;
                    return;
                }

                if (maxDepth - 2 * delt <= current)
                {
                    this.label1.BackColor = this.colorBadSignal;
                    this.label2.BackColor = this.colorBadSignal;
                    return;
                }

                if (maxDepth - 3 * delt <= current)
                {
                    this.label1.BackColor = this.colorSignal;
                    this.label2.BackColor = this.colorSignal;
                    this.label3.BackColor = this.colorSignal;
                    return;
                }

                if (maxDepth - 4 * delt <= current)
                {
                    this.label1.BackColor = this.colorSignal;
                    this.label2.BackColor = this.colorSignal;
                    this.label3.BackColor = this.colorSignal;
                    this.label4.BackColor = this.colorSignal;
                    return;
                }

                this.label1.BackColor = this.colorSignal;
                this.label2.BackColor = this.colorSignal;
                this.label3.BackColor = this.colorSignal;
                this.label4.BackColor = this.colorSignal;
                this.label5.BackColor = this.colorSignal;
            }
        } 
        #endregion

        #region ColorSignal
        private Color colorSignal = Color.Green;
        /// <summary>
        /// 有信号部分的指示条的颜色。
        /// </summary>
        public Color ColorSignal
        {
            get { return colorSignal; }
            set
            { 
                colorSignal = value;
                this.DisplaySignal(this.lastValue);
            }
        } 
        #endregion

        #region ColorNoSignal
        private Color colorNoSignal = Color.LightGray;
        /// <summary>
        /// 无信号部分的指示条颜色。
        /// </summary>
        public Color ColorNoSignal
        {
            get { return colorNoSignal; }
            set 
            { 
                colorNoSignal = value;
                this.DisplaySignal(this.lastValue);
            }
        } 
        #endregion

        #region ColorBadSignal
        private Color colorBadSignal = Color.Red;
        /// <summary>
        /// 信号差时，有信号部分的指示条的颜色。
        /// </summary>
        public Color ColorBadSignal
        {
            get { return colorBadSignal; }
            set 
            { 
                colorBadSignal = value;
                this.DisplaySignal(this.lastValue);
            }
        } 
        #endregion
    }
}


