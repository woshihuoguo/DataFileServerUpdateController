using System;
using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "alarm_history")]
    public class AlarmHistoryEntity : IEntity, IAutoClear
    {
        public const string SysIdName = "SysId";
        public const string CodeName = "Code";
        public const string OccurTimeName = "OccurTime";

        [Id(Name = SysIdName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string SysId { get; set; }

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

        [Property(Name = "OccurTime", Type = PropertyType.日期)]
        public virtual DateTime OccurTime { get; set; }

        [Property(Name = "RecoverTime", Type = PropertyType.日期)]
        public virtual DateTime RecoverTime { get; set; }

        [Property(Name = "Duration", Type = PropertyType.长整数)]
        public virtual long Duration { get; set; }

        [Property(Name = "EquipId", Type = PropertyType.字符, MaxLength = 20)]
        public virtual string EquipId { get; set; }

        [Property(Name = "EquipName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string EquipName { get; set; }

        public virtual string GetTimePropertyName
        {
            get
            {
                return "OccurTime";
            }
            set { }
        }

        public virtual string GetTableName
        {
            get
            {
                return "alarm_history";
            }
            set { }
        }
    }
}
