using BusinessObjects.DTO.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITestSessionService
    {
        Task<StudentTestSessionResponse> CreateTestSessionAsync(CreateTestSessionRequest request);
        Task<StudentTestSessionResponse?> GetSessionByIdAsync(int sessionId);
        Task<List<StudentTestSessionResponse>> GetSessionsByUserAsync(int userId, int testId);
    }
}
