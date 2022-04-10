using LightBulbsStore.Core.Models.Cart;

namespace LightBulbsStore.Core.Services.Contracts
{
    public interface ICartService
    {
        Task<IEnumerable<CartProductViewModel>> GetProductsAsync(string userId);

        Task<bool> AddProductAsync(string productId, string userId);

        Task<int> TotalCartItemsAsync(string userId);

        Task<decimal> TotalPrice(string userId);

        Task UpdateCartAsync(CartViewModel cartViewModel, string userId);

        Task<string> GetCartIdAsync(string userId);

        Task RemoveProductAsync(string userId, string productId);

        Task UpdateAsync(CartViewModel model);

        Task EmptyCartAsync(string userId);

        Task<bool> IsEmpty(string userId);
    }
}
