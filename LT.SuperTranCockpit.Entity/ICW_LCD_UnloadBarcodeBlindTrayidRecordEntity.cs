using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "ivs_lcd_unloadbarcodeblindtrayidrecord")]
    public class ICW_LCD_UnloadBarcodeBlindTrayidRecordEntity : IEntity, IAutoClear
    {
        public const string SysIDName = "SysId";
        public const string UniqueIdName = "UniqueId";
        [Id(Name = SysIDName, Type = PropertyType.长整数,IsNative =true)]
        public virtual long SysId { get; set; }
        [Property(Name = UniqueIdName, Type = PropertyType.字符, MaxLength = 38)]
        public virtual string UniqueId { get; set; }
        [Property(Name = "Barcode", Type = PropertyType.字符, MaxLength = 200)]
        public virtual string Barcode { get; set; }

        [Property(Name = "TrayID", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string TrayID { get; set; }
        [Property(Name = "ReportFlag", Type = PropertyType.字符, MaxLength = 10)]
        public virtual string ReportFlag { get; set; }
        [Property(Name = "Result", Type = PropertyType.字符, MaxLength = 10)]
        public virtual string Result { get; set; }

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
                return "ivs_lcd_unloadbarcodeblindtrayidrecord";
            }
            set { }
        }
    }
}
