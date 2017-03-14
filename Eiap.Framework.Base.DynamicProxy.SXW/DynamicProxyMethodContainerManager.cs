using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.DynamicProxy.SXW
{
    public class DynamicProxyMethodContainerManager : IDynamicProxyMethodContainerManager
    {
        private ConcurrentDictionary<string, DynamicProxyMethodContainer> _DynamicProxyMethodContainerList = null;

        public DynamicProxyMethodContainerManager()
        {
            _DynamicProxyMethodContainerList = new ConcurrentDictionary<string, DynamicProxyMethodContainer>();
        }

        /// <summary>
        /// 添加动态代理方法容器
        /// </summary>
        /// <param name="container"></param>
        public void AddDynamicProxyContainer(DynamicProxyMethodContainer container)
        {
            if (!_DynamicProxyMethodContainerList.ContainsKey(container.DynamicProxyMethidFullName))
            {
                _DynamicProxyMethodContainerList.TryAdd(container.DynamicProxyMethidFullName, container);
            }
        }

        /// <summary>
        /// 获取动态代理方法容器
        /// </summary>
        /// <param name="dynamicProxyMethodName"></param>
        /// <returns></returns>
        public DynamicProxyMethodContainer GetDynamicProxyMethodContainerByDynamicProxyMethodName(string dynamicProxyMethodName)
        {
            if (_DynamicProxyMethodContainerList.ContainsKey(dynamicProxyMethodName))
            {
                return _DynamicProxyMethodContainerList[dynamicProxyMethodName];
            }
            return null;
        }
    }
}
