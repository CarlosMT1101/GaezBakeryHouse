using System;
using System.Collections.Generic;
using System.Text;

namespace GaezBakeryHouse.App.Models
{
    public class AuthResponseModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
