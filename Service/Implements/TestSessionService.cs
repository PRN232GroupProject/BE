using BusinessObjects.DTO.Test;
using BusinessObjects.DTO.TestSession;
using BusinessObjects.Mapper;
using Microsoft.Extensions.Logging;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Implements
{
    public class TestSessionService : ITestSessionService
    {
        private readonly ITestSessionRepository _sessionRepository;
        private readonly IStudentTestSessionRepository _sessionRepo;
        private readonly IMapperlyMapper _mapper;

        public TestSessionService(ITestSessionRepository sessionRepository, IMapperlyMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
        }

        public async Task<TestSessionResponse> CreateTestSessionAsync(CreateTestSessionRequest request)
        {
            try
            {
                Console.WriteLine("Creating Student Test Session...");
                // Map to entity
                var session = _mapper.CreateTestSessionRequestToTestSession(request);
                Console.WriteLine($"Mapped session - ID: {session.Id}, Test - ID: {session.TestId}");

                // Create session
                var createdSession = await _sessionRepository.CreateTestSessionAsync(session);
                if (createdSession == null)
                {
                    throw new ArgumentException("Failed to create session. Session may already exist.");
                }
                Console.WriteLine($"Session created successfully with ID: {createdSession.Id}");

                // Map to response DTO
                var response = _mapper.TestSessionToTestSessionResponse(createdSession);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating session: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> DeleteTestSessionAsync(int sessionId)
        {
            try
            {
                Console.WriteLine($"Deleting session with ID: {sessionId}...");

                var result = await _sessionRepository.DeleteTestSessionAsync(sessionId);
                if (!result)
                {
                    throw new KeyNotFoundException($"No session found with ID: {sessionId} to delete.");
                }
                else
                {
                    Console.WriteLine($"Session with ID: {sessionId} deleted successfully.");
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting session: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<TestSessionResponse>> GetAllTestSessionsAsync()
        {
            try
            {
                var sessions = await _sessionRepository.GetAllTestSessionsAsync();
                return _mapper.TestSessionsToTestSessionResponses(sessions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving sessions: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<TestSessionResponse?> GetTestSessionByIdAsync(int sessionId)
        {
            try
            {
                var session = await _sessionRepository.GetTestSessionByIdAsync(sessionId);
                if (session == null)
                {
                    throw new KeyNotFoundException($"No session found with ID: {sessionId}");
                }
                return _mapper.TestSessionToTestSessionResponse(session);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving session: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<TestSessionResponse> UpdateTestSessionAsync(UpdateTestSessionRequest request)
        {
            try
            {
                Console.WriteLine($"Updating session ID: {request.Id}, Test ID: {request.TestId}");

                // Get existing session
                var existingSession = await _sessionRepository.GetTestSessionByIdAsync(request.Id);
                if (existingSession == null)
                {
                    throw new KeyNotFoundException($"Session with ID {request.Id} not found.");
                }

                Console.WriteLine($"Found existing session ID: {existingSession.Id}");

                // Update properties manually to avoid tracking issues
                existingSession.TestId = request.TestId;
                existingSession.StartTime = request.StartTime;
                existingSession.EndTime = request.EndTime;
                existingSession.Status = request.Status;

                Console.WriteLine($"Updated properties - Test ID: {request.TestId}, Start Time: {request.StartTime}, End Time: {request.EndTime}, Status: {request.Status} ");

                // Save changes
                var isUpdated = await _sessionRepository.UpdateTestSessionAsync(existingSession);

                if (!isUpdated)
                {
                    throw new InvalidOperationException("Failed to update session.");
                }

                Console.WriteLine("Resource updated successfully");

                // Return updated session
                var response = _mapper.TestSessionToTestSessionResponse(existingSession);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateTestSessionAsync: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<TestSessionResponse>> GetSessionsByUserAsync(int userId, int testId)
        {
            try
            {
                var sessions = await _sessionRepo.GetSessionsByUserAndTestAsync(userId, testId);

                return _mapper.TestSessionsToTestSessionResponses(sessions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetSessionsByUserAsync: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<TestSessionResponse?> GetStudentAnswersFromSessionIdAsync(int sessionId)
        {
            try
            {
                var session = await _sessionRepository.GetStudentAnswersFromSessionIdAsync(sessionId);
                if (session == null)
                {
                    throw new KeyNotFoundException($"No session found with ID: {sessionId}");
                }
                return _mapper.TestSessionToTestSessionResponse(session);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving session with student answers: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
