using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository 
    {
        /// Get user by email with role information
        Task<User> GetUserByEmailWithRoleAsync(string email);
        Task<User?> GetUserByIdAsync(int userId);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
    }
}
