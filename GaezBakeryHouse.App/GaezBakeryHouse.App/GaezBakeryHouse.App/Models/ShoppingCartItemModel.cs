using System;
using System.Collections.Generic;
using System.Text;

namespace GaezBakeryHouse.App.Models
{
    public class ShoppingCartItemModel
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
