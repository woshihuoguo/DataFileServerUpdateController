using LT.Common.IO.Storage.Model;
using System;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "activation_history")]
    public class ActivationHistoryEntity : IEntity
    {
        [CompositeId(Name = "Id")]
        public virtual ActivationHistoryEntityId Id { get; set; }

        [Property(Name = "ShiftTime", Type = PropertyType.日期)]
        public virtual DateTime ShiftTime { get; set; }

        [Property(Name = "ShiftType", Type = PropertyType.字符)]
        public virtual string ShiftType { get; set; }

        [Property(Name = "ActivationTime", Type = PropertyType.单精度浮点数)]
        public virtual float ActivationTime { get; set; }

        [Property(Name = "ActivationRate", Type = PropertyType.单精度浮点数)]
        public virtual float ActivationRate { get; set; }
    }
}
