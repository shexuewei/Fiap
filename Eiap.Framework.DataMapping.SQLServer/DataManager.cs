using Eiap.Framework.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.DataMapping.SQLServer
{
    //TODO:启动时，自动将所有领域对象加载进来
    public class DataManager
    {
        private static DataManager _DataManager;

        public static DataManager Instance
        {
            get 
            {
                if (_DataManager == null)
                {
                    _DataManager = new DataManager();
                }
                return _DataManager;
            }
        }

        private Dictionary<string, DataDescription> _DataDescriptionList;

        private DataManager()
        {
            _DataDescriptionList = new Dictionary<string, DataDescription>();
        }

        public DataDescription GetDataDescription(Type type)
        {
            DataDescription datadescription = null;
            if (_DataDescriptionList.ContainsKey(type.FullName))
            {
                datadescription = _DataDescriptionList[type.FullName];
            }
            else
            {
                datadescription = new DataDescription();
                datadescription.TableName = type.GetTableName();
                datadescription.PrimaryKeyName = type.GetPrimaryKeyName(datadescription.TableName);
                datadescription.PrimaryKeyParameterName = type.GetPrimaryKeyParameterName();
                datadescription.SelectAllSQL = GetSelectAllSQL(datadescription, type);
                datadescription.SelectSQL = GetSelectSQL(datadescription,type);
                datadescription.InsertSQL = GetInsertSQL(datadescription, type);
                datadescription.UpdateSQL = GetUpdateSQL(datadescription, type);
                datadescription.DeleteSQL = GetDeleteSQL(datadescription, type);
                datadescription.JoinSQL = GetJoinSQL(datadescription, type);
                _DataDescriptionList.Add(type.FullName, datadescription);
            }
            return datadescription;
        }

        private string GetSelectSQL(DataDescription datadescription, Type t)
        {

            return datadescription.SelectAllSQL + " where " + datadescription.PrimaryKeyName + " = @" + datadescription.PrimaryKeyParameterName;
        }

        private string GetSelectAllSQL(DataDescription datadescription, Type t)
        {
            string propertyInfostring = t.GetProperties().ToList().GetPropertyInfoToSelectSql(datadescription.TableName);
            return "select " + propertyInfostring + " from " + datadescription.TableName + " with(nolock) ";
        }

        private string GetInsertSQL(DataDescription datadescription, Type t)
        {
            string sql = "insert into " + datadescription.TableName + " ({0}) values ({1});";
            PropertyInfo[] pi = t.GetProperties();
            string fields = "";
            string values = "";
            foreach (PropertyInfo info in pi)
            {
                if (!info.IsComplexClass())
                {
                    if (info.IsPrimaryKey() && info.Name == typeof(int).Name)
                    {
                        continue;
                    }
                    fields += info.GetColumnName(datadescription.TableName) + ",";
                    values += "@" + info.Name + "_{0}" + ",";
                }
            }
            return string.Format(sql, fields.Substring(0, fields.Length - 1), values.Substring(0, values.Length - 1));
        }

        private string GetUpdateSQL(DataDescription datadescription, Type t)
        {
            string sql = "update " + datadescription.TableName + " set ";
            PropertyInfo[] pi = t.GetProperties();
            foreach (PropertyInfo info in pi)
            {
                if (!info.IsComplexClass())
                {
                    if (info.IsPrimaryKey())
                    {
                        continue;
                    }
                    sql += info.GetColumnName(datadescription.TableName)+ " = @" + info.Name + "_{0}" + ",";
                }
            }
            return sql = sql.Substring(0, sql.Length - 1) + " where " + datadescription.PrimaryKeyName + " = @" + datadescription.PrimaryKeyParameterName + "_{0};";
        }

        private string GetDeleteSQL(DataDescription datadescription, Type t)
        {
            return "delete " + datadescription.TableName + " where " + datadescription.PrimaryKeyName + " = @" + datadescription.PrimaryKeyParameterName + "_{0};";
        }

        private string GetJoinSQL(DataDescription datadescription, Type t)
        {
            string joinsql = "";
            List<PropertyInfo> propertyInfoList = t.GetProperties().Where(m => m.IsNavigationProperty()).ToList();
            if (propertyInfoList.Count > 0)
            {
                for (int i = 0; i < propertyInfoList.Count; i++)
                {
                    PropertyInfo m = propertyInfoList[i];
                    string tmpTableName = DataManager.Instance.GetDataDescription(m.PropertyType).TableName;
                    string tmpIndexTableName = tmpTableName.Replace("]", "_" + i.ToString() + "]");
                    string tmpKeyName = DataManager.Instance.GetDataDescription(m.PropertyType).PrimaryKeyName;
                    joinsql += " join (" + DataManager.Instance.GetDataDescription(m.PropertyType).SelectAllSQL + ") " + tmpIndexTableName + " on " + tmpKeyName.Replace(tmpTableName, tmpIndexTableName) + " = " + t.GetForeignKeyName(m.PropertyType);
                    joinsql += DataManager.Instance.GetDataDescription(m.PropertyType).JoinSQL.Replace(tmpTableName, tmpIndexTableName);
                }
            }
            return joinsql;
        }
    }
}
