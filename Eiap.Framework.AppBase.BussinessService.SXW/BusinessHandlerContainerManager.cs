using Eiap.Framework.AppBase.BussinessService;
using Eiap.Framework.Common.Logger;
using Eiap.Framework.Base.Dependency.SXW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.BussinessService.SXW
{
    public class BusinessHandlerContainerManager : IBusinessHandlerContainerManager
    {
        private List<BusinessHandlerContainer> _businessHandlerContainerList = null;

        public BusinessHandlerContainerManager()
        {
            _businessHandlerContainerList = new List<BusinessHandlerContainer>();
        }

        public void Register(Type contextManagerType, Type handler)
        {
            foreach (BusinessHandlerContainer containerItem in _businessHandlerContainerList)
            {
                if (containerItem.BusinessContextManagerImpName == contextManagerType.FullName && containerItem.BusinessHandlerImpName == handler.FullName)
                {
                    return;
                }
            }
            _businessHandlerContainerList.Add(new BusinessHandlerContainer { BusinessContextManagerImpName = contextManagerType.FullName, BusinessContextManagerImpType = contextManagerType, BusinessHandlerImpType = handler, BusinessHandlerImpName = handler.FullName });
        }

        public List<Type> GetHandlerList(Type contextManagerType)
        {
            List<Type> handlerTypeList = new List<Type>();
            foreach (BusinessHandlerContainer containerItem in _businessHandlerContainerList)
            {
                if (containerItem.BusinessContextManagerImpName == contextManagerType.FullName)
                {
                    handlerTypeList.Add(containerItem.BusinessHandlerImpType);
                }
            }
            return handlerTypeList;
        }

        public void Remove(Type contextManagerType, Type handler)
        {
            //TODO:暂时不需要 2016-04-05 shexuewei
        }
    }
}
