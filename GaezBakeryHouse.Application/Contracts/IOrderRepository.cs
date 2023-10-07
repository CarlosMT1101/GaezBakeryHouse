using GaezBakeryHouse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaezBakeryHouse.Application.Contracts
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        
    }
}
