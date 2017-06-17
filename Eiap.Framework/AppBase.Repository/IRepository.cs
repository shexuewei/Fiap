using Eiap.Framework.AppBase.UnitOfWork;
using Eiap.Framework.Base.Dependency;
using Eiap.Framework.Common.DataMapping.SQL;
using Eiap.Framework.Common.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.Repository
{
    public interface IRepository<tEntity, TPrimarykey> : IRepositoryCommit, IUnitOfWorkCommandConnection, IRealtimeDependency
        where tEntity : IEntity<TPrimarykey>
        where TPrimarykey : struct
    {
        tEntity Add(tEntity entity);

        tEntity Update(tEntity entity);

        void Delete(TPrimarykey primarykey);

        ISQLDataQueryMapping<tEntity, TPrimarykey> Query();

        TResult Query<TResult>(string cmdText, CommandType cmdType, IDataParameter[] paramters = null);
    }
}
