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
        private readonly ITestDAO _testDAO;

        public TestRepository(ITestDAO testDAO)
        {
            _testDAO = testDAO;
        }

        public async Task<Test?> GetTestWithQuestionsAsync(int testId)
        {
            return await _testDAO.GetTestWithQuestionsAsync(testId);
        }
    }
}
