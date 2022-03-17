using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class User : IdentityUser<Guid>
{
    [Key]
    public Guid Id { get; set; }
}