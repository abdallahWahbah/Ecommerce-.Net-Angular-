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

namespace ECommerce.Application.Features.Auth.Commands.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public RegisterHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<RegisterResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<User>();

            var existingUser = (await repo.FindAsync(x => x.Email == request.Email)).FirstOrDefault();

            if (existingUser != null)
                throw new ValidationException(new[]
                {
                    new ValidationFailure("Register", "Email already exists")
                });

            var passwordHasher = new PasswordHasher<User>();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Role = "User"
            };

            user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

            await repo.AddAsync(user);

            await _unitOfWork.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role);

            return new RegisterResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Token = token
            };
        }
    }
}
