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
                else if (charitem == Convert.ToChar(JsonSymbol.JsonPropertySymbol))
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
                    jsonStringStack.Pop();//,出栈
                    char valueSymbol = jsonStringStack.Pop();
                    if (valueSymbol == Convert.ToChar(JsonSymbol.JsonObjectSymbol_End))
                    {
                        jsonStringStack.Push(valueSymbol);
                        jsonStringStack.Push(Convert.ToChar(JsonSymbol.JsonSeparateSymbol));
                        List<DeserializeObjectContainer> propertylist = new List<DeserializeObjectContainer>();
                        List<DeserializeObjectContainer> valuelist = new List<DeserializeObjectContainer>();
                        while (true)
                        {
                            DeserializeObjectContainer objlist = containerStack.Pop();
                            if (objlist.ContainerType != DeserializeObjectContainerType.Property && objlist.ContainerType != DeserializeObjectContainerType.Object)
                            {

                            }
                        }
                    }
                    else
                    {
                        PropertyInfo currentPropertyInfo = containerStack.Peek().ContainerObject as PropertyInfo;
                        jsonStringStack.Pop();
                        List<char> value = new List<char>();
                        value.Add(valueSymbol);
                        if (currentPropertyInfo.PropertyType == typeof(int))
                        {
                            while (true)
                            {
                                char tmpValueSymbol = jsonStringStack.Pop();
                                if (tmpValueSymbol == Convert.ToChar(JsonSymbol.JsonPropertySymbol))
                                {
                                    break;
                                }
                                else
                                {
                                    value.Add(tmpValueSymbol);
                                }
                            }
                            containerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_Int, ContainerObject = Convert.ToInt32(new string(value.ToArray()).Trim()) });
                        }
                        else if (currentPropertyInfo.PropertyType == typeof(string))
                        {
                            while (true)
                            {
                                char tmpValueSymbol = jsonStringStack.Pop();
                                if (tmpValueSymbol == Convert.ToChar(JsonSymbol.JsonPropertySymbol))
                                {
                                    break;
                                }
                                else
                                {
                                    value.Add(tmpValueSymbol);
                                }
                            }
                            containerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_String, ContainerObject = new string(value.ToArray()).Trim() });
                        }
                        else if (currentPropertyInfo.PropertyType == typeof(Decimal))
                        {
                            while (true)
                            {
                                char tmpValueSymbol = jsonStringStack.Pop();
                                if (tmpValueSymbol == Convert.ToChar(JsonSymbol.JsonPropertySymbol))
                                {
                                    break;
                                }
                                else
                                {
                                    value.Add(tmpValueSymbol);
                                }
                            }
                            containerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_Decimal, ContainerObject = Convert.ToDecimal(new string(value.ToArray()).Trim()) });
                        }
                        else if (currentPropertyInfo.PropertyType == typeof(DateTime))
                        {
                            while (true)
                            {
                                char tmpValueSymbol = jsonStringStack.Pop();
                                if (tmpValueSymbol == Convert.ToChar(JsonSymbol.JsonPropertySymbol))
                                {
                                    break;
                                }
                                else
                                {
                                    value.Add(tmpValueSymbol);
                                }
                            }
                            containerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_DateTime, ContainerObject = Convert.ToDateTime(new string(value.ToArray()).Trim()) });
                        }
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
            object objectInstance = Activator.CreateInstance(currentObjectType);
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
                    GetValueContainerByPropertyType(value, Convert.ToChar(JsonSymbol.JsonQuotesSymbol), e.JsonStringStack, true);
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

        private static void GetValueContainerByPropertyType(List<char> value, char breakSymbol, Stack<char> jsonStringStack, bool isContainSpace)
        {
            while (true)
            {
                char valueSymbol = jsonStringStack.Pop();
                if (valueSymbol == breakSymbol)
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
            object currentObj = e.ContainerStack.Peek();
            PropertyInfo propertyinfo = currentObj.GetType().GetProperty(propertyNameStr);
            e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Property, ContainerObject = propertyinfo });
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
            char valueSymbol = e.JsonStringStack.Pop();
            if (valueSymbol == Convert.ToChar(JsonSymbol.JsonObjectSymbol_End))
            {
                e.JsonStringStack.Push(valueSymbol);
                e.JsonStringStack.Push(Convert.ToChar(JsonSymbol.JsonSeparateSymbol));
                List<DeserializeObjectContainer> propertylist = new List<DeserializeObjectContainer>();
                List<DeserializeObjectContainer> valuelist = new List<DeserializeObjectContainer>();
                while (true)
                {
                    DeserializeObjectContainer objlist = e.ContainerStack.Pop();
                    if (objlist.ContainerType != DeserializeObjectContainerType.Property && objlist.ContainerType != DeserializeObjectContainerType.Object)
                    {

                    }
                }
            }
            else
            {
                PropertyInfo currentPropertyInfo = e.ContainerStack.Peek().ContainerObject as PropertyInfo;
                e.JsonStringStack.Pop();
                List<char> value = new List<char>();
                value.Add(valueSymbol);
                if (currentPropertyInfo.PropertyType == typeof(int))
                {
                    while (true)
                    {
                        char tmpValueSymbol = e.JsonStringStack.Pop();
                        if (tmpValueSymbol == Convert.ToChar(JsonSymbol.JsonPropertySymbol))
                        {
                            break;
                        }
                        else
                        {
                            value.Add(tmpValueSymbol);
                        }
                    }
                    e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_Int, ContainerObject = Convert.ToInt32(new string(value.ToArray()).Trim()) });
                }
                else if (currentPropertyInfo.PropertyType == typeof(string))
                {
                    while (true)
                    {
                        char tmpValueSymbol = e.JsonStringStack.Pop();
                        if (tmpValueSymbol == Convert.ToChar(JsonSymbol.JsonPropertySymbol))
                        {
                            break;
                        }
                        else
                        {
                            value.Add(tmpValueSymbol);
                        }
                    }
                    e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_String, ContainerObject = new string(value.ToArray()).Trim() });
                }
                else if (currentPropertyInfo.PropertyType == typeof(Decimal))
                {
                    while (true)
                    {
                        char tmpValueSymbol = e.JsonStringStack.Pop();
                        if (tmpValueSymbol == Convert.ToChar(JsonSymbol.JsonPropertySymbol))
                        {
                            break;
                        }
                        else
                        {
                            value.Add(tmpValueSymbol);
                        }
                    }
                    e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_Decimal, ContainerObject = Convert.ToDecimal(new string(value.ToArray()).Trim()) });
                }
                else if (currentPropertyInfo.PropertyType == typeof(DateTime))
                {
                    while (true)
                    {
                        char tmpValueSymbol = e.JsonStringStack.Pop();
                        if (tmpValueSymbol == Convert.ToChar(JsonSymbol.JsonPropertySymbol))
                        {
                            break;
                        }
                        else
                        {
                            value.Add(tmpValueSymbol);
                        }
                    }
                    e.ContainerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Value_DateTime, ContainerObject = Convert.ToDateTime(new string(value.ToArray()).Trim()) });
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
    }


}
