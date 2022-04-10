using LightBulbsStore.Core.Models.Customer;

namespace LightBulbsStore.Core.Services.Contracts
{
    public interface IUserService
    {

        Task<CustomerInfoViewModel> GetCustomerInfoAsync(string userId);

        Task SetCustomerInfoAsync(string userId, CustomerInfoViewModel model);

        Task CreateCustomerAsync(string userId);

    }
}
