using AddToCartBusiness.Data;
using System.Threading.Tasks;

namespace AddToCartBusiness.Interfaces
{
    public interface ICartOperation
    {
        Task<Cart> AddToCartAsync(long product, string userId);

        Task<Cart> DeleteProductFromCartAsync(Product product, long cartId);

        Task<bool> IsProductEnoughInStockAsync(string productId, long count);
    }
}