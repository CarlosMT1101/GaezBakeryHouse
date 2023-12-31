﻿using System.ComponentModel.DataAnnotations;

namespace GaezBakeryHouse.Application.DTOs
{
    public class RegistrationRequestDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
