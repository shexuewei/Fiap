﻿using Eiap.Framework.Base.AssemblyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.DataAccess.SQLServer
{
    public class DataAccessSQLServerModule : IComponentModule
    {
        public void Initialize()
        {
            AssemblyManager.Instance.RegisterAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
