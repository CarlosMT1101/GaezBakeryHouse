namespace GaezBakeryHouse.Domain.Entities;

public partial class Role
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string NormalizedName { get; set; }

    public string ConcurrencyStamp { get; set; }

    public ICollection<RoleClaim> RoleClaims { get; set; }

    public ICollection<User> Users { get; set; }
}
