using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest
{
    public enum UnitTestCaseAssertType
    {
        AssertEquals,//引用对象的比较（内存地址的比较）
        AssertFalse,
        AssertTrue,
        AssertNotNull,
        AssertNull,
        AssertNotSame,
        AssertSame,//==比较
    }
}
