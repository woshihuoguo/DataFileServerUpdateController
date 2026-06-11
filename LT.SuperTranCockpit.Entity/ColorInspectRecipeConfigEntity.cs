using System;
using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "color_inspect_recipe_config")]
    public class ColorInspectRecipeConfigEntity : IEntity
    {
        [Id(Name = "SysId", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }

        [Property(Name = "RecipeName", Type = PropertyType.字符)]
        public virtual string RecipeName { get; set; }

        [Property(Name = "PatternConfigName", Type = PropertyType.字符)]
        public virtual string PatternConfigName { get; set; }

        [Property(Name = "PatternConfigWaitTime", Type = PropertyType.整数)]
        public virtual int PatternConfigWaitTime { get; set; }

        [Property(Name = "PatternConfigParamName", Type = PropertyType.字符)]
        public virtual string PatternConfigParamName { get; set; }

        [Property(Name = "PatternConfigParamValue", Type = PropertyType.字符)]
        public virtual string PatternConfigParamValue { get; set; }
    }
}
