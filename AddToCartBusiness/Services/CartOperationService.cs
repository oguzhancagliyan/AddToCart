using AddToCartBusiness.Data;
using AddToCartBusiness.Interfaces;
using AddToCartBusiness.Managers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddToCartBusiness.Services
{
    public class CartOperationService : ICartOperation
    {
        private IMongoDB<Product> _product;
        private RedisProvider _redisProvider;

        public CartOperationService(IMongoDB<Product> product)
        {
            _product = product;
            _redisProvider = new RedisProvider();
        }

        public async Task<Cart> AddToCartAsync(long product, string userId)
        {
            Random random = new Random();

            var userCart = await _redisProvider.Get<Cart>(string.Format(Constants.USER_CART_INFO, userId));
            if (userCart == null)
                userCart = new Cart
                {
                    Products = new List<Product>(),
                    Id = random.Next(0, 1000)
                };

            var currentProduct = await _product.GetAsync(product.ToString());
            userCart.Products.Add(currentProduct);
            await _redisProvider.Set(string.Format(Constants.USER_CART_INFO, userId), userCart, TimeSpan.FromDays(3));

            return userCart;
        }

        public Task<Cart> DeleteProductFromCartAsync(Product product, long cartId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsProductEnoughInStockAsync(string productId, long count)
        {
            try
            {
                var result = await _product.GetAsync(productId);
                return (count > result.Quantity ? false : true);
            }
            catch (NullReferenceException ex)
            {
                throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}