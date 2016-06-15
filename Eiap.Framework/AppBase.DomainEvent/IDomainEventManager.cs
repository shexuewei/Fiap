using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.DomainEvent
{
    public interface IDomainEventManager
    {
        void Register(List<Assembly> assemblyList);

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="TDomainEventData"></typeparam>
        /// <typeparam name="TDomainEventHandler"></typeparam>
        void SubscribeEvent<TDomainEventData, TDomainEventHandler>() 
            where TDomainEventData : IDomainEventData
            where TDomainEventHandler : IDomainEventHandler<TDomainEventData>;

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="TDomainEventData"></typeparam>
        /// <param name="domainEventData"></param>
        void Trigger<TDomainEventData>(TDomainEventData domainEventData) where TDomainEventData : IDomainEventData;
    }
}
