using System;
using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "northbound_interface_history")]
    public class NorthboundInterfaceHistoryEntity : IEntity, IAutoClear
    {
        [Id(Name = "SysID", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysID { get; set; }

        [Property(Name = "Barcode", Type = PropertyType.字符, MaxLength = 200)]
        public virtual string Barcode { get; set; }

        [Property(Name = "Result", Type = PropertyType.字符)]
        public virtual string Result { get; set; }
        
        [Property(Name = "Message", Type = PropertyType.字符, MaxLength = 500)]
        public virtual string Message { get; set; }

        [Property(Name = "Type", Type = PropertyType.字符)]
        public virtual string Type { get; set; }

        [Property(Name = "OccurTime", Type = PropertyType.日期)]
        public virtual DateTime OccurTime { get; set; }

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
                return "northbound_interface_history";
            }
            set { }
        }
    }
}
