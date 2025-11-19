using BusinessObjects.DTO;
using BusinessObjects.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetCurrentUser();
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO?> GetUserById(int userId);
        Task<bool> CreateUser(UserRequestDTO userRequest);
        Task<bool> UpdateUser(int userId, UserRequestDTO userRequest);
        Task<bool> DeleteUser(int userId);
        Task<bool> UpdateProfile(UpdateProfileRequest request);
        Task<bool> UpdatePassword(ChangePasswordRequest request);
    }
}
