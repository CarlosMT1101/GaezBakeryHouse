using System;
using System.Collections.Generic;

namespace GaezBakeryHouse.Domain.Entities;

public partial class ShoppingCart
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Product Product { get; set; }
}
