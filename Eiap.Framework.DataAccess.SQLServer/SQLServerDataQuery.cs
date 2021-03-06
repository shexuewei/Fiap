﻿using Eiap.Framework.Common.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.DataAccess.SQLServer
{
    public class SQLServerDataQuery : ISQLDataQuery
    {
        private ISQLDataQueryDataAccessConnection _SQLDataAccessConnection;

        public virtual IDataReader ExcuteGetDataReader(string cmdText, CommandType cmdType, IDataParameter[] paramters)
        {
            IDataReader dr = null;
            try
            {
                IDbCommand _DbCommand = _SQLDataAccessConnection.CreateCommand();
                dr = _DbCommand.ExcuteCommand<IDataReader>(_DbCommand.ExecuteReader, cmdText, cmdType, paramters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;
        }

        public virtual object ExecuteScalar(string cmdText, CommandType cmdType, IDataParameter[] paramters)
        {
            object retuObj = null;
            try
            {
                IDbCommand _DbCommand = _SQLDataAccessConnection.CreateCommand();
                retuObj = _DbCommand.ExcuteCommand<object>(_DbCommand.ExecuteScalar, cmdText, cmdType, paramters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retuObj;
        }

        public virtual ISQLDataQueryDataAccessConnection SQLDataAccessConnection
        {
            set
            {
                _SQLDataAccessConnection = value;
            }

            get { return _SQLDataAccessConnection; }
        }
    }
}
