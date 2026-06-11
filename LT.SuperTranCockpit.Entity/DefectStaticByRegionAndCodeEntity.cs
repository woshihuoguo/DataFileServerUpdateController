using LT.Common.IO.Storage.Model;
using System;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "defect_static_by_region_and_code")]
    public class DefectStaticByRegionAndCodeEntity : IEntity
    {
        [Id(Name = "SysId", Type = PropertyType.字符, MaxLength = 200)]
        public virtual string SysId { get; set; }

        [Property(Name = "DateTime", Type = PropertyType.日期)]
        public virtual DateTime DateTime { get; set; }

        [Property(Name = "Shift", Type = PropertyType.字符)]
        public virtual string Shift { get; set; }

        [Property(Name = "Region", Type = PropertyType.字符)]
        public virtual string Region { get; set; }

        [Property(Name = "Code", Type = PropertyType.字符)]
        public virtual string Code { get; set; }

        [Property(Name = "Name", Type = PropertyType.字符)]
        public virtual string Name { get; set; }

        [Property(Name = "Count", Type = PropertyType.长整数)]
        public virtual long Count { get; set; }

        public virtual void GenerateSysId()
        {
            SysId = DateTime.ToString("yyyyMMdd") + "_" + Shift + "_" + Region + "_" + Code;
        }
    }
}
