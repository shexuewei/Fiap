using Eiap.Framework.Base.AssemblyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.DTOMapper.SXW
{
    public class DTOMapperModule : IComponentModule
    {
        public void AssemblyInitialize()
        {
            AssemblyManager.Instance.RegisterAssembly(Assembly.GetExecutingAssembly());
        }

        public void RegisterInitialize()
        {
            
        }
    }
}
