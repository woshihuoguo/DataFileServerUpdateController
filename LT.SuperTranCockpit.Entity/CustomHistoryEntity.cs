using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "custom_history")]
    public class CustomHistoryEntity : IEntity
    {

        [Id(Name = "SysId", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }

        [Property(Name = "Name", Type = PropertyType.字符)]
        public virtual string Name { get; set; }

        [Property(Name = "Content", Type = PropertyType.字符, MaxLength = 500)]
        public virtual string Content { get; set; }

        [Property(Name = "RecordDate", Type = PropertyType.日期)]
        public virtual DateTime RecordDate { get; set; }
    }
}
