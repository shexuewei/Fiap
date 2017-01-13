using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.DynamicProxy
{
    public class DynamicProxyContainer
    {
        /// <summary>
        /// 动态代理类型
        /// </summary>
        public Type DynamicProxyType { get; set; }

        /// <summary>
        /// 动态代理类型名称
        /// </summary>
        public string DynamicProxyTypeFullName { get; set; }
    }
}
