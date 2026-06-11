using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "removepanelrecord")]
    public class ICW_LCD_RemovePanelResultEntity : IEntity, IAutoClear
    {
        public const string SysIDName = "SysId";
        public const string UniqueIdName = "Uniqueid";
        [Id(Name = SysIDName, Type = PropertyType.长整数)]
        public virtual long SysId { get; set; }
        [Property(Name = UniqueIdName, Type = PropertyType.字符, MaxLength = 38)]
        public virtual string Uniqueid { get; set; }
        [Property(Name = "Barcode", Type = PropertyType.字符, MaxLength = 200)]
        public virtual string Barcode { get; set; }
        [Property(Name = "CreationTime", Type = PropertyType.日期)]
        public virtual DateTime CreationTime { get; set; }
        [Property(Name = "RemoveReason", Type = PropertyType.字符, MaxLength = 500)]
        public virtual string RemoveReason { get; set; }
        [Property(Name = "ShiftDetail", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ShiftDetail { get; set; }
        [Property(Name = "SideId", Type = PropertyType.整数)]
        public virtual int SideId { get; set; }

        [Property(Name = "SideName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string SideName { get; set; }

        [Property(Name = "SectionId", Type = PropertyType.整数)]
        public virtual int SectionId { get; set; }

        [Property(Name = "SectionName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string SectionName { get; set; }
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
                return "removepanelrecord";
            }
            set { }
        }
    }
}
