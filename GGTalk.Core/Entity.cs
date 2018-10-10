using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ESBasic.Helpers;
using ESBasic;
using JustLib;
using DataRabbit.DBAccessing;

namespace GGTalk
{
    #region GGUser
    [Serializable]
    public class GGUser : IUser
    {
        #region Force Static Check
        public const string TableName = "GGUser";
        public const string _UserID = "UserID";
        public const string _PasswordMD5 = "PasswordMD5";
        public const string _Name = "Name";
        public const string _Department = "Department";
        public const string _Signature = "Signature";
        public const string _HeadImageIndex = "HeadImageIndex";
        public const string _HeadImageData = "HeadImageData";
        public const string _Groups = "Groups";
        public const string _Friends = "Friends";
        public const string _DefaultFriendCatalog = "DefaultFriendCatalog";
        public const string _CreateTime = "CreateTime";
        #endregion

        public GGUser() { }
        public GGUser(string id, string pwd, string _name, string _friends, string _signature, int headIndex, string _groups)
        {
            this.UserID = id;
            this.passwordMD5 = pwd;
            this.Name = _name;
            this.friends = _friends;
            this.Signature = _signature;           
            this.HeadImageIndex = headIndex;
            this.groups = _groups;
        }  

        #region PasswordMD5
        private string passwordMD5 = "";
        /// <summary>
        /// 登录密码(MD5加密)。
        /// </summary>
        public string PasswordMD5
        {
            get { return passwordMD5; }
            set { passwordMD5 = value; }
        }
        #endregion       

        #region UserID
        private string userID = "";
        /// <summary>
        /// 用户登录帐号。
        /// </summary>
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        #endregion

        #region Name
        private string name = "";
        /// <summary>
        /// 昵称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion        

        #region Friends
        private string friends = "";
        /// <summary>
        /// 好友。如 我的好友：10000,10001,1234;家人:1200,1201 。
        /// </summary>
        public string Friends
        {
            get { return friends; }
            set
            {
                friends = value;
                this.friendDicationary = null;
                this.allFriendList = null;
            }
        }

        #region 非DB字段
        private Dictionary<string,List<string>> friendDicationary = null;
        /// <summary>
        /// 好友ID的分组。非DB字段。
        /// </summary>
        public Dictionary<string, List<string>> FriendDicationary
        {
            get
            {
                if (this.friendDicationary == null)
                {
                    if (string.IsNullOrEmpty(this.friends))
                    {
                        this.friends = "我的好友:";
                    }
                    this.friendDicationary = new Dictionary<string, List<string>>();
                    var catalogs = this.friends.Split(';');
                    foreach (var catalog in catalogs)
                    {
                        var ary = catalog.Split(':');
                        var catalogName = ary[0];
                        var friends = new List<string>(ary[1].Split(','));
                        if (friends.Count == 1)
                        {
                            friends.Remove("");
                        }
                        this.friendDicationary.Add(catalogName, friends);
                    }                          
                }
                return friendDicationary;
            }
        }

        private List<string> allFriendList = null;
        public List<string> GetAllFriendList()
        {
            if (this.allFriendList == null)
            {
                var list = new List<string>();
                foreach (var tmp in this.FriendDicationary.Values)
                {
                    list.AddRange(tmp);
                }
                this.allFriendList = list;
            }

            return this.allFriendList;
        }

        private string GetFriendsVal(Dictionary<string, List<string>> friendDic)
        {
            var sb = new StringBuilder("");
            var count = 0;
            foreach (var pair in friendDic)
            {
                if (count > 0)
                {
                    sb.Append(";");
                }
                var ff = ESBasic.Helpers.StringHelper.ContactString(pair.Value, ",");
                sb.Append(string.Format("{0}:{1}" ,pair.Key ,ff));
                ++count;
            }
            return sb.ToString();
        }
        #endregion

