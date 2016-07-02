using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest
{
    public interface IUnitTestProxyInterceptor : IRealtimeDependency
    {
        object Invoke(object instance, string interfaceTypeName, string name, object[] parameters);
    }
}
