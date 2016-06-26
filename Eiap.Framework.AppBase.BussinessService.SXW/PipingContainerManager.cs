using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.BussinessService.SXW
{
    public class PipingContainerManager : IPipingContainerManager, ISingletonDependency
    {
        private List<PipingContainer> _pipingContainerList = null;

        public PipingContainerManager()
        {
            _pipingContainerList = new List<PipingContainer>();
        }

        /// <summary>
        /// 注册管道和业务上下文管理
        /// </summary>
        /// <param name="pipingImpType"></param>
        /// <param name="businessContextManagerImpType"></param>
        /// <returns></returns>
        public IPipingContainerManager RegisterPiping(Type pipingImpType, Type businessContextManagerImpType)
        {
            foreach (PipingContainer containerItem in _pipingContainerList)
            {
                if (containerItem.PipingName == pipingImpType.FullName)
                {
                    return this;
                }
            }
            _pipingContainerList.Add(new PipingContainer { PipingImpType = pipingImpType, BusinessContextManagerImpType = businessContextManagerImpType });
            return this;
        }

        /// <summary>
        /// 获取管道容器
        /// </summary>
        /// <param name="pipingImpType"></param>
        /// <returns></returns>
        public PipingContainer GetPipingContainer(Type pipingImpType)
        {
            foreach (PipingContainer containerItem in _pipingContainerList)
            {
                if (containerItem.PipingName == pipingImpType.FullName)
                {
                    return containerItem;
                }
            }
            return null;
        }

        /// <summary>
        /// 移除管道
        /// </summary>
        /// <param name="pipingImpType"></param>
        /// <returns></returns>
        public IPipingContainerManager RemovePiping(Type pipingImpType)
        {
            //TODO:暂时不需要 2016-04-05 shexuewei
            return this;
        }
    }
}
