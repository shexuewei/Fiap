using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Logger.LocalFile
{
    public class LoggerConfigeration : ILoggerConfigeration
    {
        private readonly string _LogPathFormat;
        private readonly long _LogSize;

        public LoggerConfigeration()
        {
            string tmpLogPathFormat = ConfigurationManager.AppSettings["LogPathFormat"];
            //TODO:需要验证格式
            if (tmpLogPathFormat == null || tmpLogPathFormat == "")
            {
                tmpLogPathFormat = @"c:\loggers\{AppCode}\{LogLevel}\{YYYY}\{MM}\{DD}\{HH}.log";
            }
            _LogPathFormat = tmpLogPathFormat;

            string tmpLogSize = ConfigurationManager.AppSettings["LogSize"];
            //TODO:需要验证格式
            if (tmpLogSize == null || tmpLogSize == "")
            {
                tmpLogSize = "204800000";
            }
            _LogSize = long.Parse(tmpLogSize);
        }

        public string LogPathFormat 
        {
            get 
            {
                return _LogPathFormat;
            }
        }

        public long LogSize
        {
            get
            {
                return _LogSize;
            }
        }
    }
}
