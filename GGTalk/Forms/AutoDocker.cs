using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GGTalk
{
    /// <summary>
    /// 让目标Form自动停靠在桌面边缘。
    /// </summary>
    public partial class AutoDocker : Component
    {       
        private Form dockedForm;
        private bool IsOrg;
        private Rectangle lastBoard;
        private FormDockHideStatus formDockHideStatus = FormDockHideStatus.ShowNormally;
        internal DockHideType dockHideType;       
        private Timer CheckPosTimer;

        public AutoDocker()
        {
            this.InitializeComponent();
        }
        public AutoDocker(IContainer container)
        {
            container.Add(this);
            this.InitializeComponent();
        }

        /// <summary>
        /// 初始化。
        /// </summary>
        /// <param name="needDockedForm">需要靠边停靠的窗体</param>
        public void Initialize(Form needDockedForm)
        {
            this.dockedForm = needDockedForm;
            if (this.dockedForm != null)
            {
                this.dockedForm.LocationChanged += new EventHandler(this.dockedForm_LocationChanged);
                this.dockedForm.SizeChanged += new EventHandler(this._form_SizeChanged);
                this.dockedForm.TopMost = true;
            }
        }       
        
        /// <summary>
        /// 定时器循环判断。        
        /// </summary>       
        private void CheckPosTimer_Tick(object sender, EventArgs e)
        {
            if (base.DesignMode)
            {
                return;
            }
            if (this.dockedForm == null || !this.IsOrg)
            {
                return;
            }

            if (this.dockedForm.Bounds.Contains(Cursor.Position))
            {
                this.showOnce = false;
            }

            if (this.showOnce)
            {
                if (this.dockHideType == DockHideType.Top)
                {
                    this.dockedForm.Location = new Point(this.dockedForm.Location.X, 0);
                }
                else if (this.dockHideType == DockHideType.Right)
                {
                    this.dockedForm.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.dockedForm.Width, this.dockedForm.Location.Y);
                }
                else if (this.dockHideType == DockHideType.Left)
                {
                    this.dockedForm.Location = new Point(0, this.dockedForm.Location.Y);
                }               
                else
                {
                }
                
                this.dockedForm.Size = new Size(this.lastBoard.Width, this.lastBoard.Height);
                return;
            }

            //当鼠标移动到窗体的范围内（此时，窗体的位置位于屏幕之外）
            if (this.dockedForm.Bounds.Contains(Cursor.Position))
            {                              
                if (this.dockHideType != DockHideType.Top)
                {
                    if (this.dockHideType != DockHideType.Left)
                    {
                        if (this.dockHideType != DockHideType.Right)
                        {
                            return;
                        }
                        if (this.formDockHideStatus == FormDockHideStatus.Hide)
                        {
                            this.dockedForm.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.dockedForm.Width, this.dockedForm.Location.Y);
                            return;
                        }
                    }
                    else
                    {
                        if (this.formDockHideStatus == FormDockHideStatus.Hide)
                        {
                            this.dockedForm.Location = new Point(0, this.dockedForm.Location.Y);
                            return;
                        }
                    }
                }
                else
                {
                    if (this.formDockHideStatus == FormDockHideStatus.Hide)
                    {
                        this.dockedForm.Location = new Point(this.dockedForm.Location.X, 0);
                        return;
                    }
                }
            }
            else //当鼠标位于窗体范围之外，则根据DockHideType的值，决定窗体的位置。
            {            
                switch (this.dockHideType)
                {
                    case DockHideType.None:
                        {
                            if (this.IsOrg && this.formDockHideStatus == FormDockHideStatus.ShowNormally && (this.dockedForm.Bounds.Width != this.lastBoard.Width || this.dockedForm.Bounds.Height != this.lastBoard.Height))
                            {
                                this.dockedForm.Size = new Size(this.lastBoard.Width, this.lastBoard.Height);
                            }
                            break;
                        }
                    case DockHideType.Top:
                        {
                            this.dockedForm.Location = new Point(this.dockedForm.Location.X, (this.dockedForm.Height - 4) * -1);
                            return;
                        }      
                    case DockHideType.Left:
                        {                           
                            this.dockedForm.Location = new Point(-1 * (this.dockedForm.Width - 4), this.dockedForm.Location.Y);
                            return;
                        }
                    default:
                        {
                            if (this.dockHideType != DockHideType.Right)
                            {
                                return;
                            }                            
                            this.dockedForm.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 4, this.dockedForm.Location.Y);
                            return;
                        }
                }
            }
        }
       
        private void dockedForm_LocationChanged(object sender, EventArgs e)
        {
            this.ComputeDockHideType();
            if (!this.IsOrg)
            {
                this.lastBoard = this.dockedForm.Bounds;
                this.IsOrg = true;
            }
        }

        /// <summary>
        /// 判断是否达到了隐藏的条件？以及是哪种类型的隐藏。
        /// </summary>
        private void ComputeDockHideType()
        {
            if (this.dockedForm.Top <= 0)
            {
                this.dockHideType = DockHideType.Top;
                if (this.dockedForm.Bounds.Contains(Cursor.Position))
                {
                    this.formDockHideStatus = FormDockHideStatus.ReadyToHide;
                    return;
                }
                this.formDockHideStatus = FormDockHideStatus.Hide;
                return;
            }
            else
            {
                if (this.dockedForm.Left <= 0)
                {
                    this.dockHideType = DockHideType.Left;
                    if (this.dockedForm.Bounds.Contains(Cursor.Position))
                    {
                        this.formDockHideStatus = FormDockHideStatus.ReadyToHide;
                        return;
                    }
                    this.formDockHideStatus = FormDockHideStatus.Hide;
                    return;
                }
                else
                {
                    if (this.dockedForm.Left < Screen.PrimaryScreen.Bounds.Width - this.dockedForm.Width)
                    {
                        this.dockHideType = DockHideType.None;
                        this.formDockHideStatus = FormDockHideStatus.ShowNormally;
                        return;
                    }
                    this.dockHideType = DockHideType.Right;
                    if (this.dockedForm.Bounds.Contains(Cursor.Position))
                    {
                        this.formDockHideStatus = FormDockHideStatus.ReadyToHide;
                        return;
                    }
                    this.formDockHideStatus = FormDockHideStatus.Hide;
                    return;
                }
            }
        }
        
        private void _form_SizeChanged(object sender, EventArgs e)
        {
            if (this.IsOrg && this.formDockHideStatus == FormDockHideStatus.ShowNormally)
            {
                this.lastBoard = this.dockedForm.Bounds;
            }
        }

        private bool showOnce = false;
        public void ShowOnce()
        {
            this.showOnce = true;
            this.formDockHideStatus = FormDockHideStatus.ShowNormally;
        }
    }

    /// <summary>
    /// 靠边隐藏的类型。
    /// </summary>
    public enum DockHideType
    {
        /// <summary>
        /// 不隐藏
        /// </summary>
        None = 0,
        /// <summary>
        /// 靠上边沿隐藏
        /// </summary>
        Top,
        /// <summary>
        /// 靠左边沿隐藏
        /// </summary>
        Left,
        /// <summary>
        /// 靠右边沿隐藏
        /// </summary>
        Right
    }

    /// <summary>
    /// 窗体的显示或隐藏状态
    /// </summary>
    public enum FormDockHideStatus
    {
        /// <summary>
        /// 已隐藏
        /// </summary>
        Hide = 0,

        /// <summary>
        /// 准备隐藏
        /// </summary>
        ReadyToHide,
        
        /// <summary>
        /// 正常显示
        /// </summary>
        ShowNormally

    }
}
