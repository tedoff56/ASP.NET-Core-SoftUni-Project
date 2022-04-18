using LightBulbsStore.Core.Models.Customer;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data.Models;
using LightBulbsStore.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LightBulbsStore.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IBulbsStoreDbRepository repo;


        public UserService(
            IBulbsStoreDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task CreateCustomerAsync(string userId)
        {
            var user = await repo.GetByIdAsync<User>(userId);

            user.Customer = new Customer()
            {
                Cart = new Cart()
            };

            repo.Update(user);
            await repo.SaveChangesAsync();
        }

        public async Task<CustomerInfoViewModel> GetCustomerInfoAsync(string userId)
        {
            var user = await repo.GetByIdAsync<User>(userId);

            return new CustomerInfoViewModel()
            {
                FirstName = user.Customer.FirstName,
                LastName = user.Customer.LastName,
                PhoneNumber = user.Customer.PhoneNumber,
                City = user.Customer.City,
                Address = user.Customer.Address,
                ZipCode = user.Customer.ZipCode
            };
        }



        public async Task SetCustomerInfoAsync(string userId, CustomerInfoViewModel model)
        {
            var user = await repo.GetByIdAsync<User>(userId);

            user.Customer.FirstName = model.FirstName;
            user.Customer.LastName = model.LastName;
            user.Customer.PhoneNumber = model.PhoneNumber;
            user.Customer.City = model.City;
            user.Customer.Address = model.Address;
            user.Customer.ZipCode = model.ZipCode;

            repo.Update(user);
            await repo.SaveChangesAsync();
        }

    }
}
