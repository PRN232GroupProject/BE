using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Context;
using Repository.Data.Entities;
using Repository.Data.Requests;
using Repository.Data.Responses;
using Repository.Repositories.Interfaces;
using Service.Helper;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork<ChemProjectDbContext> _unitOfWork;
        private readonly IConfiguration _config;

        public AuthService(IUnitOfWork<ChemProjectDbContext> unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var userRepo = _unitOfWork.GetRepository<User>();

            var user = await userRepo.FirstOrDefaultAsync(
                predicate: u => u.Email == request.Username,
                include: q => q.Include(u => u.Role)
            );

            if (user == null || !PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
                return ApiResponse<LoginResponse>.Fail("Invalid username or password.");

            // Sinh JWT
            var token = GenerateJwtToken(user);

            var response = new LoginResponse
            {
                Token = token,
                Role = user.Role.Name
            };

            return ApiResponse<LoginResponse>.Ok(response, "Login successful");
        }
        public async Task<ApiResponse<string>> RegisterAsync(string fullName, string email, string password, int roleId)
        {
            var userRepo = _unitOfWork.GetRepository<User>();

            var existing = await userRepo.FirstOrDefaultAsync(predicate: u => u.Email == email);
            if (existing != null)
                return ApiResponse<string>.Fail("Email already exists");

            var newUser = new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = PasswordHelper.HashPassword(password),
                RoleId = roleId,
                CreatedAt = DateTime.UtcNow
            };

            await userRepo.InsertAsync(newUser);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<string>.Ok("User registered successfully");
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
