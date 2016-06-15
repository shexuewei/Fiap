using Eiap.Framework.Common.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.DataAccess.SQLServer
{
    public class DataAccessConnectionString : ISQLDataAccessConnectionString
    {
        public DataAccessConnectionString()
        { }

        public virtual string Command()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        public virtual string Query()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        public virtual string DataQuery()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        public virtual string Default()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }
    }
}