        public void AddFriend(string friendID ,string catalog)
        {
            if (!this.FriendDicationary.ContainsKey(catalog))
            {
                return;
            }
            if (this.FriendDicationary[catalog].Contains(friendID))
            {
                return;
            }

            this.FriendDicationary[catalog].Add(friendID);
            this.friends = this.GetFriendsVal(this.friendDicationary);
            this.allFriendList = null;
        }

        public void RemoveFriend(string friendID)
        {
            foreach (var pair in this.FriendDicationary)
            {
                pair.Value.Remove(friendID);
            }

            this.friends = this.GetFriendsVal(this.friendDicationary);
            this.allFriendList = null;
        }

        public void ChangeFriendCatalogName(string oldName, string newName)
        {
            if (!this.FriendDicationary.ContainsKey(oldName))
            {
                return;
            }

            var merged = new List<string>();
            if (this.FriendDicationary.ContainsKey(newName))
            {
                merged = this.FriendDicationary[newName];
                this.FriendDicationary.Remove(newName);
            }
            var friends = this.friendDicationary[oldName];
            friends.AddRange(merged);
            this.FriendDicationary.Remove(oldName);
            this.FriendDicationary.Add(newName, friends);
            this.friends = this.GetFriendsVal(this.friendDicationary);
            if (oldName == this.defaultFriendCatalog)
            {
                this.defaultFriendCatalog = newName;
            }
        }

        public void AddFriendCatalog(string name)
        {
            if (this.FriendDicationary.ContainsKey(name))
            {
                return;
            }

            this.FriendDicationary.Add(name, new List<string>());
            this.friends = this.GetFriendsVal(this.friendDicationary);
        }

        public void RemvoeFriendCatalog(string name)
        {
            if (!this.FriendDicationary.ContainsKey(name) || this.defaultFriendCatalog == name)
            {
                return;
            }

            this.FriendDicationary.Remove(name);
            this.friends = this.GetFriendsVal(this.friendDicationary);
        }

        public void MoveFriend(string friendID, string oldCatalog, string newCatalog)
        {
            if (!this.FriendDicationary.ContainsKey(oldCatalog) || !this.FriendDicationary.ContainsKey(newCatalog))
            {
                return;
            }
            this.friendDicationary[oldCatalog].Remove(friendID);
            if (!this.friendDicationary[newCatalog].Contains(friendID))
            {
                this.friendDicationary[newCatalog].Add(friendID);
            }
            this.friends = this.GetFriendsVal(this.friendDicationary);
        }

        public List<string> GetFriendCatalogList()
        {
            return new List<string> ( this.FriendDicationary.Keys);
        }
        #endregion

        #region Groups
        private string groups = "";
        /// <summary>
        /// 该用户所属的组。组ID用英文逗号隔开
        /// </summary>
        public string Groups
        {
            get { return groups; }
            set
            {                
                groups = value;
                this.groupList = null;
            }
        }

        #region 非DB字段
        private List<string> groupList = null;
        /// <summary>
        /// 所属组ID的数组。非DB字段。
        /// </summary>
        public List<string> GroupList
        {
            get
            {
                if (this.groupList == null)
                {
                    this.groupList = new List<string>(this.groups.Split(','));
                    if (this.groupList.Count == 1)
                    {
                        this.groupList.Remove("");
                    }
                }
                return groupList;
            }
        }
        #endregion
        public void JoinGroup(string groupID)
        {
            if (this.GroupList.Contains(groupID))
            {
                return;
            }
            this.GroupList.Add(groupID);
            this.groups = ESBasic.Helpers.StringHelper.ContactString(this.GroupList, ",");
        }

        public void QuitGroup(string groupID)
        {
            this.GroupList.Remove(groupID);
            this.groups = ESBasic.Helpers.StringHelper.ContactString(this.GroupList, ",");
        }
        #endregion        

        #region CreateTime
        private DateTime createTime = DateTime.Now;
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        } 
        #endregion        

