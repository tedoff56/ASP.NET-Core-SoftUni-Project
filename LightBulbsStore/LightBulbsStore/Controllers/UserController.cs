using LightBulbsStore.Core.Models.Customer;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace LightBulbsStore.Controllers
{
    [Route("/Identity/Account/Manage/")]
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<User> userManager;

        private readonly IUserService userService;

        public UserController(
            RoleManager<IdentityRole> _roleManager,
            UserManager<User> _userManager,
            IUserService _userService)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            userService = _userService;
        }

        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole()
        {

            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Administrator"
            });

            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Customer"
            });

            return Ok();
        }

        [Route("SetRole")]
        public async Task<IActionResult> SetRole()
        {
            var user = userManager.Users
                .Where(u => u.Email == "tedoff.96@gmail.com")
                .SingleOrDefault();

            if (user is null)
            {
                return BadRequest();
            }

            await userManager.AddToRoleAsync(user, "Administrator");

            return Ok();
        }

        [Route("CustomerInfo")]
        public async Task<IActionResult> CustomerInfo()
        {

            var userId = (await userManager.GetUserAsync(User)).Id;

            var customer = await userService.GetCustomerInfoAsync(userId);

            return View(customer);

        }

        [HttpPost]
        [Route("CustomerInfo")]
        public async Task<IActionResult> CustomerInfo(CustomerInfoViewModel customerInfoViewModel)
        {
            var userId = (await userManager.GetUserAsync(User)).Id;

            if(!ModelState.IsValid)
            {
                return View(customerInfoViewModel);
            }

            await userService.SetCustomerInfoAsync(userId, customerInfoViewModel);

            return View(customerInfoViewModel);
        }
    }
}
