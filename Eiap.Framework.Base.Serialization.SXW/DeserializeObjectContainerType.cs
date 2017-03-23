using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public enum DeserializeObjectContainerType
    {
        List = 0,
        Object = 1,
        Property = 2,
        Value_String = 3,
        Value_Int = 4,
        Value_Decimal = 5,
        Value_DateTime = 6
    }
}
