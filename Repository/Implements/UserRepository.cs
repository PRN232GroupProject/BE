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
    }
}
