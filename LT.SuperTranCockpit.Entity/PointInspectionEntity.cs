using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "pointinspection")]
    public class PointInspectionEntity : IEntity, IAutoClear
    {
        public const string SysIDName = "SysId";
        public const string UniqueIdName = "UniqueId";

        [Id(Name = SysIDName, Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }
        [Property(Name = UniqueIdName, Type = PropertyType.字符, MaxLength = 38)]
        public virtual string UniqueId { get; set; }
        [Property(Name = "CellID", Type = PropertyType.字符, MaxLength = 200)]
        public virtual string CellID { get; set; }
        [Property(Name = "Image_Index", Type = PropertyType.整数, MaxLength = 11)]
        public virtual int Image_Index { get; set; }
        [Property(Name = "Left", Type = PropertyType.整数, MaxLength = 11)]
        public virtual float Left { get; set; }
        [Property(Name = "Top", Type = PropertyType.整数, MaxLength = 11)]
        public virtual float Top { get; set; }
        [Property(Name = "Right", Type = PropertyType.整数, MaxLength = 11)]
        public virtual float Right { get; set; }
        [Property(Name = "Bottom", Type = PropertyType.整数, MaxLength = 11)]
        public virtual float Bottom { get; set; }
        [Property(Name = "DefectFlag", Type = PropertyType.整数, MaxLength = 11)]
        public virtual float DefectFlag { get; set; }
        [Property(Name = "MeasureObject", Type = PropertyType.字符, MaxLength = 150)]
        public virtual string MeasureObject { get; set; }
        [Property(Name = "StdValueHigh", Type = PropertyType.单精度浮点数)]
        public virtual float StdValueHigh { get; set; }
        [Property(Name = "StdValueLow", Type = PropertyType.单精度浮点数)]
        public virtual float StdValueLow { get; set; }
        [Property(Name = "RealValue", Type = PropertyType.单精度浮点数)]
        public virtual float RealValue { get; set; }
        [Property(Name = "CalibrateImagePath", Type = PropertyType.字符, MaxLength = 300)]
        public virtual string CalibrateImagePath { get; set; }
        [Property(Name = "StaUniformOffset", Type = PropertyType.单精度浮点数)]
        public virtual float StaUniformOffset { get; set; }
        [Property(Name = "StartTime", Type = PropertyType.日期)]
        public virtual DateTime? StartTime { get; set; }

        [Property(Name = "StopTime", Type = PropertyType.日期)]
        public virtual DateTime? StopTime { get; set; }
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
                return "pointinspection";
            }
            set { }
        }

    }
}
