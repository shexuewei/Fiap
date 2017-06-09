using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public static class JsonDeserializeProcess
    {
        public static event EventHandler<JsonDeserializeEventArgs> JsonDeserializeArraySymbol_Begin_Event;
        public static event EventHandler<JsonDeserializeEventArgs> JsonDeserializeArraySymbol_End_Event;
        public static event EventHandler<JsonDeserializeEventArgs> JsonDeserializeObjectSymbol_Begin_Event;
        public static event EventHandler<JsonDeserializeEventArgs> JsonDeserializeObjectSymbol_End_Event;
        public static event EventHandler<JsonDeserializeEventArgs> JsonDeserializePropertySymbol_Event;
        public static event EventHandler<JsonDeserializeEventArgs> JsonDeserializeSeparateSymbol_Event;

        public static object Deserialize(string jsonString, Type objectType, SerializationSetting setting)
        {
            Stack<char> jsonStringStack = new Stack<char>();
            Stack<DeserializeObjectContainer> containerStack = new Stack<DeserializeObjectContainer>();
            JsonDeserializeEventArgs args = new JsonDeserializeEventArgs { RootType = objectType, ContainerStack = containerStack, JsonStringStack = jsonStringStack };
            char[] jsonCharList = jsonString.ToCharArray();
            foreach (char charitem in jsonCharList)
            {
                jsonStringStack.Push(charitem);
                args.CurrentCharItem = charitem;
                //数组开始
                if (charitem == Convert.ToChar(JsonSymbol.JsonArraySymbol_Begin))
                {
                    if (JsonDeserializeArraySymbol_Begin_Event != null)
                    {
                        JsonDeserializeArraySymbol_Begin_Event(null, args);
                    }
                }
                //数组结束
                if (charitem == Convert.ToChar(JsonSymbol.JsonArraySymbol_End))
                {
                    if (JsonDeserializeArraySymbol_End_Event != null)
                    {
                        JsonDeserializeArraySymbol_End_Event(null, args);
                    }
                }
                //对象开始
                else if (charitem == Convert.ToChar(JsonSymbol.JsonObjectSymbol_Begin))
                {
                    if (JsonDeserializeObjectSymbol_Begin_Event != null)
                    {
                        JsonDeserializeObjectSymbol_Begin_Event(null, args);
                    }
                }
                //属性名
                else if (charitem == Convert.ToChar(JsonSymbol.JsonPropertySymbol) && IsPropertyHandler(args))
                {
                    if (JsonDeserializePropertySymbol_Event != null)
                    {
                        JsonDeserializePropertySymbol_Event(null, args);
                    }
                }
                //对象结束
                else if (charitem == Convert.ToChar(JsonSymbol.JsonObjectSymbol_End))
                {
                    if (JsonDeserializeObjectSymbol_End_Event != null)
                    {
                        JsonDeserializeObjectSymbol_End_Event(null, args);
                    }
                }
                //逗号
                else if (charitem == Convert.ToChar(JsonSymbol.JsonSeparateSymbol))
                {
                    if (JsonDeserializeSeparateSymbol_Event != null)
                    {
                        JsonDeserializeSeparateSymbol_Event(null, args);
                    }
                }
            }
            return containerStack.Pop().ContainerObject;
        }

        /// <summary>
        /// 数组开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void JsonDeserializeProcess_JsonDeserializeArraySymbol_Begin_Event(object sender, JsonDeserializeEventArgs e)
        {
            e.JsonStringStack.Pop();//]出栈
            Type currentObjectType = null;
            if (e.ContainerStack.Count() == 0)
            {
                currentObjectType = e.RootType;
            }
            else
            {
                DeserializeObjectContainer container = e.ContainerStack.Peek();
                if (container.ContainerType == DeserializeObjectContainerType.Property)
                {
                    PropertyInfo currentPropertyInfo = container.ContainerObject as PropertyInfo;
                    if (currentPropertyInfo != null)
                    {
                        currentObjectType = currentPropertyInfo.PropertyType;
                    }
                }
            }
            IList objectInstance = null;
            if (currentObjectType.IsGenericType)
            {
                Type genType = currentObjectType.GetGenericTypeDefinition();
                Type[] genParaType = currentObjectType.GetGenericArguments();
                Type objtype = typeof(List<>).MakeGenericType(genParaType);
                objectInstance = Activator.CreateInstance(objtype) as IList;
            }
            else if (currentObjectType.IsArray)
            {
                Type arrayElementType = currentObjectType.GetElementType();
                Type[] genParaType = new Type[] { arrayElementType };
                Type objtype = typeof(List<>).MakeGenericType(genParaType);
                objectInstance = Activator.CreateInstance(objtype) as IList;
            }
            else
            {
                objectInstance = Activator.CreateInstance(currentObjectType) as IList;
            }
            e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.List, ContainerObject = objectInstance });
        }

        /// <summary>
        /// 数组结束事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void JsonDeserializeProcess_JsonDeserializeArraySymbol_End_Event(object sender, JsonDeserializeEventArgs e)
        {
            e.JsonStringStack.Pop();//]出栈
            DeserializeObjectContainer currentObjectContainer = e.ContainerStack.Pop();
            PropertyInfo currentPropertyInfo = null;
            object objvalue = null;
            IList listvalue = null;
            if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.Object)
            {
                objvalue = currentObjectContainer.ContainerObject;
                currentObjectContainer = e.ContainerStack.Peek();
                if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.List)
                {
                    listvalue = currentObjectContainer.ContainerObject as IList;
                    listvalue.Add(objvalue);
                    //currentObjectContainer = e.ContainerStack.Pop();
                    //if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.Property)
                    //{
                    //    objvalue = listvalue;
                    //    currentPropertyInfo = currentObjectContainer.ContainerObject as PropertyInfo;
                    //    currentPropertyInfo.SetValue(e.ContainerStack.Peek().ContainerObject, objvalue);
                    //}
                }
            }
            else if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.List)
            {
                string valuestring = GetValueContainerByPropertyType(e.JsonStringStack);
                listvalue = currentObjectContainer.ContainerObject as IList;
                listvalue.Add(valuestring);
                e.ContainerStack.Push(currentObjectContainer);
                //currentObjectContainer = e.ContainerStack.Peek();
                //if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.Property)
                //{
                //    currentPropertyInfo = currentObjectContainer.ContainerObject as PropertyInfo;
                //    Type currentPropertyType = currentPropertyInfo.PropertyType;
                //    if (currentPropertyType.IsArray)
                //    {
                //        objvalue = IListToArray(listvalue);
                //        currentPropertyInfo.SetValue(e.ContainerStack.Peek().ContainerObject, objvalue);
                //    }
                //}
            }

        }

        /// <summary>
        /// 对象开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void JsonDeserializeProcess_JsonDeserializeObjectSymbol_Begin_Event(object sender, JsonDeserializeEventArgs e)
        {
            Type currentObjectType = null;
            if (e.ContainerStack.Count() == 0)
            {
                currentObjectType = e.RootType;
            }
            else
            {
                DeserializeObjectContainer container = e.ContainerStack.Peek();
                if (container.ContainerType == DeserializeObjectContainerType.Property)
                {
                    PropertyInfo currentPropertyInfo = container.ContainerObject as PropertyInfo;
                    if (currentPropertyInfo != null)
                    {
                        currentObjectType = currentPropertyInfo.PropertyType;
                    }
                }
                else if (container.ContainerType == DeserializeObjectContainerType.List)
                {
                    currentObjectType = container.ContainerObject.GetType().GetGenericArguments()[0];
                }
            }
            object objectInstance = Activator.CreateInstance(currentObjectType);
            e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Object, ContainerObject = objectInstance });
            e.JsonStringStack.Pop();
        }

        /// <summary>
        /// 对象结束事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void JsonDeserializeProcess_JsonDeserializeObjectSymbol_End_Event(object sender, JsonDeserializeEventArgs e)
        {
            e.JsonStringStack.Pop();//}出栈
            DeserializeObjectContainer currentObjectContainer = e.ContainerStack.Pop();
            PropertyInfo currentPropertyInfo = null;
            object objvalue = null;

            if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.Object 
                || currentObjectContainer.ContainerType == DeserializeObjectContainerType.List)
            {
                objvalue = currentObjectContainer.ContainerObject;
                currentObjectContainer = e.ContainerStack.Pop();
                currentPropertyInfo = currentObjectContainer.ContainerObject as PropertyInfo;
                if (currentPropertyInfo != null)
                {
                    currentPropertyInfo.SetValue(e.ContainerStack.Peek().ContainerObject, objvalue);
                }
            }
            else if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.DictionaryKey)
            {
                IDictionary dic = e.ContainerStack.Peek().ContainerObject as IDictionary;
                string valuestring = GetValueContainerByPropertyType(e.JsonStringStack);
                dic.Add(currentObjectContainer.ContainerObject, valuestring);
            }
            else if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.Property)
            {
                Type currentPropertyType = null;
                currentPropertyInfo = currentObjectContainer.ContainerObject as PropertyInfo;
                if (currentPropertyInfo.PropertyType.IsGenericType)
                {
                    currentPropertyType = currentPropertyInfo.PropertyType.GetGenericArguments()[0];
                }
                else
                {
                    currentPropertyType = currentPropertyInfo.PropertyType;
                }
                string valuestring = GetValueContainerByPropertyType(e.JsonStringStack);
                if (currentPropertyType == typeof(int))
                {
                    if (valuestring != "null")
                    {
                        objvalue = int.Parse(valuestring);
                    }
                }
                else if (currentPropertyType == typeof(string))
                {
                    if (valuestring != "null")
                    {
                        objvalue = valuestring;
                    }
                }
                else if (currentPropertyType == typeof(DateTime))
                {
                    if (valuestring != "null")
                    {
                        objvalue = DateTime.Parse(valuestring);
                    }
                }
                else if (currentPropertyType == typeof(decimal))
                {
                    if (valuestring != "null")
                    {
                        objvalue = Decimal.Parse(valuestring);
                    }
                }
                else if (currentPropertyType == typeof(bool))
                {
                    if (valuestring != "null")
                    {
                        objvalue = bool.Parse(valuestring);
                    }
                }
                currentPropertyInfo.SetValue(e.ContainerStack.Peek().ContainerObject, objvalue);
            }
        }

        private static string GetValueContainerByPropertyType(Stack<char> jsonStringStack)
        {
            List<char> valuestring = new List<char>();
            int count = -1;
            while (jsonStringStack.Count > 0)
            {
                count++;
                char valueSymbol = jsonStringStack.Pop();
                if ((count == 0 && valueSymbol == Convert.ToChar(JsonSymbol.JsonQuotesSymbol))
                    || (jsonStringStack.Count == 0 && valueSymbol == Convert.ToChar(JsonSymbol.JsonQuotesSymbol)))
                {
                    continue;
                }
                else
                {
                    valuestring.Insert(0, valueSymbol);
                }
            }
            return new string(valuestring.ToArray());
        }

        /// <summary>
        /// 属性事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void JsonDeserializeProcess_JsonDeserializePropertySymbol_Event(object sender, JsonDeserializeEventArgs e)
        {
            e.JsonStringStack.Pop();//属性分隔符出栈
            List<char> propertyNameList = new List<char>();
            
            //属性引号出栈
            while (true)
            {
                char beginQuotes = e.JsonStringStack.Pop();
                if (beginQuotes == Convert.ToChar(JsonSymbol.JsonQuotesSymbol))
                {
                    break;
                }
            }
            //属性引号出栈
            while (true)
            {
                char propertyNameChar = e.JsonStringStack.Pop();
                if (propertyNameChar == Convert.ToChar(JsonSymbol.JsonQuotesSymbol))
                {
                    break;
                }
                else if(propertyNameChar != Convert.ToChar(JsonSymbol.JsonSpaceSymbol))
                {
                    propertyNameList.Insert(0, propertyNameChar);
                }
            }
            string propertyNameStr = new string(propertyNameList.ToArray());
            if (propertyNameStr == "Dict")
            { }
            DeserializeObjectContainer currentObj = e.ContainerStack.Peek() as DeserializeObjectContainer;
            if (currentObj != null)
            {
                if (typeof(IDictionary).IsAssignableFrom(currentObj.ContainerObject.GetType()))
                {
                    e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.DictionaryKey, ContainerObject = propertyNameStr });
                }
                else
                {
                    PropertyInfo propertyinfo = GetCurrentObject(e).ContainerObject.GetType().GetProperty(propertyNameStr);
                    e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Property, ContainerObject = propertyinfo });
                }
            }
        }

        /// <summary>
        /// 逗号事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void JsonDeserializeProcess_JsonDeserializeSeparateSymbol_Event(object sender, JsonDeserializeEventArgs e)
        {
            e.JsonStringStack.Pop();//,出栈
            DeserializeObjectContainer currentObjectContainer = e.ContainerStack.Pop();
            PropertyInfo currentPropertyInfo = null;
            object objvalue = null;
            IList listvalue = null;
            if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.List)
            {
                listvalue = currentObjectContainer.ContainerObject as IList;
                if (e.JsonStringStack.Count > 0)
                {
                    string valuestring = GetValueContainerByPropertyType(e.JsonStringStack);
                    listvalue.Add(valuestring);
                    e.ContainerStack.Push(currentObjectContainer);
                }
                else
                {
                    objvalue = IListToArray(listvalue);
                    currentObjectContainer = e.ContainerStack.Pop();
                    if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.Property)
                    {
                        currentPropertyInfo = currentObjectContainer.ContainerObject as PropertyInfo;
                        currentPropertyInfo.SetValue(e.ContainerStack.Peek().ContainerObject, objvalue);
                    }
                }
            }
            else if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.DictionaryKey)
            {
                IDictionary dic = e.ContainerStack.Peek().ContainerObject as IDictionary;
                string valuestring = GetValueContainerByPropertyType(e.JsonStringStack);
                dic.Add(currentObjectContainer.ContainerObject, valuestring);
            }
            else if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.Object)
            {
                objvalue = currentObjectContainer.ContainerObject;
                currentObjectContainer = e.ContainerStack.Pop();
                if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.Property)
                {
                    currentPropertyInfo = currentObjectContainer.ContainerObject as PropertyInfo;
                    currentPropertyInfo.SetValue(e.ContainerStack.Peek().ContainerObject, objvalue);
                }
                else if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.List)
                {
                    listvalue = currentObjectContainer.ContainerObject as IList;
                    listvalue.Add(objvalue);
                    e.ContainerStack.Push(currentObjectContainer);
                }
            }
            else if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.Property)
            {
                currentPropertyInfo = currentObjectContainer.ContainerObject as PropertyInfo;
                List<char> value = new List<char>();
                Type currentPropertyType = null;
                if (currentPropertyInfo.PropertyType.IsGenericType)
                {
                    currentPropertyType = currentPropertyInfo.PropertyType.GetGenericArguments()[0];
                }
                else
                {
                    currentPropertyType = currentPropertyInfo.PropertyType;
                }
                string valuestring = GetValueContainerByPropertyType(e.JsonStringStack);
                if (currentPropertyType == typeof(int))
                {
                    if (valuestring != "null")
                    {
                        objvalue = int.Parse(valuestring);
                    }
                }
                else if (currentPropertyType == typeof(string))
                {
                    if (valuestring != "null")
                    {
                        objvalue = valuestring;
                    }
                }
                else if (currentPropertyType == typeof(DateTime))
                {
                    if (valuestring != "null")
                    {
                        objvalue = DateTime.Parse(valuestring);
                    }
                }
                else if (currentPropertyType == typeof(decimal))
                {
                    if (valuestring != "null")
                    {
                        objvalue = Decimal.Parse(valuestring);
                    }
                }
                else if (currentPropertyType == typeof(bool))
                {
                    if (valuestring != "null")
                    {
                        objvalue = bool.Parse(valuestring);
                    }
                }
                currentPropertyInfo.SetValue(e.ContainerStack.Peek().ContainerObject, objvalue);
            }
        }

        private static DeserializeObjectContainer GetCurrentObject(JsonDeserializeEventArgs e)
        {
            DeserializeObjectContainer currentObjectContainer = null;
            Stack<DeserializeObjectContainer> tmpDeserializeObjectContainerStack = new Stack<DeserializeObjectContainer>();
            while (true)
            {
                currentObjectContainer = e.ContainerStack.Pop();
                tmpDeserializeObjectContainerStack.Push(currentObjectContainer);
                if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.Object)
                {
                    break;
                }
            }
            while (tmpDeserializeObjectContainerStack.Count() > 0)
            {
                e.ContainerStack.Push(tmpDeserializeObjectContainerStack.Pop());
            }
            return currentObjectContainer;
        }

        private static bool IsPropertyHandler(JsonDeserializeEventArgs e)
        {
            bool res = false;
            bool isQuotesSymbol = false;
            Stack<char> charList = new Stack<char>();
            while (true)
            {
                var currentChar = e.JsonStringStack.Pop();
                charList.Push(currentChar);
                if (currentChar != Convert.ToChar(JsonSymbol.JsonPropertySymbol) && currentChar != Convert.ToChar(JsonSymbol.JsonQuotesSymbol) && currentChar != Convert.ToChar(JsonSymbol.JsonSpaceSymbol))
                {
                    if (isQuotesSymbol)
                    {
                        res = true;
                    }
                    break;
                }
                else if (currentChar == Convert.ToChar(JsonSymbol.JsonQuotesSymbol))
                {
                    isQuotesSymbol = true;
                }
            }
            while (charList.Count > 0)
            {
                e.JsonStringStack.Push(charList.Pop());
            }
            return res;
        }

        private static object IListToArray(IList list)
        {
            object arrayObj = null;
            Type listType = list.GetType().GetGenericArguments()[0];
            IEnumerator enumeratorList = list.GetEnumerator();
            int currentListIndex = 0;
            if (listType == typeof(int))
            {
                int[] tmpIntArray = new int[list.Count];
                while (enumeratorList.MoveNext())
                {
                    tmpIntArray[currentListIndex] = Convert.ToInt32(enumeratorList.Current);
                    currentListIndex++;
                }
                arrayObj = tmpIntArray;
            }
            else if (listType == typeof(string))
            {
                string[] tmpStringArray = new string[list.Count];
                while (enumeratorList.MoveNext())
                {
                    tmpStringArray[currentListIndex] = Convert.ToString(enumeratorList.Current);
                    currentListIndex++;
                }
                arrayObj = tmpStringArray;
            }
            else if (listType == typeof(DateTime))
            {
                DateTime[] tmpDTArray = new DateTime[list.Count];
                while (enumeratorList.MoveNext())
                {
                    tmpDTArray[currentListIndex] = Convert.ToDateTime(enumeratorList.Current);
                    currentListIndex++;
                }
                arrayObj = tmpDTArray;
            }
            else if (listType == typeof(decimal))
            {
                decimal[] tmpDecimalArray = new decimal[list.Count];
                while (enumeratorList.MoveNext())
                {
                    tmpDecimalArray[currentListIndex] = Convert.ToDecimal(enumeratorList.Current);
                    currentListIndex++;
                }
                arrayObj = tmpDecimalArray;
            }
            else if (listType == typeof(bool))
            {
                bool[] tmpBoolArray = new bool[list.Count];
                while (enumeratorList.MoveNext())
                {
                    tmpBoolArray[currentListIndex] = Convert.ToBoolean(enumeratorList.Current);
                    currentListIndex++;
                }
                arrayObj = tmpBoolArray;
            }
            else
            {
                arrayObj = list;
            }
            return arrayObj;
        }
    }
}
