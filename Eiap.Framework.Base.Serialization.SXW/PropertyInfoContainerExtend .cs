using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public static class PropertyInfoContainerExtend
    {
        public static PropertyInfo PropertyInfoContainerToPropertyInfo(this PropertyInfoContainer container)
        {
            return container.InstanceType.GetProperty(container.PropertyName);
        }
    }
}
