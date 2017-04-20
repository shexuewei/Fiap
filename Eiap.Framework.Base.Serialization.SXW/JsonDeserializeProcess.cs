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
        public static event EventHandler<JsonDeserializeEventArgs> JsonDeserializeQuotesSymbol_Event;
        public static event EventHandler<JsonDeserializeEventArgs> JsonDeserializePropertySymbol_Event;
        public static event EventHandler<JsonDeserializeEventArgs> JsonDeserializeNullSymbol_Event;
        public static event EventHandler<JsonDeserializeEventArgs> JsonDeserializeSeparateSymbol_Event;
        public static event EventHandler<JsonDeserializeEventArgs> JsonDeserializeSpaceSymbol_Event;

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

        /// <summary>
        /// 数组开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void JsonDeserializeProcess_JsonDeserializeArraySymbol_Begin_Event(object sender, JsonDeserializeEventArgs e)
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
            }
            IList objectInstance = null;
            if (currentObjectType.IsGenericType)
            {
                Type genType = currentObjectType.GetGenericTypeDefinition();
                Type[] genParaType = currentObjectType.GetGenericArguments();
                Type objtype = genType.MakeGenericType(genParaType);
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
            throw new NotImplementedException();
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
            }
            object objectInstance = null;
            if (currentObjectType.IsGenericType)
            {
                objectInstance = null;
            }
            else
            {
                objectInstance = Activator.CreateInstance(currentObjectType);
            }
            e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Object, ContainerObject = objectInstance });
        }

        /// <summary>
        /// 对象结束事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void JsonDeserializeProcess_JsonDeserializeObjectSymbol_End_Event(object sender, JsonDeserializeEventArgs e)
        {
            e.JsonStringStack.Pop();//}出栈
            SpaceProcess(e.JsonStringStack);
            DeserializeObjectContainer currentObjectContainer = e.ContainerStack.Peek();
            PropertyInfo currentPropertyInfo = null;
            if (currentObjectContainer.ContainerType == DeserializeObjectContainerType.Property)
            {
                currentPropertyInfo = currentObjectContainer.ContainerObject as PropertyInfo;
            }
            if (currentPropertyInfo != null)
            {
                List<char> value = new List<char>();
                if (currentPropertyInfo.PropertyType == typeof(int))
                {
                    GetValueContainerByPropertyType(value, Convert.ToChar(JsonSymbol.JsonPropertySymbol), e.JsonStringStack,false);
                    e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_Int, ContainerObject = int.Parse(new string(value.ToArray())) });
                }
                else if (currentPropertyInfo.PropertyType == typeof(string))
                {
                    GetValueContainerByPropertyType(value, Convert.ToChar(JsonSymbol.JsonPropertySymbol), e.JsonStringStack, true);
                    e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_String, ContainerObject = new string(value.ToArray()) });
                }
                else if (currentPropertyInfo.PropertyType == typeof(DateTime))
                {
                    GetValueContainerByPropertyType(value, Convert.ToChar(JsonSymbol.JsonQuotesSymbol), e.JsonStringStack, true, true);
                    e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_DateTime, ContainerObject = DateTime.Parse(new string(value.ToArray())) });
                }
                else if (currentPropertyInfo.PropertyType == typeof(Decimal))
                {
                    GetValueContainerByPropertyType(value, Convert.ToChar(JsonSymbol.JsonPropertySymbol), e.JsonStringStack, false);
                    e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_Decimal, ContainerObject = Decimal.Parse(new string(value.ToArray()).Trim()) });
                }
            }
            e.JsonStringStack.Push(e.CurrentCharItem);
        }

        private static void GetValueContainerByPropertyType(List<char> value, char breakSymbol, Stack<char> jsonStringStack, bool isContainSpace, bool isDateTime = false)
        {
            while (true)
            {
                char valueSymbol = jsonStringStack.Pop();
                if (valueSymbol == breakSymbol && !isDateTime)
                {
                    break;
                }
                else if (valueSymbol == Convert.ToChar(JsonSymbol.JsonSpaceSymbol) && isContainSpace)
                {
                    value.Insert(0, valueSymbol);
                }
                else if (valueSymbol != Convert.ToChar(JsonSymbol.JsonSpaceSymbol))
                {
                    value.Insert(0, valueSymbol);
                }
                isDateTime = false;
            }
        }

        private static void SpaceProcess(Stack<char> jsonStringStack)
        {
            while (true)
            {
                if (jsonStringStack.Peek() == Convert.ToChar(JsonSymbol.JsonSpaceSymbol))
                {
                    jsonStringStack.Pop();
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 引号事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void JsonDeserializeProcess_JsonDeserializeQuotesSymbol_Event(object sender, JsonDeserializeEventArgs e)
        {
            throw new NotImplementedException();
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
            DeserializeObjectContainer currentObj = e.ContainerStack.Peek() as DeserializeObjectContainer;
            if (currentObj != null)
            {
                PropertyInfo propertyinfo = GetCurrentObject(e).ContainerObject.GetType().GetProperty(propertyNameStr);
                e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Property, ContainerObject = propertyinfo });
            }
            e.JsonStringStack.Push(e.CurrentCharItem);
        }

        /// <summary>
        /// Null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void JsonDeserializeProcess_JsonDeserializeNullSymbol_Event(object sender, JsonDeserializeEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 逗号事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void JsonDeserializeProcess_JsonDeserializeSeparateSymbol_Event(object sender, JsonDeserializeEventArgs e)
        {
            e.JsonStringStack.Pop();//,出栈
            SpaceProcess(e.JsonStringStack);
            char valueSymbol = e.JsonStringStack.Peek();
            if (valueSymbol == Convert.ToChar(JsonSymbol.JsonObjectSymbol_End))
            {
                e.JsonStringStack.Push(Convert.ToChar(JsonSymbol.JsonSeparateSymbol));
                List<DeserializeObjectContainer> propertylist = new List<DeserializeObjectContainer>();
                List<DeserializeObjectContainer> valuelist = new List<DeserializeObjectContainer>();
                DeserializeObjectContainer objlist = null;
                while (true)
                {
                    objlist = e.ContainerStack.Pop();
                    if (objlist.ContainerType != DeserializeObjectContainerType.Property && objlist.ContainerType != DeserializeObjectContainerType.Object)
                    {
                        valuelist.Add(objlist);
                    }
                    else if (objlist.ContainerType == DeserializeObjectContainerType.Property)
                    {
                        propertylist.Add(objlist);
                    }
                    else if (objlist.ContainerType == DeserializeObjectContainerType.Object)
                    {
                        break;
                    }
                }
                int propertycount = propertylist.Count;
                for (int i = 0; i < propertycount; i++)
                {
                    PropertyInfo propertyinfo = propertylist[i].ContainerObject as PropertyInfo;
                    if (propertyinfo != null)
                    {
                        propertyinfo.SetValue(objlist, valuelist[i].ContainerObject);
                    }
                }
            }
            else
            {
                PropertyInfo currentPropertyInfo = e.ContainerStack.Peek().ContainerObject as PropertyInfo;
                if (currentPropertyInfo != null)
                {
                    List<char> value = new List<char>();
                    if (currentPropertyInfo.PropertyType == typeof(int))
                    {
                        GetValueContainerByPropertyType(value, Convert.ToChar(JsonSymbol.JsonPropertySymbol), e.JsonStringStack, false);
                        e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_Int, ContainerObject = Convert.ToInt32(new string(value.ToArray()).Trim()) });
                    }
                    else if (currentPropertyInfo.PropertyType == typeof(string))
                    {
                        GetValueContainerByPropertyType(value, Convert.ToChar(JsonSymbol.JsonPropertySymbol), e.JsonStringStack, true);
                        e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_String, ContainerObject = new string(value.ToArray()).Trim() });
                    }
                    else if (currentPropertyInfo.PropertyType == typeof(Decimal))
                    {
                        GetValueContainerByPropertyType(value, Convert.ToChar(JsonSymbol.JsonPropertySymbol), e.JsonStringStack, false);
                        e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_Decimal, ContainerObject = Convert.ToDecimal(new string(value.ToArray()).Trim()) });
                    }
                    else if (currentPropertyInfo.PropertyType == typeof(DateTime))
                    {
                        GetValueContainerByPropertyType(value, Convert.ToChar(JsonSymbol.JsonQuotesSymbol), e.JsonStringStack, true, true);
                        e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_DateTime, ContainerObject = Convert.ToDateTime(new string(value.ToArray()).Trim()) });
                    }
                }
            }
        }

        /// <summary>
        /// 空格事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void JsonDeserializeProcess_JsonDeserializeSpaceSymbol_Event(object sender, JsonDeserializeEventArgs e)
        {
            throw new NotImplementedException();
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
    }
}
