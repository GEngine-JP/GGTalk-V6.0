using CCWin.SkinControl;

namespace GGTalk
{
    partial class PaintForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaintForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.handWritingBoard1 = new OMCS.Tools.HandwritingPanel();
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.skinButtom1 = new CCWin.SkinControl.SkinButton();
            this.btnClose = new CCWin.SkinControl.SkinButton();
            this.panel_top = new System.Windows.Forms.Panel();
            this.skinButtom2 = new CCWin.SkinControl.SkinButton();
            this.comboBox_brushWidth = new CCWin.SkinControl.SkinComboBox();
            this.button_color = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            this.panel_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.handWritingBoard1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // handWritingBoard1
            // 
            resources.ApplyResources(this.handWritingBoard1, "handWritingBoard1");
            this.handWritingBoard1.Name = "handWritingBoard1";
            // 
            // panel_bottom
            // 
            this.panel_bottom.BackColor = System.Drawing.Color.Transparent;
            this.panel_bottom.Controls.Add(this.skinButtom1);
            this.panel_bottom.Controls.Add(this.btnClose);
            resources.ApplyResources(this.panel_bottom, "panel_bottom");
            this.panel_bottom.Name = "panel_bottom";
            // 
            // skinButtom1
            // 
            resources.ApplyResources(this.skinButtom1, "skinButtom1");
            this.skinButtom1.BackColor = System.Drawing.Color.Transparent;
            this.skinButtom1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.skinButtom1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButtom1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.skinButtom1.DownBack = ((System.Drawing.Image)(resources.GetObject("skinButtom1.DownBack")));
            this.skinButtom1.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.skinButtom1.MouseBack = ((System.Drawing.Image)(resources.GetObject("skinButtom1.MouseBack")));
            this.skinButtom1.Name = "skinButtom1";
            this.skinButtom1.NormlBack = ((System.Drawing.Image)(resources.GetObject("skinButtom1.NormlBack")));
            this.skinButtom1.UseVisualStyleBackColor = false;
            this.skinButtom1.Click += new System.EventHandler(this.button_Ok_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.btnClose.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.DownBack = ((System.Drawing.Image)(resources.GetObject("btnClose.DownBack")));
            this.btnClose.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnClose.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnClose.MouseBack")));
            this.btnClose.Name = "btnClose";
            this.btnClose.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnClose.NormlBack")));
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.Button_cancel_Click);
            // 
            // panel_top
            // 
            this.panel_top.BackColor = System.Drawing.Color.Transparent;
            this.panel_top.Controls.Add(this.skinButtom2);
            this.panel_top.Controls.Add(this.comboBox_brushWidth);
            this.panel_top.Controls.Add(this.button_color);
            this.panel_top.Controls.Add(this.label1);
            resources.ApplyResources(this.panel_top, "panel_top");
            this.panel_top.Name = "panel_top";
            // 
            // skinButtom2
            // 
            resources.ApplyResources(this.skinButtom2, "skinButtom2");
            this.skinButtom2.BackColor = System.Drawing.Color.Transparent;
            this.skinButtom2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.skinButtom2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButtom2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.skinButtom2.DownBack = ((System.Drawing.Image)(resources.GetObject("skinButtom2.DownBack")));
            this.skinButtom2.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.skinButtom2.MouseBack = ((System.Drawing.Image)(resources.GetObject("skinButtom2.MouseBack")));
            this.skinButtom2.Name = "skinButtom2";
            this.skinButtom2.NormlBack = ((System.Drawing.Image)(resources.GetObject("skinButtom2.NormlBack")));
            this.skinButtom2.UseVisualStyleBackColor = false;
            this.skinButtom2.Click += new System.EventHandler(this.Button_clear_Click);
            // 
            // comboBox_brushWidth
            // 
            this.comboBox_brushWidth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox_brushWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_brushWidth.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_brushWidth, "comboBox_brushWidth");
            this.comboBox_brushWidth.Name = "comboBox_brushWidth";
            this.comboBox_brushWidth.WaterText = "";
            this.comboBox_brushWidth.SelectedIndexChanged += new System.EventHandler(this.comboBox_brushWidth_SelectedIndexChanged);
            // 
            // button_color
            // 
            this.button_color.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.button_color, "button_color");
            this.button_color.Name = "button_color";
            this.button_color.UseVisualStyleBackColor = false;
            this.button_color.Click += new System.EventHandler(this.button_color_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // PaintForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.CaptionFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CloseDownBack = global::GGTalk.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::GGTalk.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::GGTalk.Properties.Resources.btn_close_disable;
            this.Controls.Add(this.panel_top);
            this.Controls.Add(this.panel_bottom);
            this.Controls.Add(this.panel1);
            this.Name = "PaintForm";
            this.panel1.ResumeLayout(false);
            this.panel_bottom.ResumeLayout(false);
            this.panel_top.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Panel panel_top;
        private OMCS.Tools.HandwritingPanel handWritingBoard1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button_color;
        private System.Windows.Forms.ToolTip toolTip1;
        private SkinComboBox comboBox_brushWidth;
        private SkinButton btnClose;
        private SkinButton skinButtom1;
        private SkinButton skinButtom2;
    }
}