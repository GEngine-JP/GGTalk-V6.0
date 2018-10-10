using System.Collections.Generic;
using DataRabbit;
using DataRabbit.DBAccessing;
using DataRabbit.DBAccessing.Application;
using JustLib.Records;

namespace GGTalk.Server
{
    /// <inheritdoc cref="_transactionScopeFactory" />
    /// <summary>
    /// 真实数据库，支持SqlServer和MySQL，通过构造函数指定数据库类型。
    /// </summary>
    public class RealDb : DefaultChatRecordPersister, IDbPersister
    {
        private readonly TransactionScopeFactory _transactionScopeFactory;

        /// <summary>
        /// 该构造函数用于SqlServer数据库。
        /// </summary>        
        public RealDb(string sqlServerDbName, string dbIp, string saPwd)
        {
            DataConfiguration config = new SqlDataConfiguration(dbIp, "sa", saPwd, sqlServerDbName);
            _transactionScopeFactory = new TransactionScopeFactory(config);
            _transactionScopeFactory.Initialize();
            Initialize(_transactionScopeFactory);
        }

        /// <summary>
        /// 该构造函数用于MySQL数据库。
        /// </summary>   
        public RealDb(string mysqlDbName, string dbIp, int dbPort, string rootPwd)
        {
            DataConfiguration config = new MysqlDataConfiguration(dbIp, dbPort, "root", rootPwd, mysqlDbName);
            _transactionScopeFactory = new TransactionScopeFactory(config);
            _transactionScopeFactory.Initialize();
            Initialize(_transactionScopeFactory);
        }

        public void InsertUser(GGUser t)
        {
            using (var scope = _transactionScopeFactory.NewTransactionScope())
            {
                var accessor = scope.NewOrmAccesser<GGUser>();
                accessor.Insert(t);
                scope.Commit();
            }
        }

        public void InsertGroup(GGGroup t)
        {
            using (var scope = _transactionScopeFactory.NewTransactionScope())
            {
                var accessor = scope.NewOrmAccesser<GGGroup>();
                accessor.Insert(t);
                scope.Commit();
            }
        }

        public void DeleteGroup(string groupId)
        {
            using (var scope = _transactionScopeFactory.NewTransactionScope())
            {
                var accessor = scope.NewOrmAccesser<GGGroup>();
                accessor.Delete(groupId);
                scope.Commit();
            }
        }

        public void UpdateUser(GGUser t)
        {
            using (var scope = _transactionScopeFactory.NewTransactionScope())
            {
                var accessor = scope.NewOrmAccesser<GGUser>();
                accessor.Update(t);
                scope.Commit();
            }
        }

        public void UpdateUserFriends(GGUser t)
        {
            using (var scope = _transactionScopeFactory.NewTransactionScope())
            {
                var accessor = scope.NewOrmAccesser<GGUser>();
                accessor.Update(new ColumnUpdating(GGUser._Friends, t.Friends), new Filter(GGUser._UserID, t.UserID));
                scope.Commit();
            }
        }

        public void UpdateGroup(GGGroup t)
        {
            using (var scope = _transactionScopeFactory.NewTransactionScope())
            {
                var accessor = scope.NewOrmAccesser<GGGroup>();
                accessor.Update(t);
                scope.Commit();
            }
        }

        public IEnumerable<GGUser> GetAllUser()
        {
            List<GGUser> list;
            using (var scope = _transactionScopeFactory.NewTransactionScope())
            {
                var accessor = scope.NewOrmAccesser<GGUser>();
                list = accessor.GetAll();
                scope.Commit();
            }

            return list;
        }

        public IEnumerable<GGGroup> GetAllGroup()
        {
            List<GGGroup> list;
            using (var scope = _transactionScopeFactory.NewTransactionScope())
            {
                var accessed = scope.NewOrmAccesser<GGGroup>();
                list = accessed.GetAll();
                scope.Commit();
            }

            return list;
        }

        public void ChangeUserPassword(string userId, string newPasswordMd5)
        {
            using (var scope = _transactionScopeFactory.NewTransactionScope())
            {
                var accessed = scope.NewOrmAccesser<GGUser>();
                accessed.Update(new ColumnUpdating(GGUser._PasswordMD5, newPasswordMd5),
                    new Filter(GGUser._UserID, userId));
                scope.Commit();
            }
        }

        public void ChangeUserGroups(string userId, string groups)
        {
            using (var scope = _transactionScopeFactory.NewTransactionScope())
            {
                var accessor = scope.NewOrmAccesser<GGUser>();
                accessor.Update(new ColumnUpdating(GGUser._Groups, groups), new Filter(GGUser._UserID, userId));
                scope.Commit();
            }
        }
    }
}