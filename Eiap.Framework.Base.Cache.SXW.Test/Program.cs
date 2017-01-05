using Eiap.Framework.Base.AssemblyService;
using Eiap.Framework.Base.Dependency.SXW;
using System;
using Newtonsoft.Json;

namespace Eiap.Framework.Base.Cache.SXW.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyManager.Instance.LoadAllAssembly().Register(DependencyManager.Instance.Register);
            ICacheManager cacheManager = DependencyManager.Instance.Resolver<ICacheManager>();
            CacheEntityTest t1 = new CacheEntityTest { Age = 20, Birthday = DateTime.Now, Id = Guid.NewGuid(), Money = 100m, Name = "123456" };
            cacheManager.SetCache(t1.Id.ToString(), t1);
            cacheManager.SetCache(t1.Id.ToString(), t1);
            Console.WriteLine("t1:" + JsonConvert.SerializeObject(cacheManager.GetCache(t1.Id.ToString())));
            Console.ReadLine();
        }
    }

    public class CacheEntityTest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public DateTime? Birthday { get; set; }
        public decimal? Money { get; set; }
    }
}
