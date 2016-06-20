using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest
{
    public class UnitTestMethodContainer
    {
        public MethodInfo TestMethodInfo { get; set; }

        public List<UnitTestCaseContainer> CaseList { get; set; }
    }
}
