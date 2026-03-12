using MediatR;

namespace ECommerce.Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<RegisterResponseDto>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
