using GaezBakeryHouse.Domain.Common;
using System;
using System.Collections.Generic;

namespace GaezBakeryHouse.Domain.Entities;

public partial class Product : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public byte[] Image { get; set; }

    public int CategoryId { get; set; }

    public decimal? Discount { get; set; }

    public bool? InOffer { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<LoveProduct> LoveProducts { get; set; } = new List<LoveProduct>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
}
