using BusinessObjects.DAO.Implements;
using BusinessObjects.DAO.Interfaces;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Implements
{
    public class UserRepository :GenericRepository<User>, IUserRepository
    {
        private readonly IUserDAO _userDao;

        public UserRepository(
            
            IUserDAO userDao,
            ChemProjectDbContext context
        ) : base(context)
        {
            _userDao = userDao;
        }

        public async Task<User?> GetUserByEmailWithRoleAsync(string email)
        {
            return await FirstOrDefaultAsync(
                predicate: u => u.Email == email,
                include: q => q.Include(u => u.Role)
            );
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userDao.EmailExistsAsync(email);
        }

        public async Task<User?> GetUserForAuthenticationAsync(string email)
        {
            return await FirstOrDefaultAsync(
                predicate: u => u.Email == email,
                include: q => q.Include(u => u.Role)
            );
        }
    }
}
