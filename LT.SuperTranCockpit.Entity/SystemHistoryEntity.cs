using LT.Common.IO.Storage.Model;
using System;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "system_history")]
    public class SystemHistoryEntity : IEntity
    {
        public const string ContentName = "Content";
        public const string OperatorName = "Operator";
        public const string TypeName = "Type";
        public const string OccurTimeName = "OccurTime";

        [Id(Name = "SysId", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }

        [Property(Name = "OccurTime", Type = PropertyType.日期)]
        public virtual DateTime OccurTime { get; set; }

        [Property(Name = ContentName, Type = PropertyType.字符, MaxLength = 100)]
        public virtual string Content { get; set; }

        [Property(Name = TypeName, Type = PropertyType.字符)]
        public virtual string Type { get; set; }

        public virtual string GetTimePropertyName
        {
            get
            {
                return "OccurTime";
            }
            set { }
        }

        public virtual string GetTableName
        {
            get
            {
                return "operation_history";
            }
            set { }
        }
    }
}
