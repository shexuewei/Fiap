using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest
{
    public interface IUnitTestMethodContainerManager : ISingletonDependency
    {
        UnitTestMethodContainer RegisterUnitTestMethod(MethodInfo methodInfo, UnitTestCaseContainer unitTestCase);
    }
}
