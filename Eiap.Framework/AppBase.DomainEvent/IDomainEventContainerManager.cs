using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.DomainEvent
{
    public interface IDomainEventContainerManager
    {
        void AddDomainEventContList(Type domainEventDataTypeItem, Type interfaceItem, Type classItem);

        List<DomainEventContainer> GetDomainEventContList(Type domainEventDataTypeItem);
    }
}
