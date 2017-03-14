using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.DynamicProxy
{
    public class DynamicProxyMethodContainer
    {
        /// <summary>
        /// 动态代理方法类型句柄
        /// </summary>
        public RuntimeMethodHandle DynamicProxyMethodHandle { get; set; }

        /// <summary>
        /// 动态代理方法类型名称
        /// </summary>
        public string DynamicProxyMethidFullName { get; set; }

        /// <summary>
        /// 动态代理方法类型
        /// </summary>
        public MethodInfo DynamicProxyMethod
        {
            get
            {
                return (MethodInfo)MethodInfo.GetMethodFromHandle(DynamicProxyMethodHandle);
            }
        }
    }
}
