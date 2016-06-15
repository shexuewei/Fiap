using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Dependency
{
    public class DependencyContainer
    {
        public virtual Type DependencyInterface { get; set; }

        public virtual string DependencyInterfaceName { get; set; }

        public virtual Type DependencyInterfaceClass { get; set; }

        public virtual string DependencyInterfaceClassName { get; set; }

        public virtual bool IsDirectRelation { get; set; }

    }
}
