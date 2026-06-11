using LT.Common.IO.Storage.Model;
using System;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "production_history")]
    public class ProductionHistoryEntity : IEntity,IAutoClear
    {
        public const string DescriptionName = "Description";
        public const string TypeName = "Type";
        public const string OccurTimeName = "OccurTime";

        [Id(Name = "SysId", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }

        [Property(Name = "Code", Type = PropertyType.字符, MaxLength = 20)]
        public virtual string Code { get; set; }

        [Property(Name = DescriptionName, Type = PropertyType.字符, MaxLength = 100)]
        public virtual string Description { get; set; }

        [Property(Name = TypeName, Type = PropertyType.字符, MaxLength = 20)]
        public virtual string Type { get; set; }

        [Property(Name = OccurTimeName, Type = PropertyType.日期)]
        public virtual DateTime OccurTime { get; set; }

        [Property(Name = "EquipName", Type = PropertyType.字符, MaxLength = 20)]
        public virtual string EquipName { get; set; }

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
                return "production_history";
            }
            set { }
        }
    }
}
