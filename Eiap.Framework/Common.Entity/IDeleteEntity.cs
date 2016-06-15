using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.Entity
{
    public interface IDeleteEntity<TUserId, TPrimarykey> : IModifyEntity<TUserId, TPrimarykey>
        where TUserId : struct
        where TPrimarykey : struct
    {
        DateTime? DeleteDate { get; set; }

        TUserId? DeleteUser { get; set; }
    }
}
