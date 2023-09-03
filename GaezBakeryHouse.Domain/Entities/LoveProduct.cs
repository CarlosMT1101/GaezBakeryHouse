using System;
using System.Collections.Generic;

namespace GaezBakeryHouse.Domain.Entities;

public partial class LoveProduct
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; }
}
