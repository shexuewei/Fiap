using Eiap.Framework.Common.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.UnitOfWork
{
    public interface IUnitOfWorkCommandConnection
    {
        ISQLCommandDataAccessConnection SQLDataAccessConnection { set; }
    }
}
