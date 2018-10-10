using System;
using ESBasic;
using DataRabbit.DBAccessing.Application;
using DataRabbit.DBAccessing.ORM;
using DataRabbit;
using System.Collections.Generic;
using System.IO;
using DataRabbit.DBAccessing;
using DataRabbit.DBAccessing.Relation;

namespace JustLib.Records
{
    #region ChatMessageRecord
    [Serializable]
    public class ChatMessageRecord 
    {
        #region Force Static Check
        public const string TableName = "ChatMessageRecord";
        public const string _AutoID = "AutoID";
        public const string _SpeakerID = "SpeakerID";
        public const string _AudienceID = "AudienceID";
        public const string _IsGroupChat = "IsGroupChat";
        public const string _Content = "Content";
        public const string _OccureTime = "OccureTime";
        #endregion

        #region IEntity Members
        public System.Int64 GetPKeyValue()
        {
            return this.AutoID;
        }
        #endregion

        public ChatMessageRecord() { }
        public ChatMessageRecord(string speaker, string audience, byte[] _content, bool groupChat)
        {
            this.speakerID = speaker;
            this.audienceID = audience;
            this.Content = _content;
            this.isGroupChat = groupChat;
        }

        #region AutoID
        private long autoID = 0;
        /// <summary>
        /// 自增ID，编号。
        /// </summary>
        public long AutoID
        {
            get { return autoID; }
            set { autoID = value; }
        }
        #endregion

        #region SpeakerID
        private string speakerID = "";
        /// <summary>
        /// 发言人的ID。
        /// </summary>
        public string SpeakerID
        {
            get { return speakerID; }
            set { speakerID = value; }
        }
        #endregion

        #region AudienceID
        private string audienceID = "";
        /// <summary>
        /// 听众ID，可以为GroupID。
        /// </summary>
        public string AudienceID
        {
            get { return audienceID; }
            set { audienceID = value; }
        }
        #endregion

        #region OccureTime
        private DateTime occureTime = DateTime.Now;
        /// <summary>
        /// 聊天记录发生的时间。
        /// </summary>
        public DateTime OccureTime
        {
            get { return occureTime; }
            set { occureTime = value; }
        }
        #endregion

        #region Content
        private byte[] content;
        /// <summary>
        /// 聊天的内容。
        /// </summary>
        public byte[] Content
        {
            get { return content; }
            set { content = value; }
        }
        #endregion

        #region IsGroupChat
        private bool isGroupChat = false;
        /// <summary>
        /// 是否为群聊记录。
        /// </summary>
        public bool IsGroupChat
        {
            get { return isGroupChat; }
            set { isGroupChat = value; }
        }
        #endregion

    }
    #endregion

    #region ChatRecordPage
    [Serializable]
    public class ChatRecordPage
    {
        public ChatRecordPage() { }
        public ChatRecordPage(int total, int index, List<ChatMessageRecord> _page)
        {
            this.totalCount = total;
            this.pageIndex = index;
            this.content = _page;
        }

        #region TotalCount
        private int totalCount = 0;
        public int TotalCount
        {
            get { return totalCount; }
            set { totalCount = value; }
        }
        #endregion

        #region PageIndex
        private int pageIndex = 0;
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
        #endregion

        #region Content
        private List<ChatMessageRecord> content = new List<ChatMessageRecord>();
        public List<ChatMessageRecord> Content
        {
            get { return content; }
            set { content = value; }
        }
        #endregion
    }
    #endregion

    #region ChatRecordTimeScope
    public enum ChatRecordTimeScope
    {
        RecentWeek = 0,
        RecentMonth,
        Recent3Month,
        All
    } 
    #endregion

    #region IChatRecordPersister
    public interface IChatRecordPersister
    {
        /// <summary>
        /// 插入一条聊天记录（包括群聊天记录）。
        /// </summary>  
        void InsertChatMessageRecord(ChatMessageRecord record);


        /// <summary>
        /// 获取一页与好友的聊天记录。
        /// </summary>
        /// <param name="timeScope">日期范围</param>
        /// <param name="myID">自己的UserID</param>
        /// <param name="friendID">好友的ID</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页索引</param>      
        /// <returns>聊天记录页</returns>
        ChatRecordPage GetChatRecordPage(ChatRecordTimeScope timeScope, string myID, string friendID, int pageSize, int pageIndex);

