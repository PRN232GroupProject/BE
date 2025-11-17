using BusinessObjects.DTO.Test;
﻿using BusinessObjects.DTO.TestSession;
using BusinessObjects.Entities;
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
        Task<List<TestSessionResponse>> GetAllTestSessionsAsync();
        Task<TestSessionResponse?> GetTestSessionByIdAsync(int sessionId);
        Task<TestSessionResponse?> CreateTestSessionAsync(CreateTestSessionRequest session);
        Task<TestSessionResponse> UpdateTestSessionAsync(UpdateTestSessionRequest session);
        Task<bool> DeleteTestSessionAsync(int sessionId);
    }
}
