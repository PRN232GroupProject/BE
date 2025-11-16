using BusinessObjects.DAO.Base.Interfaces;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DAO.Interfaces
{
    public interface IUserDAO : IGenericRepository<User>
    {
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}