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
    public class TestSessionDAO : GenericRepository<StudentTestSession>, ITestSessionDAO
    {
        public TestSessionDAO(ChemProjectDbContext context) : base(context)
        {
        }

        public async Task<StudentTestSession?> CreateTestSessionAsync(StudentTestSession session)
        {
            try
            {
                var exists = await CheckExistingTestSessionAsync(session.Id);
                if (exists)
                {
                    return null;
                }
                await _context.StudentTestSessions.AddAsync(session);
                await _context.SaveChangesAsync();
                return session;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating test session: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> DeleteTestSessionAsync(int sessionId)
        {
            try
            {
                var session = await _context.StudentTestSessions.FindAsync(sessionId);
                if (session == null)
                {
                    return false;
                }
                _context.StudentTestSessions.Remove(session);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting session: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<StudentTestSession>> GetAllTestSessionsAsync()
        {
            try
            {
                return await _context.StudentTestSessions
                    .AsNoTracking()
                    .OrderBy(s => s.UserId)
                    .ThenBy(s => s.TestId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all sessions: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<StudentTestSession?> GetTestSessionByIdAsync(int sessionId)
        {
            try
            {
                return await _context.StudentTestSessions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == sessionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving session by ID: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> UpdateTestSessionAsync(StudentTestSession session)
        {
            try
            {
                if (session == null)
                {
                    return false;
                }

                var existingSession = _context.StudentTestSessions.Local.FirstOrDefault(s => s.Id == session.Id);
                if (existingSession != null)
                {
                    _context.Entry(existingSession).State = EntityState.Detached;
                }

                _context.StudentTestSessions.Update(session);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating resource: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> CheckExistingTestSessionAsync(int sessionId)
        {
            try
            {
                var existingSession = _context.StudentTestSessions
                    .AsNoTracking()
                    .AnyAsync(s => s.Id == sessionId);

                if (existingSession != null)
                {
                    return await existingSession;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking existing session: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<StudentTestSession?> GetStudentAnswersFromSessionIdAsync(int sessionId)
        {
            try
            {
                return await _context.StudentTestSessions
                    .AsNoTracking()
                    .Include(s => s.StudentAnswers)
                    .FirstOrDefaultAsync(s => s.Id == sessionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving student answers from session ID: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
