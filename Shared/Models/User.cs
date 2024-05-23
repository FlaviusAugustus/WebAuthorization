using Microsoft.AspNetCore.Identity;
namespace Shared.Models;

public class User : IdentityUser<Guid>, IEntity
{
    public DateTime CreatedAt { get; set; }
}