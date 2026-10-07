using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public  class AppUsers
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required  string DisplayName { get; set; }

        public required  string Email { get; set; }


    }
}
