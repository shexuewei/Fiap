using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public interface IPropertyAccessorManager: ISingletonDependency, IDynamicProxyDisable
    {
        /// <summary>
        /// 根据属性Key获取属性访问器
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        IPropertyAccessor GetPropertyAccessor(string propertyKey, PropertyInfoContainer container);
    }
}