        #region Signature
        private string signature = "";
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature
        {
            get { return signature; }
            set { signature = value; }
        }
        #endregion

        #region HeadImageIndex
        private int headImageIndex = 0;
        /// <summary>
        /// 头像图片的索引。如果为-1，表示自定义头像。
        /// </summary>
        public int HeadImageIndex
        {
            get { return headImageIndex; }
            set
            {
                headImageIndex = value;
                this.headIcon = null;
            }
        }
        #endregion

        #region HeadImageData
        private byte[] headImageData = null;
        public byte[] HeadImageData
        {
            get { return headImageData; }
            set
            {
                headImageData = value;
                this.headImage = null;
                this.headImageGrey = null;
                this.headIcon = null;
            }
        }
        #endregion

        #region DefaultFriendCatalog
        private string defaultFriendCatalog = "我的好友";
        /// <summary>
        /// 默认好友分组。不能被删除。
        /// </summary>
        public string DefaultFriendCatalog
        {
            get 
            {
                if (string.IsNullOrEmpty(this.defaultFriendCatalog))
                {
                    this.defaultFriendCatalog = "我的好友";
                }
                return defaultFriendCatalog; 
            }
            set { defaultFriendCatalog = value; }
        } 
        #endregion        

        #region Version
        private int version = 0;
        public int Version
        {
            get { return version; }
            set { version = value; }
        } 
        #endregion

        #region 非DB字段
        #region HeadImage
        [NonSerialized]
        private Image headImage = null;
        /// <summary>
        /// 自定义头像。非DB字段。
        /// </summary>
        public Image HeadImage
        {
            get
            {
                if (this.headImage == null && this.headImageData != null)
                {
                    this.headImage = ESBasic.Helpers.ImageHelper.Convert(this.headImageData);
                }
                return headImage;
            }
        }
        #endregion

        #region HeadImageGrey
        [NonSerialized]
        private Image headImageGrey = null;
        /// <summary>
        /// 自定义头像。非DB字段。
        /// </summary>
        public Image HeadImageGrey
        {
            get
            {
                if (this.headImageGrey == null && this.headImageData != null)
                {
                    this.headImageGrey = ESBasic.Helpers.ImageHelper.ConvertToGrey(this.HeadImage);
                }
                return this.headImageGrey;
            }
        }
        #endregion

        #region GetHeadIcon
        [NonSerialized]
        private Icon headIcon = null;
        /// <summary>
        /// 自定义头像。非DB字段。
        /// </summary>
        public Icon GetHeadIcon(Image[] defaultHeadImages)
        {
            if (this.headIcon != null)
            {
                return this.headIcon;
            }

            if (this.HeadImage != null)
            {
                this.headIcon = ImageHelper.ConvertToIcon(this.headImage ,64);
                return this.headIcon;
            }

            this.headIcon = ImageHelper.ConvertToIcon(defaultHeadImages[this.headImageIndex],64);
            return this.headIcon;
        }
        #endregion

        #region UserStatus
        private UserStatus userStatus = UserStatus.OffLine;
        /// <summary>
        /// 在线状态。非DB字段。
        /// </summary>
        [NotDBField] 
        public UserStatus UserStatus
        {
            get { return userStatus; }
            set { userStatus = value; }
        }
        #endregion 

