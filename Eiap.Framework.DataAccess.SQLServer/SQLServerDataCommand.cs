using Eiap.Framework.Common.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.DataAccess.SQLServer
{
    public class SQLServerDataCommand : ISQLDataCommand
    {
        private ISQLCommandDataAccessConnection _SQLDataAccessConnection;

        public virtual int ExcuteNonQuery(string cmdText, System.Data.CommandType cmdType, System.Data.IDataParameter[] paramters)
        {
            int res = 0;
            try
            {
                IDbCommand _DbCommand = _SQLDataAccessConnection.CreateCommand();
                if (_SQLDataAccessConnection.IsTransaction)
                {
                    _DbCommand.Transaction = _SQLDataAccessConnection.GetTransaction();
                }
                res = _DbCommand.ExcuteCommand<int>(_DbCommand.ExecuteNonQuery, cmdText, cmdType, paramters);
            }
            catch (Exception ex)
            {
                if (_SQLDataAccessConnection.IsTransaction)
                {
                    _SQLDataAccessConnection.Rollback();
                }
                throw ex;
            }
            return res;
        }

        public virtual ISQLCommandDataAccessConnection SQLDataAccessConnection
        {
            set
            {
                _SQLDataAccessConnection = value;
            }

            get { return _SQLDataAccessConnection; }
        }
    }
}