        /// <summary>
        /// 获取一页群聊天记录。
        /// </summary>
        /// <param name="timeScope">日期范围</param>
        /// <param name="groupID">群ID</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页索引</param>     
        /// <returns>聊天记录页</returns>
        ChatRecordPage GetGroupChatRecordPage(ChatRecordTimeScope timeScope, string groupID, int pageSize, int pageIndex);
    } 
    #endregion

    #region DefaultChatMessageRecordPersister
    /// <summary>
    /// 聊天记录本地持久化器。
    /// </summary>
    public class DefaultChatRecordPersister : IChatRecordPersister
    {
        private TransactionScopeFactory transactionScopeFactory;

        public void Initialize(TransactionScopeFactory fac)
        {
            this.transactionScopeFactory = fac;
        }

        /// <summary>
        /// 插入一条聊天记录。
        /// </summary>      
        public void InsertChatMessageRecord(ChatMessageRecord record)
        {
            if (this.transactionScopeFactory == null)
            {
                return;
            }

            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<ChatMessageRecord> accesser = scope.NewOrmAccesser<ChatMessageRecord>();
                accesser.Insert(record);
                scope.Commit();
            }
        }

        /// <summary>
        /// 获取一页群聊天记录。
        /// </summary>
        /// <param name="groupID">群ID</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页索引</param>     
        /// <returns>聊天记录页</returns>
        public ChatRecordPage GetGroupChatRecordPage(ChatRecordTimeScope chatRecordTimeScope, string groupID, int pageSize, int pageIndex)
        {
            if (this.transactionScopeFactory == null)
            {
                return new ChatRecordPage(0, 0, new List<ChatMessageRecord>());
            }

            DateTimeScope timeScope = null;
            DateTime now = DateTime.Now;
            if (chatRecordTimeScope == ChatRecordTimeScope.RecentWeek) //一周
            {
                timeScope = new DateTimeScope(now.AddDays(-7), now);
            }
            else if (chatRecordTimeScope == ChatRecordTimeScope.RecentMonth)//一月
            {
                timeScope = new DateTimeScope(now.AddDays(-31), now);
            }
            else if (chatRecordTimeScope == ChatRecordTimeScope.Recent3Month)//三月
            {
                timeScope = new DateTimeScope(now.AddDays(-91), now);
            }
            else //全部
            {
            }

            List<Filter> filterList = new List<Filter>();
            filterList.Add(new Filter(ChatMessageRecord._AudienceID, groupID));
            filterList.Add(new Filter(ChatMessageRecord._IsGroupChat, true));
            if (timeScope != null)
            {
                filterList.Add(new Filter(ChatMessageRecord._OccureTime, new DateTime[] { timeScope.StartDate, timeScope.EndDate }, ComparisonOperators.BetweenAnd));
            }
            SimpleFilterTree tree = new SimpleFilterTree(filterList);

            //最后一页
            if (pageIndex == int.MaxValue)
            {
                int total = 0;
                using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
                {
                    IOrmAccesser<ChatMessageRecord> accesser = scope.NewOrmAccesser<ChatMessageRecord>();
                    total = (int)accesser.GetCount(tree);
                    scope.Commit();
                }
                int pageCount = total / pageSize;
                if (total % pageSize > 0)
                {
                    pageCount += 1;
                }
                pageIndex = pageCount - 1;
            }

            int totalCount = 0;
            ChatMessageRecord[] page = null;
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<ChatMessageRecord> accesser = scope.NewOrmAccesser<ChatMessageRecord>();
                page = accesser.GetPage(tree, pageSize, pageIndex, ChatMessageRecord._AutoID, true, out totalCount);
                scope.Commit();
            }
            return new ChatRecordPage(totalCount, pageIndex, new List<ChatMessageRecord>(page));
        }

        /// <summary>
        /// 获取一页与好友的聊天记录。
        /// </summary>
        /// <param name="myID">自己的UserID</param>
        /// <param name="friendID">好友的ID</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页索引</param>      
        /// <returns>聊天记录页</returns>
        public ChatRecordPage GetChatRecordPage(ChatRecordTimeScope chatRecordTimeScope, string myID, string friendID, int pageSize, int pageIndex)
        {
            if (this.transactionScopeFactory == null)
            {
                return new ChatRecordPage(0, 0, new List<ChatMessageRecord>());
            }

            DateTimeScope timeScope = null;
            DateTime now = DateTime.Now;
            if (chatRecordTimeScope == ChatRecordTimeScope.RecentWeek) //一周
            {
                timeScope = new DateTimeScope(now.AddDays(-7), now);
            }
            else if (chatRecordTimeScope == ChatRecordTimeScope.RecentMonth)//一月
            {
                timeScope = new DateTimeScope(now.AddDays(-31), now);
            }
            else if (chatRecordTimeScope == ChatRecordTimeScope.Recent3Month)//三月
            {
                timeScope = new DateTimeScope(now.AddDays(-91), now);
            }
            else //全部
            {
            }

            IFilterTree tree1 = new SimpleFilterTree(new Filter(ChatMessageRecord._SpeakerID, myID), new Filter(ChatMessageRecord._AudienceID, friendID));
            IFilterTree tree2 = new SimpleFilterTree(new Filter(ChatMessageRecord._SpeakerID, friendID), new Filter(ChatMessageRecord._AudienceID, myID));
            IFilterTree tree = new ComplexFilterTree(LogicType.Or, tree1, tree2);
            if (timeScope != null)
            {
                tree = new ComplexFilterTree(LogicType.And, tree, new Filter(ChatMessageRecord._OccureTime, new DateTime[] { timeScope.StartDate, timeScope.EndDate }, ComparisonOperators.BetweenAnd));
            }
            //最后一页
            if (pageIndex == int.MaxValue)
            {
                int total = 0;
                using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
                {
                    IOrmAccesser<ChatMessageRecord> accesser = scope.NewOrmAccesser<ChatMessageRecord>();
                    total = (int)accesser.GetCount(tree);
                    scope.Commit();
                }
                int pageCount = total / pageSize;
                if (total % pageSize > 0)
                {
                    pageCount += 1;
                }
                pageIndex = pageCount - 1;
            }

            int totalCount = 0;
            ChatMessageRecord[] page = null;
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<ChatMessageRecord> accesser = scope.NewOrmAccesser<ChatMessageRecord>();
                page = accesser.GetPage(tree, pageSize, pageIndex, ChatMessageRecord._AutoID, true, out totalCount);
                scope.Commit();
            }
            return new ChatRecordPage(totalCount, pageIndex, new List<ChatMessageRecord>(page));
        }
    } 
    #endregion

    #region SqliteChatRecordPersister
    /// <summary>
    /// 聊天记录本地持久化器（Sqlite数据库）。
    /// </summary>
    public class SqliteChatRecordPersister : DefaultChatRecordPersister
    {
        public SqliteChatRecordPersister(string sqlitePath)
        {
            try
            {
                bool isNew = !File.Exists(sqlitePath);
                //2014.11.27
                string dirName = Path.GetDirectoryName(sqlitePath);
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }


                //初始化Sqlite数据库
                DataConfiguration config = new SqliteDataConfiguration(sqlitePath);
                TransactionScopeFactory transactionScopeFactory = new TransactionScopeFactory(config);
                transactionScopeFactory.Initialize();

                if (isNew)
                {
                    string sql = "CREATE TABLE ChatMessageRecord (AutoID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, SpeakerID VARCHAR( 0, 20 ) NOT NULL, AudienceID VARCHAR( 0, 20 ) NOT NULL, IsGroupChat BOOLEAN NOT NULL, Content BLOB NOT NULL, OccureTime DATETIME NOT NULL ); "
                               + "CREATE INDEX idx_ChatMessageRecord ON ChatMessageRecord ( SpeakerID, AudienceID, OccureTime DESC );"
                               + "CREATE INDEX idx2_ChatMessageRecord ON ChatMessageRecord ( AudienceID, IsGroupChat, OccureTime );";
                    using (TransactionScope scope = transactionScopeFactory.NewTransactionScope())
                    {
                        IRelationAccesser accesser = scope.NewRelationAccesser();
                        accesser.DoCommand(sql);
                        scope.Commit();
                    }
                }

                base.Initialize(transactionScopeFactory);
            }
            catch { }

        }
    } 
    #endregion
}
