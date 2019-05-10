using AddToCartBusiness.Dto;
using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AddToCart.Test
{
    public class CartControllerShould : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;
        public CartControllerShould(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task AddToCartShouldReturnCartData()
        {
            var fixture = new Fixture();
            var requestDto = fixture.Build<AddToCartRequestDto>()
                .With(x => x.ProductId, 1)
                .With(x => x.Quantity, 100)
                .Create();

            var response = await _fixture.Client.PostAsync("api/cart/add", new StringContent(JsonConvert.SerializeObject(requestDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData(100, 0)]
        [InlineData(0, 1)]
        public async Task AddToCartShouldReturnErrorWhenQuantityOrProductIdEqualsZero(int quantity, int productId)
        {
            var fixture = new Fixture();
            var requestDto = fixture.Build<AddToCartRequestDto>()
                .With(x => x.ProductId, productId)
                .With(x => x.Quantity, quantity)
                .Create();

            var response = _fixture.Client.PostAsync("api/cart/add", new StringContent(JsonConvert.SerializeObject(requestDto), Encoding.UTF8, "application/json")).Result;

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<GeneralResponse<string>>(responseContent);
            responseJson.ErrorMsg.Should().Be("ProductId or Quantity can not be 0");
        }

        [Fact]
        public async Task AddToCartShouldReturnErrorWhenProductNotEnoughInStock()
        {
            var fixture = new Fixture();
            var requestDto = fixture.Build<AddToCartRequestDto>()
                .With(x => x.ProductId, 2)
                .With(x => x.Quantity, 50000)
                .Create();

            var response = _fixture.Client.PostAsync("api/cart/add", new StringContent(JsonConvert.SerializeObject(requestDto), Encoding.UTF8, "application/json")).Result;

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<GeneralResponse<string>>(responseContent);
            responseJson.ErrorMsg.Should().Be("There are not enough products in the warehouse");
        }
    }
}