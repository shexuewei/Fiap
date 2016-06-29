using Eiap.Framework.Base.Interceptor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest.SXW
{
    public class UnitTestProxyInterceptor : IUnitTestProxyInterceptor
    {
        private readonly IInterceptorMethodManager _InterceptorMethodManager;

        public UnitTestProxyInterceptor(IInterceptorMethodManager interceptorMethodManager)
        {
            _InterceptorMethodManager = interceptorMethodManager;
        }

        public object Invoke(object instance, string name, object[] parameters)
        {
            object objres = null;
            MethodInfo methodinfo = null;
            try
            {
                if (instance != null)
                {
                    methodinfo = instance.GetType().GetMethod(name);
                    if (methodinfo != null)
                    {
                        objres = methodinfo.Invoke(instance, parameters);
                    }
                    else
                    {
                        //TODO:自定义异常
                        throw new Exception("No Method");
                    }
                }
                else
                {
                    //TODO:自定义异常
                    throw new Exception("No Instance Object");
                }
            }
            catch (Exception ex)
            {
                //TODO：异常处理
                throw ex;
            }
            return objres;
        }
    }
}
