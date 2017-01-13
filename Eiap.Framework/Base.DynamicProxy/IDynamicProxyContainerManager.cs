using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.DynamicProxy
{
    public interface IDynamicProxyContainerManager: ISingletonDependency, IDynamicProxyDisable
    {
        /// <summary>
        /// 添加动态代理容器
        /// </summary>
        /// <param name="container"></param>
        void AddDynamicProxyContainer(DynamicProxyContainer container);

        /// <summary>
        /// 获取动态代理容器
        /// </summary>
        /// <param name="dynamicProxyTypeName"></param>
        /// <returns></returns>
        DynamicProxyContainer GetDynamicProxyContainerByDynamicProxyTypeName(string dynamicProxyTypeName);
    }
}
