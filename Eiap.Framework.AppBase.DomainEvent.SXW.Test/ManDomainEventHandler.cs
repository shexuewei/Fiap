using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eiap.Framework.Common.Logger;

namespace Eiap.Framework.AppBase.DomainEvent.SXW.Test
{
    /// <summary>
    /// TODO:1个IDomainEventHandler<ManEventData>对应多个DomainEventHandler
    /// </summary>
    public class ManDomainEventHandler : IDomainEventHandler<ManEventData>, IDomainEventHandler<ShManEventData>
    {
        public void ProcessEvent(ManEventData eventData)
        {
            Console.WriteLine(eventData.Name + ":" + eventData.Age);
        }

        public void ProcessEvent(ShManEventData eventData)
        {
            Console.WriteLine(eventData.Name + ":" + eventData.Age);
        }
    }

    public class ShManDomainEventHandler : IDomainEventHandler<ShManEventData>
    {
        public void ProcessEvent(ShManEventData eventData)
        {
            Console.WriteLine(eventData.manEventData.Name + ":" + eventData.Name + ":" + eventData.Age);
        }
    }
}
