using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DAO.Interfaces
{
    public interface IStudentTestSessionDAO
    {
        Task<StudentTestSession?> GetSessionWithAnswersAsync(int sessionId);
        Task<List<StudentTestSession>> GetSessionsByUserAndTestAsync(int userId, int testId);
        Task<int> CreateAsync(StudentTestSession session);
    }
}
