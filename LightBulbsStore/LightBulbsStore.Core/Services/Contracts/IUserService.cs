using LightBulbsStore.Core.Models.Customer;
using LightBulbsStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Services.Contracts
{
    public interface IUserService
    {

        Task<CustomerInfoViewModel> GetCustomerInfo(string userId);

        Task SetCustomerInfo(string userId, CustomerInfoViewModel model);

        Task CreateCustomer(string userId);

    }
}
