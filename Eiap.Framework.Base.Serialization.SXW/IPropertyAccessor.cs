using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public interface IPropertyAccessor
    {
        object GetValue(object instance);

        void SetValue(object instance, object newValue);
    }
}
