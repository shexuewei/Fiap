﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Eiap.Framework.Base.Extension;
using System.Diagnostics;

namespace Eiap.Framework.Base.AssemblyService
{
    public class AssemblyManager
    {
        private static AssemblyManager _AssemblyManager = null;

        private List<Assembly> assemblyList;

        private AssemblyManager()
        {
            assemblyList = new List<Assembly>();
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static AssemblyManager Instance
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
        public AssemblyManager Register(Action<List<Assembly>> reg)
        {
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            reg(assemblyList);
            //stopwatch.Stop();
            //Console.WriteLine("AssemblyManager Register:" + stopwatch.ElapsedMilliseconds);
            return Instance;
        }

        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public AssemblyManager RegisterAssembly(Assembly assembly)
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
        public AssemblyManager AssemblyInitialize()
        {
            var loadDllList = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory).Where(m => m.EndsWith(".dll") || m.EndsWith(".exe")).ToList();
            //初始化组件
            foreach (string dllname in loadDllList)
            {
                Assembly assembly = Assembly.LoadFile(dllname);
                ModuleInitialize(assembly);
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

        /// <summary>
        /// 组件模块初始化
        /// </summary>
        /// <param name="assembly"></param>
        private void ModuleInitialize(Assembly assembly)
        {
            Type componentModuleType = assembly.GetTypes().Where(m => typeof(IComponentModule).IsAssignableFrom(m)).FirstOrDefault();
            if (componentModuleType.IsNotNull() && !componentModuleType.IsInterface)
            {
                IComponentModule componentModule = (IComponentModule)Activator.CreateInstance(componentModuleType);
                componentModule.AssemblyInitialize();
            }
        }

        /// <summary>
        /// 模块初始化注册信息
        /// </summary>
        public void RegisterInitialize()
        {
            assemblyList.ForEach(assemblyItem => {
                Type componentModuleType = assemblyItem.GetTypes().Where(m => typeof(IComponentModule).IsAssignableFrom(m)).FirstOrDefault();
                if (componentModuleType.IsNotNull() && !componentModuleType.IsInterface)
                {
                    IComponentModule componentModule = (IComponentModule)Activator.CreateInstance(componentModuleType);
                    componentModule.RegisterInitialize();
                }
            });
        }
    }
}
