using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.DynamicProxy
{
    public interface IDynamicProxyMethodContainerManager : ISingletonDependency, IDynamicProxyDisable
    {
        /// <summary>
        /// 添加动态代理方法容器
        /// </summary>
        /// <param name="dynamicProxyMethodFullName"></param>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        Func<object, object[], object> AddDynamicProxyContainer(string dynamicProxyMethodFullName, MethodInfo methodInfo);

        /// <summary>
        /// 获取动态代理方法容器
        /// </summary>
        /// <param name="dynamicProxyMethodFullName"></param>
        /// <returns></returns>
        Func<object, object[], object> GetDynamicProxyMethodByDynamicProxyMethodName(string dynamicProxyMethodFullName);
    }
}
