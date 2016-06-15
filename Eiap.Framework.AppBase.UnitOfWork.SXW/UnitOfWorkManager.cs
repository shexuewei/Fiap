using Eiap.Framework.AppBase.Repository;
using Eiap.Framework.Common.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.UnitOfWork.SXW
{
    public class UnitOfWorkManager : IUnitOfWork, IDisposable
    {
        List<IUnitOfWorkCommandConnection> resList = null;
        ISQLCommandDataAccessConnection _Con = null;

        public UnitOfWorkManager(ISQLCommandDataAccessConnection con)
        {
            _Con = con;
            resList = new List<IUnitOfWorkCommandConnection>();
        }

        public void SetRepository(IUnitOfWorkCommandConnection res)
        {
            res.SQLDataAccessConnection = _Con;
            resList.Add(res);
        }

        public void Commit(bool IsTransaction = false)
        {
            _Con.Create();
            _Con.DBOpen();
            if (IsTransaction)
            {
                _Con.BeginTransaction();
            }
            try
            {
                foreach (IRepositoryCommit res in resList)
                {
                    res.Commit();
                }
                if (IsTransaction)
                {
                    _Con.Commit();
                }
            }
            catch (Exception ex)
            {
                _Con.Rollback();
                throw ex;
            }
            finally
            {
                _Con.DBClose();
            }
        }

        public void Dispose()
        {
            //TODO:工作单元销毁时需要处理的
        }
    }
}
