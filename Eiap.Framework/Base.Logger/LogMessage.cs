﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Logger
{
    public class LogMessage
    {
        public virtual LogHead LogHead { get; set; }

        public virtual LogBody LogBody { get; set; }
    }
}
