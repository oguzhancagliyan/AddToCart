using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AddToCartBusiness.Interfaces
{
    public interface IMongoDB<T>
    {
        T Get(string id);
        List<T> GetAll();
        void Create(T instance);
        void CreateMany(List<T> createList);
        void Update(string id, T instance);
        void UpdateMany(IDictionary<string, T> updateList);
        void Delete(T instance);
        void DeleteMany(List<T> deleteList);
        void DeleteAll();
        Task<List<T>> Search(Expression<Func<T, bool>> expresion);

        #region Async

        /// <summary>
        /// Get one result from an id
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="id">Id</param>
        /// <returns>T result</returns>
        Task<T> GetAsync(string id);
        /// <summary>
        /// Get all list from MongoDb via async
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <returns>Return all element from DB</returns>
        Task<List<T>> GetAllAsync();
        Task CreateAsync(T instance);
        Task CreateManyAsync(List<T> createList);
        Task UpdateAsync(string id, T instance);
        Task UpdateManyAsync(IDictionary<string, T> updateList);
        Task DeleteAsync(T instance);
        Task DeleteManyAsync(List<T> deleteList);

        #endregion
    }
}