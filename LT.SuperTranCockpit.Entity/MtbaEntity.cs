using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "mtba")]
    public class MtbaEntity : IEntity
    {

        [Id(Name = "SysId", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }

        [Property(Name = "PlcIndex", Type = PropertyType.整数)]
        public virtual int PlcIndex { get; set; }

        [Property(Name = "StartTime", Type = PropertyType.日期)]
        public virtual DateTime? StartTime { get; set; }

        [Property(Name = "StopTime", Type = PropertyType.日期)]
        public virtual DateTime? StopTime { get; set; }

        [Property(Name = "Duration", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Duration { get; set; }
    }
}
