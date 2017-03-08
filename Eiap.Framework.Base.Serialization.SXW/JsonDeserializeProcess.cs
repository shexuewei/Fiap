using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public static class JsonDeserializeProcess<T>
    {
        public static T Deserialize(string jsonString, SerializationSetting setting)
        {
            Type objectType = typeof(T);

            if (typeof(IEnumerable).IsAssignableFrom(objectType) && objectType != typeof(String) && !typeof(IDictionary).IsAssignableFrom(objectType))
            {

            }
            else if (typeof(IDictionary).IsAssignableFrom(objectType))
            {

            }
            else if (objectType.IsNormalType())
            {

            }
            else
            { }
            return default(T);
        }
    }
}
