using Microsoft.AspNetCore.Identity;

namespace GaezBakeryHouse.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order> Orders { get; set; }
    }
}
