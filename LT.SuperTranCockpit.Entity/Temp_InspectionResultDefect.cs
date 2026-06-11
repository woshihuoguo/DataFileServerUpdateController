using System;
using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "temp_inspectionresult_defect")]
    public class Temp_InspectionResultDefect : IEntity, IAutoClear
    {
        public const string SysIDName = "SysID";
        [Id(Name = SysIDName, Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysID { get; set; }
        /// <summary>
        /// <summary>
        /// <summary>
        /// ICW发来的uniqueID
        /// </summary>
        [Property(Name = "UniqueID", Type = PropertyType.字符, MaxLength = 128)]
        public virtual string UniqueID { get; set; }

        [Property(Name = "TestMode", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string TestMode { get; set; }

        [Property(Name = "ImageIndex", Type = PropertyType.整数, MaxLength = 11)]
        public virtual int ImageIndex { get; set; }

        [Property(Name = "DefectIndex", Type = PropertyType.整数, MaxLength = 11)]
        public virtual int DefectIndex { get; set; }

        [Property(Name = "Type", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string Type { get; set; }
        [Property(Name = "Name", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string Name { get; set; }
        [Property(Name = "CellID", Type = PropertyType.字符, MaxLength = 200)]
        public virtual string CellID { get; set; }
        [Property(Name = "Left", Type = PropertyType.单精度浮点数)]
        public virtual float Left { get; set; }
        [Property(Name = "Top", Type = PropertyType.单精度浮点数)]
        public virtual float Top { get; set; }
        [Property(Name = "Right", Type = PropertyType.单精度浮点数)]
        public virtual float Right { get; set; }
        [Property(Name = "Bottom", Type = PropertyType.单精度浮点数)]
        public virtual float Bottom { get; set; }
        [Property(Name = "Description", Type = PropertyType.字符, MaxLength = 500)]
        public virtual string Description { get; set; }
        [Property(Name = "Location", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Location { get; set; }
        [Property(Name = "MM_x_center", Type = PropertyType.单精度浮点数)]
        public virtual float MM_x_center { get; set; }
        [Property(Name = "MM_y_center", Type = PropertyType.单精度浮点数)]
        public virtual float MM_y_center { get; set; }
        [Property(Name = "MM_z_center", Type = PropertyType.单精度浮点数)]
        public virtual float MM_z_center { get; set; }
        [Property(Name = "MM_x_size", Type = PropertyType.单精度浮点数)]
        public virtual float MM_x_size { get; set; }
        [Property(Name = "MM_y_size", Type = PropertyType.单精度浮点数)]
        public virtual float MM_y_size { get; set; }
        [Property(Name = "MM_z_size", Type = PropertyType.单精度浮点数)]
        public virtual float MM_z_size { get; set; }
        [Property(Name = "Code", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Code { get; set; }
        [Property(Name = "Grade", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Grade { get; set; }
        [Property(Name = "Img_def", Type = PropertyType.字符, MaxLength = 300)]
        public virtual string Img_def { get; set; }
        [Property(Name = "Gray", Type = PropertyType.单精度浮点数)]
        public virtual float Gray { get; set; }
        [Property(Name = "Area", Type = PropertyType.单精度浮点数)]
        public virtual float Area { get; set; }
        [Property(Name = "Len_major", Type = PropertyType.单精度浮点数)]
        public virtual float Len_major { get; set; }
        [Property(Name = "Len_minor", Type = PropertyType.单精度浮点数)]
        public virtual float Len_minor { get; set; }
        [Property(Name = "Ext", Type = PropertyType.字符, MaxLength = 1000)]
        public virtual string Ext { get; set; }
        [Property(Name = "StartTime", Type = PropertyType.日期)]
        public virtual DateTime? StartTime { get; set; }

        [Property(Name = "StopTime", Type = PropertyType.日期)]
        public virtual DateTime? StopTime { get; set; }
        [Property(Name = "PatternName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string PatternName { get; set; }
        [Property(Name = "location_real_name", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string location_real_name { get; set; }
        [Property(Name = "location_name", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string location_name { get; set; }

        [Property(Name = "target_name", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string target_name { get; set; }
        [Property(Name = "pix_target_x", Type = PropertyType.整数)]
        public virtual int pix_target_x { get; set; }
        [Property(Name = "pix_target_y", Type = PropertyType.整数)]
        public virtual int pix_target_y { get; set; }
        [Property(Name = "pix_target_w", Type = PropertyType.整数)]
        public virtual int pix_target_w { get; set; }
        [Property(Name = "pix_target_h", Type = PropertyType.整数)]
        public virtual int pix_target_h { get; set; }
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
                return "temp_inspectionresult_defect";
            }
            set { }
        }
    }
}
