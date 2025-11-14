using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IUserRepository 
    {
        /// Get user by email with role information
        Task<User> GetUserByEmailWithRoleAsync(string email);

        /// Check if email already exists
        Task<bool> EmailExistsAsync(string email);

        /// Get user for authentication (includes role)
        Task<User> GetUserForAuthenticationAsync(string email);
    }
}
