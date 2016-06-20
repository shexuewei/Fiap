using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest
{
    public class UnitTestCaseContainer
    {
        public List<object> MethodParas { get; set; }

        public object MethodReturn { get; set; }

        public UnitTestCaseAssertType CaseAssertType { get; set; }
    }
}
