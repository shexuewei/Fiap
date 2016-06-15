using Eiap.Framework.Common.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.Logger.SXW
{
    public class LogDataAccessConnectionString : ILogSQLDataAccessConnectionString
    {
        public LogDataAccessConnectionString()
        { }

        public virtual string Command()
        {
            return ConfigurationManager.ConnectionStrings["Log"].ConnectionString;
        }

        public virtual string Query()
        {
            return ConfigurationManager.ConnectionStrings["Log"].ConnectionString;
        }

        public virtual string DataQuery()
        {
            return ConfigurationManager.ConnectionStrings["Log"].ConnectionString;
        }

        public virtual string Default()
        {
            return ConfigurationManager.ConnectionStrings["Log"].ConnectionString;
        }
    }
}
