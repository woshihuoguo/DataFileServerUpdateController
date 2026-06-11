using System;
using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "ivs_lcd_finaljudge")]
    public class ICW_LCD_FinalJudgeEntity : IEntity
    {
        public const string SysIDName = "SysID";
        public const string ScreenIDName = "BarCode";
        public const string ScanBarCodeName = "ScanBarCode";
        public const string FinalJudgeName = "FinalJudge";
        public const string StartTimeName = "StartTime";
        public const string StopTimeName = "StopTime";
        public const string UniqueIDName = "UniqueID";
        public const string AoiResultName = "AoiResult";
        public const string ColorResultName = "ColorResult";
        public const string OpenResultName = "OpenResult";
        public const string EdidResultName = "EdidResult";
        public const string VcomResultName = "VcomResult";
        public const string VcomValueName = "VcomValue";
        public const string ReAoiResultName = "ReAoiResult";
        public const string SurfaceResultName = "SurfaceResult";
        public const string GradeName = "Grade";
        public const string CodeName = "Code";
        public const string HwMuraResultName = "HwMuraResult";

        //====================================================================================================
        //以上是类型常量字段
        //----------------------------------------------------------------------------------------------------
        //以下是类型属性
        //====================================================================================================
        [Id(Name = SysIDName, Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysID { get; set; }

        [Property(Name = ScreenIDName, Type = PropertyType.字符, MaxLength = 128)]
        public virtual string BarCode { get; set; }
        [Property(Name = ScanBarCodeName, Type = PropertyType.字符, MaxLength = 128)]
        public virtual string ScanBarCode { get; set; }

        [Property(Name = FinalJudgeName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string FinalJudge { get; set; }

        [Property(Name = StartTimeName, Type = PropertyType.日期)]
        public virtual DateTime StartTime { get; set; }

        [Property(Name = StopTimeName, Type = PropertyType.日期)]
        public virtual DateTime StopTime { get; set; }

        [Property(Name = UniqueIDName, Type = PropertyType.字符, MaxLength = 38)]
        public virtual string UniqueID { get; set; }

        [Property(Name = AoiResultName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string AoiResult { get; set; }

        [Property(Name = EdidResultName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string EdidResult { get; set; }

        [Property(Name = VcomResultName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string VcomResult { get; set; }

        [Property(Name = VcomValueName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string VcomValue { get; set; }

        [Property(Name = OpenResultName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string OpenResult { get; set; }

        [Property(Name = ColorResultName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string ColorResult { get; set; }

        [Property(Name = ReAoiResultName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string ReAoiResult { get; set; }

        [Property(Name = SurfaceResultName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string SurfaceResult { get; set; }

        [Property(Name = GradeName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Grade { get; set; }

        [Property(Name = CodeName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Code { get; set; }

        [Property(Name = HwMuraResultName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string HwMuraResult { get; set; }

        [Property(Name = "AoiStatus", Type = PropertyType.字符, MaxLength = 30)]
        public virtual string AoiStatus { get; set; }

        [Property(Name = "Code_AutoReview", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Code_AutoReview { get; set; }

        [Property(Name = "Code_Aoi", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Code_Aoi { get; set; }

        [Property(Name = "Grade_Mes", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Grade_Mes { get; set; }
    }
}
