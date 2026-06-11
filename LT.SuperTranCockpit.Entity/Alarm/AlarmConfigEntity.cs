using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "alarm_config")]
    public class AlarmConfigEntity : IEntity
    {
        public const string PlcIndexName = "PlcIndex";

        [Id(Name = "SysId", Type = PropertyType.长整数)]
        public virtual long SysId { get; set; }

        [Property(Name = PlcIndexName, Type = PropertyType.整数)]
        public virtual int PlcIndex { get; set; }

        [Property(Name = "Address", Type = PropertyType.字符, MaxLength = 20)]
        public virtual string Address { get; set; }

        [Property(Name = "BitPostion", Type = PropertyType.整数)]
        public virtual int BitPostion { get; set; }

        [Property(Name = "Message", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string Message { get; set; }

        [Property(Name = "Description", Type = PropertyType.字符, MaxLength = 100)]
        public virtual string Description { get; set; }

        [Property(Name = "Type", Type = PropertyType.字符, MaxLength = 20)]
        public virtual string Type { get; set; }

        [Property(Name = "Code", Type = PropertyType.字符, MaxLength = 20)]
        public virtual string Code { get; set; }

        [Property(Name = "Component", Type = PropertyType.字符, MaxLength = 20)]
        public virtual string Component { get; set; }

        [Property(Name = "Level", Type = PropertyType.字符, MaxLength = 20)]
        public virtual string Level { get; set; }
    }
}
