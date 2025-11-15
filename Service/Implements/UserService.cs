using BusinessObjects.DTO;
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
    }
}
