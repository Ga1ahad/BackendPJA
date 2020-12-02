using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clothesy.Api.DTOs
{
    public class RegisterRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
