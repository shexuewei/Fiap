﻿using System;
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

        public SerializationManager(IPropertyAccessorManager propertyAccessorManager)
        {
            _PropertyAccessorManager = propertyAccessorManager;
            RegisterDeserializeHandleEvent();
        }

        /// <summary>
        /// 将字符串反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public T DeserializeObject<T>(string value, SerializationSetting setting = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("Value is null");
            }
            setting = setting ?? new SerializationSetting { DataTimeFomatter = DefaultDataTimeFomatter, SerializationType = SerializationType.JSON };
            Type objectType = typeof(T);
            switch (setting.SerializationType)
            {
                case SerializationType.JSON:
                    return (T)JsonDeserializeProcess.Deserialize(value, objectType, setting, _PropertyAccessorManager);
                default:
                    return (T)JsonDeserializeProcess.Deserialize(value, objectType, setting, _PropertyAccessorManager);
            }
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
                    JsonSerializeProcess.Serialize(serializeObject, setting, false, valueSb, _PropertyAccessorManager);
                    break;
                default:
                    JsonSerializeProcess.Serialize(serializeObject, setting, false, valueSb, _PropertyAccessorManager);
                    break;
            }
            return valueSb.ToString();
        }

        /// <summary>
        /// 注册反序列化事件
        /// </summary>
        private void RegisterDeserializeHandleEvent()
        {
            JsonDeserializeProcess.JsonDeserializeObjectSymbol_Begin_Event += JsonDeserializeProcess.JsonDeserializeProcess_JsonDeserializeObjectSymbol_Begin_Event;
            JsonDeserializeProcess.JsonDeserializeObjectSymbol_End_Event += JsonDeserializeProcess.JsonDeserializeProcess_JsonDeserializeObjectSymbol_End_Event;
            JsonDeserializeProcess.JsonDeserializePropertySymbol_Event += JsonDeserializeProcess.JsonDeserializeProcess_JsonDeserializePropertySymbol_Event;
            JsonDeserializeProcess.JsonDeserializeSeparateSymbol_Event += JsonDeserializeProcess.JsonDeserializeProcess_JsonDeserializeSeparateSymbol_Event;
            JsonDeserializeProcess.JsonDeserializeArraySymbol_Begin_Event += JsonDeserializeProcess.JsonDeserializeProcess_JsonDeserializeArraySymbol_Begin_Event;
            JsonDeserializeProcess.JsonDeserializeArraySymbol_End_Event += JsonDeserializeProcess.JsonDeserializeProcess_JsonDeserializeArraySymbol_End_Event;
        }
    }
}
