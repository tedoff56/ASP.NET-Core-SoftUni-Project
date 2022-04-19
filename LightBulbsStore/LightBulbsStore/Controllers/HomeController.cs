using System.Diagnostics;
using LightBulbsStore.Core.Models;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Core.Services.Models.ContactForm;
using Microsoft.AspNetCore.Mvc;

namespace LightBulbsStore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;

    private readonly IEmailService emailService;

    public HomeController(
        ILogger<HomeController> _logger,
        IEmailService _emailService)
    {
        logger = _logger;
        emailService = _emailService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Stores()
    {
        return View();
    }

    public IActionResult Contacts()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Contacts(ContactFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var toAddress = new EmailAddress()
        {
            Address = "krushki.com.contacts@gmail.com",
        };

        toAddress.Name = toAddress.Address.Split('@')[0];

        var message = new EmailMessage
        {
            FromAddresses = new List<EmailAddress> { new EmailAddress()
            {
                Address = model.Email,
                Name = model.Email.Split('@')[0]
            }},
            ToAddresses = new List<EmailAddress> { toAddress },
            Content = model.Message,
            Subject = model.Subject
        };

        emailService.Send(message);

        return RedirectToAction(nameof(Index));
    }


}