﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest
{
    public class UnitTestResultContainer
    {
        /// <summary>
        /// 单元测试模块命名空间
        /// </summary>
        public string UnitTestNamespace { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        public Exception MethodException { get; set; }

        /// <summary>
        /// 测试结果
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public List<object> MethodParas { get; set; }

    }
}
