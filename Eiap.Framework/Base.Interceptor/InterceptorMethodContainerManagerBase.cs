using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Interceptor
{
    public abstract class InterceptorMethodContainerManagerBase : IInterceptorMethodContainerManager
    {
        protected static IInterceptorMethodContainerManager _IInterceptorMethodContainerManager;

        protected List<InterceptorMethodContainer> _InterceptorMethodContainerList = null;

        protected InterceptorMethodContainerManagerBase()
        {
            _InterceptorMethodContainerList = new List<InterceptorMethodContainer>();
        }

        /// <summary>
        /// 判断是否有相同拦截方法
        /// </summary>
        /// <param name="interceptorMethodAttibute"></param>
        /// <param name="interceptorMethod"></param>
        /// <returns></returns>
        protected bool IsExistSameInterceptorMethod(InterceptorMethodContainer interceptorMethodContainer, Func<InterceptorMethodArgs, bool> interceptorMethod)
        {
            bool res = false;
            interceptorMethodContainer.InterceptorMethodList.ForEach(n =>
            {
                if (n.Method.Name == interceptorMethod.Method.Name)
                {
                    res = true;
                }
            });
            return res;
        }

        /// <summary>
        /// 判断是否有相同拦截特性
        /// </summary>
        /// <param name="interceptorMethodAttibute"></param>
        /// <returns></returns>
        protected InterceptorMethodContainer IsExistSameInterceptorMethodAttibute(Type interceptorMethodAttibute)
        {
            InterceptorMethodContainer res = null;
            foreach (InterceptorMethodContainer item in _InterceptorMethodContainerList)
            {
                if (item.InterceptorMethodAttibuteName == interceptorMethodAttibute.FullName)
                {
                    res = item;
                    break;
                }   
            }
            return res;
        }

        /// <summary>
        /// 注册特性和对应的拦截方法
        /// </summary>
        /// <param name="interceptorMethodAttibute"></param>
        /// <param name="interceptorMethod"></param>
        public virtual void RegisterAttibuteAndInterceptorMethod(Type interceptorMethodAttibute, Func<InterceptorMethodArgs, bool> interceptorMethod) { }

        /// <summary>
        /// 根据特性名称获取拦截容器
        /// </summary>
        /// <param name="interceptorMethodAttibute"></param>
        /// <returns></returns>
        public virtual InterceptorMethodContainer GetInterceptorMethodContainer(Type interceptorMethodAttibute) { return null; }
    }
}
