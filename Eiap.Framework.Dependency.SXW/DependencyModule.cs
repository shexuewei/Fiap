﻿using Eiap.Framework.Base.AssemblyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Dependency.SXW
{
    public class DependencyModule : IComponentModule
    {
        public void Initialize()
        {
            AssemblyManager.Instance.RegisterAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
