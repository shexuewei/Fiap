using Eiap.Framework.Common.DataMapping.SQL;
using Eiap.Framework.Common.Entity;
using System;
using System.Collections;
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
    public class SQLDataMappingExtension<tEntity, TPrimarykey> : ISQLDataMappingExtension<tEntity, TPrimarykey>
        where tEntity : IEntity<TPrimarykey>
        where TPrimarykey : struct
    {
        public string GetOperationValue(Expression expr)
        {
            switch (expr.NodeType)
            {
                case ExpressionType.Equal:
                    return " = ";
                case ExpressionType.GreaterThan:
                    return " > ";
                case ExpressionType.GreaterThanOrEqual:
                    return " >= ";
                case ExpressionType.LessThan:
                    return " < ";
                case ExpressionType.LessThanOrEqual:
                    return " <= ";
                case ExpressionType.NotEqual:
                    return " != ";
                case ExpressionType.Call:
                    return " like '%'+{0}+'%' ";
                default:
                    return "";
            }
        }

        public IDataParameter[] GetDataParameter(tEntity entity, int index = 0)
        {
            PropertyInfo[] pi = entity.GetType().GetProperties().Where(prop => !prop.IsComplexClass()).ToArray();
            IDataParameter[] para = new SqlParameter[pi.Length];
            for (int i = 0; i < pi.Length; i++)
            {
                object objvalue = pi[i].GetValue(entity, null);
                if (objvalue == null)
                {

                    para[i] = new SqlParameter("@" + pi[i].Name + "_" + index, DBNull.Value);
                }
                else
                {
                    para[i] = new SqlParameter("@" + pi[i].Name + "_" + index, objvalue);
                }
            }
            return para;
        }
    }
}
