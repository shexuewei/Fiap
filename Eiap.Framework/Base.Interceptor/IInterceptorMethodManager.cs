﻿using Eiap.Framework.Base.Dependency;
using Eiap.Framework.Base.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Interceptor
{
    public interface IInterceptorMethodManager : ISingletonDependency, IDynamicProxyDisable
    {
        /// <summary>
        /// 注册拦截属性和拦截方法
        /// </summary>
        /// <param name="interceptorMethodAttibute"></param>
        /// <param name="interceptorMethod"></param>
        void RegisterAttibuteAndInterceptorMethod(Type interceptorMethodAttibute);

        /// <summary>
        /// 根据拦截属性获取拦截方法
        /// </summary>
        /// <param name="interceptorMethodAttibute"></param>
        /// <returns></returns>
        List<Action<InterceptorMethodArgs>> GetInterceptorMethodList(Type interceptorMethodAttibute);
    }
}
