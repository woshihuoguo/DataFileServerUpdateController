using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame
{
    public interface VerbOnStorage : IDisposable
    {
        void Load<T>(out T[] items);
        void Save<T>(params T[] items);

        void Insert<T>(T[] items);
        void Delete<T>(T[] items);
        void Update<T>(T[] items);
        void InsertOrUpdate<T>(T[] items);

        void Insert<T>(bool isNecessary, T[] items);
        void Delete<T>(bool isNecessary, T[] items);
        void Update<T>(bool isNecessary, T[] items);
        void InsertOrUpdate<T>(bool isNecessary, T[] items, bool isImmediately);
        void Insert<T>(bool isNecessary, T[] items, bool isImmediately);
        void Delete<T>(bool isNecessary, T[] items, bool isImmediately);
        VerbOnStorageQuery<T> CreateQuery<T>();
        VerbOnStorageSession CreateSession();

        void WaitForExit();
    }
}
