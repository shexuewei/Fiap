using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public class DeserializeObjectContainer
    {
        /// <summary>
        /// 容器存储类型
        /// </summary>
        public DeserializeObjectContainerType ContainerType { get; set; }

        /// <summary>
        /// 容器存储对象
        /// </summary>
        public object ContainerObject { get; set; }
    }
}
