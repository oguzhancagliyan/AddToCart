using AddToCartBusiness.Data;
using AddToCartBusiness.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AddToCartBusiness.Services
{
    public class MongoDBProductService : IMongoDB<Product>
    {
        private readonly IMongoCollection<Product> _product;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private Random rnd;

        public MongoDBProductService(IConfiguration config)
        {
            try
            {
                _client = new MongoClient(config.GetConnectionString("MongoProductDb"));
                _database = _client.GetDatabase("MongoProductDb");
                _product = _database.GetCollection<Product>("Product");
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (MongoConnectionException ex)
            {
                throw ex;
            }
            catch (MongoConfigurationException ex)
            {
                throw ex;
            }
        }

        public void Create(Product instance)
        {
            _product.InsertOne(instance);
        }

        public Task CreateAsync(Product instance)
        {
            throw new NotImplementedException();
        }

        public void CreateMany(List<Product> createList)
        {
            throw new NotImplementedException();
        }

        public Task CreateManyAsync(List<Product> createList)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product instance)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Product deleteItem)
        {
            throw new NotImplementedException();
        }

        public void DeleteMany(List<Product> deleteList)
        {
            throw new NotImplementedException();
        }

        public Task DeleteManyAsync(List<Product> deleteList)
        {
            throw new NotImplementedException();
        }

        public Product Get(string Id)
        {
            return _product.Find(a => a.Id == Id).FirstOrDefault();
        }

        public List<Product> GetAll()
        {
            return _product.Find(_ => true).ToList();
        }

        public Task<List<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetAsync(string id)
        {
            try
            {
                var result =  _product.Find(pr => pr.Id == id);

                if (!(await result.AnyAsync()))
                {
                    rnd = new Random();
                    Create(new Product
                    {
                        Id = id,
                        Price = rnd.Next(1, 100000),
                        ProductName = GenerateCoupon(rnd.Next(0, 10), rnd),
                        Quantity = rnd.Next(1, 1000),
                    });
                    result =  _product.Find(pr => pr.Id == id);
                }

                return await result.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private string GenerateCoupon(int length, Random random)
        {
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

        public Task<List<Product>> Search(Expression<Func<Product, bool>> expresion)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, Product instance)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string id, Product instance)
        {
            throw new NotImplementedException();
        }

        public void UpdateMany(IDictionary<string, Product> updateList)
        {
            throw new NotImplementedException();
        }

        public Product UpdateManyAsync(IDictionary<string, Product> updateList)
        {
            throw new NotImplementedException();
        }

        Task IMongoDB<Product>.UpdateManyAsync(IDictionary<string, Product> updateList)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            var result2 = _product.DeleteMany(_ => true);
        }
    }
}