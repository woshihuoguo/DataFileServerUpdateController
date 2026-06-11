using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame
{
    public interface VerbOnStorageSession
    {
        void Insert<T>(T data);
        void Update<T>(T data);
        void Delete<T>(T data);

        void Commit();
        void BeginCommit();
        void RunSql(string sql, out object data);
        void RunSql(List<string> sql, out object data);
    }
}
