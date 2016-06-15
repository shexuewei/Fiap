using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.DomainEvent
{
    public class DomainEventContainer
    {
        public Type DomainEventDataType { get; set; }

        public string DomainEventDataTypeName { get; set; }

        public Type DomainEventHandlerType { get; set; }

        public string DomainEventHandlerTypeName { get; set; }

        public Type InterfaceDomainEventHandlerType { get; set; }

        public string InterfaceDomainEventHandlerName { get; set; }
    }
}
