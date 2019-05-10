using System;
using System.Threading.Tasks;

namespace AddToCartBusiness.Interfaces
{
    public interface ICacheProvider
    {
        Task<T> Get<T>(string key, bool zip) where T : class;

        Task Set<T>(string key, T data, TimeSpan cacheTime, bool zip) where T : class;

        Task Remove(string key);

        Task<bool> Exist(string key);
    }
}