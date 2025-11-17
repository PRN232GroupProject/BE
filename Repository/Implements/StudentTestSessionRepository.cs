using BusinessObjects.DAO.Interfaces;
using BusinessObjects.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class StudentTestSessionRepository : IStudentTestSessionRepository
    {
        private readonly IStudentTestSessionDAO _dao;

        public StudentTestSessionRepository(IStudentTestSessionDAO dao)
        {
            _dao = dao;
        }

        public async Task<StudentTestSession?> GetSessionWithAnswersAsync(int sessionId)
        {
            return await _dao.GetSessionWithAnswersAsync(sessionId);
        }

        public async Task<List<StudentTestSession>> GetSessionsByUserAndTestAsync(int userId, int testId)
        {
            return await _dao.GetSessionsByUserAndTestAsync(userId, testId);
        }

        public async Task<int> CreateNewSessionAsync(StudentTestSession session)
        {
            return await _dao.CreateAsync(session);
        }
    }
}
