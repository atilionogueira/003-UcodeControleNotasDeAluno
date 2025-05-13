using Microsoft.AspNetCore.Identity;

namespace Ucode.Api.Models
{
    public class User : IdentityUser<long>
    {
        public string FullName { get; set; } = string.Empty;
        public List<IdentityRole<long>>? Roles { get; set; }
    }
}
