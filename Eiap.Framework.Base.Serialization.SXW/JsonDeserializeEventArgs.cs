using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public class JsonDeserializeEventArgs
    {
        public Stack<char> JsonStringStack { get; set; }

        public Stack<DeserializeObjectContainer> ContainerStack { get; set; }

        public Type RootType { get; set; }

        public char CurrentCharItem { get; set; }
    }
}
