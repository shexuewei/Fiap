using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Interceptor
{
    /// <summary>
    /// 拦截方法容器
    /// </summary>
    public class InterceptorMethodContainer
    {
        public InterceptorMethodContainer()
        {
            InterceptorMethodList = new List<Func<InterceptorMethodArgs, bool>>();
        }

        /// <summary>
        /// 拦截方法列表
        /// </summary>
        public List<Func<InterceptorMethodArgs, bool>> InterceptorMethodList { get; set; }

        /// <summary>
        /// 拦截方法特性
        /// </summary>
        public Type InterceptorMethodAttibute
        {
            get {
                return Type.GetTypeFromHandle(InterceptorMethodAttibuteTypeHandle);
            }
        }

        /// <summary>
        /// 拦截方法特性类型句柄
        /// </summary>
        public RuntimeTypeHandle InterceptorMethodAttibuteTypeHandle { get; set; }

    }
}
