using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest
{
    public interface IUnitTestContainerManager : ISingletonDependency
    {
        UnitTestContainer RegisterUnitTestInterface(Type unitTestInterface, string methodName, UnitTestCaseContainer unitTestCase);

        UnitTestContainer GetUnitTestContainer(string interfaceTypeName);
    }
}
