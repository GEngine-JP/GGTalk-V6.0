using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CCWin;
using CCWin.Win32;
using CCWin.Win32.Const;
using System.Diagnostics;
using System.Configuration;
using ESBasic.Security;
using ESPlus.Rapid;
using CCWin.SkinControl;
using ESBasic;
using JustLib;
using GGTalk.Controls;
using JustLib.Records;
using ESPlus.Serialization;

namespace GGTalk
{
    public interface IUserNameGetter
    {
        string GetUserName(string userID);
    }

    /// <summary>
    /// 聊天记录查看窗口。
    /// </summary>
    public partial class ChatRecordForm : BaseForm
    {
        private IChatRecordPersister remotePersister;
        private IChatRecordPersister localPersister;
        private int totalPageCount = 1;
        private int currentPageIndex = -1;
        private int pageSize = 25; 
        private Parameter<string,string> my; //ID - Name
        private Parameter<string, string> friend;
        private Parameter<string, string> group;
        private bool isGroupChat = false;
        private IUserNameGetter userNameGetter;

        public ChatRecordForm(IChatRecordPersister remote, IChatRecordPersister local, Parameter<string, string> _my, Parameter<string, string> _friend)
        {
            InitializeComponent();

            this.chatBox_history.Initialize(GlobalResourceManager.EmotionDictionary);
            this.isGroupChat = false;
            this.my = _my;
            this.friend = _friend;
            this.Text += " - " + this.friend.Arg2;            
            this.remotePersister = remote;
            this.localPersister = local;
        }

        public ChatRecordForm(IChatRecordPersister remote,IChatRecordPersister local, Parameter<string, string> gr, Parameter<string, string> _my, IUserNameGetter getter)
        {
            InitializeComponent();

            this.chatBox_history.Initialize(GlobalResourceManager.EmotionDictionary);
            this.isGroupChat = true;
            this.group = gr;
            this.my = _my;
            this.userNameGetter = getter;
            this.Text = "群消息记录 - " + gr.Arg2;
            this.remotePersister = remote;
            this.localPersister = local;
        }

        #region ServerRecordEnabled
        public bool ServerRecordEnabled
        {
            get
            {
                return this.skinRadioButton_Server.Visible;
            }
            set
            {
                this.skinRadioButton1.Checked = true;
                this.skinRadioButton_Server.Visible = false;
                this.skinRadioButton1.Visible = false;
            }
        } 
        #endregion

        private IChatRecordPersister CurrentPersister
        {
            get
            {
                if (this.skinRadioButton_Server.Checked)
                {
                    return this.remotePersister;
                }

                return this.localPersister;
            }
        }

        private void MessageRecordForm_Shown(object sender, EventArgs e)
        {
            this.skinComboBox1.SelectedIndex = 0;            
        }

        private void ShowRecord(int pageIndex)
        {
            this.ShowRecord(pageIndex, true);
        }

