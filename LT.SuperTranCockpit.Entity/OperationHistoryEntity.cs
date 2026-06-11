using LT.Common.IO.Storage.Model;
using System;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "operation_history")]
    public class OperationHistoryEntity : IEntity, IAutoClear
    {
        public const string ContentName = "Content";
        public const string OperatorName = "Operator";
        public const string TypeName = "Type";
        public const string OccurTimeName = "OccurTime";

        [Id(Name = "SysId", Type = PropertyType.长整数)]
        public virtual long SysId { get; set; }

        [Property(Name = "Operator", Type = PropertyType.字符, MaxLength = 20)]
        public virtual string Operator { get; set; }

        [Property(Name = ContentName, Type = PropertyType.字符, MaxLength = 100)]
        public virtual string Content { get; set; }

        [Property(Name = TypeName, Type = PropertyType.字符, MaxLength = 20)]
        public virtual string Type { get; set; }

        [Property(Name = OccurTimeName, Type = PropertyType.日期)]
        public virtual DateTime OccurTime { get; set; }
        
        [Property(Name = "OldValue", Type = PropertyType.字符, MaxLength = 20)]
        public virtual string OldValue { get; set; }

        [Property(Name = "NewValue", Type = PropertyType.字符, MaxLength = 20)]
        public virtual string NewValue { get; set; }

        [Property(Name = "Code", Type = PropertyType.字符, MaxLength = 20)]
        public virtual string Code { get; set; }

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
