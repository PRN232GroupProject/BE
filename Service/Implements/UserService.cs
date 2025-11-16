using BusinessObjects.DTO;
using BusinessObjects.DTO.User;
using BusinessObjects.Entities;
using BusinessObjects.Mapper;
using Microsoft.AspNetCore.Http;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implements
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IMapperlyMapper _mapper;

        public UserService(IHttpContextAccessor httpContextAccessor, 
                           IUserRepository userRepository,
                           IMapperlyMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _mapper = mapper;   
        }

        public async Task<bool> CreateUser(UserRequestDTO userRequest)
        {
            try
            {
                if (await CheckExistingUser(userRequest.Email))
                {
                    throw new Exception("User with this email already exists.");
                }

                var user = _mapper.RequestDTOToUser(userRequest);
                return await _userRepository.CreateUserAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByIdAsync(userId);

                if (existingUser == null)
                {
                    throw new Exception("User does not exist.");
                }

                return await _userRepository.DeleteUserAsync(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();

                var dtoList = users.Select(user => _mapper.UserToUserDto(user)).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserDTO> GetCurrentUser()
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var profile = await _userRepository.GetUserByIdAsync(int.Parse(userIdClaim));

                var dto = _mapper.UserToUserDto(profile);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserDTO?> GetUserById(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);

                return _mapper.UserToUserDto(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateUser(int userId, UserRequestDTO userRequest)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByIdAsync(userId);

                if (existingUser == null)
                {
                    throw new Exception("User does not exist.");
                }

                var user = _mapper.RequestDTOToUser(userRequest);

                return await _userRepository.UpdateUserAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> CheckExistingUser(string email)
        {
            var existingUser = await _userRepository.GetUserByEmailWithRoleAsync(email);
            if (existingUser == null)
            {
                return false;
            }
            return true;
        }
    }
}
