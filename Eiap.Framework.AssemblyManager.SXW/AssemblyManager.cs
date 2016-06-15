using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.AssemblyService.SXW
{
    public class AssemblyManager : IAssemblyManager
    {
        private static IAssemblyManager _AssemblyManager = null;

        private List<Assembly> assemblyList;

        private AssemblyManager()
        {
            assemblyList = new List<Assembly>();
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static IAssemblyManager Instance
        {
            get
            {
                if (_AssemblyManager == null)
                {
                    _AssemblyManager = new AssemblyManager();
                }
                return _AssemblyManager;
            }
        }

        /// <summary>
        /// 处理程序集
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        public IAssemblyManager Register(Action<List<Assembly>> reg)
        {
            reg(assemblyList);
            return Instance;
        }

        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public IAssemblyManager RegisterAssembly(Assembly assembly)
        {
            if (!IsExistSameAssembly(assembly))
            {
                assemblyList.Add(assembly);
            }
            return Instance;
        }

        /// <summary>
        /// 根据程序集路径注册程序集
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <returns></returns>
        public IAssemblyManager RegisterAssembly(string assemblyPath)
        {
            var loadDllList = Directory.GetFiles(assemblyPath).Where(m => m.EndsWith(".dll") || m.EndsWith(".exe")).ToList();
            foreach (string dllname in loadDllList)
            {
                Assembly assembly = Assembly.LoadFile(dllname);
                RegisterAssembly(assembly);
            }
            return Instance;
        }

        /// <summary>
        /// 判断是否存在相同程序集
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private bool IsExistSameAssembly(Assembly assembly)
        {
            foreach (Assembly assemblyItem in assemblyList)
            {
                if (assemblyItem.FullName == assembly.FullName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
