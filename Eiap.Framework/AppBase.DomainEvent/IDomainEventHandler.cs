using Eiap.Framework.Base.Dependency;
using Eiap.Framework.Base.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.DomainEvent
{
    public interface IDomainEventHandler<in TDomainEventData> : IRealtimeDependency 
        where TDomainEventData : IDomainEventData
    {
        void ProcessEvent(TDomainEventData eventData);
    }
}
