using LightBulbsStore.Core.Models.Cart;

namespace LightBulbsStore.Core.Services.Contracts
{
    public interface ICartService
    {
        Task<IEnumerable<CartProductViewModel>> GetProducts(string userId);

        Task<bool> AddProduct(string productId, string userId);

        Task<int> TotalCartItems(string userId);

        Task<decimal> TotalPrice(string userId);

        Task UpdateCart(CartViewModel cartViewModel);
    }
}
