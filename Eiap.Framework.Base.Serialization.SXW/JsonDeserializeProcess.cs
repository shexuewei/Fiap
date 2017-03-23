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
        public static object Deserialize(string jsonString, Type objectType, SerializationSetting setting)
        {
            Stack<char> jsonStringStack = new Stack<char>();
            Stack<DeserializeObjectContainer> containerStack = new Stack<DeserializeObjectContainer>();
            char[] jsonCharList = jsonString.ToCharArray();
            foreach (char charitem in jsonCharList)
            {
                jsonStringStack.Push(charitem);
                if (charitem == Convert.ToChar(JsonSymbol.JsonArraySymbol_Begin))
                {
                    Type currentObjectType = null;
                    if (containerStack.Count() == 0)
                    {
                        currentObjectType = objectType;
                    }
                    else
                    {
                        DeserializeObjectContainer container = containerStack.Peek();
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
                    containerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.List, ContainerObject = objectInstance });
                }
                else if (charitem == Convert.ToChar(JsonSymbol.JsonObjectSymbol_Begin))
                {
                    Type currentObjectType = null;
                    if (containerStack.Count() == 0)
                    {
                        currentObjectType = objectType;
                    }
                    else
                    {
                        DeserializeObjectContainer container = containerStack.Peek();
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
                    containerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Object, ContainerObject = objectInstance });
                }
                else if (charitem == Convert.ToChar(JsonSymbol.JsonPropertySymbol))
                {
                    List<char> propertyNameList = new List<char>();
                    jsonStringStack.Pop();//属性分隔符出栈
                    //属性引号出栈
                    while (true)
                    {
                        char beginQuotes = jsonStringStack.Pop();
                        if (beginQuotes == Convert.ToChar(JsonSymbol.JsonQuotesSymbol))
                        {
                            break;
                        }
                    }
                    //属性引号出栈
                    while (true)
                    {
                        char propertyNameChar = jsonStringStack.Pop();
                        if (propertyNameChar == Convert.ToChar(JsonSymbol.JsonQuotesSymbol))
                        {
                            break;
                        }
                        else
                        {
                            propertyNameList.Add(propertyNameChar);
                        }
                    }
                    string propertyNameStr = new string(propertyNameList.ToArray());
                    object currentObj = containerStack.Peek();
                    PropertyInfo propertyinfo = currentObj.GetType().GetProperty(propertyNameStr);
                    containerStack.Push(new DeserializeObjectContainer { ContainerType = DeserializeObjectContainerType.Property, ContainerObject = propertyinfo });
                    jsonStringStack.Push(charitem);
                }
                else if (charitem == Convert.ToChar(JsonSymbol.JsonObjectSymbol_End))
                {
                    DeserializeObjectContainer currentContainer = containerStack.Peek();
                    jsonStringStack.Pop();//}出栈
                    List<char> value = new List<char>();
                    while (true)
                    {
                        char tmpSymbol = jsonStringStack.Pop();
                        if (tmpSymbol == Convert.ToChar(JsonSymbol.JsonPropertySymbol))
                        {
                            break;
                        }
                    }
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
