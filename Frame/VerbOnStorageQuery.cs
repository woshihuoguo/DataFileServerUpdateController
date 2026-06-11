using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame
{
    public interface VerbOnStorageQuery<T> : IDisposable
    {
        List<T> ToList();
        List<T> GetRange(int startIndex);
        List<T> GetRange(int startIndex, int length);

        Int64 Count();
        TResult Max<TResult>(string name);

        VerbOnStorageQuery<T> EqualTo(string name, string value);
        VerbOnStorageQuery<T> GreaterThan(string name, string value);
        VerbOnStorageQuery<T> GreaterThanOrEqualTo(string name, string value);
        VerbOnStorageQuery<T> In(string name, IEnumerable<string> values);
        VerbOnStorageQuery<T> LessThan(string name, string value);
        VerbOnStorageQuery<T> LessThanOrEqualTo(string name, string value);
        VerbOnStorageQuery<T> Like(string name, string value);
        VerbOnStorageQuery<T> LikeEndWith(string name, string value);
        VerbOnStorageQuery<T> NotEqualTo(string name, string value);
        VerbOnStorageQuery<T> Or(VerbOnStorageQuery<T> nextQuery);
        VerbOnStorageQuery<T> OrderBy(string[] names);
        VerbOnStorageQuery<T> OrderByDescending(string[] names);
    }
}
