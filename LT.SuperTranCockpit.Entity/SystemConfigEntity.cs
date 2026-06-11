using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "systemconfig")]
    public class SystemConfigEntity : IEntity
    {
        public const string ConfigNameName = "ConfigName";
        public const string ConfigValueName = "ConfigValue";

        public const int ConfigNameMaxLength = 100;
        public const int ConfigValueMaxLength = 100;

        //====================================================================================================
        //以上是类型常量字段
        //----------------------------------------------------------------------------------------------------
        //以下是类型属性
        //====================================================================================================
        [Id(Name = ConfigNameName, Type = PropertyType.字符, MaxLength = ConfigNameMaxLength)]
        public virtual string ConfigName { get; set; }

        [Property(Name = ConfigValueName, Type = PropertyType.字符, MaxLength = ConfigValueMaxLength)]
        public virtual string ConfigValue { get; set; }
    }
}