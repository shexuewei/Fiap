using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public static class JsonSerializeProcess
    {
        /// <summary>
        /// 序列化成JSON字符串
        /// </summary>
        /// <param name="serializeObject"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static void SerializeObjectJSON(object serializeObject, SerializationSetting setting, bool isShowPropertyName, StringBuilder valueSb, IPropertyAccessorManager propertyAccessorManager, string propertyName = "")
        {
            if (serializeObject == null)
            {
                if (isShowPropertyName)
                {
                    valueSb.Append(JsonSymbol.JsonQuotesSymbol);
                    valueSb.Append(propertyName);
                    valueSb.Append(JsonSymbol.JsonQuotesSymbol);
                    valueSb.Append(JsonSymbol.JsonPropertySymbol);
                    valueSb.Append(JsonSymbol.JsonNullSymbol);
                }
                else
                {
                    valueSb.Append(JsonSymbol.JsonNullSymbol);
                }
                return;
            }
            Type serializeObjectType = serializeObject.GetType();
            if (typeof(IEnumerable).IsAssignableFrom(serializeObjectType) && serializeObjectType != typeof(String) && !typeof(IDictionary).IsAssignableFrom(serializeObjectType))
            {
                IEnumerable objectValue = (IEnumerable)serializeObject;
                IEnumerator enumeratorList = objectValue.GetEnumerator();
                int objCount = GetEnumeratorCount(enumeratorList);
                enumeratorList.Reset();
                int tmpObjCount = 0;
                if (!isShowPropertyName)
                {
                    valueSb.Append(JsonSymbol.JsonArraySymbol_Begin);
                    while (enumeratorList.MoveNext())
                    {
                        Type enumeratorCurrentType = enumeratorList.Current.GetType();
                        if (IsNormalType(enumeratorCurrentType))
                        {
                            SerializeObjectJSON(enumeratorList.Current, setting, false, valueSb, propertyAccessorManager);
                        }
                        else
                        {
                            SerializeObjectJSON(enumeratorList.Current, setting, true, valueSb, propertyAccessorManager);
                        }
                        tmpObjCount++;
                        if (tmpObjCount < objCount)
                        {
                            valueSb.Append(JsonSymbol.JsonSeparateSymbol);
                        }
                    }
                    valueSb.Append(JsonSymbol.JsonArraySymbol_End);
                }
                else
                {
                    valueSb.Append(JsonSymbol.JsonQuotesSymbol);
                    valueSb.Append(propertyName);
                    valueSb.Append(JsonSymbol.JsonQuotesSymbol);
                    valueSb.Append(JsonSymbol.JsonPropertySymbol);
                    valueSb.Append(JsonSymbol.JsonArraySymbol_Begin);
                    while (enumeratorList.MoveNext())
                    {
                        Type enumeratorCurrentType = enumeratorList.Current.GetType();
                        if (IsNormalType(enumeratorCurrentType))
                        {
                            SerializeObjectJSON(enumeratorList.Current, setting, false, valueSb, propertyAccessorManager);
                        }
                        else if (typeof(IDictionary).IsAssignableFrom(enumeratorCurrentType))
                        {
                            IDictionary dictObject = (IDictionary)enumeratorList.Current;
                        }
                        else
                        {
                            SerializeObjectJSON(enumeratorList.Current, setting, true, valueSb, propertyAccessorManager);
                        }
                        tmpObjCount++;
                        if (tmpObjCount < objCount)
                        {
                            valueSb.Append(JsonSymbol.JsonSeparateSymbol);
                        }
                    }
                    valueSb.Append(JsonSymbol.JsonObjectSymbol_End);
                }
            }
            else if (typeof(IDictionary).IsAssignableFrom(serializeObjectType))
            {
                IDictionary objectValue = (IDictionary)serializeObject;
                int objCount = objectValue.Count;
                ICollection keyList = objectValue.Keys;
                IEnumerator enumeratorList = keyList.GetEnumerator();
                int tmpObjCount = 0;
                if (!isShowPropertyName)
                {
                    valueSb.Append(JsonSymbol.JsonObjectSymbol_Begin);
                    while (enumeratorList.MoveNext())
                    {
                        if (enumeratorList.Current == null)
                        {
                            throw new Exception("Key Is Not Null");
                        }
                        Type enumeratorCurrentType = objectValue[enumeratorList.Current].GetType();
                        SerializeObjectJSON(objectValue[enumeratorList.Current], setting, true, valueSb, propertyAccessorManager, enumeratorList.Current.ToString());
                        tmpObjCount++;
                        if (tmpObjCount < objCount)
                        {
                            valueSb.Append(JsonSymbol.JsonSeparateSymbol);
                        }
                    }
                    valueSb.Append(JsonSymbol.JsonObjectSymbol_End);
                }
                else
                {
                    valueSb.Append(JsonSymbol.JsonQuotesSymbol);
                    valueSb.Append(propertyName);
                    valueSb.Append(JsonSymbol.JsonQuotesSymbol);
                    valueSb.Append(JsonSymbol.JsonPropertySymbol);
                    valueSb.Append(JsonSymbol.JsonObjectSymbol_Begin);
                    while (enumeratorList.MoveNext())
                    {
                        if (enumeratorList.Current == null)
                        {
                            throw new Exception("Key Is Not Null");
                        }
                        Type enumeratorCurrentType = objectValue[enumeratorList.Current].GetType();
                        SerializeObjectJSON(objectValue[enumeratorList.Current], setting, true, valueSb, propertyAccessorManager, enumeratorList.Current.ToString());
                        tmpObjCount++;
                        if (tmpObjCount < objCount)
                        {
                            valueSb.Append(JsonSymbol.JsonSeparateSymbol);
                        }

                    }
                    valueSb.Append(JsonSymbol.JsonObjectSymbol_End);
                }
            }
            else if (IsNormalType(serializeObjectType))
            {
                Process(serializeObject, valueSb, setting, isShowPropertyName, propertyName);
            }
            else
            {
                valueSb.Append(JsonSymbol.JsonObjectSymbol_Begin);

                PropertyInfo[] propertyInfoList = serializeObject.GetType().GetProperties();
                int propertyCount = propertyInfoList.Length;
                int propertyIndex = 0;
                foreach (PropertyInfo propertyInfoItem in propertyInfoList)
                {
                    string propertyKey = serializeObjectType.FullName + "." + propertyInfoItem.Name;
                    PropertyInfoContainer container = new PropertyInfoContainer { PropertyName = propertyInfoItem.Name, InstanceTypeHandle = serializeObjectType.TypeHandle, PropertyTypeHandle = propertyInfoItem.PropertyType.TypeHandle };
                    object objectValue = propertyAccessorManager.GetPropertyAccessor(propertyKey, container).GetValue(serializeObject); //propertyInfoItem.GetValue(serializeObject);//
                    SerializeObjectJSON(objectValue, setting, true, valueSb, propertyAccessorManager, propertyInfoItem.Name);
                    propertyIndex++;
                    if (propertyIndex < propertyCount)
                    {
                        valueSb.Append(JsonSymbol.JsonSeparateSymbol);
                    }
                }
                valueSb.Append(JsonSymbol.JsonObjectSymbol_End);
            }
        }

        /// <summary>
        /// 判断是否常用类型
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        private static bool IsNormalType(Type objectType)
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
        /// 常用类型对象序列化处理
        /// </summary>
        /// <param name="objectValue"></param>
        /// <param name="valueSb"></param>
        /// <param name="setting"></param>
        /// <param name="isShowPropertyName"></param>
        private static void Process(object objectValue, StringBuilder valueSb, SerializationSetting setting, bool isShowPropertyName, string propertyName = "")
        {
            Type objectType = objectValue.GetType();
            if (isShowPropertyName)
            {
                valueSb.Append(JsonSymbol.JsonQuotesSymbol);
                valueSb.Append(propertyName);
                valueSb.Append(JsonSymbol.JsonQuotesSymbol);
                valueSb.Append(JsonSymbol.JsonPropertySymbol);
            }
            if (objectValue != null)
            {
                if (objectType == typeof(DateTime))
                {
                    valueSb.Append(JsonSymbol.JsonQuotesSymbol);
                    valueSb.Append(DateTime.Parse(objectValue.ToString()).ToString(setting.DataTimeFomatter));
                    valueSb.Append(JsonSymbol.JsonQuotesSymbol);
                }
                else if (objectType == typeof(Int32)
                    || objectType == typeof(Decimal)
                    || objectType == typeof(Guid))
                {
                    valueSb.Append(objectValue.ToString());
                }
                else if (objectType == typeof(String))
                {
                    valueSb.Append(JsonSymbol.JsonQuotesSymbol);
                    valueSb.Append(objectValue.ToString());
                    valueSb.Append(JsonSymbol.JsonQuotesSymbol);
                }
                else if (objectType == typeof(Boolean))
                {
                    valueSb.Append(objectValue.ToString().ToLower());
                }
            }
            else
            {
                valueSb.Append(JsonSymbol.JsonNullSymbol);
            }
        }

        /// <summary>
        /// 获取集合数
        /// </summary>
        /// <param name="enumeratorList"></param>
        /// <returns></returns>
        private static int GetEnumeratorCount(IEnumerator enumeratorList)
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
