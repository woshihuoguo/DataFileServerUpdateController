using LT.Common.IO.Storage.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame
{
    sealed class DBSession : VerbOnStorageSession
    {
        private readonly WriteSession session;
        public DBSession(WriteSession session)
        {
            this.session = session;
        }

        //====================================================================================================
        //以上是构造函数与基本数据
        //----------------------------------------------------------------------------------------------------
        //以下是事务接口的具体实现
        //====================================================================================================

        void VerbOnStorageSession.Insert<T>(T data)
        {
            session.Insert(data);
        }

        void VerbOnStorageSession.Update<T>(T data)
        {
            session.Update(data);
        }

        void VerbOnStorageSession.Delete<T>(T data)
        {
            session.Delete(data);
        }

        void VerbOnStorageSession.Commit()
        {
            session.Commit();
        }

        void VerbOnStorageSession.BeginCommit()
        {
            session.BeginCommit();
        }

        void VerbOnStorageSession.RunSql(string sql, out object data)
        {
            data = null;
            session.RunSql(sql, out data);
        }

        void VerbOnStorageSession.RunSql(List<string> sql, out object data)
        {
            //throw new NotImplementedException();
            data = null;
            sql.ForEach(sqlItem => session.RunSql(sqlItem, out var _));
        }

        //void VerbOnStorageSession.RunSql(List<string> sqls, out object data)
        //{
        //    data = null;
        //    session.RunSql(sqls, out data);
        //}


    }
}
