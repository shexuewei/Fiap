using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public static class JsonDeserializeProcess
    {
        public static object Deserialize(string jsonString, Type objectType, SerializationSetting setting)
        {
            Stack<char> jsonStringStack = new Stack<char>();
            Stack<object> objectStack = new Stack<object>();
            char[] jsonCharList = jsonString.ToCharArray();
            foreach (char charitem in jsonCharList)
            {
                if (charitem == Convert.ToChar(JsonSymbol.JsonArraySymbol_Begin))
                {
                    jsonStringStack.Push(charitem);
                    IList objectInstance = null;
                    if (objectType.IsGenericType)
                    {
                        Type genType = objectType.GetGenericTypeDefinition();
                        Type[] genParaType = objectType.GetGenericArguments();
                        Type objtype = genType.MakeGenericType(genParaType);
                        objectInstance = Activator.CreateInstance(objtype) as IList;
                    }
                    else
                    {
                        objectInstance = Activator.CreateInstance(objectType) as IList;
                    }
                    objectStack.Push(objectInstance);
                }
                else if (charitem == Convert.ToChar(JsonSymbol.JsonObjectSymbol_Begin))
                {

                }

            }



            if (typeof(IEnumerable).IsAssignableFrom(objectType) && objectType != typeof(String) && !typeof(IDictionary).IsAssignableFrom(objectType))
            {

            }
            else if (typeof(IDictionary).IsAssignableFrom(objectType))
            {

            }
            else if (objectType.IsNormalType())
            {
                return Process(jsonString, objectType, setting);
            }
            else
            {
                Activator.CreateInstance(objectType);
            }
            return null;
        }


        private static object Process(string jsonString, Type objectType, SerializationSetting setting)
        {
            if (objectType == typeof(DateTime))
            {
                return Convert.ToDateTime(jsonString);
            }
            else if (objectType == typeof(Int32))
            {
                return Convert.ToInt32(jsonString);
            }
            else if (objectType == typeof(Decimal))
            {
                return Convert.ToDecimal(jsonString);
            }
            else if (objectType == typeof(Guid))
            {
                return Guid.Parse(jsonString);
            }
            else if (objectType == typeof(String))
            {
                return jsonString;
            }
            else if (objectType == typeof(Boolean))
            {
                return Convert.ToBoolean(jsonString);
            }
            return null;
        }
    }
}
