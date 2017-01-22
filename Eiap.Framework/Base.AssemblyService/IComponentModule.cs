using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.AssemblyService
{
    public interface IComponentModule
    {
        /// <summary>
        /// 初始化程序集
        /// </summary>
        void AssemblyInitialize();

        /// <summary>
        /// 初始化注册信息
        /// </summary>
        void RegisterInitialize();
    }
}
