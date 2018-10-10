using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ESBasic;

namespace GGTalk.Controls
{
    public partial class DecibelDisplayer : UserControl
    {
        private ESBasic.ObjectManagement.CircleQueue<byte[]> queue = new ESBasic.ObjectManagement.CircleQueue<byte[]>(10);
        public DecibelDisplayer()
        {
            InitializeComponent();            
        }        

        private void timer1_Tick(object sender, EventArgs e)
        {
            var data = this.queue.Dequeue();
            this.DoDisplayAudioData(data);
        }

        public void DisplayAudioData(byte[] data)
        {
            if (this.enabled)
            {
                this.queue.Enqueue(data);
            }
        }

        private bool enabled = true ;
        public bool Working
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;
                if (this.enabled)
                {
                    this.timer1.Start();
                }
                else
                {
                    this.timer1.Stop();
                }
                this.Clear();
            }
        }

        public bool ValueVisialbe
        {
            get
            {
                return this.label1.Visible;
            }
            set
            {
                this.label1.Visible = value;
            }
        }

        public bool Error
        {
            set
            {
                this.skinProgressBar1.Border = value ? Color.Red : Color.FromArgb(158, 158, 158);
            }
        }

        private void Clear()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric(this.Clear));
            }
            else
            {
                this.queue.Clear();
                this.skinProgressBar1.Value = 0;
            }
        }

        //OMCS的音频采样位数为16bit
        private void DoDisplayAudioData(byte[] data)
        {
            if (!this.enabled)
            {
                return;
            }

            if (data == null || data.Length == 0)
            {
                this.showResult(0);
                return;
            }

            var wave = new double[data.Length / 2];
            var h = 0;
            for (var i = 0; i < wave.Length; i += 2)
            {
                wave[h] = (double)BitConverter.ToInt16(data, i); //采样位数为16bit
                ++h;
            }

            var res = FourierTransformer.FFTDb(wave);
            double kk = 0;
            foreach (var dd in res)
            {
                kk += dd;
            }
            if (kk < 0)
            {
                kk = 0;
            }
            var rs = kk / res.Length;
            this.showResult(rs);
        }

        private void showResult(double rs)
        {
            if (!this.enabled)
            {
                return;
            }

            var tmp = rs;
            if (tmp > int.MaxValue || tmp < int.MinValue)
            {
                tmp = 0;
            }

            var val = (int)(tmp - 20) * 2;
            if (val < 0)
            {
                val = 0;
            }
            if (val > 100)
            {
                val = 100;
            }

            this.skinProgressBar1.Value = val;
            this.label1.Text = val.ToString();
        }            
    }
}
