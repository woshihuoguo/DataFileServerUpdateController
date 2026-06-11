using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.SuperTranCockpit.Entity
{
    public interface IAutoClear
    {
        string GetTableName { get; set; }
        string GetTimePropertyName { get; set; }
    }
}
