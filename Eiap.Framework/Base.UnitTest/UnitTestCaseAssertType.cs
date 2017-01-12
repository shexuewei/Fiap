using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest
{
    public enum UnitTestCaseAssertType
    {
        AssertEquals,//值相等
        AssertFalse,//等于False
        AssertTrue,//等于True
        AssertNotNull,//不为Null
        AssertNull,//为Null
        AssertNotSame,//引用不相等
        AssertSame,//引用相等
    }
}
