using Eiap.Framework.Base.AssemblyService.SXW;
using Eiap.Framework.Base.Dependency.SXW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.Logger.SQLServer.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyManager.Instance.RegisterAssembly(@"C:\MyWork\EiapV3.0\Eiap.Framework\Eiap.Framework.Common.Logger.SXW.Test\bin\Debug").Register(DependencyManager.Instance.Register);
            var logger = (LoggerTest)DependencyManager.Instance.Resolver(typeof(LoggerTest));
            logger.logger.Info("", "");
        }
    }

    public class LoggerTest
    {
        public ILogger logger { get; set; }
    }
}
