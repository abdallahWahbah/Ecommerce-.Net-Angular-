using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Auth.Commands.Login
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
