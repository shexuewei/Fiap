using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.BussinessService
{
    public interface IBusinessHandlerContainerManager : ISingletonDependency
    {
        void Register(Type contextManagerType ,Type handler);

        List<Type> GetHandlerList(Type contextManagerType);

        void Remove(Type contextManagerType, Type handler);
    }
}
