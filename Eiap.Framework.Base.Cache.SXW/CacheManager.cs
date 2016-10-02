using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Cache.SXW
{
    public class CacheManager : ICacheManager
    {
        private ConcurrentDictionary<string, CacheEntity> _Dict = null;

        public CacheManager()
        {
            _Dict = new ConcurrentDictionary<string, CacheEntity>();
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cachecontent"></param>
        /// <param name="CacheExpiredTimeType"></param>
        /// <param name="timer"></param>
        public void SetCache(string key, object cacheContent, int? absoluteExpiration = null, int? slidingExpiration = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetCache(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public void RemoveCache(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取缓存个数
        /// </summary>
        /// <returns></returns>
        public int GetCacheCount()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取缓存所有键
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllCacheKey()
        {
            throw new NotImplementedException();
        }
    }
}
