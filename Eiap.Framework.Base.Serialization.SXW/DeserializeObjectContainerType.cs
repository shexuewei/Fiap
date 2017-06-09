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
        DictionaryKey = 3,
        Value_String = 4,
        Value_Int = 5,
        Value_Decimal = 6,
        Value_DateTime = 7,
        Value_Bool = 8
    }
}
