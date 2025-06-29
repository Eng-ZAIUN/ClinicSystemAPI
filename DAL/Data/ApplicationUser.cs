using Microsoft.AspNetCore.Identity;

namespace DAL.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
