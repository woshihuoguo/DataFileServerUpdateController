using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "ivs_lcd_idmap")]
    public class ICW_LCD_IdMapEntity : IEntity
    {
        public const string MarkIDName = "MarkID";
        public const string PosIDName = "PosID";
        public const string UniqueIDName = "UniqueID";
        public const string TableSuffixName = "TableSuffix";
        public const string BarcodeName = "Barcode";
        public const string MainAoiFixIDName = "MainAoiFixID";
        public const string Fix_IDCodeName = "Fix_IDCode";
        
        //====================================================================================================
        //以上是类型常量字段
        //----------------------------------------------------------------------------------------------------
        //以下是类型属性
        //====================================================================================================
        [Id(Name = MarkIDName, Type = PropertyType.字符, MaxLength = 15)]
        public virtual string MarkID { get; set; }

        [Property(Name = PosIDName, Type = PropertyType.整数, MaxLength = 11)]
        public virtual int PosID { get; set; }

        [Property(Name = UniqueIDName, Type = PropertyType.字符, MaxLength = 38)]
        public virtual string UniqueID { get; set; }

        [Property(Name = TableSuffixName, Type = PropertyType.字符, MaxLength = 10)]
        public virtual string TableSuffix { get; set; }

        [Property(Name = BarcodeName, Type = PropertyType.字符, MaxLength = 128)]
        public virtual string Barcode { get; set; }

        [Property(Name = MainAoiFixIDName, Type = PropertyType.字符, MaxLength = 10)]
        public virtual string MainAoiFixID { get; set; }

        [Property(Name = Fix_IDCodeName, Type = PropertyType.字符, MaxLength = 128)]
        public virtual string Fix_IDCode { get; set; }
        
    }
}
