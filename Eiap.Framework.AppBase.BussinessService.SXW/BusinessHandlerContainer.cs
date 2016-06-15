using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.BussinessService.SXW
{
    public class BusinessHandlerContainer
    {
        public string BusinessContextManagerImpName { get; set; }

        public Type BusinessContextManagerImpType { get; set; }

        public string BusinessHandlerImpName { get; set; }

        public Type BusinessHandlerImpType { get; set; }
    }
}
