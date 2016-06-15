using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Interceptor.SXW
{
    public class InterceptorMethodContainerManager : InterceptorMethodContainerManagerBase
    {
        /// <summary>
        /// 注册特性和对应的拦截方法
        /// </summary>
        /// <param name="interceptorMethodAttibute"></param>
        /// <param name="interceptorMethod"></param>
        public override void RegisterAttibuteAndInterceptorMethod(Type interceptorMethodAttibute, Func<InterceptorMethodArgs, bool> interceptorMethod)
        {
            if (interceptorMethodAttibute.IsAssignableFrom(typeof(InterceptorMethodAttibute)))
            {
                InterceptorMethodContainer containter = IsExistSameInterceptorMethodAttibute(interceptorMethodAttibute);
                if (containter != null)
                {
                    if (!IsExistSameInterceptorMethod(containter, interceptorMethod))
                    {
                        containter.InterceptorMethodList.Add(interceptorMethod);
                    }
                    else
                    {
                        //TODO:存在相同interceptorMethod
                    }
                }
                else
                {
                    _InterceptorMethodContainerList.Add(new InterceptorMethodContainer { InterceptorMethodAttibuteName = interceptorMethodAttibute.FullName, InterceptorMethodAttibute = interceptorMethodAttibute, InterceptorMethodList = { interceptorMethod } });
                }
            }
        }

        /// <summary>
        /// 根据特性名称获取拦截容器
        /// </summary>
        /// <param name="interceptorMethodAttibute"></param>
        /// <returns></returns>
        public override InterceptorMethodContainer GetInterceptorMethodContainer(Type interceptorMethodAttibute)
        {
            InterceptorMethodContainer interceptorMethodContainer = null;
            if (interceptorMethodAttibute.IsAssignableFrom(typeof(InterceptorMethodAttibute)))
            {
                _InterceptorMethodContainerList.ForEach(m =>
                {
                    if (m.InterceptorMethodAttibuteName == interceptorMethodAttibute.FullName)
                    {
                        interceptorMethodContainer = m;
                    }
                });
            }
            return interceptorMethodContainer;
        }
    }
}
