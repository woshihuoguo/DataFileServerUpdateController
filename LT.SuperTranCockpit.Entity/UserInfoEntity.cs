using LT.Common.IO.Storage.Model;

namespace LT.SuperTranCockpit.Entity
{
    [Entity(Name = "user_info")]
    public class UserInfoEntity : IEntity
    {
        public const string UserNameName = "UserName";
        public const string PasswordName = "Password";

        [Id(Name = "SysId", Type = PropertyType.长整数)]
        public virtual long SysId { get; set; }

        [Property(Name = UserNameName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string UserName { get; set; }

        [Property(Name = PasswordName, Type = PropertyType.字符, MaxLength = 50)]
        public virtual string Password { get; set; }

        [Property(Name = "FullName", Type = PropertyType.字符, MaxLength = 50)]
        public virtual string FullName { get; set; }

        [Property(Name = "Authority", Type = PropertyType.整数)]
        public virtual int Authority { get; set; }

        [Property(Name = "Fingerprint", Type = PropertyType.字符, MaxLength = 2048)]
        public virtual string Fingerprint { get; set; }
    }
}
