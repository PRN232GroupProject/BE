using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IStudentTestSessionRepository
    {
        Task<StudentTestSession?> GetSessionWithAnswersAsync(int sessionId);
        Task<List<StudentTestSession>> GetSessionsByUserAndTestAsync(int userId, int testId);
        Task<int> CreateNewSessionAsync(StudentTestSession session);
    }
}
