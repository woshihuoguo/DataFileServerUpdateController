using LT.Common.IO.Storage.Model;
using System;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "ivs_lcd_surfacedefect")]
    public class ICW_LCD_SurfaceDefectEntity : IEntity, IAutoClear
    {
        public const string SysIDName = "SysID";
        public const string GUIDName = "GUID";
        public const string BarcodeName = "Barcode";
        public const string TypeName = "Type";
        public const string StartTimeName = "StartTime";
        public const string StopTimeName = "StopTime";
        public const string Code_AOIName = "Code_AOI";
        public const string Grade_AOIName = "Grade_AOI";
        public const string Level_AOIName = "Level_AOI";
        public const string Pos_xName = "Pos_x";
        public const string Pos_yName = "Pos_y";
        public const string Pos_zName = "Pos_z";
        public const string Pos_widthName = "Pos_width";
        public const string Pos_heightName = "Pos_height";
        public const string PatternNameName = "PatternName";
        public const string PatternIDName = "PatternID";
        public const string UniqueIDName = "UniqueID";
        public const string AreaName = "Area";

        //====================================================================================================
        //以上是类型常量字段
        //----------------------------------------------------------------------------------------------------
        //以下是类型属性
        //====================================================================================================
        [Id(Name = SysIDName, Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysID { get; set; }

        [Property(Name = GUIDName, Type = PropertyType.字符, MaxLength = 128)]
        public virtual string GUID { get; set; }

        [Property(Name = BarcodeName, Type = PropertyType.字符, MaxLength = 128)]
        public virtual string Barcode { get; set; }

        [Property(Name = PatternIDName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int PatternID { get; set; }

        [Property(Name = "PatternName", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string PatternName { get; set; }
        [Property(Name = TypeName, Type = PropertyType.字符, MaxLength = 32)]
        public virtual string Type { get; set; }
        [Property(Name = "CusDefCode", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string CusDefCode { get; set; }

        [Property(Name = "CusDefName", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string CusDefName { get; set; }
        [Property(Name = Code_AOIName, Type = PropertyType.字符, MaxLength = 100)]
        public virtual string Code_AOI { get; set; }

        [Property(Name = Grade_AOIName, Type = PropertyType.字符, MaxLength = 100)]
        public virtual string Grade_AOI { get; set; }

        [Property(Name = Level_AOIName, Type = PropertyType.字符, MaxLength = 100)]
        public virtual string Level_AOI { get; set; }
        [Property(Name = "ReClaResultName", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string ReClaResultName { get; set; }
        [Property(Name = "DefIndex", Type = PropertyType.整数, MaxLength = 11)]
        public virtual int DefIndex { get; set; }

        [Property(Name = Pos_xName, Type = PropertyType.单精度浮点数)]
        public virtual float Pos_x { get; set; }

        [Property(Name = Pos_yName, Type = PropertyType.单精度浮点数)]
        public virtual float Pos_y { get; set; }

        [Property(Name = Pos_zName, Type = PropertyType.单精度浮点数)]
        public virtual float Pos_z { get; set; }
        [Property(Name = "DefLocation", Type = PropertyType.整数)]
        public virtual int DefLocation { get; set; }
        [Property(Name = "location_name", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string location_name { get; set; }

        [Property(Name = Pos_widthName, Type = PropertyType.单精度浮点数)]
        public virtual float Pos_width { get; set; }

        [Property(Name = Pos_heightName, Type = PropertyType.单精度浮点数)]
        public virtual float Pos_height { get; set; }
        [Property(Name = "Pos_sizeZ", Type = PropertyType.单精度浮点数)]
        public virtual float Pos_sizeZ { get; set; }
        [Property(Name = "Pos_x_pat", Type = PropertyType.整数)]
        public virtual int Pos_x_pat { get; set; }

        [Property(Name = "Pos_y_pat", Type = PropertyType.整数)]
        public virtual int Pos_y_pat { get; set; }
        [Property(Name = "Pos_width_pat", Type = PropertyType.整数)]
        public virtual int Pos_width_pat { get; set; }
        [Property(Name = "Pos_height_pat", Type = PropertyType.整数)]
        public virtual int Pos_height_pat { get; set; }
        [Property(Name = "StationID", Type = PropertyType.整数)]
        public virtual int StationID { get; set; }

        [Property(Name = "StationName", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string StationName { get; set; }

        [Property(Name = "AverageGray", Type = PropertyType.整数)]
        public virtual int AverageGray { get; set; }
        [Property(Name = AreaName, Type = PropertyType.单精度浮点数)]
        public virtual float Area { get; set; }

        [Property(Name = "GrayRange", Type = PropertyType.整数)]
        public virtual int GrayRange { get; set; }
        [Property(Name = "Depth", Type = PropertyType.单精度浮点数)]
        public virtual float Depth { get; set; }
        [Property(Name = "Volume", Type = PropertyType.单精度浮点数)]
        public virtual float Volume { get; set; }
        [Property(Name = "ClassLevel", Type = PropertyType.字符, MaxLength = 20)]
        public virtual string ClassLevel { get; set; }
        [Property(Name = "MajorLenth", Type = PropertyType.单精度浮点数)]
        public virtual float MajorLenth { get; set; }
        [Property(Name = "MinorLenth", Type = PropertyType.单精度浮点数)]
        public virtual float MinorLenth { get; set; }
        [Property(Name = "HaiLenth", Type = PropertyType.单精度浮点数)]
        public virtual float HaiLenth { get; set; }
        [Property(Name = "PtIndex", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string PtIndex { get; set; }
        [Property(Name = "physics_x", Type = PropertyType.单精度浮点数)]
        public virtual float physics_x { get; set; }
        [Property(Name = "physics_y", Type = PropertyType.单精度浮点数)]
        public virtual float physics_y { get; set; }
        [Property(Name = "Mask", Type = PropertyType.字符, MaxLength = 5000)]
        public virtual string Mask { get; set; }
        [Property(Name = "Mask_ManualReview", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string Mask_ManualReview { get; set; }
        [Property(Name = "Cut_Area_mm", Type = PropertyType.单精度浮点数)]
        public virtual float Cut_Area_mm { get; set; }
        [Property(Name = "Cut_Length_mm", Type = PropertyType.单精度浮点数)]
        public virtual float Cut_Length_mm { get; set; }
        [Property(Name = "Cut_Depth_mm", Type = PropertyType.单精度浮点数)]
        public virtual float Cut_Depth_mm { get; set; }
        [Property(Name = "Cut_Area_pix", Type = PropertyType.单精度浮点数)]
        public virtual float Cut_Area_pix { get; set; }
        [Property(Name = "Cut_Length_pix", Type = PropertyType.单精度浮点数)]
        public virtual float Cut_Length_pix { get; set; }
        [Property(Name = "Cut_Depth_pix", Type = PropertyType.单精度浮点数)]
        public virtual float Cut_Depth_pix { get; set; }

        [Property(Name = "ImagePath", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string ImagePath { get; set; }
        [Property(Name = "FTPImgPath", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string FTPImgPath { get; set; }
        [Property(Name = "Code_ManualReview", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Code_ManualReview { get; set; }

        [Property(Name = "DGrade_ManualReview", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string DGrade_ManualReview { get; set; }
        [Property(Name = "Grade_ManualReview", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Grade_ManualReview { get; set; }

        [Property(Name = "Level_ManualReview", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Level_ManualReview { get; set; }
        [Property(Name = "XMLInfo", Type = PropertyType.字符, MaxLength = 5000)]
        public virtual string XMLInfo { get; set; }

        [Property(Name = UniqueIDName, Type = PropertyType.字符, MaxLength = 128)]
        public virtual string UniqueID { get; set; }

        [Property(Name = StartTimeName, Type = PropertyType.日期)]
        public virtual DateTime StartTime { get; set; }

        [Property(Name = StopTimeName, Type = PropertyType.日期)]
        public virtual DateTime StopTime { get; set; }

        [Property(Name = "ext", Type = PropertyType.字符, MaxLength = 5000)]
        public virtual string ext { get; set; }
        [Property(Name = "location_real_name", Type = PropertyType.字符, MaxLength = 255)]
        public virtual string location_real_name { get; set; }
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
        //[Property(Name = "Square", Type = PropertyType.单精度浮点数)]
        //public virtual float Square { get; set; }
        //[Property(Name = "MeaDistmm", Type = PropertyType.单精度浮点数)]
        //public virtual float MeaDistmm { get; set; }
        //[Property(Name = "Mea_Dist_Diff_mm", Type = PropertyType.单精度浮点数)]
        //public virtual float Mea_Dist_Diff_mm { get; set; }
        //[Property(Name = "Length", Type = PropertyType.单精度浮点数)]
        //public virtual float Length { get; set; }
        //[Property(Name = "Score", Type = PropertyType.单精度浮点数)]
        //public virtual float Score { get; set; }
        //[Property(Name = "A", Type = PropertyType.单精度浮点数)]
        //public virtual float A { get; set; }
        //[Property(Name = "MaA", Type = PropertyType.单精度浮点数)]
        //public virtual float MaA { get; set; }
        //[Property(Name = "MiA", Type = PropertyType.单精度浮点数)]
        //public virtual float MiA { get; set; }

        //[Property(Name = "IsDefect", Type = PropertyType.整数)]
        //public virtual int IsDefect { get; set; }
        //[Property(Name = "IsSideDefect", Type = PropertyType.整数)]
        //public virtual int IsSideDefect { get; set; }
        //[Property(Name = "AreaLow", Type = PropertyType.单精度浮点数)]
        //public virtual float AreaLow { get; set; }
        //[Property(Name = "AreaUp", Type = PropertyType.单精度浮点数)]
        //public virtual float AreaUp { get; set; }
        //[Property(Name = "AreaAll", Type = PropertyType.单精度浮点数)]
        //public virtual float AreaAll { get; set; }
        //[Property(Name = "LineLow", Type = PropertyType.单精度浮点数)]
        //public virtual float LineLow { get; set; }
        //[Property(Name = "LineUp", Type = PropertyType.单精度浮点数)]
        //public virtual float LineUp { get; set; }
        //[Property(Name = "LineAll", Type = PropertyType.单精度浮点数)]
        //public virtual float LineAll { get; set; }
        //[Property(Name = "WidthLow", Type = PropertyType.单精度浮点数)]
        //public virtual float WidthLow { get; set; }
        //[Property(Name = "WidthUp", Type = PropertyType.单精度浮点数)]
        //public virtual float WidthUp { get; set; }
        //[Property(Name = "BendingCoe", Type = PropertyType.单精度浮点数)]
        //public virtual float BendingCoe { get; set; }
        //[Property(Name = "PointLineCnt", Type = PropertyType.整数)]
        //public virtual int PointLineCnt { get; set; }
        //[Property(Name = "ContrastLow", Type = PropertyType.单精度浮点数)]
        //public virtual float ContrastLow { get; set; }
        //[Property(Name = "ContrastUp", Type = PropertyType.单精度浮点数)]
        //public virtual float ContrastUp { get; set; }
        //[Property(Name = "ContrastRange", Type = PropertyType.单精度浮点数)]
        //public virtual float ContrastRange { get; set; }
        //[Property(Name = "DefectSpace", Type = PropertyType.单精度浮点数)]
        //public virtual float DefectSpace { get; set; }

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
                return "ivs_lcd_surfacedefect";
            }
            set { }
        }
    }
}
