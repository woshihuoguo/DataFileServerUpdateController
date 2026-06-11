using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.SuperTranCockpit.Entity
{
    public class PlcConfigInitialEntityId
    {
        //====================================================================================================
        //以上是类型常量字段
        //----------------------------------------------------------------------------------------------------
        //以下是类型属性
        //====================================================================================================

        [Property(Name = "PlcIndex", Type = PropertyType.整数)]
        public virtual int PlcIndex { get; set; }

        [Property(Name = "ParamName", Type = PropertyType.字符)]
        public virtual string ParamName { get; set; }

        public PlcConfigInitialEntityId(int plcIndex, string paramName)
        {
            PlcIndex = plcIndex;
            ParamName = paramName;
        }

        public PlcConfigInitialEntityId()
        {

        }

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

            var id = obj as PlcConfigInitialEntityId;
            if (id != null)
            {
                return id.PlcIndex == PlcIndex &&
                       id.ParamName == ParamName;
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = PlcIndex.GetHashCode();
            hashCode ^= ParamName.GetHashCode();

            return hashCode;
        }
    }
}
