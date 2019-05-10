using AddToCartBusiness.Interfaces;
using AddToCartBusiness.Services;
using FluentAssertions;
using System;
using Xunit;

namespace AddToCart.Test
{
    public class CartOperationServiceTests : TestBase
    {
        private readonly ICartOperation _cartOperation;

        public CartOperationServiceTests()
        {
            _cartOperation = new CartOperationService(new MongoDBProductService(Configuration));
        }

        [Theory]
        [InlineData(32)]
        public async void AddToCartShouldAddProductToCart(long productId)
        {
            var cart = await _cartOperation.AddToCartAsync(productId, Guid.NewGuid().ToString());

            cart.Should().NotBeNull();
        }
    }
}
