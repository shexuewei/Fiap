using Eiap.Framework.Base.AssemblyService.SXW;
using Eiap.Framework.Base.Dependency.SXW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.DomainEvent.SXW.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyManager.Instance.Register(DependencyManager.Instance.Register).Register(DomainEventManager.Instance.Register);
            DomainEventManager.Instance.Trigger<ShManEventData>(new ShManEventData { Age = 10, manEventData = new ManEventData { Age = 11, Name = "11" }, Name = "10" });
            DomainEventManager.Instance.Trigger<ManEventData>(new ManEventData { Age = 12, Name = "12" });
            Console.ReadLine();
        }
    }

}
