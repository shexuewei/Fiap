using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Dependency
{
    public class DependencyContainer
    {
        public Type DependencyInterface { get; set; }

        public string DependencyInterfaceName { get { return DependencyInterface.FullName; } }

        public Type DependencyInterfaceClass { get; set; }

        public string DependencyInterfaceClassName { get { return DependencyInterfaceClass.FullName; } }

        public bool IsDirectRelation { get; set; }

    }
}
