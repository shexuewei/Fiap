using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Dependency
{
    public abstract class DependencyContainerManagerBase : IDependencyContainerManager
    {
        protected static IDependencyContainerManager _DependencyContainerManager = null;
        //protected Dictionary<string, DependencyContainer> _DepeDicList = null;
        protected List<DependencyContainer> _DepeDicList = null;

        protected DependencyContainerManagerBase()
        {
            _DepeDicList = new List<DependencyContainer>();
            //_DepeDicList = new Dictionary<string, DependencyContainer>();
        }


        /// <summary>
        /// 判断是否注册过
        /// </summary>
        /// <param name="dependencyInterface"></param>
        /// <param name="dependencyInterfaceClass"></param>
        /// <returns></returns>
        protected bool IsExistSameDependencyInterfaceClass(Type dependencyInterface, Type dependencyInterfaceClass)
        {
            //return _DepeDicList.ContainsKey(dependencyInterfaceClass.FullName + "." + dependencyInterface.FullName);
            foreach (DependencyContainer container in _DepeDicList)
            {
                if (container.DependencyInterfaceClassName == dependencyInterfaceClass.FullName
                    && container.DependencyInterfaceName == dependencyInterface.FullName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 注册依赖接口和依赖实现类
        /// </summary>
        /// <param name="dependencyInterface"></param>
        /// <param name="dependencyInterfaceClass"></param>
        public virtual void RegisterDependencyInterfaceClass(Type dependencyInterface, Type dependencyInterfaceClass, bool isDirectRelation = false) { }


        /// <summary>
        /// 加载类和依赖实现类
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="interfaceTypeList"></param>
        public virtual void RegisterClassAndDependencyInterfaceClass(Type classType, List<Type> interfaceTypeList) { }

        /// <summary>
        /// 根据接口和实现类返回依赖容器
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public virtual DependencyContainer GetDependencyContainer(Type interfaceType, Type entityType = null) { return null; }
    }
}
