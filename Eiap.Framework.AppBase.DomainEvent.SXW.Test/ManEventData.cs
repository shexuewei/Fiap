using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.DomainEvent.SXW.Test
{
    public class ManEventData : IDomainEventData
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class ShManEventData : IDomainEventData
    {
        public ManEventData manEventData { get; set; }

        public string Name { get; set; }
        public int Age { get; set; } 
    }
}
