using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "config_change_history")]
    public class ConfigChangeHistoryEntity : IEntity
    {
        [Id(Name = "SysId", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }

        [Property(Name = "PlcIndex", Type = PropertyType.整数)]
        public virtual int PlcIndex { get; set; }


        [Property(Name = "RecordTime", Type = PropertyType.日期)]
        public virtual DateTime RecordTime { get; set; }

        [Property(Name = "ParamName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ParamName { get; set; }

        [Property(Name = "OldValue", Type = PropertyType.整数)]
        public virtual int OldValue { get; set; }

        [Property(Name = "NewValue", Type = PropertyType.整数)]
        public virtual int NewValue { get; set; }

        [Property(Name = "Rate", Type = PropertyType.单精度浮点数)]
        public virtual float Rate { get; set; }

        [Property(Name = "Unit", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Unit { get; set; }

        [Property(Name = "Description", Type = PropertyType.字符, MaxLength = 200)]
        public virtual string Description { get; set; }
    }
}
