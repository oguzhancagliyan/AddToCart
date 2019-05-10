using AddToCartBusiness.Interfaces;
using System;
using System.Threading.Tasks;

namespace AddToCartBusiness.Managers
{
    public class RedisProvider : ICacheProvider
    {
        public async Task<bool> Exist(string key)
        {
            return await RedisCacheManager.Instance.Database.KeyExistsAsync(key);
        }

        public async Task<T> Get<T>(string key, bool zip = false) where T : class
        {
            return RedisCacheManager.Instance.DeSerializeData<T>(await RedisCacheManager.Instance.Database.StringGetAsync(key), zip);
        }

        public async Task Remove(string key)
        {
            await RedisCacheManager.Instance.Database.KeyDeleteAsync(key);
        }

        public async Task Set<T>(string key, T data, TimeSpan cacheTime, bool zip = false) where T : class
        {
            if (data == null)
                return;
            else
            {
                await RedisCacheManager.Instance.Database.StringSetAsync(key, RedisCacheManager.Instance.SerializeData(data, zip), cacheTime);
            }
        }
    }
}