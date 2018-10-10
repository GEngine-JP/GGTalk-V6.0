using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GGTalk.Properties;
using CCWin;

namespace GGTalk
{
    /// <summary>
    /// 手写板 窗口。
    /// </summary>
    public partial class PaintForm : BaseForm
    {
        private Color currentColor = Color.Red;
        private List<float> penWidthList = new List<float>();
        private Bitmap currentImage;
        public Bitmap CurrentImage
        {
            get { return currentImage; }            
        }
        
        public PaintForm()
        {
            InitializeComponent();
        
            this.toolTip1.SetToolTip(this.button_color,"颜色");


            this.handWritingBoard1.PenColor = this.currentColor;
            
            this.penWidthList.Add(2);
            this.penWidthList.Add(4);
            this.penWidthList.Add(6);
            this.penWidthList.Add(8);
            this.penWidthList.Add(10);
            this.comboBox_brushWidth.DataSource = this.penWidthList;
            this.comboBox_brushWidth.SelectedIndex = 1;
        }

        private void button_color_Click(object sender, EventArgs e)
        {
            try
            {
                this.colorDialog1.Color = this.currentColor;
                var result = this.colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.currentColor = this.colorDialog1.Color;
                    this.handWritingBoard1.PenColor = this.currentColor;                    
                }
            }
            catch (Exception ee)
            {             
                MessageBoxEx.Show(ee.Message, "GGTalk");
            }
        }

        private void comboBox_brushWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox_brushWidth.SelectedIndex > 0)
            {
                this.handWritingBoard1.PenWidth = this.penWidthList[this.comboBox_brushWidth.SelectedIndex];
            }
            else
            {
                this.handWritingBoard1.PenWidth = this.penWidthList[0];
            }
        }

        private void Button_clear_Click(object sender, EventArgs e)
        {
            this.handWritingBoard1.Clear();
            this.DialogResult = System.Windows.Forms.DialogResult.None;
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            this.currentImage = this.handWritingBoard1.GetHandWriting();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            //this.Close();

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        
    }
}
