using BusinessObjects.Context;
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
    public class StudentTestSessionDAO : IStudentTestSessionDAO
    {
        private readonly ChemProjectDbContext _context;
        public StudentTestSessionDAO(ChemProjectDbContext context)
        {
            _context = context;
        }
        public async Task<StudentTestSession?> GetSessionWithAnswersAsync(int sessionId)
        {
            return await _context.StudentTestSessions
                .Include(s => s.StudentAnswers)
                    .ThenInclude(sa => sa.Question)
                .FirstOrDefaultAsync(s => s.Id == sessionId);
        }

        public async Task<List<StudentTestSession>> GetSessionsByUserAndTestAsync(int userId, int testId)
        {
            return await _context.StudentTestSessions
                .Where(s => s.UserId == userId && s.TestId == testId)
                .OrderByDescending(s => s.StartTime)
                .ToListAsync();
        }
        public async Task<int> CreateAsync(StudentTestSession session)
        {
            await _context.StudentTestSessions.AddAsync(session);
            return await _context.SaveChangesAsync();
        }
    }
}
