using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Interceptor.SXW
{
    public class InterceptorMethodManager : IInterceptorMethodManager
    {
        private readonly IInterceptorMethodContainerManager _InterceptorMethodContainerManager;
        public InterceptorMethodManager(IInterceptorMethodContainerManager interceptorMethodContainerManager)
        {
            _InterceptorMethodContainerManager = interceptorMethodContainerManager;
        }

        /// <summary>
        /// 注册拦截属性和拦截方法
        /// </summary>
        /// <param name="interceptorMethodAttibute"></param>
        /// <param name="interceptorMethod"></param>
        public void RegisterAttibuteAndInterceptorMethod(Type interceptorMethodAttibute, Func<InterceptorMethodArgs, bool> interceptorMethod)
        {
            _InterceptorMethodContainerManager.RegisterAttibuteAndInterceptorMethod(interceptorMethodAttibute, interceptorMethod);
        }

        /// <summary>
        /// 根据拦截属性获取拦截方法
        /// </summary>
        /// <param name="interceptorMethodAttibute"></param>
        /// <returns></returns>
        public List<Func<InterceptorMethodArgs, bool>> GetInterceptorMethodList(Type interceptorMethodAttibute)
        {
            InterceptorMethodContainer container = _InterceptorMethodContainerManager.GetInterceptorMethodContainer(interceptorMethodAttibute);
            if (container != null)
            {
                return container.InterceptorMethodList;
            }
            return null;
        }
    }
}
