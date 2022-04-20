using LightBulbsStore.Core.Models.Cart;
using LightBulbsStore.Core.Services.Models.Cart;

namespace LightBulbsStore.Core.Services.Contracts
{
    public interface ICartService
    {
        Task<List<CartProductViewModel>> GetCartProductsAsync(string userId);

        Task<bool> AddProductAsync(AddProductServiceModel model);

        Task<int> TotalCartItemsAsync(string userId);

        Task<decimal> TotalPrice(string userId);

        Task RemoveProductAsync(string userId, string productId);

        Task UpdateAsync(UpdateProductQuantityServiceModel model);

        Task EmptyCartAsync(string cartId);

        Task<bool> IsEmpty(string userId);

        Task<string> GetCartIdAsync(string userId);

        Task<CartViewModel> GetCartModelAsync(string userId);
    }
}
