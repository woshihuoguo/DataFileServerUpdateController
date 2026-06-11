using LT.Common.IO.Storage;
using LT.Common.IO.Storage.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame
{
    sealed class DBQuery<T> : VerbOnStorageQuery<T>
    {
        public DBQuery(ReadSession session)
        {
            this.session = session;
        }

        //====================================================================================================
        //以上是类型构造函数
        //----------------------------------------------------------------------------------------------------
        //以下是类型字段
        //====================================================================================================

        private readonly ReadSession session;

        //====================================================================================================
        //以上是类型字段
        //----------------------------------------------------------------------------------------------------
        //以下是类型属性
        //====================================================================================================

        internal ReadSession Session
        {
            get { return session; }
        }

        //====================================================================================================
        //以上是类型属性
        //----------------------------------------------------------------------------------------------------
        //以下是查询接口的具体实现
        //====================================================================================================

        List<T> VerbOnStorageQuery<T>.ToList()
        {
            return session.ToList<T>();
        }

        List<T> VerbOnStorageQuery<T>.GetRange(int startIndex)
        {
            return session.ToList<T>((uint)startIndex, uint.MaxValue);
        }

        List<T> VerbOnStorageQuery<T>.GetRange(int startIndex, int length)
        {
            return session.ToList<T>((uint)startIndex, (uint)length);
        }

        Int64 VerbOnStorageQuery<T>.Count()
        {
            return session.Count<T>();
        }

        TResult VerbOnStorageQuery<T>.Max<TResult>(string name)
        {
            return session.Max<T, TResult>(name);
        }

        VerbOnStorageQuery<T> VerbOnStorageQuery<T>.EqualTo(string name, string value)
        {
            return new DBQuery<T>(session.EqualTo(name, value));
        }

        VerbOnStorageQuery<T> VerbOnStorageQuery<T>.GreaterThan(string name, string value)
        {
            return new DBQuery<T>(session.GreaterThan(name, value));
        }

        VerbOnStorageQuery<T> VerbOnStorageQuery<T>.GreaterThanOrEqualTo(string name, string value)
        {
            return new DBQuery<T>(session.GreaterThanOrEqualTo(name, value));
        }

        VerbOnStorageQuery<T> VerbOnStorageQuery<T>.In(string name, IEnumerable<string> values)
        {
            return new DBQuery<T>(session.In(name, values));
        }

        VerbOnStorageQuery<T> VerbOnStorageQuery<T>.LessThan(string name, string value)
        {
            return new DBQuery<T>(session.LessThan(name, value));
        }

        VerbOnStorageQuery<T> VerbOnStorageQuery<T>.LessThanOrEqualTo(string name, string value)
        {
            return new DBQuery<T>(session.LessThanOrEqualTo(name, value));
        }

        VerbOnStorageQuery<T> VerbOnStorageQuery<T>.Like(string name, string value)
        {
            return new DBQuery<T>(session.Like(name, value));
        }

        VerbOnStorageQuery<T> VerbOnStorageQuery<T>.LikeEndWith(string name, string value)
        {
            return new DBQuery<T>(session.LikeEndWith(name, value));
        }

        VerbOnStorageQuery<T> VerbOnStorageQuery<T>.NotEqualTo(string name, string value)
        {
            return new DBQuery<T>(session.NotEqualTo(name, value));
        }

        VerbOnStorageQuery<T> VerbOnStorageQuery<T>.Or(VerbOnStorageQuery<T> nextQuery)
        {
            var dbQuery = nextQuery as DBQuery<T>;
            if (dbQuery == null)
            {
                throw new InvalidCastException("无法识别的查询基类型");
            }

            return new DBQuery<T>(session.Or(dbQuery.session));
        }

        VerbOnStorageQuery<T> VerbOnStorageQuery<T>.OrderBy(string[] names)
        {
            return new DBQuery<T>(session.OrderBy(names));
        }

        VerbOnStorageQuery<T> VerbOnStorageQuery<T>.OrderByDescending(string[] names)
        {
            return new DBQuery<T>(session.OrderByDescending(names));
        }

        public void Dispose()
        {

        }
    }
}
