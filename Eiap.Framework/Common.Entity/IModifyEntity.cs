using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.Entity
{
    public interface IModifyEntity<TUserId, TPrimarykey> : ICreationEntity<TUserId, TPrimarykey>
        where TUserId : struct
        where TPrimarykey : struct
    {
        DateTime? ModifyDate { get; set; }

        TUserId? ModifyUser { get; set; }
    }
}
