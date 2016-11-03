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
        private ConcurrentDictionary<string, int> _DicIndex = null;//缓存Key和队列的索引
        private readonly int _CacheMaxLength; //缓存最大值（byte）
        private readonly decimal _CurrentCacheClearScale;//当前缓存清理比例
        private readonly CacheClearMode _CacheClearMode;
        private int _CurrentLength = 0;//当前缓存总大小（byte）
        ConcurrentQueue<CacheEntity> _DictValue = null;//缓存对了

        public LocalCacheManager()
        {
            _CacheMaxLength = 1024000000;
            _CurrentCacheClearScale = 0.7m;
            _CacheClearMode = CacheClearMode.FIFO;
            _DicIndex = new ConcurrentDictionary<string, int>();
            _DictValue = new ConcurrentQueue<CacheEntity>();
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
            string jsonObject = JsonConvert.SerializeObject(cacheContent);
            //当前缓存大小
            int currentCacheLength = UnicodeEncoding.UTF8.GetByteCount(jsonObject);
            //如果当前缓存大小+当前缓存总大小>=缓存最大值*当前缓存清理比例
            if (_CurrentLength + currentCacheLength >= _CacheMaxLength * _CurrentCacheClearScale)
            { 
                //触发清理缓存事件
            }
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

        /// <summary>
        /// 清理缓存（先进先出原则）
        /// </summary>
        private void ClearCache()
        { 
            
        }
    }
}
