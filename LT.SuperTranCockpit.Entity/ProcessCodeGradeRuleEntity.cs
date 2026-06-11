using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "process_code_grade_rules")]
    public class ProcessCodeGradeRuleEntity : IEntity
    {
        [Id(Name = "SysId", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }

        [Property(Name = "ProcessName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string ProcessName { get; set; }

        [Property(Name = "Code", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Code { get; set; }

        [Property(Name = "Grade", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Grade { get; set; }
    }
}
