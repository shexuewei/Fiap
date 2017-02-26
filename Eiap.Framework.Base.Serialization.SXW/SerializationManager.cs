using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public class SerializationManager : ISerializationManager
    {
        private const string DefaultDataTimeFomatter = "yyyy-MM-dd HH:mm:ss";

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
                    valueSb.Append("\"" + propertyName + "\": null");
                }
                else
                {
                    valueSb.Append("null");
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
                    valueSb.Append("[");
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
                            valueSb.Append(",");
                        }
                    }
                    valueSb.Append("]");
                }
                else
                {
                    valueSb.Append("\"" + propertyName + "\":[");
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
                            valueSb.Append(",");
                        }
                    }
                    valueSb.Append("]");
                }
            }
            else if(typeof(IDictionary).IsAssignableFrom(serializeObjectType))
            {
                IDictionary objectValue = (IDictionary)serializeObject;
                int objCount = objectValue.Count;
                ICollection keyList = objectValue.Keys;
                IEnumerator enumeratorList = keyList.GetEnumerator();
                int tmpObjCount = 0;
                if (!isShowPropertyName)
                {
                    valueSb.Append("{");
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
                            valueSb.Append(",");
                        }
                    }
                    valueSb.Append("}");
                }
                else
                {
                    valueSb.Append("\"" + propertyName + "\":{");
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
                            valueSb.Append(",");
                        }

                    }
                    valueSb.Append("}");
                }
            }
            else if (IsNormalType(serializeObjectType))
            {
                Process(serializeObject, valueSb, setting, isShowPropertyName, propertyName);
            }
            else
            {
                valueSb.Append("{");
                PropertyInfo[] propertyInfoList = serializeObject.GetType().GetProperties();
                int propertyCount = propertyInfoList.Length;
                int propertyIndex = 0;
                foreach (PropertyInfo propertyInfoItem in propertyInfoList)
                {
                    object objectValue = propertyInfoItem.GetValue(serializeObject);
                    SerializeObjectJSON(objectValue, setting, true, valueSb, propertyInfoItem.Name);
                    propertyIndex++;
                    if (propertyIndex < propertyCount)
                    {
                        valueSb.Append(",");
                    }
                }
                valueSb.Append("}");
            }
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
            string tmpPropertyName = isShowPropertyName ? "\"" + propertyName + "\":" : "";
            if (objectValue != null)
            {
                if (objectType == typeof(DateTime))
                {
                    valueSb.Append(tmpPropertyName + "\"" + DateTime.Parse(objectValue.ToString()).ToString(setting.DataTimeFomatter) + "\"");
                }
                else if (objectType == typeof(Int32)
                    || objectType == typeof(Decimal)
                    || objectType == typeof(Guid))
                {
                    valueSb.Append(tmpPropertyName + objectValue.ToString());
                }
                else if (objectType == typeof(String))
                {
                    valueSb.Append(tmpPropertyName + "\"" + objectValue.ToString() + "\"");
                }
                else if (objectType == typeof(Boolean))
                {
                    string tmpPropertyValue = objectValue.ToString();
                    valueSb.Append(tmpPropertyName + tmpPropertyValue.Substring(0, 1).ToLower() + tmpPropertyValue.Substring(1));
                }
            }
            else
            {
                valueSb.Append(tmpPropertyName + "null");
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
