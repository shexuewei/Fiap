using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Eiap.Framework.Base.UnitTest.SXW
{
    public class UnitTestResultManager : IUnitTestResultManager
    {
        private readonly IUnitTestResultContainerManager _ContainerManager;

        public UnitTestResultManager(IUnitTestResultContainerManager unitTestResultContainerManager)
        {
            _ContainerManager = unitTestResultContainerManager;
        }

        /// <summary>
        /// 添加单元测试结果
        /// </summary>
        /// <param name="container"></param>
        public void AddUnitTestResult(UnitTestResultContainer container)
        {
            _ContainerManager.AddUnitTestResult(container);
        }

        /// <summary>
        /// 根据命名空间输出单元测试结果
        /// </summary>
        /// <param name="unitTestNamespace"></param>
        public void PrintUnitTestResultByNamespace(string unitTestNamespace)
        {
            List<UnitTestResultContainer> unitTestResultContainerList = _ContainerManager.GetUnitTestResultByNamespace(unitTestNamespace);
            foreach (UnitTestResultContainer unitTestResultContainer in unitTestResultContainerList)
            {
                Console.WriteLine("UnitTestNamespace:" + unitTestResultContainer.UnitTestNamespace);
                Console.WriteLine("ClassName:" + unitTestResultContainer.ClassName);
                Console.WriteLine("MethodName:" + unitTestResultContainer.MethodName);
                Console.WriteLine("MethodParas:" + JsonConvert.SerializeObject(unitTestResultContainer.MethodParas));
                Console.WriteLine("Result:" + unitTestResultContainer.Result);
                if (unitTestResultContainer.MethodException != null)
                {
                    Console.WriteLine("MethodExceptionErrorMessage:" + unitTestResultContainer.MethodException.Message);
                }
            }
        }
    }
}
