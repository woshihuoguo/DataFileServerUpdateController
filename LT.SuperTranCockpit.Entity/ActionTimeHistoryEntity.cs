using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "action_time_history")]
    public class ActionTimeHistoryEntity : IEntity
    {
        [Id(Name = "SysId", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }

        [Property(Name = "PlcIndex", Type = PropertyType.整数)]
        public virtual int PlcIndex { get; set; }

        [Property(Name = "Channel", Type = PropertyType.整数)]
        public virtual int Channel { get; set; }

        [Property(Name = "Barcode", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Barcode { get; set; }

        [Property(Name = "UniqueId", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string UniqueId { get; set; }

        [Property(Name = "StartTime", Type = PropertyType.日期)]
        public virtual DateTime? StartTime { get; set; }

        [Property(Name = "StopTime", Type = PropertyType.日期)]
        public virtual DateTime? StopTime { get; set; }

        [Property(Name = "RecordTime", Type = PropertyType.日期)]
        public virtual DateTime? RecordTime { get; set; }

        [Property(Name = "ActionName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ActionName { get; set; }
        [Property(Name = "StartTimeMilliSeconds", Type = PropertyType.整数)]
        public virtual int StartTimeMilliSeconds { get; set; }
        [Property(Name = "StopTimeMilliSeconds", Type = PropertyType.整数)]
        public virtual int StopTimeMilliSeconds { get; set; }
    }
}
