﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public static class JsonProcessExtend
    {
        /// <summary>
        /// 判断是否常用类型
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public static bool IsNormalType(this Type objectType)
        {
            if (objectType == typeof(DateTime)
                        || objectType == typeof(Int32)
                        || objectType == typeof(String)
                        || objectType == typeof(Boolean)
                        || objectType == typeof(Decimal)
                        || objectType == typeof(Guid))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取集合数
        /// </summary>
        /// <param name="enumeratorList"></param>
        /// <returns></returns>
        public static int GetEnumeratorCount(this IEnumerator enumeratorList)
        {
            int objCount = 0;
            while (enumeratorList.MoveNext())
            {
                objCount++;
            }
            return objCount;
        }
    }
}
