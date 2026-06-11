using LT.Common.IO.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.SuperTranCockpit.Entity
{
    public class ActivationHistoryEntityId
    {
        //====================================================================================================
        //以上是类型常量字段
        //----------------------------------------------------------------------------------------------------
        //以下是类型属性
        //====================================================================================================

        [Property(Name = "StartTime", Type = PropertyType.日期)]
        public virtual DateTime StartTime { get; set; }

        [Property(Name = "StopTime", Type = PropertyType.日期)]
        public virtual DateTime StopTime { get; set; }

        [Property(Name = "ActivationType", Type = PropertyType.字符)]
        public virtual string ActivationType { get; set; }

        [Property(Name = "Section", Type = PropertyType.字符)]
        public virtual string Section { get; set; }        

        public ActivationHistoryEntityId(DateTime startTime, DateTime stopTime, string activationType, string section)
        {
            StartTime = startTime;
            StopTime = stopTime;
            ActivationType = activationType;
            Section = section;
        }

        public ActivationHistoryEntityId()
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

            var id = obj as ActivationHistoryEntityId;
            if (id != null)
            {
                return id.StartTime == StartTime &&
                       id.StopTime == StopTime &&
                       id.ActivationType == ActivationType&&
                       id.Section == Section;
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = StartTime.GetHashCode();
            hashCode ^= StopTime.GetHashCode();
            hashCode ^= ActivationType.GetHashCode();
            hashCode ^= Section.GetHashCode();

            return hashCode;
        }
    }
}
