using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GGTalk.UnitViews;
using ESBasic.ObjectManagement.Managers;
using ESBasic;
using ESBasic.Helpers;
using System.Collections;

namespace GGTalk.UnitViews
{
    public partial class OrgView : UserControl, IComparer
    {
        private System.Windows.Forms.Timer timer;
        private IUser currentUser;
        private TreeNode root;
        private string rootName;
        
        private ObjectManager<string, TreeNode> orgNodeManager = new ObjectManager<string, TreeNode>();
        private ObjectManager<string, TreeNode> userNodeManager = new ObjectManager<string, TreeNode>();
        private ESBasic.Collections.SortedArray<string> twinkleList = new ESBasic.Collections.SortedArray<string>();
        private UiSafeInvoker invoker ;
           
        public event CbGeneric<IUser> UserDoubleClicked;
        public event CbGeneric<IUser> ChatRecordClicked;

        #region Ctor
        public OrgView()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);// 禁止擦除背景.
            this.SetStyle(ControlStyles.UserPaint, true);//自行绘制            
            this.UpdateStyles();

            this.treeView1.TreeViewNodeSorter = this;
            this.timer = new Timer();
            this.timer.Interval = 500;
            this.timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            foreach (string userID in this.twinkleList.GetAllReadonly())
            {
                TreeNode node = this.userNodeManager.Get(userID);
                if (node != null)
                {
                    if (!node.Parent.IsExpanded)
                    {
                        continue;
                    }

                    int nonImageIndex = this.GetImageIndex(null);
                    if (node.ImageIndex != nonImageIndex)
                    {
                        node.ImageIndex = nonImageIndex;
                        node.SelectedImageIndex = node.ImageIndex;
                    }
                    else
                    {
                        IUser user = (IUser)node.Tag;
                        int imgIndex = this.GetImageIndex(user.UserStatus);
                        node.ImageIndex = imgIndex;
                        node.SelectedImageIndex = imgIndex;
                    }
                    this.treeView1.Invalidate(node.Bounds, false);
                }
            }
        } 
        #endregion

        #region OrgGetter
        private IOrgGetter orgGetter = new DefaultOrgGetter();
        public IOrgGetter OrgGetter
        {
            get { return orgGetter; }
            set { orgGetter = value ?? new DefaultOrgGetter(); }
        } 
        #endregion

        private void ResetNodeImage(string userID)
        {
            TreeNode node = this.userNodeManager.Get(userID);
            if (node == null)
            {
                return;
            }
            IUser user = (IUser)node.Tag;
            int imgIndex = this.GetImageIndex(user.UserStatus);
            node.ImageIndex = imgIndex;
            node.SelectedImageIndex = imgIndex;
            this.treeView1.Invalidate(node.Bounds, false);
        }

        public void SetTwinkleState(string userID, bool twinkle)
        {
            this.invoker.ActionOnUI<string, bool>(this.do_SetTwinkleState, userID, twinkle);
        }

        private void do_SetTwinkleState(string userID, bool twinkle)
        {  
            if (twinkle)
            {
                this.twinkleList.Add(userID);
                this.timer.Start();
            }
            else
            {
                this.twinkleList.Remove(userID);                
                if (this.twinkleList.Count == 0)
                {
                    this.timer.Stop();
                }
                this.ResetNodeImage(userID);
            }
        }

        public void Initialize(IUser current ,Dictionary<UserStatus, Image> statusImage ,ESBasic.Loggers.IAgileLogger logger)
        {
            this.currentUser = current;
            this.invoker = new UiSafeInvoker(this, true, true, logger);

            //Online = 2,
            //Away = 3,
            //Busy = 4,
            //DontDisturb = 5,
            //OffLine = 6,
            //Hide = 7

            // 索引处的状态位置 0-公司，1-部门，2-无，3-在线用户
            this.imageList1.Images.Add(this.CombineStateImage(this.imageList1.Images[3], statusImage[UserStatus.Away]));
            this.imageList1.Images.Add(this.CombineStateImage(this.imageList1.Images[3], statusImage[UserStatus.Busy]));
            this.imageList1.Images.Add(this.CombineStateImage(this.imageList1.Images[3], statusImage[UserStatus.DontDisturb]));
            this.imageList1.Images.Add(ESBasic.Helpers.ImageHelper.ConvertToGrey(this.imageList1.Images[3]));
            this.imageList1.Images.Add(ESBasic.Helpers.ImageHelper.ConvertToGrey(this.imageList1.Images[3]));
        }

        private int GetImageIndex(UserStatus? status)
        {
            if (status == null) //表示无，闪烁
            {
                return 2;
            }
            return (int)status.Value + 1;
        }

        private Image CombineStateImage(Image img ,Image stateImage)
        {
            Bitmap bm = new Bitmap(img);
            using (Graphics g = Graphics.FromImage(bm))
            {
                int len = (int)(img.Width * 0.6);
                g.DrawImage(stateImage, new Rectangle(len, len, img.Width - len, img.Height - len), new Rectangle(0, 0, stateImage.Width, stateImage.Height), GraphicsUnit.Pixel);
            }
            
            return bm;     
        }

        private void CheckOnline()
        {
            int online = 0;
            foreach (TreeNode node in this.userNodeManager.GetAll())
            {
                IUser user = (IUser)node.Tag;
                if (user.UserStatus != UserStatus.OffLine && user.UserStatus != UserStatus.Hide)
                {
                    ++online;
                }
            }
            this.root.Text = string.Format("{0} [{1}/{2}]" ,this.rootName,online,this.userNodeManager.Count);
        }

        public void AddUser(IUser user)
        {
            if (!user.IsInOrg) //位于组织之外
            {
                return;
            }

            if(user.OrgPath == null || user.OrgPath.Length == 0)
            {
                return ;
            }            

            if (this.root == null)
            {                
                string rootID = user.OrgPath[0] ;
                string orgName = this.orgGetter.GetOrgName(rootID) ;
                if (string.IsNullOrEmpty(orgName))
                {
                    return;
                }
                this.root = new TreeNode(orgName, 0, 0);
                this.root.Tag = rootID;
                this.orgNodeManager.Add(rootID ,this.root) ;
                this.treeView1.Nodes.Add(this.root);
                this.rootName = this.root.Text;
            }
            
            //确保路径存在
            TreeNode parent = this.root;
            for (int i = 1; i < user.OrgPath.Length; i++)
            {
                TreeNode child = this.orgNodeManager.Get(user.OrgPath[i]);
                if (child == null)
                {
                    string nodeID = user.OrgPath[i];
                    string orgName = this.orgGetter.GetOrgName(nodeID);
                    if (string.IsNullOrEmpty(orgName))
                    {
                        return;
                    }
                    child = new TreeNode(orgName, 1, 1);
                    child.Tag = nodeID;
                    this.orgNodeManager.Add(nodeID, child);
                    parent.Nodes.Add(child);
                }
                parent = child;    
            }

            TreeNode userNode = this.userNodeManager.Get(user.ID);
            if (userNode != null) //如果存在，则先删除
            {
                userNode.Parent.Nodes.Remove(userNode);
                this.userNodeManager.Remove(user.ID);
            }
            int imgIndex = this.GetImageIndex(user.UserStatus);
            userNode = new TreeNode(user.Name, imgIndex, imgIndex);
            userNode.Tag = user;
            userNode.ToolTipText = user.ID;
            this.userNodeManager.Add(user.ID, userNode);
            parent.Nodes.Add(userNode);            

            this.treeView1.Sort();
            this.CheckOnline();
            //this.treeView1.Invalidate();
        }

        public void UpdateUser(IUser user)
        {
            this.RemoveUser(user.ID);
            this.AddUser(user);
        }

        public void ExpandRoot()
        {
            this.root.Expand();
        }      

        public void RemoveUser(string userID)
        {
            TreeNode node = this.userNodeManager.Get(userID);
            if (node == null)
            {
                return;
            }

            node.Parent.Nodes.Remove(node);
            this.userNodeManager.Remove(userID);
        }

        public void SetAllUserOffline()
        {
            if (this.root == null)
            {
                return;
            }

            int index = this.GetImageIndex(UserStatus.OffLine);
            foreach (TreeNode node in this.userNodeManager.GetAllReadonly())
            {
                node.ImageIndex = index;
                node.SelectedImageIndex = index;
            }
            this.root.Text = string.Format("{0} [0/{1}]", this.rootName, this.userNodeManager.Count);
            //this.treeView1.Invalidate();
        }

        public void UserStatusChanged(string userID, UserStatus userStatus)
        {
            TreeNode node = this.userNodeManager.Get(userID);
            if (node != null)
            {
                node.ImageIndex = this.GetImageIndex(userStatus); 
                node.SelectedImageIndex = node.ImageIndex;
            }
            this.CheckOnline();
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            IUser user = e.Node.Tag as IUser;
            if (user != null && user.ID != this.currentUser.ID)
            {
                if (this.UserDoubleClicked != null)
                {
                    this.UserDoubleClicked(user);
                }
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeViewHitTestInfo info = this.treeView1.HitTest(e.Location);
            if (info == null || info.Node == null)
            {
                this.treeView1.ContextMenuStrip = null;
                return;
            }
            IUser user = info.Node.Tag as IUser;
            if (user == null)
            {
                this.treeView1.ContextMenuStrip = null;
                return;
            }

            if (user.ID == this.currentUser.ID)
            {
                this.treeView1.ContextMenuStrip = null;
                return;
            }
            
            this.treeView1.SelectedNode = info.Node;
            this.treeView1.ContextMenuStrip = this.skinContextMenuStrip_user;
        }

        private void toolStripMenuItem51_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
            {
                return;
            }

            IUser user = this.treeView1.SelectedNode.Tag as IUser;
            if (user != null && user.ID != this.currentUser.ID)
            {
                if (this.UserDoubleClicked != null)
                {
                    this.UserDoubleClicked(user);
                }
            }
        }

        private void 消息记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
            {
                return;
            }

            IUser user = this.treeView1.SelectedNode.Tag as IUser;
            if (user != null && user.ID != this.currentUser.ID)
            {
                if (this.ChatRecordClicked != null)
                {
                    this.ChatRecordClicked(user);
                }
            }
        }

        public int Compare(object x, object y)
        {
            TreeNode n1 = x as TreeNode;
            TreeNode n2 = y as TreeNode;
            if (n1 == null || n2 == null)
            {
                return 0;
            }

            IUser user1 = n1.Tag as IUser;
            IUser user2 = n2.Tag as IUser;
            if (user1 == null && user2 == null)
            {
                return n1.Text.CompareTo(n2.Text);
            }
            if (user1 != null && user2 != null)
            {
                return user1.Name.CompareTo(user2.Name);
            }

            return user1 == null ? 1 : -1;
        }
    }

    public interface IOrgGetter
    {
        string GetOrgName(string orgID);
    }

    public class DefaultOrgGetter : IOrgGetter
    {
        public string GetOrgName(string orgID)
        {
            return orgID;
        }
    }   
}
