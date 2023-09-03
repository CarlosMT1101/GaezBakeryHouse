using System;
using System.Collections.Generic;

namespace GaezBakeryHouse.Domain.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public byte[] Image { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
