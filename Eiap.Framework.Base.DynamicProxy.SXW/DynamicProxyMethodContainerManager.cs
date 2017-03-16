using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.DynamicProxy.SXW
{
    public class DynamicProxyMethodContainerManager : IDynamicProxyMethodContainerManager
    {
        private ConcurrentDictionary<string, DynamicProxyMethodContainer> _DynamicProxyMethodContainerList = null;

        public DynamicProxyMethodContainerManager()
        {
            _DynamicProxyMethodContainerList = new ConcurrentDictionary<string, DynamicProxyMethodContainer>();
        }

        /// <summary>
        /// 添加动态代理方法容器
        /// </summary>
        /// <param name="container"></param>
        public Func<object, object[], object> AddDynamicProxyContainer(string dynamicProxyMethodFullName, MethodInfo methodInfo)
        {
            DynamicProxyMethodContainer container = null;
            if (!_DynamicProxyMethodContainerList.ContainsKey(dynamicProxyMethodFullName))
            {
                container = new DynamicProxyMethodContainer
                {
                    DynamicProxyMethod = GetExecuteDelegate(methodInfo)
                };
                _DynamicProxyMethodContainerList.TryAdd(dynamicProxyMethodFullName, container);
            }
            else
            {
                container = _DynamicProxyMethodContainerList[dynamicProxyMethodFullName];
            }
            return container.DynamicProxyMethod;
        }

        /// <summary>
        /// 获取动态代理方法容器
        /// </summary>
        /// <param name="dynamicProxyMethodFullName"></param>
        /// <returns></returns>
        public Func<object, object[], object> GetDynamicProxyMethodByDynamicProxyMethodName(string dynamicProxyMethodFullName)
        {
            if (_DynamicProxyMethodContainerList.ContainsKey(dynamicProxyMethodFullName))
            {
                return _DynamicProxyMethodContainerList[dynamicProxyMethodFullName].DynamicProxyMethod;
            }
            return null;
        }

        /// <summary>
        /// 根据MethodInfo转表达式树，并构造成委托
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        private Func<object, object[], object> GetExecuteDelegate(MethodInfo methodInfo)
        {
            ParameterExpression instanceParameter = Expression.Parameter(typeof(object), "instance");
            ParameterExpression parametersParameter = Expression.Parameter(typeof(object[]), "parameters");
            List<Expression> parameterExpressions = new List<Expression>();
            ParameterInfo[] paramInfos = methodInfo.GetParameters();
            for (int i = 0; i < paramInfos.Length; i++)
            {
                BinaryExpression valueObj = Expression.ArrayIndex(parametersParameter, Expression.Constant(i));
                UnaryExpression valueCast = Expression.Convert(valueObj, paramInfos[i].ParameterType);
                parameterExpressions.Add(valueCast);
            }
            Expression instanceCast = methodInfo.IsStatic ? null : Expression.Convert(instanceParameter, methodInfo.ReflectedType);
            MethodCallExpression methodCall = Expression.Call(instanceCast, methodInfo, parameterExpressions);
            if (methodCall.Type == typeof(void))
            {
                Expression<Action<object, object[]>> lambda = Expression.Lambda<Action<object, object[]>>(methodCall, instanceParameter, parametersParameter);

                Action<object, object[]> execute = lambda.Compile();
                return (instance, parameters) =>
                {
                    execute(instance, parameters);
                    return null;
                };
            }
            else
            {
                {
                    UnaryExpression castMethodCall = Expression.Convert(methodCall, typeof(object));
                    Expression<Func<object, object[], object>> lambda = Expression.Lambda<Func<object, object[], object>>(castMethodCall, instanceParameter, parametersParameter);
                    return lambda.Compile();
                }
            }
        }
    }
}
