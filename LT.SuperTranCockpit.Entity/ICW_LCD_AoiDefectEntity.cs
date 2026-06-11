using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "ivs_lcd_aoidefect")]
    public class ICW_LCD_AoiDefectEntity : IEntity
    {
        public const string SysIDName = "SysID";
        public const string GUID_IVS_LCD_InspectionResultName = "GUID_IVS_LCD_InspectionResult";
        public const string DefectIndexName = "DefectIndex";
        public const string TypeName = "Type";
        public const string StartTimeName = "StartTime";
        public const string StopTimeName = "StopTime";
        public const string UniqueIDName = "UniqueID";
        public const string PatternIDName = "PatternID";
        public const string PatternNameName = "PatternName";
        public const string Pos_xName = "Pos_x";
        public const string Pos_yName = "Pos_y";
        public const string Pos_widthName = "Pos_width";
        public const string Pos_heightName = "Pos_height";
        public const string TrueSizeName = "TrueSize";
        public const string TrueDiameterName = "TrueDiameter";
        public const string TrueLongSizeName = "TrueLongSize";
        public const string TrueShortSizeName = "TrueShortSize";
        public const string GrayscaleName = "Grayscale";
        public const string Grayscale_BKName = "Grayscale_BK";
        public const string GrayscaleDiffName = "GrayscaleDiff";
        public const string ReviewResult_WorkerName = "ReviewResult_Worker";
        public const string ReviewResult_MachineName = "ReviewResult_Machine";
        public const string MachineReviewDefectNameName = "MachineReviewDefectName";
        public const string TrueSize_ReviewName = "TrueSize_Review";
        public const string TrueDiameter_ReviewName = "TrueDiameter_Review";
        public const string TrueLongSize_ReviewName = "TrueLongSize_Review";
        public const string TrueShortSize_ReviewName = "TrueShortSize_Review";
        public const string Grayscale_ReviewName = "Grayscale_Review";
        public const string GrayscaleBK_ReviewName = "GrayscaleBK_Review";
        public const string GrayscaleDiff_ReviewName = "GrayscaleDiff_Review";
        public const string InspTypeName = "InspType";
        public const string XMLInfoName = "XMLInfo";
        public const string GradeName = "Grade";
        public const string TypeExName = "TypeEx";
        public const string InspCamName = "InspCam";
        public const string LayerName = "Layer";
        public const string AreaName = "Area";
        public const string RoundnessName = "Roundness";
        public const string GrayscaleMeanName = "GrayscaleMean";
        public const string GrayscaleMinName = "GrayscaleMin";
        public const string GrayscaleMaxName = "GrayscaleMax";
        public const string JNDName = "JND";
        public const string ShapeName = "Shape";
        public const string MajorAxisAngleName = "MajorAxisAngle";
        public const string ImagePathName = "ImagePath";
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
        public const string AlgNameName = "AlgName";
        public const string DefName_AOIName = "DefName_AOI";
        //====================================================================================================
        //以上是类型常量字段
        //----------------------------------------------------------------------------------------------------
        //以下是类型属性
        //====================================================================================================
        [Id(Name = SysIDName, Type = PropertyType.长整数)]
        public virtual long SysID { get; set; }

        [Property(Name = GUID_IVS_LCD_InspectionResultName, Type = PropertyType.字符, MaxLength = 38)]
        public virtual string GUID_IVS_LCD_InspectionResult { get; set; }

        [Property(Name = DefectIndexName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int DefectIndex { get; set; }

        [Property(Name = TypeName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string Type { get; set; }

        [Property(Name = StartTimeName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string StartTime { get; set; }

        [Property(Name = StopTimeName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string StopTime { get; set; }

        [Property(Name = UniqueIDName, Type = PropertyType.字符, MaxLength = 38)]
        public virtual string UniqueID { get; set; }

        [Property(Name = PatternIDName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int PatternID { get; set; }

        [Property(Name = PatternNameName, Type = PropertyType.字符, MaxLength = 100)]
        public virtual string PatternName { get; set; }

        [Property(Name = Pos_xName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int Pos_x { get; set; }

        [Property(Name = Pos_yName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int Pos_y { get; set; }

        [Property(Name = Pos_widthName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int Pos_width { get; set; }

        [Property(Name = Pos_heightName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int Pos_height { get; set; }

        [Property(Name = TrueSizeName, Type = PropertyType.单精度浮点数)]
        public virtual float TrueSize { get; set; }

        [Property(Name = TrueDiameterName, Type = PropertyType.单精度浮点数)]
        public virtual float TrueDiameter { get; set; }

        [Property(Name = TrueLongSizeName, Type = PropertyType.单精度浮点数)]
        public virtual float TrueLongSize { get; set; }

        [Property(Name = TrueShortSizeName, Type = PropertyType.单精度浮点数)]
        public virtual float TrueShortSize { get; set; }

        [Property(Name = GrayscaleName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int Grayscale { get; set; }

        [Property(Name = Grayscale_BKName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int Grayscale_BK { get; set; }

        [Property(Name = GrayscaleDiffName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int GrayscaleDiff { get; set; }

        [Property(Name = ReviewResult_WorkerName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string ReviewResult_Worker { get; set; }

        [Property(Name = ReviewResult_MachineName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string ReviewResult_Machine { get; set; }

        [Property(Name = MachineReviewDefectNameName, Type = PropertyType.字符, MaxLength = 100)]
        public virtual string MachineReviewDefectName { get; set; }

        [Property(Name = TrueSize_ReviewName, Type = PropertyType.单精度浮点数, MaxLength = 11)]
        public virtual float TrueSize_Review { get; set; }

        [Property(Name = TrueDiameter_ReviewName, Type = PropertyType.单精度浮点数, MaxLength = 11)]
        public virtual float TrueDiameter_Review { get; set; }

        [Property(Name = TrueLongSize_ReviewName, Type = PropertyType.单精度浮点数, MaxLength = 11)]
        public virtual float TrueLongSize_Review { get; set; }

        [Property(Name = TrueShortSize_ReviewName, Type = PropertyType.单精度浮点数, MaxLength = 11)]
        public virtual float TrueShortSize_Review { get; set; }

        [Property(Name = Grayscale_ReviewName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int Grayscale_Review { get; set; }

        [Property(Name = GrayscaleBK_ReviewName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int GrayscaleBK_Review { get; set; }

        [Property(Name = GrayscaleDiff_ReviewName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int GrayscaleDiff_Review { get; set; }

        [Property(Name = InspTypeName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string InspType { get; set; }

        [Property(Name = XMLInfoName, Type = PropertyType.字符, MaxLength = 5000)]
        public virtual string XMLInfo { get; set; }

        [Property(Name = GradeName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string Grade { get; set; }

        [Property(Name = TypeExName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string TypeEx { get; set; }

        [Property(Name = InspCamName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string InspCam { get; set; }

        [Property(Name = LayerName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Layer { get; set; }

        [Property(Name = AreaName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int Area { get; set; }

        [Property(Name = RoundnessName, Type = PropertyType.单精度浮点数)]
        public virtual float Roundness { get; set; }

        [Property(Name = GrayscaleMeanName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int GrayscaleMean { get; set; }

        [Property(Name = GrayscaleMinName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int GrayscaleMin { get; set; }

        [Property(Name = GrayscaleMaxName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int GrayscaleMax { get; set; }

        [Property(Name = JNDName, Type = PropertyType.单精度浮点数)]
        public virtual float JND { get; set; }

        [Property(Name = ShapeName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Shape { get; set; }

        [Property(Name = MajorAxisAngleName, Type = PropertyType.单精度浮点数)]
        public virtual float MajorAxisAngle { get; set; }

        [Property(Name = ImagePathName, Type = PropertyType.字符, MaxLength = 255)]
        public virtual string ImagePath { get; set; }

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
        [Property(Name = AlgNameName, Type = PropertyType.字符, MaxLength = 100)]
        public virtual string AlgName { get; set; }
        [Property(Name = "AlgID", Type = PropertyType.整数)]
        public virtual int AlgID { get; set; }
        [Property(Name = "OriArea", Type = PropertyType.整数)]
        public virtual int OriArea { get; set; }
        [Property(Name = "OriLongSize", Type = PropertyType.整数)]
        public virtual int OriLongSize { get; set; }
        [Property(Name = "OriShortSize", Type = PropertyType.整数)]
        public virtual int OriShortSize { get; set; }
        [Property(Name = "DefClass_AOI", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string DefClass_AOI { get; set; }
        [Property(Name = "DefName_AOI", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string DefName_AOI { get; set; }
        [Property(Name = "ReasonCode", Type = PropertyType.字符, MaxLength = 200)]
        public virtual string ReasonCode { get; set; }
        [Property(Name = "FeatureName", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string FeatureName { get; set; }
        [Property(Name = "FeatureMin", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string FeatureMin { get; set; }
        [Property(Name = "FeatureMax", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string FeatureMax { get; set; }
        [Property(Name = "FeatureUnit", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string FeatureUnit { get; set; }
        [Property(Name = "FeatureValue", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string FeatureValue { get; set; }
        [Property(Name = "DefColor", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string DefColor { get; set; }
        [Property(Name = "DefColorValue", Type = PropertyType.单精度浮点数)]
        public virtual float DefColorValue { get; set; }
        [Property(Name = "DefClass_AutoReview", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string DefClass_AutoReview { get; set; }
        [Property(Name = "DefName_AutoReview", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string DefName_AutoReview { get; set; }
    }
}
