using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public  class UserDTO
    {
        public required  string Id { get; set; }
        public required string email { get; set; }

        public required string displayName { get; set; }

        public string? ImageUrl { get; set; }

        public required string Token { get; set; }
    }
}
