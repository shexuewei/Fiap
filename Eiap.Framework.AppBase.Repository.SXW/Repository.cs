using Eiap.Framework.AppBase.UnitOfWork;
using Eiap.Framework.Base.Dependency;
using Eiap.Framework.Base.Dependency.SXW;
using Eiap.Framework.Common.DataAccess.SQL;
using Eiap.Framework.Common.DataMapping.SQL;
using Eiap.Framework.Common.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.Repository.SXW
{
    public class Repository<tEntity, TPrimarykey> : IRepository<tEntity, TPrimarykey>
        where tEntity : IEntity<TPrimarykey>
        where TPrimarykey : struct
    {
        private List<tEntity> _AddEntityList;
        private List<tEntity> _UpdateEntityList;
        private List<TPrimarykey> _DeletePrimarykeyList;
        private ISQLCommandMapping<tEntity, TPrimarykey> _CommandMapping;
        private ISQLDataQueryMapping<tEntity, TPrimarykey> _QueryMapping;
        private ISQLQuery _SQLQuery;
        private ICurrentUnitOfWork _CurrentUnitOfWork;
        public Repository(ISQLCommandMapping<tEntity, TPrimarykey> CommandMapping,
            ISQLDataQueryMapping<tEntity, TPrimarykey> QueryMapping,
            ISQLQuery SQLQuery)
        {
            _CommandMapping = CommandMapping;
            _QueryMapping = QueryMapping;
            _SQLQuery = SQLQuery;
            _CurrentUnitOfWork = DependencyManager.Instance.Resolver<ICurrentUnitOfWork>();
            _CurrentUnitOfWork.CurrentUnitOfWork.SetRepository(this);
        }

        public virtual tEntity Add(tEntity entity)
        {
            if (_AddEntityList == null)
            {
                _AddEntityList = new List<tEntity>();
            }
            _AddEntityList.Add(entity);
            return entity;
        }

        public virtual tEntity Update(tEntity entity)
        {
            if (_UpdateEntityList == null)
            {
                _UpdateEntityList = new List<tEntity>();
            }
            _UpdateEntityList.Add(entity);
            return entity;
        }

        public virtual void Delete(TPrimarykey primarykey)
        {
            if (_DeletePrimarykeyList == null)
            {
                _DeletePrimarykeyList = new List<TPrimarykey>();
            }
            _DeletePrimarykeyList.Add(primarykey);
        }

        public virtual ISQLDataQueryMapping<tEntity, TPrimarykey> Query()
        {
            return _QueryMapping;
        }

        public virtual TResult Query<TResult>(string cmdText, System.Data.CommandType cmdType, System.Data.IDataParameter[] paramters = null)
        {
            TResult result = default(TResult);
            DataSet dataset = _SQLQuery.ExcuteGetDateSet(cmdText, cmdType, paramters);
            result = dataset.DataSetToEntityList<TResult>();
            return result;
        }

        public virtual void Commit()
        {
            try
            {
                if (_AddEntityList != null && _AddEntityList.Count > 0)
                {
                    _CommandMapping.BatchInsertEntity(_AddEntityList);
                }
                if (_UpdateEntityList != null && _UpdateEntityList.Count > 0)
                {
                    _CommandMapping.BatchUpdateEntity(_UpdateEntityList);
                }
                if (_DeletePrimarykeyList != null && _DeletePrimarykeyList.Count > 0)
                {
                    _CommandMapping.BatchDeleteEntity(_DeletePrimarykeyList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ISQLCommandDataAccessConnection SQLDataAccessConnection
        {
            set 
            { 
                _CommandMapping.SQLDataAccessConnection = value; 
            }
        }

        public virtual ICurrentUnitOfWork CurrentUnitOfWork
        {
            get { return _CurrentUnitOfWork; }
        }
    }
}
