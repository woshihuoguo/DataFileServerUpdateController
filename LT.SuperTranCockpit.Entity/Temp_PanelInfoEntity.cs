using LT.Common.IO.Storage.Model;
using System;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "temp_panelinfo")]
    public class Temp_PanelInfoEntity : IEntity, IAutoClear
    {
        public const string SysIDName = "SysId";
        public const string UniqueIdName = "UniqueId";

        [Id(Name = SysIDName, Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }
        [Property(Name = UniqueIdName, Type = PropertyType.字符, MaxLength = 38)]
        public virtual string UniqueId { get; set; }

        [Property(Name = "Barcode", Type = PropertyType.字符, MaxLength = 200)]
        public virtual string Barcode { get; set; }

        [Property(Name = "ParamName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ParamName { get; set; }
        [Property(Name = "ParamValue", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string ParamValue { get; set; }

        [Property(Name = "CreationTime", Type = PropertyType.日期)]
        public virtual DateTime CreationTime { get; set; }

        public virtual string GetTimePropertyName
        {
            get
            {
                return "CreationTime";
            }
            set { }
        }

        public virtual string GetTableName
        {
            get
            {
                return "temp_panelinfo";
            }
            set { }
        }
    }
}
