using Eiap.Framework.Base.Interceptor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest.SXW
{
    public class UnitTestProxyInterceptor : IUnitTestProxyInterceptor
    {
        private readonly IUnitTestManager _UnitTestManager;
        public UnitTestProxyInterceptor(IUnitTestManager unitTestManager)
        {
            _UnitTestManager = unitTestManager;
        }

        public object Invoke(object instance, string interfaceTypeName, string name, object[] parameters)
        {
            object objres = null;
            MethodInfo methodinfo = null;
            try
            {
                if (instance != null)
                {
                    methodinfo = instance.GetType().GetMethod(name);
                    if (methodinfo != null)
                    {
                        List<UnitTestCaseContainer> unitTestCaseContainerList = _UnitTestManager.GetUnitTestCaseByInterfaceTypeNameAndMethodName(interfaceTypeName, methodinfo);
                        //TODO:循环执行测试方法和用例
                        objres = methodinfo.Invoke(instance, parameters);
                    }
                    else
                    {
                        //TODO:自定义异常
                        throw new Exception("No Method");
                    }
                }
                else
                {
                    //TODO:自定义异常
                    throw new Exception("No Instance Object");
                }
            }
            catch (Exception ex)
            {
                //TODO：异常处理
                throw ex;
            }
            return objres;
        }
    }
}
