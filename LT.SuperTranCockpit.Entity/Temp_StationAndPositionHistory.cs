using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "temp_stationandpositionhistory")]
    public class Temp_StationAndPositionHistory: IEntity, IAutoClear
    {
        [Id(Name = "SysId", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }
        [Property(Name = "UniqueId", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string UniqueId { get; set; }

        [Property(Name = "Barcode", Type = PropertyType.字符, MaxLength = 200)]
        public virtual string Barcode { get; set; }
        [Property(Name = "StationName", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string StationName { get; set; }
        [Property(Name = "Position", Type = PropertyType.整数)]
        public virtual int Position { get; set; }
        [Property(Name = "Remark", Type = PropertyType.整数)]
        public virtual int Remark { get; set; }
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
                return "temp_stationandpositionhistory";
            }
            set { }
        }
    }
}
