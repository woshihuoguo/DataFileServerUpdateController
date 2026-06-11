using System;
using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "recipe_config")]
    public class RecipeConfigEntity : IEntity
    {
        [Id(Name = "SysId", Type = PropertyType.长整数, IsNative = true)]
        public virtual long SysId { get; set; }
        
        [Property(Name = "CraftName", Type = PropertyType.字符)]
        public virtual string CraftName { get; set; }

        [Property(Name = "RecipeName", Type = PropertyType.字符)]
        public virtual string RecipeName { get; set; }

        [Property(Name = "RecipeItemType", Type = PropertyType.整数)]
        public virtual int RecipeItemType { get; set; }

    }
}
