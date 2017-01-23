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
        public override void RegisterAttibuteAndInterceptorMethod(Type interceptorMethodAttibute)
        {
            if (typeof(InterceptorMethodAttibute).IsAssignableFrom(interceptorMethodAttibute)&& typeof(IInterceptorMethod).IsAssignableFrom(interceptorMethodAttibute))
            {
                object interceptorMethodAttibuteInstance = Activator.CreateInstance(interceptorMethodAttibute);
                Func<InterceptorMethodArgs, bool> interceptorMethod = ((IInterceptorMethod)interceptorMethodAttibuteInstance).Execute;
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
                    _InterceptorMethodContainerList.Add(interceptorMethodAttibute.FullName, new InterceptorMethodContainer { InterceptorMethodAttibuteTypeHandle = interceptorMethodAttibute.TypeHandle, InterceptorMethodList = { interceptorMethod } });
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
            if (typeof(InterceptorMethodAttibute).IsAssignableFrom(interceptorMethodAttibute) && typeof(IInterceptorMethod).IsAssignableFrom(interceptorMethodAttibute))
            {
                interceptorMethodContainer = IsExistSameInterceptorMethodAttibute(interceptorMethodAttibute);
            }
            return interceptorMethodContainer;
        }
    }
}
