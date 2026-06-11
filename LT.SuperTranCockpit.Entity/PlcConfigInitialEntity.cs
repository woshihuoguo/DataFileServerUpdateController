using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "plc_config_initial")]
    public class PlcConfigInitialEntity : IEntity
    {
        [CompositeId(Name = "Id")]
        public virtual PlcConfigInitialEntityId Id { get; set; }
        
        [Property(Name = "RecordTime", Type = PropertyType.日期)]
        public virtual DateTime RecordTime { get; set; }
        
        [Property(Name = "Value", Type = PropertyType.整数)]
        public virtual int Value { get; set; }

        [Property(Name = "Rate", Type = PropertyType.单精度浮点数)]
        public virtual float Rate { get; set; }

        [Property(Name = "Unit", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Unit { get; set; }
    }
}
