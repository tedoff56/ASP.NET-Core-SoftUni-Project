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

        private readonly UserManager<User> userManager;

        public UserService(
            IBulbsStoreDbRepository _repo,
            UserManager<User> _userManager)
        {
            repo = _repo;
            userManager = _userManager;
        }

        public async Task CreateCustomerAsync(string userId)
        {
            var user = await repo.GetByIdAsync<User>(userId);

            if (user is null)
            {
                return;
            }

            var customer = new Customer()
            {
                UserId = userId
            };

            await repo.AddAsync(customer);

            var cart = new Cart()
            {
                CustomerId = customer.Id
            };

            await repo.AddAsync(cart);

            await repo.SaveChangesAsync();
        }

        public async Task<CustomerInfoViewModel> GetCustomerInfoAsync(string userId)
        {
            var customer = await repo.All<Customer>()
                .SingleOrDefaultAsync(c => c.UserId == userId);

            if (customer is null)
            {
                return new CustomerInfoViewModel();
            }

            return new CustomerInfoViewModel()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                City = customer.City,
                Address = customer.Address,
                ZipCode = customer.ZipCode
            };
        }



        public async Task SetCustomerInfoAsync(string userId, CustomerInfoViewModel model)
        {
            var user = await repo.GetByIdAsync<User>(userId);

            var customer = await repo.All<Customer>()
                .FirstOrDefaultAsync(c => c.UserId == userId);

            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.PhoneNumber = model.PhoneNumber;
            customer.City = model.City;
            customer.Address = model.Address;
            customer.ZipCode = model.ZipCode;

            repo.Update(customer);
            await repo.SaveChangesAsync();
        }

    }
}
