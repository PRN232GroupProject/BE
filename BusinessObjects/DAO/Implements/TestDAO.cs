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
    public class TestDAO : GenericRepository<Test>, ITestDAO
    {
        public TestDAO(ChemProjectDbContext context) : base(context)
        {
        }

        public async Task<Test?> CreateTestAsync(Test test)
        {
            try
            {
                await _context.Tests.AddAsync(test);
                await _context.SaveChangesAsync();
                return test;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating test: {ex.Message}");
                throw;
            }
        }

        public async Task<Test?> GetTestByIdAsync(int testId)
        {
            try
            {
               
                return await _context.Tests
                    .Include(t => t.TestQuestions)
                        .ThenInclude(tq => tq.Question)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == testId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting test by id: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Test>> GetAllTestsAsync()
        {
            try
            {
                return await _context.Tests
                    .Include(t => t.TestQuestions)
                    .AsNoTracking()
                    .OrderByDescending(t => t.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all tests: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateTestAsync(Test test)
        {
            try
            {
                var existingEntity = _context.Tests.Local
                    .FirstOrDefault(t => t.Id == test.Id);

                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).State = EntityState.Detached;
                }

                _context.Tests.Update(test);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating test: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteTestAsync(int testId)
        {
            try
            {
                var test = await _context.Tests.FindAsync(testId);
                if (test == null)
                {
                    return false;
                }
                _context.Tests.Remove(test);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting test: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> IsTestInUseAsync(int testId)
        {
            try
            {
                var inUse = await _context.StudentTestSessions
                    .AsNoTracking()
                    .AnyAsync(s => s.TestId == testId && s.Status == "in_progress");
                return inUse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if test is in use: {ex.Message}");
                throw;
            }
        }
        public async Task<List<Test>> GetTestsByCreatorIdAsync(int creatorId)
        {
            try
            {
                return await _context.Tests
                    .Where(t => t.CreatedById == creatorId) 
                    .Include(t => t.TestQuestions) 
                    .AsNoTracking()
                    .OrderByDescending(t => t.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting tests by creator: {ex.Message}");
                throw;
            }
        }
    }
}
