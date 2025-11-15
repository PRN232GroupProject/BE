using BusinessObjects.DTO;
using BusinessObjects.Entities;
using BusinessObjects.Mapper;
using BusinessObjects.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
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
  
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IMapperlyMapper _mapper;

        public AuthService(
            
            IUserRepository userRepository,
            IConfiguration config,
            IMapperlyMapper mapper)
        {
            
            _userRepository = userRepository;
            _config = config;
            _mapper = mapper;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // Query logic được đóng gói trong repository
            var user = await _userRepository.GetUserByEmailWithRoleAsync(request.Email);

            if (user == null || !PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
            {
                return null;
            }

            // Map user to LoginResponse
            var response = _mapper.UserToLoginResponse(user);

            // Generate JWT token
            response.Token = GenerateJwtToken(user);

            return response;
        }

        public async Task<UserDTO> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        #region JWT Methods

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

        #endregion
    }
}