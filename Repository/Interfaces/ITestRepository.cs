using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ITestRepository
    {
        Task<Test?> CreateTestAsync(Test test);
        Task<Test?> GetTestByIdAsync(int testId);
        Task<List<Test>> GetAllTestsAsync();
        Task<bool> UpdateTestAsync(Test test);
        Task<bool> DeleteTestAsync(int testId);
        Task<bool> IsTestInUseAsync(int testId);
    }
}
