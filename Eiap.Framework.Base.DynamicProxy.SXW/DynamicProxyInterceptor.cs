﻿using Eiap.Framework.Base.Interceptor;
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
        private List<Func<InterceptorMethodArgs, bool>> _InterceptorActionList = null;
        private readonly IInterceptorMethodManager _InterceptorMethodManager;

        public DynamicProxyInterceptor(IInterceptorMethodManager interceptorMethodManager)
        {
            _InterceptorMethodManager = interceptorMethodManager;
            _InterceptorActionList = new List<Func<InterceptorMethodArgs, bool>>();
        }

        public object Invoke(object instance, string name, object[] parameters)
        {
            object objres = null;
            MethodInfo methodinfo = null;
            InterceptorMethodArgs args = null;
            Stopwatch stopwatch = null;
            ClearInterceptorActionList();
            try
            {
                if (instance != null)
                {
                    methodinfo = instance.GetType().GetMethod(name);
                    if (methodinfo != null)
                    {
                        stopwatch = new Stopwatch();
                        args = new InterceptorMethodArgs { Instance = instance, MethodName = name, MethodDateTime = DateTime.Now, MethodParameters = parameters };

                        methodinfo.GetCustomAttributes(typeof(InterceptorMethodAttibute), true).ToList().ForEach(m => {
                            List<Func<InterceptorMethodArgs, bool>> tmpInterceptorActionList = _InterceptorMethodManager.GetInterceptorMethodList(m.GetType());
                            if (tmpInterceptorActionList != null && tmpInterceptorActionList.Count > 0)
                            {
                                _InterceptorActionList.AddRange(tmpInterceptorActionList);
                            }
                        });

                        foreach (Func<InterceptorMethodArgs, bool> interceptorActionItem in _InterceptorActionList)
                        {
                            if (!interceptorActionItem(args))
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
                        stopwatch.Start();
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
            ClearInterceptorActionList();
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
    }
}
