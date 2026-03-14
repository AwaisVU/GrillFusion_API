using Microsoft.AspNetCore.Identity;

namespace GrillFusion_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
    }
}
