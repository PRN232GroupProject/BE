using BusinessObjects.Context;
using BusinessObjects.DAO.Base.Implements;
using BusinessObjects.DAO.Interfaces;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DAO.Implements
{
    public class UserDAO : GenericRepository<User>, IUserDAO
    {
        public UserDAO(ChemProjectDbContext context) : base(context)
        {
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Role)
                .AnyAsync(u => u.Email == email);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            if (user == null)
            {
                return false;
            }
            await UpdateAsync(user);
            return true;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.StudentTestSessions).ThenInclude(sts => sts.Test)
                .Include(u => u.StudentTestSessions).ThenInclude(sts => sts.StudentAnswers)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            var existingUser = await GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return false; // Email already exists
            }

            return await CreateAsync(user) > 0;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var existingUser = GetByIdAsync(userId).Result;
            if (existingUser == null)
            {
                return false; // User does not exist
            }

            existingUser.IsActive = false;
            await UpdateAsync(existingUser);
            return true;

        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .ToListAsync();
        }
    }
}
