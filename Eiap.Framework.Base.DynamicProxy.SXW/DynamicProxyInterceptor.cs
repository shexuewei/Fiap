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
            Func<object, object[], object> methodDel = null;
            string dynamicProxyMethidFullName = null;
            MethodInfo excuMethod = null;

            try
            {
                if (instance != null)
                {
                    Type instanceType = instance.GetType();
                    methodinfo = instanceType.GetMethod(name);
                    if (methodinfo != null)
                    {
                        if (methodinfo.IsGenericMethod)
                        {
                            Type[] genericArgumentsList = methodinfo.GetGenericArguments().Select(m => m.DeclaringType).ToArray();
                            excuMethod = methodinfo.MakeGenericMethod(genericArgumentsList);
                        }
                        else
                        {
                            excuMethod = methodinfo;
                        }
                        dynamicProxyMethidFullName = GetDynamicProxyMethidFullName(instanceType, excuMethod);
                        methodDel = _DynamicProxyMethodContainerManager.GetDynamicProxyMethodByDynamicProxyMethodName(dynamicProxyMethidFullName);
                        if (methodDel == null)
                        {
                            methodDel = _DynamicProxyMethodContainerManager.AddDynamicProxyContainer(dynamicProxyMethidFullName, methodinfo);
                        }
                        if (methodDel != null)
                        {
                            stopwatch = new Stopwatch();
                            stopwatch.Start();
                            args = new InterceptorMethodArgs { MethodName = name, MethodDateTime = DateTime.Now, MethodParameters = parameters, InstanceObject = instance };
                            InvokeBegin(methodinfo, args);
                            objres = methodDel(instance, parameters);
                            stopwatch.Stop();
                            args.MethodExecute = stopwatch.Elapsed.TotalMilliseconds;
                            args.ReturnValue = objres;
                            InvokeEnd(methodinfo, args);
                        }
                        else
                        {
                            //TODO:自定义异常
                            throw new Exception("No Method");
                        }
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

        /// <summary>
        /// 根据方法和实例对象返回方法全名
        /// </summary>
        /// <param name="instanceType"></param>
        /// <param name="methodinfo"></param>
        /// <returns></returns>
        private string GetDynamicProxyMethidFullName(Type instanceType, MethodInfo methodinfo)
        {
            StringBuilder dynamicProxyMethidFullNameStrBui = new StringBuilder();
            dynamicProxyMethidFullNameStrBui.Append(instanceType.FullName);
            dynamicProxyMethidFullNameStrBui.Append(".");
            dynamicProxyMethidFullNameStrBui.Append(methodinfo.Name);
            ParameterInfo[] parametersList = methodinfo.GetParameters();
            foreach(ParameterInfo parameterItem in parametersList)
            {
                dynamicProxyMethidFullNameStrBui.Append(".");
                dynamicProxyMethidFullNameStrBui.Append(parameterItem.ParameterType.Name);
            }
            return dynamicProxyMethidFullNameStrBui.ToString();
        }
    }
}
