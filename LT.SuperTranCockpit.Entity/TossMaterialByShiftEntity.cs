using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "toss_material_by_shift")]
    public class TossMaterialByShiftEntity : IEntity
    {
        [CompositeId(Name = "Id")]
        public virtual TossMaterialByShiftId Id { get; set; }

        [Property(Name = "TossCount", Type = PropertyType.整数)]
        public virtual int TossCount { get; set; }
    }
}
