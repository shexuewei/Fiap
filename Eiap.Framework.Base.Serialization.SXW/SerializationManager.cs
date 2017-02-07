using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Eiap.Framework.Base.Serialization.SXW
{
    public class SerializationManager : ISerializationManager
    {
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
            string value = "";
            setting = setting ?? new SerializationSetting { DataTimeFomatter = "yyyy-MM-dd HH:mm:ss", SerializationType = SerializationType.JSON };
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
            StringBuilder value = new StringBuilder("{");
            PropertyInfo[] propertyInfoList = serializeObject.GetType().GetProperties();
            foreach (PropertyInfo propertyInfoItem in propertyInfoList)
            {
                if (propertyInfoItem.PropertyType == typeof(DateTime))
                {
                    value.Append("\"" + propertyInfoItem.Name + "\":" + "\"" + Convert.ToDateTime(propertyInfoItem.GetValue(serializeObject)).ToString(setting.DataTimeFomatter) + "\"");
                }
            }
            return value.ToString();
        }
    }
}
