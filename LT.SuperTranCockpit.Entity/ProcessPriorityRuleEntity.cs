using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "process_priority_rules")]
    public class ProcessPriorityRuleEntity : IEntity
    {
        [Id(Name = "SysId", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }

        [Property(Name = "ProcessName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ProcessName { get; set; }

        [Property(Name = "Index", Type = PropertyType.整数)]
        public virtual int Index { get; set; }
    }
}
