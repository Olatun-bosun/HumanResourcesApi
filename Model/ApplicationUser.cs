using Microsoft.AspNetCore.Identity;

namespace HumanResourcesApi.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
