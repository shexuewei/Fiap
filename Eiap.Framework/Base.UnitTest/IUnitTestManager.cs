﻿using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest
{
    public interface IUnitTestManager : IRealtimeDependency
    {
        IUnitTestManager Register<T>(string methodName, UnitTestCaseContainer unitTestCase);

        IUnitTestManager Register(Type interfaceType, string methodName, UnitTestCaseContainer unitTestCase);

        void Run(string assemblyName);
    }
}
