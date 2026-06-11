using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "ivs_lcd_inspectionresult")]
    public class ICW_LCD_InspectionResultEntity : IEntity
    {
        public const string SysIDName = "SysID";
        public const string GUIDName = "GUID";
        public const string ScreenIDName = "ScreenID";
        public const string DeviceIDName = "DeviceID";
        public const string PlatformIDName = "PlatformID";
        public const string ShiftIDName = "ShiftID";
        public const string LotIDName = "LotID";
        public const string ModelNameName = "ModelName";
        public const string StartTimeName = "StartTime";
        public const string StopTimeName = "StopTime";
        public const string StatusName = "Status";
        public const string AOIResultName = "AOIResult";
        public const string ReviewResult_WorkerName = "ReviewResult_Worker";
        public const string ReviewResult_MachineName = "ReviewResult_Machine";
        public const string AllPerspectiveResultName = "AllPerspectiveResult";
        public const string MarkIDName = "MarkID";
        public const string ProcessTypeName = "ProcessType";
        public const string LineIDName = "LineID";
        public const string UniqueIDName = "UniqueID";
        public const string MainAoiFixIDName = "MainAoiFixID";
        public const string ReviewFixID_WorkerName = "ReviewFixID_Worker";
        public const string ReviewFixID_MachineName = "ReviewFixID_Machine";
        public const string AllPerspectiveFixIDName = "AllPerspectiveFixID";
        public const string LocateShiftXName = "LocateShiftX";
        public const string LocateShiftYName = "LocateShiftY";
        public const string LocateAngleName = "LocateAngle";
        public const string XMLInfoName = "XMLInfo";
        public const string RawImageXLenName = "RawImageXLen";
        public const string RawImageYLenName = "RawImageYLen";
        public const string GridImageXLenName = "GridImageXLen";
        public const string GridImageYLenName = "GridImageYLen";
        public const string PanelPhysicalXLenName = "PanelPhysicalXLen";
        public const string PanelPhysicalYLenName = "PanelPhysicalYLen";
        public const string LocalIPName = "LocalIP";
        public const string CIMModeName = "CIMMode";
        public const string RuncardIDName = "RuncardID";
        public const string CassetteIDName = "CassetteID";
        public const string SlotIDName = "SlotID";
        public const string OperatorIDName = "OperatorID";
        public const string ProductIDName = "ProductID";
        public const string L255_GrayscaleName = "L255_Grayscale";
        public const string L0_GrayscaleName = "L0_Grayscale";
        public const string DevUnitIDName = "DevUnitID";
        public const string Code_AOIName = "Code_AOI";
        public const string Grade_AOIName = "Grade_AOI";
        public const string Level_AOIName = "Level_AOI";
        public const string Code_AllViewName = "Code_AllView";
        public const string Grade_AllViewName = "Grade_AllView";
        public const string Level_AllViewName = "Level_AllView";
        public const string Code_AutoReviewName = "Code_AutoReview";
        public const string Grade_AutoReviewName = "Grade_AutoReview";
        public const string Level_AutoReviewName = "Level_AutoReview";
        public const string Code_ManualReviewName = "Code_ManualReview";
        public const string Grade_ManualReviewName = "Grade_ManualReview";
        public const string Level_ManualReviewName = "Level_ManualReview";
        public const string Code_FinalName = "Code_Final";
        public const string Grade_FinalName = "Grade_Final";
        public const string Level_FinalName = "Level_Final";
        public const string Station_AllViewName = "Station_AllView";
        public const string Station_AutoReviewName = "Station_AutoReview";
        public const string Station_ManualReviewName = "Station_ManualReview";
        public const string Operator_ManualReviewName = "Operator_ManualReview";
        public const string StartTime_AllViewName = "StartTime_AllView";
        public const string StopTime_AllViewName = "StopTime_AllView";
        public const string StartTime_AutoReviewName = "StartTime_AutoReview";
        public const string StopTime_AutoReviewName = "StopTime_AutoReview";
        public const string StartTime_ManualReviewName = "StartTime_ManualReview";
        public const string StopTime_ManualReviewName = "StopTime_ManualReview";
        //====================================================================================================
        //以上是类型常量字段
        //----------------------------------------------------------------------------------------------------
        //以下是类型属性
        //====================================================================================================
        [Id(Name = SysIDName, Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysID { get; set; }

        [Property(Name = GUIDName, Type = PropertyType.字符, MaxLength = 38)]
        public virtual string GUID { get; set; }

        [Property(Name = ScreenIDName, Type = PropertyType.字符, MaxLength = 128)]
        public virtual string ScreenID { get; set; }

        [Property(Name = DeviceIDName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string DeviceID { get; set; }

        [Property(Name = PlatformIDName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int PlatformID { get; set; }

        [Property(Name = ShiftIDName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string ShiftID { get; set; }

        [Property(Name = LotIDName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string LotID { get; set; }

        [Property(Name = ModelNameName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ModelName { get; set; }

        [Property(Name = StartTimeName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string StartTime { get; set; }

        [Property(Name = StopTimeName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string StopTime { get; set; }

        [Property(Name = StatusName, Type = PropertyType.字符, MaxLength = 30)]
        public virtual string Status { get; set; }

        [Property(Name = AOIResultName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string AOIResult { get; set; }

        [Property(Name = ReviewResult_WorkerName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string ReviewResult_Worker { get; set; }

        [Property(Name = ReviewResult_MachineName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string ReviewResult_Machine { get; set; }

        [Property(Name = AllPerspectiveResultName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string AllPerspectiveResult { get; set; }

        [Property(Name = MarkIDName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string MarkID { get; set; }

        [Property(Name = ProcessTypeName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string ProcessType { get; set; }

        [Property(Name = LineIDName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string LineID { get; set; }

        [Property(Name = UniqueIDName, Type = PropertyType.字符, MaxLength = 38)]
        public virtual string UniqueID { get; set; }

        [Property(Name = MainAoiFixIDName, Type = PropertyType.字符, MaxLength = 10)]
        public virtual string MainAoiFixID { get; set; }

        [Property(Name = ReviewFixID_WorkerName, Type = PropertyType.字符, MaxLength = 10)]
        public virtual string ReviewFixID_Worker { get; set; }

        [Property(Name = ReviewFixID_MachineName, Type = PropertyType.字符, MaxLength = 10)]
        public virtual string ReviewFixID_Machine { get; set; }

        [Property(Name = AllPerspectiveFixIDName, Type = PropertyType.字符, MaxLength = 10)]
        public virtual string AllPerspectiveFixID { get; set; }

        [Property(Name = LocateShiftXName, Type = PropertyType.单精度浮点数)]
        public virtual float LocateShiftX { get; set; }

        [Property(Name = LocateShiftYName, Type = PropertyType.单精度浮点数)]
        public virtual float LocateShiftY { get; set; }

        [Property(Name = LocateAngleName, Type = PropertyType.单精度浮点数)]
        public virtual float LocateAngle { get; set; }

        [Property(Name = XMLInfoName, Type = PropertyType.字符, MaxLength = 5000)]
        public virtual string XMLInfo { get; set; }

        [Property(Name = RawImageXLenName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int RawImageXLen { get; set; }

        [Property(Name = RawImageYLenName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int RawImageYLen { get; set; }

        [Property(Name = GridImageXLenName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int GridImageXLen { get; set; }

        [Property(Name = GridImageYLenName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int GridImageYLen { get; set; }

        [Property(Name = PanelPhysicalXLenName, Type = PropertyType.单精度浮点数)]
        public virtual float PanelPhysicalXLen { get; set; }

        [Property(Name = PanelPhysicalYLenName, Type = PropertyType.单精度浮点数)]
        public virtual float PanelPhysicalYLen { get; set; }

        [Property(Name = LocalIPName, Type = PropertyType.字符, MaxLength = 100)]
        public virtual string LocalIP { get; set; }

        [Property(Name = CIMModeName, Type = PropertyType.字符, MaxLength = 10)]
        public virtual string CIMMode { get; set; }

        [Property(Name = RuncardIDName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string RuncardID { get; set; }

        [Property(Name = CassetteIDName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string CassetteID { get; set; }

        [Property(Name = SlotIDName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string SlotID { get; set; }

        [Property(Name = OperatorIDName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string OperatorID { get; set; }

        [Property(Name = ProductIDName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ProductID { get; set; }

        [Property(Name = L255_GrayscaleName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int L255_Grayscale { get; set; }

        [Property(Name = L0_GrayscaleName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int L0_Grayscale { get; set; }

        [Property(Name = DevUnitIDName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string DevUnitID { get; set; }

        [Property(Name = Code_AOIName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Code_AOI { get; set; }

        [Property(Name = Grade_AOIName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Grade_AOI { get; set; }

        [Property(Name = Level_AOIName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Level_AOI { get; set; }

        [Property(Name = Code_AllViewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Code_AllView { get; set; }

        [Property(Name = Grade_AllViewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Grade_AllView { get; set; }

        [Property(Name = Level_AllViewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Level_AllView { get; set; }

        [Property(Name = Code_AutoReviewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Code_AutoReview { get; set; }

        [Property(Name = Grade_AutoReviewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Grade_AutoReview { get; set; }

        [Property(Name = Level_AutoReviewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Level_AutoReview { get; set; }

        [Property(Name = Code_ManualReviewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Code_ManualReview { get; set; }

        [Property(Name = Grade_ManualReviewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Grade_ManualReview { get; set; }

        [Property(Name = Level_ManualReviewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Level_ManualReview { get; set; }

        [Property(Name = Code_FinalName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Code_Final { get; set; }

        [Property(Name = Grade_FinalName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Grade_Final { get; set; }

        [Property(Name = Level_FinalName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Level_Final { get; set; }

        [Property(Name = Station_AllViewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Station_AllView { get; set; }

        [Property(Name = Station_AutoReviewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Station_AutoReview { get; set; }

        [Property(Name = Station_ManualReviewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Station_ManualReview { get; set; }

        [Property(Name = Operator_ManualReviewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Operator_ManualReview { get; set; }

        [Property(Name = StartTime_AllViewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string StartTime_AllView { get; set; }

        [Property(Name = StopTime_AllViewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string StopTime_AllView { get; set; }

        [Property(Name = StartTime_AutoReviewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string StartTime_AutoReview { get; set; }

        [Property(Name = StopTime_AutoReviewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string StopTime_AutoReview { get; set; }

        [Property(Name = StartTime_ManualReviewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string StartTime_ManualReview { get; set; }

        [Property(Name = StopTime_ManualReviewName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string StopTime_ManualReview { get; set; }
        [Property(Name = "ElecRes", Type = PropertyType.字符, MaxLength = 30)]
        public virtual string ElecRes { get; set; }
        
    }
}
