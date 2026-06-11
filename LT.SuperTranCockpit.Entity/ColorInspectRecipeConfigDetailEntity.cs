using System;
using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "color_inspect_recipe_config_detail")]
    public class ColorInspectRecipeConfigDetailEntity : IEntity
    {
        [Id(Name = "SysId", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }

        [Property(Name = "RecipeName", Type = PropertyType.字符)]
        public virtual string RecipeName { get; set; }

        [Property(Name = "PatternConfigName", Type = PropertyType.字符)]
        public virtual string PatternConfigName { get; set; }

        [Property(Name = "ConfigName", Type = PropertyType.字符)]
        public virtual string ConfigName { get; set; }

        [Property(Name = "CompareType", Type = PropertyType.整数)]
        public virtual int CompareType { get; set; }

        [Property(Name = "CompareValue1", Type = PropertyType.单精度浮点数)]
        public virtual float CompareValue1 { get; set; }
        
        [Property(Name = "CompareValue2", Type = PropertyType.单精度浮点数)]
        public virtual float CompareValue2 { get; set; }

        [Property(Name = "Code", Type = PropertyType.字符)]
        public virtual string Code { get; set; }

        [Property(Name = "Grade", Type = PropertyType.字符)]
        public virtual string Grade { get; set; }
    }
}
