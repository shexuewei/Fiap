﻿using Eiap.Framework.Common.DataAccess.SQL;
using Eiap.Framework.Common.DataMapping.SQL;
using Eiap.Framework.Common.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.DataMapping.SQLServer
{
    //TODO:目前主键只能是Guid，需要扩展
    public class SQLServerCommandMapping<tEntity, TPrimarykey> : ISQLCommandMapping<tEntity, TPrimarykey>
        where tEntity : IEntity<TPrimarykey>
        where TPrimarykey : struct
    {

        private readonly ISQLDataCommand _SQLDataCommand;
        private readonly ISQLDataMappingExtension<tEntity, TPrimarykey> _SQLDataMappingExtension;
        private const int _DefaultIndex = 0;
        public SQLServerCommandMapping(ISQLDataCommand SQLDataCommand, 
            ISQLDataMappingExtension<tEntity, TPrimarykey> SQLDataMappingExtension)
        {
            _SQLDataCommand = SQLDataCommand;
            _SQLDataMappingExtension = SQLDataMappingExtension;
        }

        public virtual int InsertEntity(tEntity entity)
        {
            int eff = 0;
            try
            {
                string insertSql = string.Format(GetInsertSQL(), _DefaultIndex.ToString());
                IDataParameter[] para = _SQLDataMappingExtension.GetDataParameter(entity, _DefaultIndex);
                eff = _SQLDataCommand.ExcuteNonQuery(insertSql, CommandType.Text, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return eff;
        }

        public virtual int UpdateEntity(tEntity entity)
        {
            int eff = 0;
            try
            {
                string updateSql = string.Format(GetUpdateSQL(), _DefaultIndex.ToString());
                IDataParameter[] para = _SQLDataMappingExtension.GetDataParameter(entity, _DefaultIndex);
                eff = _SQLDataCommand.ExcuteNonQuery(updateSql, CommandType.Text, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return eff;
        }

        public virtual int DeleteEntity(TPrimarykey Id)
        {
            int eff = 0;
            try
            {
                string deleteSql = string.Format(GetDeleteSQL(), _DefaultIndex.ToString());
                IDataParameter[] para = new SqlParameter[] { new SqlParameter() { ParameterName = "@" + GetPrimaryKeyName() + "_" + _DefaultIndex, Value = Id } };
                eff = _SQLDataCommand.ExcuteNonQuery(deleteSql, CommandType.Text, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return eff;
        }

        public ISQLCommandDataAccessConnection SQLDataAccessConnection
        {
            set { _SQLDataCommand.SQLDataAccessConnection = value; }
            get { return _SQLDataCommand.SQLDataAccessConnection; }
        }

        private string GetInsertSQL()
        {
            return DataManager.Instance.GetDataDescription(typeof(tEntity)).InsertSQL;
        }

        private string GetUpdateSQL()
        {
            return DataManager.Instance.GetDataDescription(typeof(tEntity)).UpdateSQL;
        }

        private string GetDeleteSQL()
        {
            return DataManager.Instance.GetDataDescription(typeof(tEntity)).DeleteSQL;
        }

        private string GetPrimaryKeyName()
        {
            return DataManager.Instance.GetDataDescription(typeof(tEntity)).PrimaryKeyName;
        }


        public int BatchInsertEntity(List<tEntity> tEntityList)
        {
            int eff = 0;
            string insertSql = "";
            List<IDataParameter> paraList = new List<IDataParameter>();
            try
            {
                for (int i = 0; i < tEntityList.Count; i++)
                {
                    insertSql += string.Format(GetInsertSQL(), i.ToString());
                    paraList.AddRange(_SQLDataMappingExtension.GetDataParameter(tEntityList[i], i));
                }
                eff = _SQLDataCommand.ExcuteNonQuery(insertSql, CommandType.Text, paraList.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return eff;
        }

        public int BatchUpdateEntity(List<tEntity> tEntityList)
        {
            int eff = 0;
            string updateSql = "";
            List<IDataParameter> paraList = new List<IDataParameter>();
            try
            {
                for (int i = 0; i < tEntityList.Count; i++)
                {
                    updateSql += string.Format(GetUpdateSQL(), i.ToString());
                    paraList.AddRange(_SQLDataMappingExtension.GetDataParameter(tEntityList[i], i));
                }
                eff = _SQLDataCommand.ExcuteNonQuery(updateSql, CommandType.Text, paraList.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return eff;
        }

        public int BatchDeleteEntity(List<TPrimarykey> idList)
        {
            int eff = 0;
            string deleteSql = "";
            List<IDataParameter> paraList = new List<IDataParameter>();
            try
            {
                for (int i = 0; i < idList.Count; i++)
                {
                    deleteSql += string.Format(GetDeleteSQL(), i.ToString());
                    paraList.Add(new SqlParameter() { ParameterName = "@" + GetPrimaryKeyName() + "_" + _DefaultIndex, Value = idList[i] });
                }

                eff = _SQLDataCommand.ExcuteNonQuery(deleteSql, CommandType.Text, paraList.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return eff;
        }
    }
}
