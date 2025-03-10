
using Microsoft.AspNetCore.Identity;

namespace Repository.Models
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string? ImageProfile { get; set; }
    }
}
