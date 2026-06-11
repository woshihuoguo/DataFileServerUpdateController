using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "ivs_lcd_color_detail_result")]
    public  class ICW_LCD_ColorInspDetailResultEntity : IEntity
    {
        public const string SysIDName = "Sysid";
        public const string MarkidName = "Markid";
        public const string StartTimeName = "StartTime";
        public const string StopTimeName = "StopTime";
        public const string BarCodeName = "BarCode";
        public const string UniqueidName = "Uniqueid";
        public const string INSPItemNameName = "INSPItemName";
        public const string ValueName = "Value";
        public const string ResultName = "Result";
        public const string PatternNameName = "PatternName";
        [Id(Name = SysIDName, Type = PropertyType.长整数)]
        public virtual long Sysid { get; set; }

        [Property(Name = MarkidName, Type = PropertyType.字符, MaxLength = 10)]
        public virtual string Markid { get; set; }
        [Property(Name = StartTimeName, Type = PropertyType.日期)]
        public virtual DateTime StartTime { get; set; }
        [Property(Name = StopTimeName, Type = PropertyType.日期)]
        public virtual DateTime StopTime { get; set; }
        [Property(Name = BarCodeName, Type = PropertyType.字符, MaxLength = 128)]
        public virtual string BarCode { get; set; }
        [Property(Name = UniqueidName, Type = PropertyType.字符, MaxLength = 38)]
        public virtual string Uniqueid { get; set; }
        [Property(Name = INSPItemNameName, Type = PropertyType.字符, MaxLength = 20)]
        public virtual string INSPItemName { get; set; }
        [Property(Name = ValueName, Type = PropertyType.字符, MaxLength = 20)]
        public virtual string Value { get; set; }
        [Property(Name = ResultName, Type = PropertyType.字符, MaxLength = 10)]
        public virtual string Result { get; set; }
        [Property(Name = PatternNameName, Type = PropertyType.字符, MaxLength = 45)]
        public virtual string PatternName { get; set; }
    }
}
