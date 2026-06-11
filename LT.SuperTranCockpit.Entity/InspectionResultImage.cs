using System;
using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "inspectionresult_image")]
    public class InspectionResultImage : IEntity, IAutoClear
    {
        public const string SysIDName = "SysID";
        [Id(Name = SysIDName, Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysID { get; set; }

        /// <summary>
        /// ICW发来的uniqueID
        /// </summary>
        [Property(Name = "UniqueID", Type = PropertyType.字符, MaxLength = 38)]
        public virtual string UniqueID { get; set; }

        [Property(Name = "TestMode", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string TestMode { get; set; }
        [Property(Name = "CellID", Type = PropertyType.字符, MaxLength = 200)]
        public virtual string CellID { get; set; }

        [Property(Name = "ImageIndex", Type = PropertyType.整数, MaxLength = 11)]
        public virtual int ImageIndex { get; set; }
        [Property(Name = "pc", Type = PropertyType.整数, MaxLength = 11)]
        public virtual int pc { get; set; }
        [Property(Name = "Starttime", Type = PropertyType.日期)]
        public virtual DateTime? Starttime { get; set; }

        [Property(Name = "Endtime", Type = PropertyType.日期)]
        public virtual DateTime? Endtime { get; set; }
        [Property(Name = "pattern_index", Type = PropertyType.整数, MaxLength = 11)]
        public virtual int pattern_index { get; set; }
        [Property(Name = "pattern", Type = PropertyType.字符, MaxLength = 20)]
        public virtual string pattern { get; set; }
        [Property(Name = "type", Type = PropertyType.字符, MaxLength = 10)]
        public virtual string type { get; set; }
        [Property(Name = "channel", Type = PropertyType.单精度浮点数)]
        public virtual float channel { get; set; }
        [Property(Name = "width", Type = PropertyType.整数, MaxLength = 11)]
        public virtual int width { get; set; }
        [Property(Name = "height", Type = PropertyType.整数, MaxLength = 11)]
        public virtual int height { get; set; }
        [Property(Name = "zoom", Type = PropertyType.单精度浮点数)]
        public virtual float zoom { get; set; }
        [Property(Name = "zoom_width", Type = PropertyType.单精度浮点数)]
        public virtual float zoom_width { get; set; }
        [Property(Name = "zoom_height", Type = PropertyType.单精度浮点数)]
        public virtual float zoom_height { get; set; }
        [Property(Name = "step", Type = PropertyType.单精度浮点数)]
        public virtual float step { get; set; }
        [Property(Name = "grabseq", Type = PropertyType.单精度浮点数)]
        public virtual float grabseq { get; set; }
        [Property(Name = "camera", Type = PropertyType.单精度浮点数)]
        public virtual float camera { get; set; }
        [Property(Name = "data", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string data { get; set; }
        [Property(Name = "img_raw", Type = PropertyType.字符, MaxLength = 300)]
        public virtual string img_raw { get; set; }
        [Property(Name = "img_zoom", Type = PropertyType.字符, MaxLength = 300)]
        public virtual string img_zoom { get; set; }
        [Property(Name = "ext", Type = PropertyType.字符, MaxLength = 1000)]
        public virtual string ext { get; set; }

        public virtual string GetTimePropertyName
        {
            get
            {
                return "StartTime";
            }
            set { }
        }

        public virtual string GetTableName
        {
            get
            {
                return "inspectionresult_image";
            }
            set { }
        }
    }
}
