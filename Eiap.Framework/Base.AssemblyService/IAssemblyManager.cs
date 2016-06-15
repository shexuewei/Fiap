using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Eiap.Framework.Base.AssemblyService
{
    public interface IAssemblyManager
    {
        IAssemblyManager Register(Action<List<Assembly>> reg);

        IAssemblyManager RegisterAssembly(Assembly assembly);

        IAssemblyManager RegisterAssembly(string assemblyPath);
    }
}
