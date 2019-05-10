using Microsoft.Extensions.Configuration;
using System;

namespace AddToCartUtility.Managers
{
    public class AddToCartConfigurationManager
    {
        IConfiguration Configuration;
        public IConfiguration GetConfiguration() => Configuration;
        public static AddToCartConfigurationManager Instance { get; } = new AddToCartConfigurationManager();
        private static readonly Lazy<AddToCartConfigurationManager> lazy = new Lazy<AddToCartConfigurationManager>();
        public string GetDatabaseConnection() => Configuration.GetConnectionString("DefaultConnection");
        public string GetRedisConnection() => Configuration.GetConnectionString("RedisConnectionString");
        public string GetMongoConnection() => Configuration.GetConnectionString("MongoProductDb");

        static AddToCartConfigurationManager()
        {

        }

        AddToCartConfigurationManager()
        {

        }

        public void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}