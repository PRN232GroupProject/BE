using BusinessObjects.DAO.Interfaces;
using BusinessObjects.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class TestRepository : ITestRepository
    {
        private readonly ITestDAO _testDao;

        public TestRepository(ITestDAO testDao)
        {
            _testDao = testDao;
        }

        public async Task<Test?> CreateTestAsync(Test test)
        {
            return await _testDao.CreateTestAsync(test);
        }

        public async Task<Test?> GetTestByIdAsync(int testId)
        {
            return await _testDao.GetTestByIdAsync(testId);
        }

        public async Task<List<Test>> GetAllTestsAsync()
        {
            return await _testDao.GetAllTestsAsync();
        }

        public async Task<bool> UpdateTestAsync(Test test)
        {
            return await _testDao.UpdateTestAsync(test);
        }

        public async Task<bool> DeleteTestAsync(int testId)
        {
            return await _testDao.DeleteTestAsync(testId);
        }

        public async Task<bool> IsTestInUseAsync(int testId)
        {
            return await _testDao.IsTestInUseAsync(testId);
        }
    }
}
