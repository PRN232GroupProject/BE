using BusinessObjects.DAO.Interfaces;
using BusinessObjects.DTO.TestSession;
using BusinessObjects.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class TestSessionRepository : ITestSessionRepository
    {
        private readonly ITestSessionDAO _sessionDao;

        public TestSessionRepository(ITestSessionDAO sessionDao)
        {
            _sessionDao = sessionDao;
        }

        public async Task<bool> CheckExistingTestSessionAsync(int sessionId)
        {
            return await _sessionDao.CheckExistingTestSessionAsync(sessionId);
        }
        public async Task<StudentTestSession?> CreateTestSessionAsync(StudentTestSession session)
        {
            return await _sessionDao.CreateTestSessionAsync(session);
        }

        public async Task<bool> DeleteTestSessionAsync(int sessionId)
        {
            return await _sessionDao.DeleteTestSessionAsync(sessionId);
        }

        public async Task<List<StudentTestSession>> GetAllTestSessionsAsync()
        {
            return await _sessionDao.GetAllTestSessionsAsync();
        }

        public async Task<StudentTestSession?> GetTestSessionByIdAsync(int sessionId)
        {
            return await _sessionDao.GetTestSessionByIdAsync(sessionId);
        }

        public async Task<bool> UpdateTestSessionAsync(StudentTestSession session)
        {
            return await _sessionDao.UpdateTestSessionAsync(session);
        }

        public async Task<StudentTestSession?> GetStudentAnswersFromSessionIdAsync(int sessionId)
        {
            return await _sessionDao.GetStudentAnswersFromSessionIdAsync(sessionId);
        }
    }
}
