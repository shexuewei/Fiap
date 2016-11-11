using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Eiap.Framework.Base.Cache.SXW
{
    public class LocalCacheManager : ICacheManager
    {
        private readonly int _CacheMaxLength; //缓存最大值（byte）
        private readonly decimal _CurrentCacheClearScale;//当前缓存清理比例
        private readonly CacheClearMode _CacheClearMode;
        private int _CurrentLength = 0;//当前缓存总大小（byte）
        ConcurrentDictionary<string, CacheEntity> _DicCacheValue = null;//缓存对象

        public LocalCacheManager()
        {
            _CacheMaxLength = 1024000000;
            _CurrentCacheClearScale = 0.7m;
            _CacheClearMode = CacheClearMode.LFU;
            _DicCacheValue = new ConcurrentDictionary<string, CacheEntity>();
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheContent"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        public void SetCache(string key, object cacheContent, int? absoluteExpiration = null, int? slidingExpiration = null)
        {
            string jsonObject = JsonConvert.SerializeObject(cacheContent);
            //当前缓存大小
            int currentCacheLength = UnicodeEncoding.UTF8.GetByteCount(jsonObject);
            //如果当前缓存大小+当前缓存总大小>=缓存最大值*当前缓存清理比例
            if (_CurrentLength + currentCacheLength >= _CacheMaxLength * _CurrentCacheClearScale)
            { 
                //触发清理缓存事件
            }
            _DicCacheValue.TryAdd(key, CreateCacheEntity(jsonObject, currentCacheLength, absoluteExpiration, slidingExpiration));
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetCache(string key)
        {
            CacheEntity cacheEntity = null;
            if (GetCacheEntity(key, out cacheEntity))
            {
                if (cacheEntity.AbsoluteExpiration.HasValue)
                {
                    if (DateTime.Now > cacheEntity.AbsoluteExpiration.Value)
                    {
                        RemoveCache(key);
                        return null;
                    }
                    else if (cacheEntity.SlidingExpiration.HasValue)
                    {
                        cacheEntity.AbsoluteExpiration = DateTime.Now.AddSeconds(cacheEntity.SlidingExpiration.Value);
                    }
                }
                cacheEntity.CacheReferencesCount++;
                return cacheEntity.CacheValue;
            }
            return null;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public bool RemoveCache(string key)
        {
            CacheEntity cacheEntity = null;
            return _DicCacheValue.TryRemove(key, out cacheEntity);
        }

        /// <summary>
        /// 获取缓存个数
        /// </summary>
        /// <returns></returns>
        public int GetCacheCount()
        {
            if (_DicCacheValue.IsEmpty)
            {
                return 0;
            }
            return _DicCacheValue.Count;
        }

        /// <summary>
        /// 获取缓存所有键
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllCacheKey()
        {
            if (_DicCacheValue.IsEmpty)
            {
                return null;
            }
            return _DicCacheValue.Keys.ToList();
        }

        /// <summary>
        /// 清理缓存
        /// </summary>
        private void ClearCache()
        { 
            
        }

        /// <summary>
        /// 创建缓存对象
        /// </summary>
        /// <param name="cacheValue"></param>
        /// <param name="cacheLength"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        /// <returns></returns>
        private CacheEntity CreateCacheEntity(string cacheValue,int cacheLength, int? absoluteExpiration = null, int? slidingExpiration = null)
        {
            CacheEntity cacheEntity = new CacheEntity { CacheValue = cacheValue, CacheLength = cacheLength, CacheReferencesCount = 0, CacheVersion = 1 };
            if (absoluteExpiration.HasValue)
            {
                cacheEntity.AbsoluteExpiration = DateTime.Now.AddSeconds(absoluteExpiration.Value);
            }
            if (slidingExpiration.HasValue)
            {
                cacheEntity.SlidingExpiration = slidingExpiration.Value;
            }
            if (!absoluteExpiration.HasValue && slidingExpiration.HasValue)
            {
                cacheEntity.AbsoluteExpiration = DateTime.Now.AddSeconds(slidingExpiration.Value);
            }
            return cacheEntity;
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheEntity"></param>
        /// <returns></returns>
        private bool GetCacheEntity(string key, out CacheEntity cacheEntity)
        {
            return _DicCacheValue.TryGetValue(key, out cacheEntity);
        }
    }
}
