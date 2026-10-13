using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public  class RegisterDto
    {
        [Required]
        public string? displayName { get; set; }

        [Required]
        [EmailAddress]
        
        public required string email { get; set; }

        [Required]
        [MinLength(5)]
        public required string password { get; set; }


    }
}
