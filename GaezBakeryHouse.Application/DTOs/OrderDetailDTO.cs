using GaezBakeryHouse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaezBakeryHouse.Application.DTOs
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductName { get; set; }
        
    }
}
