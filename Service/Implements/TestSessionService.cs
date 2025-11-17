using BusinessObjects.DTO.Test;
using BusinessObjects.Mapper;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implements
{
    public class TestSessionService : ITestSessionService
    {
        private readonly IStudentTestSessionRepository _sessionRepo;
        private readonly IMapperlyMapper _mapper;

        public TestSessionService(
            IStudentTestSessionRepository sessionRepo,
            IMapperlyMapper mapper)
        {
            _sessionRepo = sessionRepo;
            _mapper = mapper;
        }

        public async Task<StudentTestSessionResponse> CreateTestSessionAsync(CreateTestSessionRequest request)
        {
            var session = _mapper.CreateTestSessionRequestToStudentTestSession(request);
            session.StartTime = DateTime.UtcNow;  // hoặc DateTime.Now nếu bạn muốn giờ local
            session.Status = "in_progress";
            await _sessionRepo.CreateNewSessionAsync(session);

            return _mapper.StudentTestSessionToResponse(session);
        }

        public async Task<StudentTestSessionResponse?> GetSessionByIdAsync(int sessionId)
        {
            var session = await _sessionRepo.GetSessionWithAnswersAsync(sessionId);
            if (session == null) return null;

            return _mapper.StudentTestSessionToResponse(session);
        }

        public async Task<List<StudentTestSessionResponse>> GetSessionsByUserAsync(int userId, int testId)
        {
            var sessions = await _sessionRepo.GetSessionsByUserAndTestAsync(userId, testId);

            return sessions
                .Select(s => _mapper.StudentTestSessionToResponse(s))
                .ToList();
        }
    }
}
