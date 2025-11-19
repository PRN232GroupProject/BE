using BusinessObjects.DTO.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITestService
    {
        Task<TestResponseDto> CreateTestAsync(CreateTestRequestDto request);
        Task<TestResponseDto?> GetTestByIdAsync(int testId);
        Task<List<TestResponseDto>> GetAllTestsAsync();

        Task<TestResponseDto> UpdateTestAsync(UpdateTestRequestDto request);
        Task<bool> DeleteTestAsync(int testId);
        Task<List<TestResponseDto>> GetTestsCreatedByMeAsync();
    }
}
