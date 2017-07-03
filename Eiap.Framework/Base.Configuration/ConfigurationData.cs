﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Configuration
{
    public class ConfigurationData
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 环境
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 配置值
        /// </summary>
        public string Value { get; set; }
    }
}
