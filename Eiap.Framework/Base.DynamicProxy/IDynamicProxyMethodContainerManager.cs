using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.DynamicProxy
{
    public interface IDynamicProxyMethodContainerManager : ISingletonDependency, IDynamicProxyDisable
    {
        /// <summary>
        /// 添加动态代理方法容器
        /// </summary>
        /// <param name="container"></param>
        void AddDynamicProxyContainer(DynamicProxyMethodContainer container);

        /// <summary>
        /// 获取动态代理方法容器
        /// </summary>
        /// <param name="dynamicProxyTypeName"></param>
        /// <returns></returns>
        DynamicProxyMethodContainer GetDynamicProxyMethodContainerByDynamicProxyMethodName(string dynamicProxyMethodName);
    }
}
