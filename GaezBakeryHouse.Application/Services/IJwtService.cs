using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaezBakeryHouse.Application.Services
{
    public interface IJwtService
    {
        string GenerateToken(string userId);
    }
}
