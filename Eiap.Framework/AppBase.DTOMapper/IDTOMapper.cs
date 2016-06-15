using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.DTOMapper
{
    public interface IDTOMapper : IPropertyDependency, IRealtimeDependency
    {
        T Mapper<T>(object entity);

        T Mapper<T>(object entity, object mapperEntity);
    }
}
