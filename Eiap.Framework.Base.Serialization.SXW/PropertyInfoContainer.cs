using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public class PropertyInfoContainer
    {
        /// <summary>
        /// 实例类型Handle
        /// </summary>
        public RuntimeTypeHandle InstanceTypeHandle { get; set; }

        /// <summary>
        /// 属性类型Handle
        /// </summary>
        public RuntimeTypeHandle PropertyTypeHandle { get; set; }

        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 实例类型
        /// </summary>
        public Type InstanceType { get { return Type.GetTypeFromHandle(InstanceTypeHandle); } }

        /// <summary>
        /// 属性类型
        /// </summary>
        public Type PropertyType { get { return Type.GetTypeFromHandle(PropertyTypeHandle); } }
    }
}
