using BusinessObjects.DTO;
using BusinessObjects.Entities;
using BusinessObjects.Mapper;
using BusinessObjects.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Context;
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
        private readonly IMapperlyMapper _mapper;

        public AuthService(IUnitOfWork<ChemProjectDbContext> unitOfWork, IConfiguration config, IMapperlyMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _mapper = mapper;
        }

        public async Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var userRepo = _unitOfWork.GetRepository<User>();

            var user = await userRepo.FirstOrDefaultAsync(
                predicate: u => u.Email == request.Email,
                include: q => q.Include(u => u.Role)
            );

            if (user == null || !PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
            {
                return ServiceResult<LoginResponse>.Failure(
                    message: "Invalid username or password.",
                    statusCode: 401
                );
            }

            // Map user to LoginResponse (only maps Role)
            var response = _mapper.UserToLoginResponse(user);

            // Generate JWT token
            response.Token = GenerateJwtToken(user);

            return ServiceResult<LoginResponse>.Success(
                data: response,
                message: "Login successful",
                statusCode: 200
            );
        }

        public async Task<ServiceResult<string>> RegisterAsync(string fullName, string email, string password, int roleId)
        {
            var userRepo = _unitOfWork.GetRepository<User>();

            var existing = await userRepo.FirstOrDefaultAsync(predicate: u => u.Email == email);
            if (existing != null)
            {
                return ServiceResult<string>.Failure(
                    message: "Email already exists",
                    statusCode: 400
                );
            }

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

            return ServiceResult<string>.Success(
                data: "Registration completed",
                message: "User registered successfully",
                statusCode: 201
            );
        }

        public async Task<ServiceResult<string>> RegisterAsync(RegisterRequest request)
        {
            var userRepo = _unitOfWork.GetRepository<User>();

            var existing = await userRepo.FirstOrDefaultAsync(predicate: u => u.Email == request.Email);
            if (existing != null)
            {
                return ServiceResult<string>.Failure(
                    message: "Email already exists",
                    statusCode: 400
                );
            }

            var newUser = _mapper.RegisterRequestToUser(request);
            newUser.PasswordHash = PasswordHelper.HashPassword(request.Password);
            newUser.CreatedAt = DateTime.UtcNow;

            await userRepo.InsertAsync(newUser);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<string>.Success(
                data: "Registration completed",
                message: "User registered successfully",
                statusCode: 201
            );
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