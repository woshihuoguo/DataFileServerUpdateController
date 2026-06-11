using LT.Common.IO.Storage.Model;
using System;

namespace LT.SuperTranCockpit.Entity
{
    /// <summary>
    /// CIP内部维护UniqueID，MarkID，BarCode，ScanBarCode关系表，区别于IdMap表用于点灯检使用
    /// </summary>
    [Entity(Name = "ivs_lcd_uniqueidmapentity")]
    public class ICW_LCD_UniqueIdMapEntity : IEntity, IAutoClear
    {
        public const string UniqueIDName = "UniqueID";
        public const string BarcodeName = "Barcode";
        public const string ScanBarcodeName = "ScanBarcode";
        public const string ProductModelName = "ProductModel";

        //====================================================================================================
        //以上是类型常量字段
        //----------------------------------------------------------------------------------------------------
        //以下是类型属性
        //====================================================================================================
        /// <summary>
        /// MarkID,不一定连续的
        /// </summary>
        [Id(Name = "SysId", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }

        [Property(Name = UniqueIDName, Type = PropertyType.字符, MaxLength = 38)]
        public virtual string UniqueID { get; set; }

        [Property(Name = BarcodeName, Type = PropertyType.字符, MaxLength = 128)]
        public virtual string Barcode { get; set; }
        [Property(Name = ScanBarcodeName, Type = PropertyType.字符, MaxLength = 128)]
        public virtual string ScanBarcode { get; set; }
        [Property(Name = ProductModelName, Type = PropertyType.字符, MaxLength = 10)]
        public virtual string ProductModel { get; set; }
        [Property(Name = "CreationTime", Type = PropertyType.日期)]
        public virtual DateTime CreationTime { get; set; }
        
        public virtual string GetTimePropertyName
        {
            get
            {
                return "CreationTime";
            }
            set { }
        }

        public virtual string GetTableName
        {
            get
            {
                return "ivs_lcd_uniqueidmapentity";
            }
            set { }
        }
    }
}
