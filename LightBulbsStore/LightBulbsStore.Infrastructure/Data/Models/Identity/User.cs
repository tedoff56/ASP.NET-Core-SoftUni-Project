using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class User : IdentityUser
{
    public string? CustomerId { get; set; }
    public Customer Customer { get; set; }
}