using System;
using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "ivs_lcd_surfaceresult")]
    public class ICW_LCD_SurfaceResultEntity : IEntity,IAutoClear
    {
        public const string SysIDName = "SysID";
        public const string GUIDName = "GUID";
        public const string BarcodeName = "Barcode";
        public const string IDName = "ID";
        public const string DeviceNameName = "DeviceName";
        public const string ProductNameName = "ProductName";
        public const string StartTimeName = "StartTime";
        public const string StopTimeName = "StopTime";
        public const string ResultName = "Result";
        public const string ReClassifiedName = "ReClassified";
        public const string Code_AOIName = "Code_AOI";
        public const string Grade_AOIName = "Grade_AOI";
        public const string Level_AOIName = "Level_AOI";
        public const string strReClaCusComCodeName = "strReClaCusComCode";
        public const string strReClaCusBarFilResName = "strReClaCusBarFilRes";
        public const string DefectCountName = "DefectCount";
        public const string StatusName = "Status";
        public const string PanelPhysicalXLenName = "PanelPhysicalXLen";
        public const string PanelPhysicalYLenName = "PanelPhysicalYLen";
        public const string PanelPhysicalZLenLenName = "PanelPhysicalZLen";
        public const string SideName = "Side";
        public const string ReviewResult_WorkerName = "ReviewResult_Worker";
        public const string ReviewFixID_WorkerName = "ReviewFixID_Worker";
        public const string ShiftIDName = "ShiftID";
        public const string OperatorIDName = "OperatorID";
        public const string StartTime_ManualReviewName = "StartTime_ManualReview";
        public const string StopTime_ManualReviewName = "StopTime_ManualReview";
        public const string Station_ManualReviewName = "Station_ManualReview";
        public const string Operator_ManualReviewName = "Operator_ManualReview";
        public const string Code_ManualReviewName = "Code_ManualReview";
        public const string Grade_ManualReviewName = "Grade_ManualReview";
        public const string Level_ManualReviewName = "Level_ManualReview";
        public const string XMLInfoName = "XMLInfo";
        public const string UniqueIDName = "UniqueID";
        //====================================================================================================
        //以上是类型常量字段
        //----------------------------------------------------------------------------------------------------
        //以下是类型属性
        //====================================================================================================
        [Id(Name = SysIDName, Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysID { get; set; }

        [Property(Name = GUIDName, Type = PropertyType.字符, MaxLength = 38)]
        public virtual string GUID { get; set; }

        [Property(Name = BarcodeName, Type = PropertyType.字符, MaxLength = 128)]
        public virtual string Barcode { get; set; }
        [Property(Name = "ID", Type = PropertyType.整数)]
        public virtual int ID { get; set; }
        [Property(Name = DeviceNameName, Type = PropertyType.字符, MaxLength = 32)]
        public virtual string DeviceName { get; set; }
        [Property(Name = ProductNameName, Type = PropertyType.字符, MaxLength = 32)]
        public virtual string ProductName { get; set; }
        [Property(Name = StartTimeName, Type = PropertyType.日期)]
        public virtual DateTime StartTime { get; set; }

        [Property(Name = StopTimeName, Type = PropertyType.日期)]
        public virtual DateTime StopTime { get; set; }
        [Property(Name = ResultName, Type = PropertyType.字符, MaxLength = 32)]
        public virtual string Result { get; set; }

        [Property(Name = ReClassifiedName, Type = PropertyType.布尔值)]
        public virtual bool ReClassified { get; set; }

        [Property(Name = Code_AOIName, Type = PropertyType.字符, MaxLength = 100)]
        public virtual string Code_AOI { get; set; }

        [Property(Name = Grade_AOIName, Type = PropertyType.字符, MaxLength = 100)]
        public virtual string Grade_AOI { get; set; }

        [Property(Name = Level_AOIName, Type = PropertyType.字符, MaxLength = 100)]
        public virtual string Level_AOI { get; set; }

        
        [Property(Name = "strReClaCusComCode", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string strReClaCusComCode { get; set; }

        [Property(Name = "strReClaCusBarFilRes", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string strReClaCusBarFilRes { get; set; }
        [Property(Name = "DefectCount", Type = PropertyType.整数)]
        public virtual int DefectCount { get; set; }

        [Property(Name = "Status", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string Status { get; set; }
        [Property(Name = "PanelPhysicalXLen", Type = PropertyType.单精度浮点数)]
        public virtual float PanelPhysicalXLen { get; set; }
        [Property(Name = "PanelPhysicalYLen", Type = PropertyType.单精度浮点数)]
        public virtual float PanelPhysicalYLen { get; set; }
        [Property(Name = "PanelPhysicalZLen", Type = PropertyType.单精度浮点数)]
        public virtual float PanelPhysicalZLen { get; set; }
        [Property(Name = "Side", Type = PropertyType.整数)]
        public virtual int Side { get; set; }

        [Property(Name = "ReviewResult_Worker", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string ReviewResult_Worker { get; set; }

        [Property(Name = "ReviewFixID_Worker", Type = PropertyType.字符, MaxLength = 10)]
        public virtual string ReviewFixID_Worker { get; set; }

        [Property(Name = "ShiftID", Type = PropertyType.字符, MaxLength = 15)]
        public virtual string ShiftID { get; set; }

        [Property(Name = "OperatorID", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string OperatorID { get; set; }

        [Property(Name = "StartTime_ManualReview", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string StartTime_ManualReview { get; set; }

        [Property(Name = "StopTime_ManualReview", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string StopTime_ManualReview { get; set; }

        [Property(Name = "Station_ManualReview", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Station_ManualReview { get; set; }

        [Property(Name = "Operator_ManualReview", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Operator_ManualReview { get; set; }

        [Property(Name = "Code_ManualReview", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Code_ManualReview { get; set; }

        [Property(Name = "Grade_ManualReview", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Grade_ManualReview { get; set; }

        [Property(Name = "Level_ManualReview", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Level_ManualReview { get; set; }

        [Property(Name = "XMLInfo", Type = PropertyType.字符, MaxLength = 5000)]
        public virtual string XMLInfo { get; set; }
        [Property(Name = UniqueIDName, Type = PropertyType.字符, MaxLength = 38)]
        public virtual string UniqueID { get; set; }
        [Property(Name = "FixtureCode", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string FixtureCode { get; set; }

        [Property(Name = "BindTimes", Type = PropertyType.整数)]
        public virtual int BindTimes { get; set; }

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
                return "ivs_lcd_surfaceresult";
            }
            set { }
        }
    }
}