        private void ShowRecord(int pageIndex ,bool allowCache)
        {
            if (this.remotePersister == null) //还未完成构造
            {
                return;
            }

            if (pageIndex != int.MaxValue)
            {
                if (pageIndex + 1 > this.totalPageCount)
                {
                    pageIndex = this.totalPageCount - 1;
                }

                if (pageIndex < 0)
                {
                    pageIndex = 0;
                }
                if (this.currentPageIndex == pageIndex && allowCache)
                {
                    this.toolStripTextBox_pageIndex.Text = (pageIndex + 1).ToString();
                    return;
                }
            }

            this.Cursor = Cursors.WaitCursor;
            try
            {
                var timeScope = ChatRecordTimeScope.All;
                var now = DateTime.Now ;
                if (this.skinComboBox1.SelectedIndex == 0) //一周
                {
                    timeScope = ChatRecordTimeScope.RecentWeek;
                }
                else if (this.skinComboBox1.SelectedIndex == 1)//一月
                {
                    timeScope = ChatRecordTimeScope.RecentMonth;
                }
                else if (this.skinComboBox1.SelectedIndex == 2)//三月
                {
                    timeScope = ChatRecordTimeScope.Recent3Month;
                }
                else //全部
                {
                }

                
                ChatRecordPage page = null;
                if (this.isGroupChat)
                {
                    page = this.CurrentPersister.GetGroupChatRecordPage(timeScope, this.group.Arg1, this.pageSize, pageIndex);
                }
                else
                {
                    page = this.CurrentPersister.GetChatRecordPage(timeScope, my.Arg1, friend.Arg1, this.pageSize, pageIndex);
                }
                this.chatBox_history.Clear(); 

                if (page == null || page.Content.Count == 0)
                {
                    MessageBoxEx.Show("没有消息记录！");                   
                    return;
                }

                this.currentPageIndex = page.PageIndex;
                this.toolStripTextBox_pageIndex.Text = (this.currentPageIndex + 1).ToString();
                for (var i = 0; i < page.Content.Count; i++)
                {
                    var record = page.Content[i];
                    var decrypted = record.Content;
                    if (this.skinRadioButton_Server.Checked)
                    {
                        if (GlobalResourceManager.Des3Encryption != null)
                        {
                            decrypted = GlobalResourceManager.Des3Encryption.Decrypt(decrypted);
                        }
                    }

                    var content = CompactPropertySerializer.Default.Deserialize<ChatBoxContent>(decrypted, 0);

                    if (this.isGroupChat)
                    {
                        if (record.SpeakerID == this.my.Arg1)
                        {
                            this.AppendChatBoxContent(record.OccureTime, string.Format("{0}({1})", this.my.Arg2, record.SpeakerID), content, Color.Green);
                        }
                        else
                        {
                            var name = this.userNameGetter.GetUserName(record.SpeakerID) ?? record.SpeakerID;                            
                            this.AppendChatBoxContent(record.OccureTime, string.Format("{0}({1})", name, record.SpeakerID), content, Color.Blue);
                        }
                    }
                    else
                    {
                        if (record.SpeakerID == this.my.Arg1)
                        {
                            this.AppendChatBoxContent(record.OccureTime, this.my.Arg2, content, Color.Green);
                        }
                        else
                        {
                            this.AppendChatBoxContent(record.OccureTime, this.friend.Arg2, content, Color.Blue);
                        }
                    }
                    
                }

                this.chatBox_history.SelectionStart = 0;
                this.chatBox_history.ScrollToCaret();

                var pageCount = page.TotalCount / this.pageSize;
                if (page.TotalCount % this.pageSize > 0)
                {
                    ++pageCount;
                }
                this.totalPageCount = pageCount;
                this.toolStripLabel_totalCount.Text = string.Format("/ {0}页", this.totalPageCount);
                this.toolStripTextBox_pageIndex.Focus();
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show(ee.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #region AppendMessage
        private void AppendChatBoxContent(DateTime showTime, string userName, ChatBoxContent content, Color color)
        {
            this.chatBox_history.AppendRichText(string.Format("{0}  {1}\n", userName, showTime), new Font(this.Font, FontStyle.Regular), color);
            this.chatBox_history.AppendText("    ");

            this.chatBox_history.AppendChatBoxContent(content);
            this.chatBox_history.AppendText("\n");
            this.chatBox_history.Select(this.chatBox_history.Text.Length, 0);
            this.chatBox_history.ScrollToCaret();
        }
        #endregion

        private void skinButton1_Click(object sender, EventArgs e)
        {
            this.ShowRecord(0);
        }       

        private void skinButton_last_Click(object sender, EventArgs e)
        {
            this.ShowRecord(this.totalPageCount - 1);
        }

        private void skinButton_pre_Click(object sender, EventArgs e)
        {
            this.ShowRecord(this.currentPageIndex - 1);
        }

        private void skinButton_next_Click(object sender, EventArgs e)
        {
            this.ShowRecord(this.currentPageIndex + 1);
        }       

        

        private void skinRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowRecord(int.MaxValue, false);
        }

        private void toolStripTextBox_pageIndex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    this.ShowRecord(int.Parse(this.toolStripTextBox_pageIndex.Text) - 1);
                }
                catch (Exception ee)
                {
                    MessageBoxEx.Show(ee.Message);
                }
            }
        }

        private void skinComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowRecord(int.MaxValue, false);
        }      
    }
}
