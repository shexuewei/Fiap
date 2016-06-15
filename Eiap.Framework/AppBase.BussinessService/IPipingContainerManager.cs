using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.BussinessService
{
    public interface IPipingContainerManager : ISingletonDependency
    {
        /// <summary>
        /// 注册管道和业务上下文管理
        /// </summary>
        /// <param name="pipingImpType"></param>
        /// <param name="businessContextManagerImpType"></param>
        /// <returns></returns>
        IPipingContainerManager RegisterPiping(Type pipingImpType, Type businessContextManagerImpType);

        /// <summary>
        /// 获取管道容器
        /// </summary>
        /// <param name="pipingImpType"></param>
        /// <returns></returns>
        PipingContainer GetPipingContainer(Type pipingImpType);

        /// <summary>
        /// 移除管道
        /// </summary>
        /// <param name="pipingImpType"></param>
        /// <returns></returns>
        IPipingContainerManager RemovePiping(Type pipingImpType);
    }
}
