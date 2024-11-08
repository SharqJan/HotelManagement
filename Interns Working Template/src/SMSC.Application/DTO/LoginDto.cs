using System;

namespace SMSC.Application.DTO
{
    public class LoginDto
    {
        public int UserId { get; set; }        
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
