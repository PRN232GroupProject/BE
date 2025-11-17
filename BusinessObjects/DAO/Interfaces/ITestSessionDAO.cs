using BusinessObjects.DAO.Base.Interfaces;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DAO.Interfaces
{
    public interface ITestSessionDAO : IGenericRepository<StudentTestSession>
    {
        Task<List<StudentTestSession>> GetAllTestSessionsAsync();
        Task<StudentTestSession?> GetTestSessionByIdAsync(int sessionId);
        Task<StudentTestSession?> CreateTestSessionAsync(StudentTestSession session);
        Task<bool> UpdateTestSessionAsync(StudentTestSession session);
        Task<bool> DeleteTestSessionAsync(int sessionId);
        Task<bool> CheckExistingTestSessionAsync(int sessionId);
    }
}
