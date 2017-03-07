using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public class PropertyAccessorManager : IPropertyAccessorManager
    {
        private ConcurrentDictionary<string, IPropertyAccessor> _DicPropertyAccessor = null;
        public PropertyAccessorManager()
        {
            _DicPropertyAccessor = new ConcurrentDictionary<string, IPropertyAccessor>();
        }

        /// <summary>
        /// 根据属性Key获取属性访问器
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        public IPropertyAccessor GetPropertyAccessor(string propertyKey, PropertyInfoContainer container)
        {
            IPropertyAccessor accessor = null;
            
            _DicPropertyAccessor.TryGetValue(propertyKey, out accessor);
            if (accessor == null)
            {
                accessor = Activator.CreateInstance(typeof(PropertyAccessor<,>).MakeGenericType(container.InstanceType, container.PropertyType), container.PropertyInfoContainerToPropertyInfo()) as IPropertyAccessor;
                _DicPropertyAccessor.TryAdd(propertyKey, accessor);
            }
            return accessor;
        }
    }
}
