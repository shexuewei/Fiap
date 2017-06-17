using System;

namespace Eiap.Framework.Base.Interceptor
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class InterceptorMethodAttribute : Attribute
    {
        /// <summary>
        /// 特性优先级
        /// </summary>
        public virtual int Priority { get; set; }
    }


    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class InterceptorMethodBeginAttibute : InterceptorMethodAttribute, IInterceptorMethodBegin
    {
        public virtual void Execute(InterceptorMethodArgs args)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class InterceptorMethodEndAttibute : InterceptorMethodAttribute, IInterceptorMethodEnd
    {
        public virtual void Execute(InterceptorMethodArgs args)
        {
            
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class InterceptorMethodExceptionAttibute : InterceptorMethodAttribute, IInterceptorMethodException
    {
        public virtual void Execute(InterceptorMethodArgs args)
        {

        }
    }
}
