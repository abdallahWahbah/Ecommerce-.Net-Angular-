namespace ECommerce.Application.Features.Auth.Commands.Register
{
    public class RegisterResponseDto
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
