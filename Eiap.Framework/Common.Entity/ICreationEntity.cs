using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.Entity
{
    public interface ICreationEntity<TUserId, TPrimarykey> : IEntity<TPrimarykey>
        where TUserId : struct
        where TPrimarykey : struct
    {
        DateTime CreateDate { get; set; }

        TUserId CreateUser { get; set; }
    }
}
