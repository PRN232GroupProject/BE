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
    public class TestQuestionService : ITestQuestionService
    {
        private readonly ITestQuestionRepository _testQuestionRepo;
        private readonly ITestRepository _testRepo; 
        private readonly IQuestionRepository _questionRepo; 
        private readonly IMapperlyMapper _mapper;

        public TestQuestionService(
            ITestQuestionRepository testQuestionRepo,
            ITestRepository testRepo,
            IQuestionRepository questionRepo,
            IMapperlyMapper mapper)
        {
            _testQuestionRepo = testQuestionRepo;
            _testRepo = testRepo;
            _questionRepo = questionRepo;
            _mapper = mapper;
        }

        public async Task<TestResponseDto> AddQuestionsToTestAsync(int testId, List<int> questionIds)
        {
            try
            {
                var test = await _testRepo.GetTestByIdAsync(testId);
                if (test == null)
                {
                    throw new KeyNotFoundException($"Test with ID {testId} not found.");
                }

                if (await _testRepo.IsTestInUseAsync(testId))
                {
                    throw new InvalidOperationException("Cannot modify a test that is currently in progress by a student.");
                }

                foreach (var qId in questionIds)
                {
                    if (await _questionRepo.GetQuestionByIdAsync(qId) == null)
                    {
                        throw new ArgumentException($"Question with ID {qId} does not exist.");
                    }
                }
                await _testQuestionRepo.AddQuestionsToTestAsync(testId, questionIds);

                var updatedTest = await _testRepo.GetTestByIdAsync(testId);
                return _mapper.TestToTestResponseDto(updatedTest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddQuestionsToTestAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> RemoveQuestionFromTestAsync(int testId, int questionId)
        {
            try
            {
                if (await _testRepo.IsTestInUseAsync(testId))
                {
                    throw new InvalidOperationException("Cannot modify a test that is currently in progress by a student.");
                }
                return await _testQuestionRepo.RemoveQuestionFromTestAsync(testId, questionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RemoveQuestionFromTestAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ClearAllQuestionsFromTestAsync(int testId)
        {
            try
            {
                if (await _testRepo.IsTestInUseAsync(testId))
                {
                    throw new InvalidOperationException("Cannot modify a test that is currently in progress by a student.");
                }

                return await _testQuestionRepo.ClearAllQuestionsFromTestAsync(testId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ClearAllQuestionsFromTestAsync: {ex.Message}");
                throw;
            }
        }
    }
}
