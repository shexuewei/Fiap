using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.DTOMapper.SXW
{
    public class DTOMapper : IDTOMapper
    {
        public T Mapper<T>(object entity)
        {
            return Mapper<T>(entity, null);
        }

        public T Mapper<T>(object entity, object mapperEntity)
        {
            if (entity.GetType().IsGenericType)
            {
                return MapperToGene<T>(entity, mapperEntity);
            }
            else
            {
                return MapperToObject<T>(entity, mapperEntity);
            }
        }

        private T MapperToGene<T>(object entity, object mapperEntity)
        {
            Type genType = typeof(T).GetGenericTypeDefinition();
            Type genParaType = typeof(T).GetGenericArguments()[0];
            Type objtype = genType.MakeGenericType(genParaType);
            object objlist = mapperEntity == null ? Activator.CreateInstance(objtype) : mapperEntity;
            if (objlist is IList && entity is IList)
            {
                IList list = (IList)objlist;
                IList entitylist = (IList)entity;

                foreach (var objentity in entitylist)
                {
                    object obj = Activator.CreateInstance(genParaType);
                    GetObjectValue(objentity, obj);
                    list.Add(obj);
                }
                return (T)list;
            }
            return default(T);
        }

        private T MapperToObject<T>(object entity, object mapperEntity)
        {
            object obj = mapperEntity == null ? Activator.CreateInstance(typeof(T)) : mapperEntity;
            GetObjectValue(entity, obj);
            return (T)obj;
        }

        private object GetObjectValue(object entity, object mapperEntity)
        {
            PropertyInfo[] propList = entity.GetType().GetProperties();
            foreach (PropertyInfo tmpInfo in propList)
            {
                PropertyInfo propInfo = null;
                if (propInfo == null)
                {
                    propInfo = mapperEntity.GetType().GetProperty(tmpInfo.Name);
                }
                if (propInfo == null)
                {
                    propInfo = mapperEntity.GetType().GetProperty(tmpInfo.Name.Replace(mapperEntity.GetType().Name, ""));
                }
                if (propInfo == null)
                {
                    propInfo = mapperEntity.GetType().GetProperty(entity.GetType().Name + tmpInfo.Name);
                }
                if (propInfo != null && propInfo.PropertyType == tmpInfo.PropertyType)
                {
                    propInfo.SetValue(mapperEntity, tmpInfo.GetValue(entity, null), null);
                }
            }
            return mapperEntity;
        }
    }
}
