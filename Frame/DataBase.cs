using LT.Common.IO;
using LT.Common.IO.Storage;
using LT.Common.IO.Storage.Model;
using LT.Common.Linq;
using LT.Common.Reflection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Frame
{
    class DataBase : VerbOnStorage, IDisposable
    {
        public DataBase()
        {
            //观察数据库异常
            WatchDBException(context);
        }

        //====================================================================================================
        //以上是类型构造函数
        //----------------------------------------------------------------------------------------------------
        //以下是类型字段
        //====================================================================================================

        private string ip = "127.0.0.1";
        private string catalog = string.Empty;
        private string userName = string.Empty;
        private string password = string.Empty;
        private readonly StorageContext context = new StorageContext();
        private StorageContextType contextType = StorageContextType.MySQL;

        //====================================================================================================
        //以上是类型字段
        //----------------------------------------------------------------------------------------------------
        //以下是观察数据库异常
        //====================================================================================================

        private static void WatchDBException(StorageContext context)
        {
            context.ExceptionOccured +=
                (sender, e) => LT.Common.Logger.Logger.Log("DataBase", "数据库后台异常。", e.Exception);
        }

        //====================================================================================================
        //以上是观察数据库异常
        //----------------------------------------------------------------------------------------------------
        //以下是连接与断开
        //====================================================================================================

        public void Connect()
        {
            //记录
            contextType = StorageContextType.MySQL;
            ip = "127.0.0.1";
            catalog = "hivenms";
            userName = "sa";
            password = "1234";

            //生成配置
            var config = new StorageContextConfig(contextType, ip, catalog, userName, password, new TimeSpan(0, 0, 6));

            //扫描类型
            var entityTypes = scanTypes();
            
            //开始连接
            context.Connect(config, entityTypes, false);
        }

        public void Disconnect()
        {
            context.Disconnect();
        }

        public void Start()
        {
        }

        //====================================================================================================
        //以上是连接与断开
        //----------------------------------------------------------------------------------------------------
        //以下是备份与恢复
        //====================================================================================================

        public void Backup(string filePath)
        {
            context.Backup(filePath);
        }

        public void Restore(string filePath)
        {
            context.Restore(filePath);
        }

        //public bool RunSql(List<string> sqls, out object data)
        //{
        //    var session = context.OpenWriteSession(IsolationLevel.ReadCommitted);
        //    return session.RunSql(sqls, out data);
        //}

        public bool RunSql(string sqls, out object data)
        {
            var session = context.OpenWriteSession(IsolationLevel.ReadCommitted);
            session.RunSql(sqls, out data);
            return true;
        }
        //====================================================================================================
        //以上是备份与恢复
        //----------------------------------------------------------------------------------------------------
        //以下是转储
        //====================================================================================================

        public void Dump(bool if_normal, int keepTime, string autoDumpFloder)
        {
            context.Dump(if_normal, keepTime, autoDumpFloder);
        }

        public bool Dump(int logCount, int logPersistCount, string autoDumpFloder)
        {
            return context.Dump(logCount, logPersistCount, autoDumpFloder);
        }

        //====================================================================================================
        //以上是转储
        //----------------------------------------------------------------------------------------------------
        //以下是存储接口的具体实现
        //====================================================================================================

        void VerbOnStorage.Load<T>(out T[] items)
        {
            var session = context.OpenReadSession(IsolationLevel.ReadCommitted);
            items = session.ToList<T>().ToArray();
        }

        void VerbOnStorage.Save<T>(params T[] items)
        {
            var session = context.OpenWriteSession(IsolationLevel.ReadCommitted);

            //Begin...
            //必须先清空旧的数据，再插入新数据
            session.Clear(typeof(T));
            //End...

            foreach (var item in items)
            {
                session.Insert(item);
            }
            session.Commit();
        }

        void VerbOnStorage.Insert<T>(T[] items)
        {
            var session = context.OpenWriteSession(IsolationLevel.ReadCommitted);
            foreach (var item in items)
            {
                session.Insert(item);
            }
            session.BeginCommit();
        }

        void VerbOnStorage.Insert<T>(bool isNecessary, T[] items)
        {
            var session = context.OpenWriteSession(IsolationLevel.ReadCommitted);
            foreach (var item in items)
            {
                session.Insert(isNecessary, item);
            }
            session.BeginCommit();
        }

        void VerbOnStorage.Insert<T>(bool isNecessary, T[] items, bool isImmediately)
        {
            var session = context.OpenWriteSession(IsolationLevel.ReadCommitted);
            foreach (var item in items)
            {
                session.Insert(isNecessary, item);
            }
            if (isImmediately)
            {
                session.Commit();
            }
            else
            {
                session.BeginCommit();
            }
        }

        void VerbOnStorage.Delete<T>(T[] items)
        {
            var session = context.OpenWriteSession(IsolationLevel.ReadCommitted);
            foreach (var item in items)
            {
                session.Delete(item);
            }
            session.BeginCommit();
        }

        void VerbOnStorage.Delete<T>(bool isNecessary, T[] items)
        {
            var session = context.OpenWriteSession(IsolationLevel.ReadCommitted);
            foreach (var item in items)
            {
                session.Delete(isNecessary, item);
            }
            session.BeginCommit();
        }

        void VerbOnStorage.Delete<T>(bool isNecessary, T[] items, bool isImmediately)
        {
            var session = context.OpenWriteSession(IsolationLevel.ReadCommitted);
            foreach (var item in items)
            {
                session.Delete(isNecessary, item);
            }
            if (isImmediately)
            {
                session.Commit();
            }
            else
            {
                session.BeginCommit();
            }
        }

        void VerbOnStorage.Update<T>(T[] items)
        {
            var session = context.OpenWriteSession(IsolationLevel.ReadCommitted);
            foreach (var item in items)
            {
                session.Update(item);
            }
            session.BeginCommit();
        }

        void VerbOnStorage.Update<T>(bool isNecessary, T[] items)
        {
            var session = context.OpenWriteSession(IsolationLevel.ReadCommitted);
            foreach (var item in items)
            {
                session.Update(isNecessary, item);
            }
            session.BeginCommit();
        }

        void VerbOnStorage.InsertOrUpdate<T>(T[] items)
        {
            var session = context.OpenWriteSession(IsolationLevel.ReadCommitted);
            foreach (var item in items)
            {
                session.InsertOrUpdate(item);
            }
            session.BeginCommit();
        }

        void VerbOnStorage.InsertOrUpdate<T>(bool isNecessary, T[] items, bool isImmediately)
        {
            var session = context.OpenWriteSession(IsolationLevel.ReadCommitted);
            foreach (var item in items)
            {
                session.InsertOrUpdate(isNecessary, item);
            }
            if (isImmediately)
            {
                session.Commit();
            }
            else
            {
                session.BeginCommit();
            }
        }

        VerbOnStorageQuery<T> VerbOnStorage.CreateQuery<T>()
        {
            return new DBQuery<T>(context.OpenReadSession(IsolationLevel.ReadCommitted));
        }

        VerbOnStorageSession VerbOnStorage.CreateSession()
        {
            return new DBSession(context.OpenWriteSession(IsolationLevel.ReadCommitted));
        }

        void VerbOnStorage.WaitForExit()
        {
            context.ExitSafely();
        }

        //====================================================================================================
        //以上是存储接口的具体实现
        //----------------------------------------------------------------------------------------------------
        //以下是扫描类型
        //====================================================================================================

        private static Type[] scanTypes()
        {
            var filePaths = FileSystemInfo.GetFilePaths(System.Environment.CurrentDirectory, "*.Entity.DLL");
            var choices = CustomTypeScan.ScanTypes<EntityRule>(filePaths);
            //var assembly1 = Assembly.LoadFile(choices[0].FilePath);
            //var ss = assembly1.GetType(choices[0].TypeNames[0]);
            //var tt = choices[0].TypeNames.Select(typename => assembly1.GetType(typename)).ToList();
            return choices
                    .SelectMany(
                        choice =>
                        {
                            var assembly = Assembly.LoadFile(choice.FilePath);
                            return choice.TypeNames.Select(typeName => assembly.GetType(typeName));
                        })
                    .ToArray();
        }

        //====================================================================================================
        //以上是扫描类型
        //----------------------------------------------------------------------------------------------------
        //以下是嵌套类型：类型检查器
        //====================================================================================================

        class EntityRule : CustomTypeScanRule
        {
            public override bool NeedToChoiceThis(Type nowType)
            {
                return nowType.DerivedFromClass<IEntity>();
                // return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
    }
}
