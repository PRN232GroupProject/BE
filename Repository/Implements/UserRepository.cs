using BusinessObjects.DAO.Base.Implements;
using BusinessObjects.DAO.Implements;
using BusinessObjects.DAO.Interfaces;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDAO _userDao;

        public UserRepository(IUserDAO userDAO) 
        {
            _userDao = userDAO;
        }

        public async Task<User?> GetUserByEmailWithRoleAsync(string email)
        {
            return await _userDao.GetUserByEmailAsync(email);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userDao.EmailExistsAsync(email);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            return await _userDao.UpdateUserAsync(user);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _userDao.GetUserByIdAsync(userId);
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            return await _userDao.CreateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _userDao.GetUserByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            await _userDao.DeleteUserAsync(userId);
            return true;
        }   
    }
}
