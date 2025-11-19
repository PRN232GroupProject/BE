using BusinessObjects.DTO.Test;
using BusinessObjects.Mapper;
using Microsoft.AspNetCore.Http;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implements
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapperlyMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TestService(ITestRepository testRepository, IMapperlyMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _testRepository = testRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TestResponseDto> CreateTestAsync(CreateTestRequestDto request)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (!int.TryParse(userIdClaim, out int creatorId))
                {
                    throw new UnauthorizedAccessException("User is not authenticated.");
                }

                var test = _mapper.CreateTestRequestToTest(request);

                test.CreatedById = creatorId;
                test.CreatedAt = DateTime.UtcNow;

                var createdTest = await _testRepository.CreateTestAsync(test);
                if (createdTest == null)
                {
                    throw new InvalidOperationException("Failed to create the test.");
                }
                return _mapper.TestToTestResponseDto(createdTest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateTestAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<TestResponseDto?> GetTestByIdAsync(int testId)
        {
            try
            {
                var test = await _testRepository.GetTestByIdAsync(testId);
                if (test == null)
                {
                    return null;
                }
                return _mapper.TestToTestResponseDto(test);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetTestByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<TestResponseDto>> GetAllTestsAsync()
        {
            try
            {
                var tests = await _testRepository.GetAllTestsAsync();

                return _mapper.TestsToTestResponseDtos(tests);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllTestsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<TestResponseDto> UpdateTestAsync(UpdateTestRequestDto request)
        {
            try
            {
                var existingTest = await _testRepository.GetTestByIdAsync(request.Id);
                if (existingTest == null)
                {
                    throw new KeyNotFoundException($"Test with ID {request.Id} not found.");
                }

                _mapper.UpdateTestFromRequest(request, existingTest);

                var isUpdated = await _testRepository.UpdateTestAsync(existingTest);
                if (!isUpdated)
                {
                    throw new InvalidOperationException("Failed to update the test.");
                }
                return _mapper.TestToTestResponseDto(existingTest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateTestAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteTestAsync(int testId)
        {
            try
            {
                var test = await _testRepository.GetTestByIdAsync(testId);
                if (test == null)
                {
                    throw new KeyNotFoundException($"Test with ID {testId} not found.");
                }
                var isInUse = await _testRepository.IsTestInUseAsync(testId);
                if (isInUse)
                {
                    throw new InvalidOperationException("Cannot delete test. One or more students are currently taking this test.");
                }

                return await _testRepository.DeleteTestAsync(testId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteTestAsync: {ex.Message}");
                throw;
            }
        }
        public async Task<List<TestResponseDto>> GetTestsCreatedByMeAsync()
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (!int.TryParse(userIdClaim, out int userId))
                {
                    throw new UnauthorizedAccessException("User is not authenticated.");
                }

                var tests = await _testRepository.GetTestsByCreatorIdAsync(userId);

                return _mapper.TestsToTestResponseDtos(tests);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetTestsCreatedByMeAsync: {ex.Message}");
                throw;
            }
        }
    }
}
