using LT.Common.IO.Storage.Model;
using System;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "temp_process_result_sub")]
    public class Temp_ProcessResultSubEntity : IEntity, IAutoClear
    {
        [Id(Name = "SysId", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }

        [Property(Name = "UniqueId", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string UniqueId { get; set; }

        [Property(Name = "Barcode", Type = PropertyType.字符, MaxLength = 200)]
        public virtual string Barcode { get; set; }

        [Property(Name = "ScanBarcode", Type = PropertyType.字符, MaxLength = 200)]
        public virtual string ScanBarcode { get; set; }
        [Property(Name = "TotalProcessName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string TotalProcessName { get; set; }

        [Property(Name = "ProcessName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ProcessName { get; set; }
        [Property(Name = "ProcessType", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ProcessType { get; set; }

        [Property(Name = "Result", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Result { get; set; }

        [Property(Name = "Grade", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Grade { get; set; }

        [Property(Name = "Code", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Code { get; set; }

        [Property(Name = "StartTime", Type = PropertyType.日期)]
        public virtual DateTime? StartTime { get; set; }

        [Property(Name = "StopTime", Type = PropertyType.日期)]
        public virtual DateTime? StopTime { get; set; }

        [Property(Name = "ShiftDate", Type = PropertyType.日期)]
        public virtual DateTime? ShiftDate { get; set; }

        [Property(Name = "ShiftDetail", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ShiftDetail { get; set; }

        [Property(Name = "ReviewResult", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ReviewResult { get; set; }

        [Property(Name = "ReviewGrade", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ReviewGrade { get; set; }

        [Property(Name = "ReviewCode", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ReviewCode { get; set; }

        [Property(Name = "SideId", Type = PropertyType.整数)]
        public virtual int SideId { get; set; }

        [Property(Name = "SideName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string SideName { get; set; }

        [Property(Name = "SectionId", Type = PropertyType.整数)]
        public virtual int SectionId { get; set; }

        [Property(Name = "SectionName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string SectionName { get; set; }

        [Property(Name = "JigId", Type = PropertyType.整数)]
        public virtual int JigId { get; set; }

        [Property(Name = "JigName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string JigName { get; set; }

        [Property(Name = "JigChannelId", Type = PropertyType.整数)]
        public virtual int JigChannelId { get; set; }

        [Property(Name = "JigChannelName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string JigChannelName { get; set; }

        [Property(Name = "ProcessPositionId", Type = PropertyType.整数)]
        public virtual int ProcessPositionId { get; set; }

        [Property(Name = "ProcessPositionName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ProcessPositionName { get; set; }

        [Property(Name = "ProcessChannelId", Type = PropertyType.整数)]
        public virtual int ProcessChannelId { get; set; }

        [Property(Name = "ProcessChannelName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ProcessChannelName { get; set; }

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
                return "temp_process_result_sub";
            }
            set { }
        }
    }
}
