using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public class PropertyInfoContainerManager : IPropertyInfoContainerManager
    {
        private ConcurrentDictionary<string, PropertyInfoContainer> _DicContainer = null;

        public PropertyInfoContainerManager()
        {
            _DicContainer = new ConcurrentDictionary<string, PropertyInfoContainer>();
        }

        /// <summary>
        /// 添加属性容器
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <param name="container"></param>
        public void AddPropertyInfoContainer(string propertyKey, PropertyInfoContainer container)
        {
            if (!_DicContainer.ContainsKey(propertyKey))
            {
                _DicContainer.TryAdd(propertyKey, container);
            }
        }
    }
}
