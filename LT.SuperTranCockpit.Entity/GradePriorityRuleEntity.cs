using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "grade_priority_rules")]
    public class GradePriorityRuleEntity : IEntity
    {
        [Id(Name = "SysId", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }

        [Property(Name = "Grade", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Grade { get; set; }

        [Property(Name = "Index", Type = PropertyType.整数)]
        public virtual int Index { get; set; }
    }
}
