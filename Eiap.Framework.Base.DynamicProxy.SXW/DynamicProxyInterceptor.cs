using Eiap.Framework.Base.Interceptor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.DynamicProxy.SXW
{
    public class DynamicProxyInterceptor : IDynamicProxyInterceptor
    {
        private List<Action<InterceptorMethodArgs>> _InterceptorActionList = null;
        private readonly IInterceptorMethodManager _InterceptorMethodManager;
        private readonly IDynamicProxyMethodContainerManager _DynamicProxyMethodContainerManager;

        public DynamicProxyInterceptor(IInterceptorMethodManager interceptorMethodManager, 
            IDynamicProxyMethodContainerManager dynamicProxyMethodContainerManager)
        {
            _InterceptorMethodManager = interceptorMethodManager;
            _DynamicProxyMethodContainerManager = dynamicProxyMethodContainerManager;
            _InterceptorActionList = new List<Action<InterceptorMethodArgs>>();
        }

        public object Invoke(object instance, string name, object[] parameters)
        {
            object objres = null;
            MethodInfo methodinfo = null;
            InterceptorMethodArgs args = null;
            Stopwatch stopwatch = null;
            DynamicProxyMethodContainer methodcontainer = null;
            try
            {
                if (instance != null)
                {
                    Type instanceType = instance.GetType();
                    string dynamicProxyMethidFullName = instanceType.FullName + "." + name;
                    methodcontainer = _DynamicProxyMethodContainerManager.GetDynamicProxyMethodContainerByDynamicProxyMethodName(dynamicProxyMethidFullName);
                    if (methodcontainer != null)
                    {
                        methodinfo = methodcontainer.DynamicProxyMethod;
                    }
                    else
                    {
                        methodinfo = instanceType.GetMethod(name);
                        methodcontainer = new DynamicProxyMethodContainer { DynamicProxyMethidFullName = dynamicProxyMethidFullName, DynamicProxyMethodHandle = methodinfo.MethodHandle };
                        _DynamicProxyMethodContainerManager.AddDynamicProxyContainer(methodcontainer);
                    }
                    if (methodinfo != null)
                    {
                        stopwatch = new Stopwatch();
                        stopwatch.Start();
                        args = new InterceptorMethodArgs { MethodName = name, MethodDateTime = DateTime.Now, MethodParameters = parameters };
                        InvokeBegin(methodinfo, args);
                        if (methodinfo.IsGenericMethod)
                        {
                            Type[] genericArgumentsList = methodinfo.GetGenericArguments().Select(m => m.DeclaringType).ToArray();
                            objres = methodinfo.MakeGenericMethod(genericArgumentsList).Invoke(instance, parameters);
                        }
                        else
                        {
                            objres = methodinfo.Invoke(instance, parameters);
                        }
                        stopwatch.Stop();
                        args.MethodExecute = stopwatch.ElapsedMilliseconds;
                        args.ReturnValue = objres;
                        InvokeEnd(methodinfo, args);
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
                if (args != null && methodinfo != null)
                {
                    args.MethodException = ex;
                    InvokeException(methodinfo, args);
                }
                throw ex;
            }
            return objres;
        }

        /// <summary>
        /// 清理拦截方法
        /// </summary>
        private void ClearInterceptorActionList()
        {
            if (_InterceptorActionList != null && _InterceptorActionList.Count > 0)
            {
                _InterceptorActionList.Clear();
            }
        }

        /// <summary>
        /// 方法前拦截方法
        /// </summary>
        /// <param name="methodinfo"></param>
        /// <param name="args"></param>
        /// <returns>True：成功执行拦截方法；False：拦截方法终止</returns>
        private void InvokeBegin(MethodInfo methodinfo, InterceptorMethodArgs args)
        {
            ClearInterceptorActionList();
            methodinfo.GetCustomAttributes(typeof(InterceptorMethodBeginAttibute), true).ToList().ForEach(m => {
                List<Action<InterceptorMethodArgs>> tmpInterceptorActionList = _InterceptorMethodManager.GetInterceptorMethodList(m.GetType());
                if (tmpInterceptorActionList != null && tmpInterceptorActionList.Count > 0)
                {
                    _InterceptorActionList.AddRange(tmpInterceptorActionList);
                }
            });
            foreach (Action<InterceptorMethodArgs> interceptorActionItem in _InterceptorActionList)
            {
                interceptorActionItem(args);
               
            }
        }

        /// <summary>
        /// 方法后拦截方法
        /// </summary>
        /// <param name="methodinfo"></param>
        /// <param name="args"></param>
        private void InvokeEnd(MethodInfo methodinfo, InterceptorMethodArgs args)
        {
            ClearInterceptorActionList();
            methodinfo.GetCustomAttributes(typeof(InterceptorMethodEndAttibute), true).ToList().ForEach(m => {
                List<Action<InterceptorMethodArgs>> tmpInterceptorActionList = _InterceptorMethodManager.GetInterceptorMethodList(m.GetType());
                if (tmpInterceptorActionList != null && tmpInterceptorActionList.Count > 0)
                {
                    _InterceptorActionList.AddRange(tmpInterceptorActionList);
                }
            });

            foreach (Action<InterceptorMethodArgs> interceptorActionItem in _InterceptorActionList)
            {
                interceptorActionItem(args);
            }
        }

        /// <summary>
        /// 方法异常拦截
        /// </summary>
        /// <param name="methodinfo"></param>
        /// <param name="args"></param>
        private void InvokeException(MethodInfo methodinfo, InterceptorMethodArgs args)
        {
            ClearInterceptorActionList();
            methodinfo.GetCustomAttributes(typeof(InterceptorMethodExceptionAttibute), true).ToList().ForEach(m => {
                List<Action<InterceptorMethodArgs>> tmpInterceptorActionList = _InterceptorMethodManager.GetInterceptorMethodList(m.GetType());
                if (tmpInterceptorActionList != null && tmpInterceptorActionList.Count > 0)
                {
                    _InterceptorActionList.AddRange(tmpInterceptorActionList);
                }
            });
            foreach (Action<InterceptorMethodArgs> interceptorActionItem in _InterceptorActionList)
            {
                interceptorActionItem(args);
            }
        }

        /// <summary>
        /// 由于拦截器终止，返回方法默认返回值
        /// </summary>
        /// <param name="methodinfo"></param>
        /// <returns></returns>
        private object GetDefaultMethodReturnType(MethodInfo methodinfo)
        {
            if (methodinfo.ReturnType.IsValueType)
            {
                return Activator.CreateInstance(methodinfo.ReturnType);
            }
            else
            {
                return null;
            }
        }
    }
}
