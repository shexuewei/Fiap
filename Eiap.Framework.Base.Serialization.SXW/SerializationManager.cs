using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;
using System.Diagnostics;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public class SerializationManager : ISerializationManager
    {
        private const string DefaultDataTimeFomatter = "yyyy-MM-dd HH:mm:ss";
        private readonly IPropertyAccessorManager _PropertyAccessorManager;
        private const string JsonArraySymbol_Begin = "[";
        private const string JsonArraySymbol_End = "]";
        private const string JsonObjectSymbol_Begin = "{";
        private const string JsonObjectSymbol_End = "}";
        private const string JsonQuotesSymbol = "\"";
        private const string JsonNullSymbol = "null";
        private const string JsonSeparateSymbol = ",";
        private const string JsonPropertySymbol = ":";

        public SerializationManager(IPropertyAccessorManager propertyAccessorManager)
        {
            _PropertyAccessorManager = propertyAccessorManager;
        }

        /// <summary>
        /// 将字符串反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public T DeserializeObject<T>(string value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据设置将对象序列化成字符串
        /// </summary>
        /// <param name="serializeObject"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public string SerializeObject(object serializeObject, SerializationSetting setting = null)
        {
            if (serializeObject == null)
            {
                throw new Exception("Object is null");
            }
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            StringBuilder valueSb = new StringBuilder();
            setting = setting ?? new SerializationSetting { DataTimeFomatter = DefaultDataTimeFomatter, SerializationType = SerializationType.JSON };
            switch (setting.SerializationType)
            {
                case SerializationType.JSON:
                    SerializeObjectJSON(serializeObject, setting, false, valueSb);
                    break;
                default:
                    SerializeObjectJSON(serializeObject, setting, false, valueSb);
                    break;
            }
            return valueSb.ToString();
        }

        /// <summary>
        /// 序列化成JSON字符串
        /// </summary>
        /// <param name="serializeObject"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        private void SerializeObjectJSON(object serializeObject, SerializationSetting setting, bool isShowPropertyName, StringBuilder valueSb, string propertyName = "")
        {
            if (serializeObject == null)
            {
                if (isShowPropertyName)
                {
                    valueSb.Append(JsonQuotesSymbol);
                    valueSb.Append(propertyName);
                    valueSb.Append(JsonQuotesSymbol);
                    valueSb.Append(JsonPropertySymbol);
                    valueSb.Append(JsonNullSymbol);
                }
                else
                {
                    valueSb.Append(JsonNullSymbol);
                }
                return;
            }
            Type serializeObjectType = serializeObject.GetType();
            #region 非字符串、字典集合
            if (typeof(IEnumerable).IsAssignableFrom(serializeObjectType) && serializeObjectType != typeof(String) && !typeof(IDictionary).IsAssignableFrom(serializeObjectType))
            {
                IEnumerable objectValue = (IEnumerable)serializeObject;
                IEnumerator enumeratorList = objectValue.GetEnumerator();
                int objCount = GetEnumeratorCount(enumeratorList);
                enumeratorList.Reset();
                int tmpObjCount = 0;
                if (!isShowPropertyName)
                {
                    valueSb.Append(JsonArraySymbol_Begin);
                    while (enumeratorList.MoveNext())
                    {
                        Type enumeratorCurrentType = enumeratorList.Current.GetType();
                        if (IsNormalType(enumeratorCurrentType))
                        {
                            SerializeObjectJSON(enumeratorList.Current, setting, false, valueSb);
                        }
                        else
                        {
                            SerializeObjectJSON(enumeratorList.Current, setting, true, valueSb);
                        }
                        tmpObjCount++;
                        if (tmpObjCount < objCount)
                        {
                            valueSb.Append(JsonSeparateSymbol);
                        }
                    }
                    valueSb.Append(JsonArraySymbol_End);
                }
                else
                {
                    valueSb.Append(JsonQuotesSymbol);
                    valueSb.Append(propertyName);
                    valueSb.Append(JsonQuotesSymbol);
                    valueSb.Append(JsonPropertySymbol);
                    valueSb.Append(JsonArraySymbol_Begin);
                    while (enumeratorList.MoveNext())
                    {
                        Type enumeratorCurrentType = enumeratorList.Current.GetType();
                        if (IsNormalType(enumeratorCurrentType))
                        {
                            SerializeObjectJSON(enumeratorList.Current, setting, false, valueSb);
                        }
                        else if (typeof(IDictionary).IsAssignableFrom(enumeratorCurrentType))
                        {
                            IDictionary dictObject = (IDictionary)enumeratorList.Current;
                        }
                        else
                        {
                            SerializeObjectJSON(enumeratorList.Current, setting, true, valueSb);
                        }
                        tmpObjCount++;
                        if (tmpObjCount < objCount)
                        {
                            valueSb.Append(JsonSeparateSymbol);
                        }
                    }
                    valueSb.Append(JsonObjectSymbol_End);
                }
            }
            #endregion
            #region 字典
            else if(typeof(IDictionary).IsAssignableFrom(serializeObjectType))
            {
                IDictionary objectValue = (IDictionary)serializeObject;
                int objCount = objectValue.Count;
                ICollection keyList = objectValue.Keys;
                IEnumerator enumeratorList = keyList.GetEnumerator();
                int tmpObjCount = 0;
                if (!isShowPropertyName)
                {
                    valueSb.Append(JsonObjectSymbol_Begin);
                    while (enumeratorList.MoveNext())
                    {
                        if (enumeratorList.Current == null)
                        {
                            throw new Exception("Key Is Not Null");
                        }
                        Type enumeratorCurrentType = objectValue[enumeratorList.Current].GetType();
                        SerializeObjectJSON(objectValue[enumeratorList.Current], setting, true, valueSb, enumeratorList.Current.ToString());
                        tmpObjCount++;
                        if (tmpObjCount < objCount)
                        {
                            valueSb.Append(JsonSeparateSymbol);
                        }
                    }
                    valueSb.Append(JsonObjectSymbol_End);
                }
                else
                {
                    valueSb.Append(JsonQuotesSymbol);
                    valueSb.Append(propertyName);
                    valueSb.Append(JsonQuotesSymbol);
                    valueSb.Append(JsonPropertySymbol);
                    valueSb.Append(JsonObjectSymbol_Begin);
                    while (enumeratorList.MoveNext())
                    {
                        if (enumeratorList.Current == null)
                        {
                            throw new Exception("Key Is Not Null");
                        }
                        Type enumeratorCurrentType = objectValue[enumeratorList.Current].GetType();
                        SerializeObjectJSON(objectValue[enumeratorList.Current], setting, true, valueSb, enumeratorList.Current.ToString());
                        tmpObjCount++;
                        if (tmpObjCount < objCount)
                        {
                            valueSb.Append(JsonSeparateSymbol);
                        }

                    }
                    valueSb.Append(JsonObjectSymbol_End);
                }
            }
            #endregion
            #region 普通类型
            else if (IsNormalType(serializeObjectType))
            {
                Process(serializeObject, valueSb, setting, isShowPropertyName, propertyName);
            }
            #endregion
            #region 属性
            else
            {
                valueSb.Append(JsonObjectSymbol_Begin);
                
                PropertyInfo[] propertyInfoList = serializeObject.GetType().GetProperties();
                int propertyCount = propertyInfoList.Length;
                int propertyIndex = 0;
                foreach (PropertyInfo propertyInfoItem in propertyInfoList)
                {
                    string propertyKey = serializeObjectType.FullName + "." + propertyInfoItem.Name;
                    PropertyInfoContainer container = new PropertyInfoContainer { PropertyName = propertyInfoItem.Name, InstanceTypeHandle = serializeObjectType.TypeHandle, PropertyTypeHandle = propertyInfoItem.PropertyType.TypeHandle };
                    object objectValue = _PropertyAccessorManager.GetPropertyAccessor(propertyKey, container).GetValue(serializeObject); //propertyInfoItem.GetValue(serializeObject);//
                    SerializeObjectJSON(objectValue, setting, true, valueSb, propertyInfoItem.Name);
                    propertyIndex++;
                    if (propertyIndex < propertyCount)
                    {
                        valueSb.Append(JsonSeparateSymbol);
                    }
                }
                valueSb.Append(JsonObjectSymbol_End);
            }
            #endregion
        }

        /// <summary>
        /// 判断是否常用类型
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        private bool IsNormalType(Type objectType)
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
        private void Process(object objectValue, StringBuilder valueSb, SerializationSetting setting, bool isShowPropertyName, string propertyName = "")
        {
            Type objectType = objectValue.GetType();
            if (isShowPropertyName)
            {
                valueSb.Append(JsonQuotesSymbol);
                valueSb.Append(propertyName);
                valueSb.Append(JsonQuotesSymbol);
                valueSb.Append(JsonPropertySymbol);
            }
            if (objectValue != null)
            {
                if (objectType == typeof(DateTime))
                {
                    valueSb.Append(JsonQuotesSymbol);
                    valueSb.Append(DateTime.Parse(objectValue.ToString()).ToString(setting.DataTimeFomatter));
                    valueSb.Append(JsonQuotesSymbol);
                }
                else if (objectType == typeof(Int32)
                    || objectType == typeof(Decimal)
                    || objectType == typeof(Guid))
                {
                    valueSb.Append(objectValue.ToString());
                }
                else if (objectType == typeof(String))
                {
                    valueSb.Append(JsonQuotesSymbol);
                    valueSb.Append(objectValue.ToString());
                    valueSb.Append(JsonQuotesSymbol);
                }
                else if (objectType == typeof(Boolean))
                {
                    valueSb.Append(objectValue.ToString().ToLower());
                }
            }
            else
            {
                valueSb.Append(JsonNullSymbol);
            }
        }

        /// <summary>
        /// 获取集合数
        /// </summary>
        /// <param name="enumeratorList"></param>
        /// <returns></returns>
        private int GetEnumeratorCount(IEnumerator enumeratorList)
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
