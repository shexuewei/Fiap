using Eiap.Framework.Common.DataMapping.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.Entity
{
    public interface IEntity<TPrimarykey> : IEntity where TPrimarykey : struct
    {
        [Property("Id", IsPrimaryKey = true)]
        TPrimarykey Id { get; set; }
    }

    public interface IEntity
    { }
}
