using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESBasic;

namespace GGTalk.Controls
{
    /// <summary>
    /// 表情选择窗体。
    /// </summary>
    public partial class EmotionForm : Form
    {      
        public EmotionForm()
        {
            InitializeComponent();           
            this.EmotionClicked += delegate { };
        }

        private int countPerLine = 9;
        private Rectangle validRegion;        
        private IList<Image> imageList = new List<Image>();  
        /// <summary>
        /// EmotionClicked 某个表情图片被点击。参数: 被点击图片的索引 - Image。
        /// </summary>
        public event CbGeneric<int, Image> EmotionClicked;     

        #region Property
        #region ImageLength
        private int imageLength = 24;
        public int ImageLength
        {
            get { return imageLength; }
            set { imageLength = value; }
        }
        #endregion

        #region Span
        private int span = 5;
        public int Span
        {
            get { return span; }
            set { span = value; }
        }
        #endregion 
        #endregion

        #region Initialize
        public void Initialize(IList<Image> _imageList)
        {
            this.imageList = _imageList;
            var count = (int)Math.Sqrt(this.imageList.Count);
            if (count < 10)
            {
                count = 10;
            }
            else
            {
                count += 2;
            }
            this.countPerLine = count;
            var countPerCol = this.imageList.Count / this.countPerLine;
            countPerCol += (this.imageList.Count % this.countPerLine == 0) ? 0 : 1;
            this.validRegion = new Rectangle(new Point(0, 0), new Size(this.countPerLine * (this.span + this.imageLength), countPerCol * (this.span + this.imageLength)));
            this.Height = this.validRegion.Height + this.span/2;
            this.Width = this.validRegion.Width + this.span / 2;
        } 
        #endregion

        #region OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var countPerCol = this.imageList.Count / this.countPerLine;
            countPerCol += (this.imageList.Count % this.countPerLine == 0) ? 0 : 1;

            var pen = new Pen(Color.LightGray ,1);
            for (var i = 0; i <= this.countPerLine; i++)
            {
                e.Graphics.DrawLine(pen, new Point(i * (this.imageLength + this.span), 2), new Point(i * (this.imageLength + this.span), countPerCol * (this.imageLength + this.span)));
            }

            for (var i = 0; i <= countPerCol; i++)
            {
                e.Graphics.DrawLine(pen, new Point(0, i * (this.imageLength + this.span) + 2), new Point((this.imageLength + this.span) * this.countPerLine, i * (this.imageLength + this.span) + 2));
            }

            for (var i = 0; i < this.imageList.Count; i++)
            {
                var y = i / this.countPerLine;
                var x = i % this.countPerLine;

                var start =new Point(x * (this.imageLength + this.span) + this.span-2, y * (this.imageLength + this.span) + this.span) ;
                e.Graphics.DrawImage(this.imageList[i],new Rectangle(start,new Size(this.imageLength,this.imageLength))) ;
                    //new Point(x * (this.imageLength + this.span) + this.span-2, y * (this.imageLength + this.span) + this.span));
            }
        } 
        #endregion

        #region FaceEmotionBoard_MouseClick
        private void FaceEmotionBoard_MouseClick(object sender, MouseEventArgs e)
        {
            var index = this.GetEmotionIndex(e.Location);
            if (index >= 0 && index < this.imageList.Count)
            {
                this.EmotionClicked(index ,this.imageList[index]);
            }
        }

        private int GetEmotionIndex(Point pt)
        {
            if (!this.validRegion.Contains(pt))
            {
                return -1;
            }

            var col = (pt.X - this.span) / (this.imageLength + this.span);
            var line = (pt.Y - this.span) / (this.imageLength + this.span);
            return line * this.countPerLine + col;
        } 
        #endregion      
    }
}
