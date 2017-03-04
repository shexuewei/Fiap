using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public interface IPropertyInfoContainerManager: ISingletonDependency
    {
        /// <summary>
        /// 添加属性容器
        /// </summary>
        /// <param name="container"></param>
        void AddPropertyInfoContainer(string propertyKey, PropertyInfoContainer container);
    }
}
