using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Auth.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public LoginHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userRepo = _unitOfWork.Repository<User>();

            var user = (await userRepo.FindAsync(x => x.Email == request.Email))
                .FirstOrDefault();

            if (user == null)
                throw new ValidationException(new[]
                {
                    new ValidationFailure("Login", "Invalid email or password")
                });

            var passwordHasher = new PasswordHasher<User>();

            var result = passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password
            );

            if (result == PasswordVerificationResult.Failed)
                throw new ValidationException(new[]
                {
                    new ValidationFailure("Login", "Invalid email or password")
                });

            var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role);

            return new LoginResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role,
            };
        }
    }
}
