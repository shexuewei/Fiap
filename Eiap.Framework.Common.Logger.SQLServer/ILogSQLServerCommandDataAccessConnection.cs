using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Eiap.Framework.Common.DataAccess.SQL;
using Eiap.Framework.Base.Dependency;

namespace Eiap.Framework.Common.Logger.SXW
{
    public interface ILogSQLServerCommandDataAccessConnection : ISQLCommandDataAccessConnection
    {
    }
}
