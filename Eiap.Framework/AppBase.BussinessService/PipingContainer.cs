using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.BussinessService
{
    public class PipingContainer
    {
        public string PipingName { get { return PipingImpType.FullName; } }

        public Type PipingImpType { get; set; }

        public Type BusinessContextManagerImpType { get; set; }
    }
}
