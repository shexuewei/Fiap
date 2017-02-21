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
            string value = "";
            setting = setting ?? new SerializationSetting { DataTimeFomatter = DefaultDataTimeFomatter, SerializationType = SerializationType.JSON };
            switch (setting.SerializationType)
            {
                case SerializationType.JSON:
                    value = SerializeObjectJSON(serializeObject, setting);
                    break;
                default:
                    value = SerializeObjectJSON(serializeObject, setting);
                    break;
            }
            return value;
        }

        /// <summary>
        /// 序列化成JSON字符串
        /// </summary>
        /// <param name="serializeObject"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        private string SerializeObjectJSON(object serializeObject, SerializationSetting setting)
        {
            Type serializeObjectType = serializeObject.GetType();
            //判断常用类型
            if (serializeObjectType == typeof(Decimal)
                || serializeObjectType == typeof(Int32)
                || serializeObjectType == typeof(String)
                || serializeObjectType == typeof(Boolean))
            {
                return serializeObject.ToString();
            }

            if (serializeObjectType == typeof(DateTime))
            {
                return DateTime.Parse(serializeObject.ToString()).ToString(setting.DataTimeFomatter);
            }

            if (serializeObjectType.IsGenericType && serializeObjectType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (serializeObjectType.GenericTypeArguments[0] == typeof(DateTime))
                {
                    return DateTime.Parse(serializeObject.ToString()).ToString(setting.DataTimeFomatter); ;
                }
                else
                {
                    return serializeObject.ToString();
                }
            }
            StringBuilder valueSb = new StringBuilder();
            if (serializeObjectType.IsClass && !serializeObjectType.IsGenericType && !typeof(IEnumerable).IsAssignableFrom(serializeObjectType))
            {
                SerializeObjectPropertyJSON(valueSb, serializeObject, setting);
            }
            if (typeof(IEnumerable).IsAssignableFrom(serializeObjectType))
            {
                IEnumerable objectValue = (IEnumerable)serializeObject;
                IEnumerableProcess(objectValue, valueSb, setting);
            }
            return valueSb.ToString();
        }

        /// <summary>
        /// 属性序列化
        /// </summary>
        /// <param name="valueSb"></param>
        /// <param name="serializeObject"></param>
        /// <param name="setting"></param>
        /// <param name="isShowPropertyName"></param>
        private void SerializeObjectPropertyJSON(StringBuilder valueSb, object serializeObject, SerializationSetting setting, bool isShowPropertyName = true)
        {
            valueSb.Append("{");
            PropertyInfo[] propertyInfoList = serializeObject.GetType().GetProperties();
            int propertyCount = propertyInfoList.Length;
            int propertyIndex = 0;
            foreach (PropertyInfo propertyInfoItem in propertyInfoList)
            {
                Type objectPropertyType = propertyInfoItem.PropertyType;
                //判断常用类型
                if (objectPropertyType == typeof(DateTime)
                    || objectPropertyType == typeof(Int32)
                    || objectPropertyType == typeof(String)
                    || objectPropertyType == typeof(Boolean)
                    || objectPropertyType == typeof(Decimal))
                {
                    ValueTypeProcess(objectPropertyType, propertyInfoItem, serializeObject, valueSb,setting, isShowPropertyName);
                }
                //判断常用类型的可空类型
                else if (objectPropertyType.IsGenericType && objectPropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    ValueTypeProcess(objectPropertyType.GenericTypeArguments[0], propertyInfoItem, serializeObject, valueSb,setting, isShowPropertyName);
                }
                //判断数组
                else if (objectPropertyType.IsArray)
                {
                    ArrayProcess(objectPropertyType, propertyInfoItem, serializeObject, valueSb,setting, true);
                }
                //判断集合类型
                else if (typeof(IEnumerable).IsAssignableFrom(objectPropertyType))
                {
                    IEnumerable propertyObject = (IEnumerable)propertyInfoItem.GetValue(serializeObject);
                    IEnumerableProcess(propertyObject, valueSb, setting, propertyInfoItem.Name);
                }
                propertyIndex++;
                if (propertyIndex < propertyCount)
                {
                    valueSb.Append(",");
                }
            }
            valueSb.Append("}");
        }

        /// <summary>
        /// 处理类型集合
        /// </summary>
        /// <param name="serializeObject"></param>
        /// <param name="valueSb"></param>
        /// <param name="setting"></param>
        /// <param name="propertyInfoItemName"></param>
        private void IEnumerableProcess(IEnumerable serializeObject, StringBuilder valueSb, SerializationSetting setting, string propertyInfoItemName = "")
        {
            if (serializeObject != null)
            {
                if (string.IsNullOrWhiteSpace(propertyInfoItemName))
                {
                    valueSb.Append("[");
                }
                else
                {
                    valueSb.Append("\"" + propertyInfoItemName + "\":[");
                }
                bool isShowPropertyName = !string.IsNullOrWhiteSpace(propertyInfoItemName);
                IEnumerator enumeratorList = serializeObject.GetEnumerator();
                int objCount = GetEnumeratorCount(enumeratorList);
                enumeratorList.Reset();
                int tmpObjCount = 0;
                while (enumeratorList.MoveNext())
                {
                    Type enumeratorCurrentType = enumeratorList.Current.GetType();
                    //判断常用类型
                    if (enumeratorCurrentType == typeof(DateTime)
                        || enumeratorCurrentType == typeof(Int32)
                        || enumeratorCurrentType == typeof(String)
                        || enumeratorCurrentType == typeof(Boolean)
                        || enumeratorCurrentType == typeof(Decimal))
                    {
                        Process(enumeratorCurrentType, enumeratorList.Current, valueSb, setting);
                    }
                    else
                    {
                        SerializeObjectPropertyJSON(valueSb, enumeratorList.Current, setting, isShowPropertyName);
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
                if (string.IsNullOrWhiteSpace(propertyInfoItemName))
                {
                    valueSb.Append("null");
                }
                else
                {
                    valueSb.Append("\"" + propertyInfoItemName + "\":null");
                }
            }
        }

        /// <summary>
        /// 值类型处理
        /// </summary>
        /// <param name="objectPropertyType"></param>
        /// <param name="propertyInfoItem"></param>
        /// <param name="serializeObject"></param>
        /// <param name="valueSb"></param>
        /// <param name="setting"></param>
        /// <param name="isShowPropertyName"></param>
        private void ValueTypeProcess(Type objectPropertyType, PropertyInfo propertyInfoItem, object serializeObject, StringBuilder valueSb, SerializationSetting setting, bool isShowPropertyName)
        {
            object propertyValue = propertyInfoItem.GetValue(serializeObject);
            string propertyInfoItemName = isShowPropertyName ? "\"" + propertyInfoItem.Name + "\":" : "";
            Process(objectPropertyType, propertyValue, valueSb, setting, propertyInfoItemName);
        }

        /// <summary>
        /// 数组处理
        /// </summary>
        /// <param name="objectPropertyType"></param>
        /// <param name="propertyInfoItem"></param>
        /// <param name="serializeObject"></param>
        /// <param name="valueSb"></param>
        /// <param name="setting"></param>
        /// <param name="isShowPropertyName"></param>
        private void ArrayProcess(Type objectPropertyType, PropertyInfo propertyInfoItem, object serializeObject, StringBuilder valueSb, SerializationSetting setting, bool isShowPropertyName)
        {
            IEnumerable propertyValue = (IEnumerable)propertyInfoItem.GetValue(serializeObject);
            if (isShowPropertyName)
            {
                if (propertyValue != null)
                {
                    valueSb.Append("\"" + propertyInfoItem.Name + "\":[");
                    IEnumerator enumeratorList = propertyValue.GetEnumerator();
                    int objCount = GetEnumeratorCount(enumeratorList);
                    enumeratorList.Reset();
                    int tmpObjCount = 0;
                    while (enumeratorList.MoveNext())
                    {
                        Process(enumeratorList.Current.GetType(), enumeratorList.Current, valueSb, setting);
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
                    valueSb.Append("\"" + propertyInfoItem.Name + "\":null");
                }
            }
            else
            {
                //TODO:不显示属性名时的处理
            }
        }

        /// <summary>
        /// 序列化处理
        /// </summary>
        /// <param name="objectPropertyType"></param>
        /// <param name="propertyValue"></param>
        /// <param name="valueSb"></param>
        /// <param name="setting"></param>
        /// <param name="propertyInfoItemName"></param>
        private void Process(Type objectPropertyType, object propertyValue, StringBuilder valueSb, SerializationSetting setting, string propertyInfoItemName = "")
        {
            if (propertyValue != null)
            {
                if (objectPropertyType == typeof(DateTime))
                {
                    valueSb.Append(propertyInfoItemName + "\"" + DateTime.Parse(propertyValue.ToString()).ToString(setting.DataTimeFomatter) + "\"");
                }
                else if (objectPropertyType == typeof(Int32))
                {
                    valueSb.Append(propertyInfoItemName + propertyValue.ToString());
                }
                else if (objectPropertyType == typeof(String))
                {
                    valueSb.Append(propertyInfoItemName + "\"" + propertyValue.ToString() + "\"");
                }
                else if (objectPropertyType == typeof(Boolean))
                {
                    string tmpPropertyValue = propertyValue.ToString();
                    valueSb.Append(propertyInfoItemName + tmpPropertyValue.Substring(0, 1).ToLower() + tmpPropertyValue.Substring(1));
                }
                else if (objectPropertyType == typeof(Decimal))
                {
                    valueSb.Append(propertyInfoItemName + propertyValue.ToString());
                }
            }
            else
            {
                valueSb.Append(propertyInfoItemName + " null");
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
