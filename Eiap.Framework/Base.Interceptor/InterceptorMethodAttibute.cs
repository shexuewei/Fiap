using System;

namespace Eiap.Framework.Base.Interceptor
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class InterceptorMethodBeginAttibute : Attribute, IInterceptorMethodBegin
    {
        public virtual void Execute(InterceptorMethodArgs args)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class InterceptorMethodEndAttibute : Attribute, IInterceptorMethodEnd
    {
        public virtual void Execute(InterceptorMethodArgs args)
        {
            
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class InterceptorMethodExceptionAttibute : Attribute, IInterceptorMethodException
    {
        public virtual void Execute(InterceptorMethodArgs args)
        {

        }
    }
}
