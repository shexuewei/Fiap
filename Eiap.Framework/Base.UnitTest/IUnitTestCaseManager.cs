using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest
{
    public interface IUnitTestCaseManager
    {
        IUnitTestCaseManager SetMethodName(string methodName);

        IUnitTestCaseManager SetMethodCase(params object[] objs);

        void Finish();
    }
}
