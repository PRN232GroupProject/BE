using BusinessObjects.DAO.Interfaces;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class TestQuestionRepository : ITestQuestionRepository
    {
        private readonly ITestQuestionDAO _testQuestionDao;

        public TestQuestionRepository(ITestQuestionDAO testQuestionDao)
        {
            _testQuestionDao = testQuestionDao;
        }

        public async Task<int> AddQuestionsToTestAsync(int testId, List<int> questionIds)
        {
            return await _testQuestionDao.AddQuestionsToTestAsync(testId, questionIds);
        }

        public async Task<bool> RemoveQuestionFromTestAsync(int testId, int questionId)
        {
            return await _testQuestionDao.RemoveQuestionFromTestAsync(testId, questionId);
        }

        public async Task<bool> ClearAllQuestionsFromTestAsync(int testId)
        {
            return await _testQuestionDao.ClearAllQuestionsFromTestAsync(testId);
        }
    }
}
