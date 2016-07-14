﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest
{
    public class UnitTestContainer
    {
        public UnitTestContainer()
        {
            TestMethodList = new List<UnitTestMethodContainer>();
        }

        public string UnitTestInterfaceAssemblyName { get; set; }

        public Type UnitTestInterfaceType { get; set; }

        public List<UnitTestMethodContainer> TestMethodList { get; set; }
    }
}
