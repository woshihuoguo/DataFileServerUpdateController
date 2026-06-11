using LT.Common.IO.Storage.Model;
using System;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "capacity_yield_by_hour_and_position")]
    public class CapacityYieldByHourAndPositionEntity : IEntity
    {
        [Id(Name = "SysId", Type = PropertyType.字符, MaxLength = 200)]
        public virtual string SysId { get; set; }

        [Property(Name = "DateTime", Type = PropertyType.日期)]
        public virtual DateTime DateTime { get; set; }

        [Property(Name = "Shift", Type = PropertyType.字符)]
        public virtual string Shift { get; set; }

        [Property(Name = "Name", Type = PropertyType.字符)]
        public virtual string Name { get; set; }

        [Property(Name = "Position", Type = PropertyType.字符)]
        public virtual string Position { get; set; }

        [Property(Name = "StartTime", Type = PropertyType.日期)]
        public virtual DateTime StartTime { get; set; }

        [Property(Name = "StopTime", Type = PropertyType.日期)]
        public virtual DateTime StopTime { get; set; }

        [Property(Name = "OkCount", Type = PropertyType.长整数)]
        public virtual long OkCount { get; set; }

        [Property(Name = "NgCount", Type = PropertyType.长整数)]
        public virtual long NgCount { get; set; }

        [Property(Name = "ShieldCount", Type = PropertyType.长整数)]
        public virtual long ShieldCount { get; set; }

        [Property(Name = "BypassCount", Type = PropertyType.长整数)]
        public virtual long BypassCount { get; set; }

        [Property(Name = "AbnormalCount", Type = PropertyType.长整数)]
        public virtual long AbnormalCount { get; set; }

        [Property(Name = "TossCount", Type = PropertyType.长整数)]
        public virtual long TossCount { get; set; }

        [Property(Name = "Yield", Type = PropertyType.单精度浮点数)]
        public virtual float Yield { get; set; }

        public virtual void GenerateSysId()
        {
            SysId = DateTime.ToString("yyyyMMdd") + "_" + Shift + "_" + StartTime.ToString("HHmm")
                + "_" + Position + "_" + Name;
        }
    }
}
