using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.SuperTranCockpit.Entity
{
    public class TossMaterialByShiftId
    {
        public const string DateTimeName = "DateTime";
        public const string ShiftName = "Shift";
        public const string MaterialNameName = "MaterialName";

        //====================================================================================================
        //以上是类型常量字段
        //----------------------------------------------------------------------------------------------------
        //以下是类型属性
        //====================================================================================================

        [Property(Name = DateTimeName, Type = PropertyType.日期)]
        public virtual DateTime DateTime { get; set; }

        [Property(Name = ShiftName, Type = PropertyType.字符)]
        public virtual string Shift { get; set; }

        [Property(Name = MaterialNameName, Type = PropertyType.字符)]
        public virtual string MaterialName { get; set; }

        //====================================================================================================
        //以上是类型属性
        //----------------------------------------------------------------------------------------------------
        //以下是重写Equals及GetHashCode
        //====================================================================================================

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var id = obj as TossMaterialByShiftId;
            if (id != null)
            {
                return id.DateTime == DateTime &&
                       id.Shift == Shift &&
                       id.MaterialName == MaterialName;
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = DateTime.GetHashCode();
            hashCode ^= Shift.GetHashCode();
            hashCode ^= MaterialName.GetHashCode();

            return hashCode;
        }
    }
}