        #region Tag
        private object tag;
        /// <summary>
        /// 可用于存储 LastWordsRecord。
        /// </summary>
        [NotDBField] 
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        } 
        #endregion

        #region LastWords
        public string LastWords
        {
            get
            {
                if (this.tag == null)
                {
                    return "";
                }

                var record = this.tag as LastWordsRecord;
                if (record == null)
                {
                    return "";
                }

                var content = record.ChatBoxContent.GetTextWithPicPlaceholder("[图]");
                return string.Format("{0}： {1}", record.IsMe ? "我" : "TA", content);
            }
        } 
        #endregion
        #endregion

        #region OnlineOrHide
        public bool OnlineOrHide
        {
            get
            {
                return this.userStatus != UserStatus.OffLine;
            }
        } 
        #endregion

        #region OfflineOrHide
        public bool OfflineOrHide
        {
            get
            {
                return this.userStatus == UserStatus.OffLine || this.userStatus == UserStatus.Hide;
            }
        }
        #endregion

        #region IEntity Members
        public System.String GetPKeyValue()
        {
            return this.UserID;
        }
        #endregion     

        public override string ToString()
        {
            return string.Format("{0}({1})-{2}，Ver：{3}" ,this.name,this.UserID,this.userStatus,this.version);
        }

        public Parameter<string, string> GetIDName()
        {
            return new Parameter<string, string>(this.UserID, this.Name);
        }       

        #region PartialCopy
        [NonSerialized]
        private GGUser partialCopy = null;
        public GGUser PartialCopy
        {
            get
            {
                if (this.partialCopy == null)
                {
                    this.partialCopy = (GGUser)this.MemberwiseClone();
                    this.partialCopy.Groups = "";
                    this.partialCopy.Friends = "";
                }
                else
                {
                    this.partialCopy.userStatus = this.userStatus;
                }
                return this.partialCopy;
            }
        }
        #endregion

        #region IUser 接口
        public string ID
        {
            get { return this.userID; }
        }

        public bool IsGroup
        {
            get { return false; }
        }
       
        #endregion       
    
        public string Department
        {
            get { return ""; }
        }

        public List<string> FriendList
        {
            get { return this.GetAllFriendList(); }
        }

        public string[] OrgPath
        {
            get { return null; }
        }


        public bool IsInOrg
        {
            get { return false; }
        }
    }
    #endregion

    #region GGGroup
    [Serializable]
    public class GGGroup : IGroup
    {
        #region Force Static Check
        public const string TableName = "GGGroup";
        public const string _GroupID = "GroupID";
        public const string _Name = "Name";
        public const string _CreatorID = "CreatorID";
        public const string _Announce = "Announce";
        public const string _Members = "Members";
        public const string _CreateTime = "CreateTime";
        public const string _Version = "Version";
        #endregion

        #region IEntity Members
        public System.String GetPKeyValue()
        {
            return this.GroupID;
        }
        #endregion

        public GGGroup() { }
        public GGGroup(string id, string _name, string _creator ,string _announce ,string _members)
        {
            this.groupID = id;
            this.name = _name;
            this.creatorID = _creator;          
            this.announce = _announce;
            this.members = _members;
        }

        #region GroupID
        private string groupID = "";
        public string GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
        #endregion

        #region Name
        private string name = "";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion

        #region CreatorID
        private string creatorID = "";
        /// <summary>
        /// 群创建者。
        /// </summary>
        public string CreatorID
        {
            get { return creatorID; }
            set { creatorID = value; }
        } 
        #endregion

        #region CreateTime
        private DateTime createTime = DateTime.Now;
        /// <summary>
        /// 创建时间。
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        } 
        #endregion

        #region Announce
        private string announce = "";
        /// <summary>
        /// 公告。
        /// </summary>
        public string Announce
        {
            get { return announce; }
            set { announce = value; }
        } 
        #endregion

        #region Members
        private string members = "";
        /// <summary>
        /// 组成员，ID使用英文逗号隔开。
        /// </summary>
        public string Members
        {
            get { return members; }
            set 
            { 
                members = value;
                this.memberList = null;
            }
        } 
        #endregion

        #region Version
        private int version = 0;
        public int Version
        {
            get { return version; }
            set { version = value; }
        }
        #endregion

        #region 非DB字段
        #region Tag
        private object tag;
        [NotDBField] 
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        #endregion

        public string LastWords
        {
            get
            {
                if (this.tag == null)
                {
                    return "";
                }

                var record = this.tag as LastWordsRecord;
                if (record == null)
                {
                    return "";
                }

                var content = record.ChatBoxContent.GetTextWithPicPlaceholder("[图]");
                return string.Format("{0}： {1}", record.SpeakerName, content);
            }
        }

        #region MemberList
        private List<string> memberList = null;
        /// <summary>
        /// 非DB字段
        /// </summary>
        public List<string> MemberList
        {
            get
            {
                if (memberList == null)
                {
                    this.memberList = new List<string>(this.members.Split(','));
                    this.memberList.Remove("");
                }

                return memberList;
            }
        }
        #endregion 
        #endregion

        public void AddMember(string userID)
        {
            if (!this.MemberList.Contains(userID))
            {
                this.MemberList.Add(userID);
                this.Members = ESBasic.Helpers.StringHelper.ContactString<string>(this.MemberList, ",");
            }
        }

        public void RemoveMember(string userID)
        {
            this.MemberList.Remove(userID);
            this.Members = ESBasic.Helpers.StringHelper.ContactString<string>(this.MemberList, ",");
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", this.name, this.groupID);
        }

        public Parameter<string, string> GetIDName()
        {
            return new Parameter<string, string>(this.GroupID, this.Name);
        }

        public string ID
        {
            get { return this.groupID; }
        }


        public bool IsGroup
        {
            get { return true; }
        }
    } 
    #endregion        

    #region OfflineMessage
    /// <summary>
    /// 离线消息。
    /// </summary>
    [Serializable]
    public class OfflineMessage
    {
        #region Ctor
        public OfflineMessage() { }
        public OfflineMessage(string _sourceUserID, string _destUserID, int _informationType, byte[] info)
        {
            this.sourceUserID = _sourceUserID;
            this.destUserID = _destUserID;
            this.informationType = _informationType;
            this.information = info;
        }
        #endregion

        #region SourceUserID
        private string sourceUserID = "";
        /// <summary>
        /// 发送离线消息的用户ID。
        /// </summary>
        public string SourceUserID
        {
            get { return sourceUserID; }
            set { sourceUserID = value; }
        }
        #endregion

        #region DestUserID
        private string destUserID = "";
        /// <summary>
        /// 接收离线消息的用户ID。
        /// </summary>
        public string DestUserID
        {
            get { return destUserID; }
            set { destUserID = value; }
        }
        #endregion

        #region InformationType
        private int informationType = 0;
        /// <summary>
        /// 信息的类型。
        /// </summary>
        public int InformationType
        {
            get { return informationType; }
            set { informationType = value; }
        }
        #endregion

        #region Information
        private byte[] information;
        /// <summary>
        /// 信息内容
        /// </summary>
        public byte[] Information
        {
            get { return information; }
            set { information = value; }
        }
        #endregion

        #region Time
        private DateTime time = DateTime.Now;
        /// <summary>
        /// 服务器接收到要转发离线消息的时间。
        /// </summary>
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }
        #endregion
    }
    #endregion

    #region OfflineFileItem
    /// <summary>
    /// 离线文件条目
    /// </summary>
    public class OfflineFileItem
    {        
        /// <summary>
        /// 条目的唯一编号，数据库自增序列，主键。
        /// </summary>
        public string AutoID { get; set; }
       
        /// <summary>
        /// 离线文件的名称。
        /// </summary>
        public string FileName { get; set; }
       
        /// <summary>
        /// 文件的大小。
        /// </summary>
        public ulong FileLength { get; set; }
       
        /// <summary>
        /// 发送者ID。
        /// </summary>
        public string SenderID { get; set; }
     
        /// <summary>
        /// 接收者ID。
        /// </summary>
        public string AccepterID { get; set; }
       
        /// <summary>
        /// 在服务器上存储离线文件的临时路径。
        /// </summary>
        public string RelayFilePath { get; set; }
    }
    #endregion         

    #region LastWordsRecord
    [Serializable]
    public class LastWordsRecord
    {
        public LastWordsRecord() { }
        public LastWordsRecord(string _speakerID, string _speakerName, bool me, ChatBoxContent content)
        {
            this.speakerID = _speakerID;
            this.speakerName = _speakerName;
            this.isMe = me;
            this.chatBoxContent = content;
        }

        #region SpeakerID
        private string speakerID;
        public string SpeakerID
        {
            get { return speakerID; }
            set { speakerID = value; }
        } 
        #endregion

        #region SpeakerName
        private string speakerName;
        public string SpeakerName
        {
            get { return speakerName; }
            set { speakerName = value; }
        } 
        #endregion

        #region ChatBoxContent
        private ChatBoxContent chatBoxContent;
        public ChatBoxContent ChatBoxContent
        {
            get { return chatBoxContent; }
            set { chatBoxContent = value; }
        }
        #endregion

        #region IsMe
        private bool isMe;
        public bool IsMe
        {
            get { return isMe; }
            set { isMe = value; }
        }
        #endregion

        #region SpeakTime
        private DateTime speakTime = DateTime.Now;
        public DateTime SpeakTime
        {
            get { return speakTime; }
            set { speakTime = value; }
        }
        #endregion

    } 
    #endregion

    #region ChatBoxContent
    [Serializable]
    public class ChatBoxContent
    {
        public ChatBoxContent() { }
        public ChatBoxContent(string _text, Font _font, Color c)
        {
            this.text = _text;
            this.font = _font;
            this.color = c;
        }

        #region Text
        private string text = "";
        /// <summary>
        /// 纯文本信息
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        #endregion

        #region Font
        private Font font;
        public Font Font
        {
            get { return font; }
            set { font = value; }
        }
        #endregion

        #region Color
        private Color color;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        #endregion

        #region ForeignImageDictionary
        private Dictionary<uint, Image> foreignImageDictionary = new Dictionary<uint, Image>();
        /// <summary>
        /// 非内置的表情图片。key - 在ChatBox中的位置。
        /// </summary>
        public Dictionary<uint, Image> ForeignImageDictionary
        {
            get { return foreignImageDictionary; }
            set { foreignImageDictionary = value; }
        }
        #endregion

        #region EmotionDictionary
        private Dictionary<uint, uint> emotionDictionary = new Dictionary<uint, uint>();
        /// <summary>
        /// 内置的表情图片。key - 在ChatBox中的位置 ，value - 表情图片在内置列表中的index。
        /// </summary>
        public Dictionary<uint, uint> EmotionDictionary
        {
            get { return emotionDictionary; }
            set { emotionDictionary = value; }
        }
        #endregion

        #region PicturePositions
        private List<uint> picturePositions = new List<uint>();
        /// <summary>
        /// 所有图片的位置。从小到大排列。
        /// </summary>
        public List<uint> PicturePositions
        {
            get { return picturePositions; }
            set { picturePositions = value; }
        }
        #endregion

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(this.text) && (this.foreignImageDictionary == null || this.foreignImageDictionary.Count == 0) && (this.emotionDictionary == null || this.emotionDictionary.Count == 0);
        }

        public bool ContainsForeignImage()
        {
            return this.foreignImageDictionary != null && this.foreignImageDictionary.Count > 0;
        }

        public void AddForeignImage(uint pos, Image img)
        {
            this.foreignImageDictionary.Add(pos, img);
        }

        public void AddEmotion(uint pos, uint emotionIndex)
        {
            this.emotionDictionary.Add(pos, emotionIndex);
        }

        public string GetTextWithPicPlaceholder(string placeholder)
        {
            if (this.picturePositions == null || this.picturePositions.Count == 0)
            {
                return this.Text;
            }

            var tmp = this.Text;
            for (var i = this.picturePositions.Count - 1; i >= 0; i--)
            {
                tmp = tmp.Insert((int)this.picturePositions[i], placeholder);
            }
            return tmp;
        }
    }
    #endregion
}