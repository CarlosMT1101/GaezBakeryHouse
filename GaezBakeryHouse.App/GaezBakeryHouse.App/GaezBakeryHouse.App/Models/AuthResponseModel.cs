using System;

namespace GaezBakeryHouse.App.Models
{
    public class AuthResponseModel
    {
        public string ApplicationUserId { get; set; }

        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
