using Microsoft.Extensions.Configuration;
using System.IO;

namespace AddToCart.Test
{
    public class TestBase
    {
        public IConfiguration Configuration { get; }

        public TestBase()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }
    }
}