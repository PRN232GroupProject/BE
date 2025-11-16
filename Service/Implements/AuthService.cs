using BusinessObjects.DTO;
using BusinessObjects.DTO.User;
using BusinessObjects.DTO.User.Auth;
using BusinessObjects.Entities;
using BusinessObjects.Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
using Service.Interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Service.Implements
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IMapperlyMapper _mapper;
        private readonly TokenProvider _tokenProvider;

        public AuthService(

            IUserRepository userRepository,
            IConfiguration config,
            IMapperlyMapper mapper,
            TokenProvider tokenProvider)
        {

            _userRepository = userRepository;
            _config = config;
            _mapper = mapper;
            _tokenProvider = tokenProvider;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // Query logic được đóng gói trong repository
            var user = await _userRepository.GetUserByEmailWithRoleAsync(request.Email);

            bool isPasswordValid = VerifyPassword(request.Password, user?.PasswordHash ?? "");

            if (!isPasswordValid)
            {
                Console.WriteLine("Password verification failed");
                return new LoginResponse { };
            }

            if (!IsBCryptHash(user.PasswordHash))
            {
                try
                {
                    await UpdatePasswordAsync(user.Id, request.Password);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating password hash: {ex.Message}");
                }
            }

            // Map user to LoginResponse
            var response = _mapper.UserToLoginResponse(user);

            var token = _tokenProvider.GenerateToken(
                user.Id.ToString(),
                user.Email ?? string.Empty,
                new List<string> { user.Role.Name }
            );

            response.Token = token;

            return response;
        }
        public async Task<UserDTO> RegisterAsync(RegisterRequest request)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(request.Email) ||
                    string.IsNullOrWhiteSpace(request.Password) ||
                    string.IsNullOrWhiteSpace(request.FullName))
                {
                    throw new Exception("All fields are required.");
                }

                // Check if email already exists using the existing method
                var emailExists = await _userRepository.EmailExistsAsync(request.Email);
                if (emailExists)
                {
                    throw new Exception("Email already exists.");
                }

                // Map RegisterRequest to User entity
                var newUser = _mapper.RegisterRequestToUser(request);

                // Hash the password
                newUser.PasswordHash = HashPassword(request.Password);

                // Set default values
                newUser.CreatedAt = DateTime.UtcNow;
                newUser.IsActive = true;
                newUser.RoleId = 1; // Assuming 1 is the default role (e.g., "User" role)

                // Save to database - returns bool
                var isCreated = await _userRepository.CreateUserAsync(newUser);

                if (!isCreated)
                {
                    throw new Exception("Failed to create user. Email may already exist.");
                }

                // Get the created user with role information for mapping
                var createdUser = await _userRepository.GetUserByEmailWithRoleAsync(request.Email);

                if (createdUser == null)
                {
                    throw new Exception("User created but failed to retrieve user details.");
                }

                // Map to UserDTO and return
                return _mapper.UserToUserDto(createdUser);
            }
            catch (Exception ex)
            {
                throw new Exception($"Registration failed: {ex.Message}");
            }
        }
        public async Task<bool> UpdatePasswordAsync(int userId, string newPassword)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByIdAsync(userId);
                if (existingUser == null)
                {
                    throw new Exception("User not found.");
                }

                existingUser.PasswordHash = HashPassword(newPassword);
                return await _userRepository.UpdateUserAsync(existingUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        



        #region Helper Methods

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool IsBCryptHash(string password)
        {
            // BCrypt hashes start with $2a$, $2b$, $2y$, or $2x$ followed by cost
            return password != null &&
                   password.Length >= 60 &&
                   (password.StartsWith("$2a$") ||
                    password.StartsWith("$2b$") ||
                    password.StartsWith("$2y$") ||
                    password.StartsWith("$2x$"));
        }

        #endregion
    }
}