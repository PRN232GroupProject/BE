using BusinessObjects.DTO.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITestQuestionService
    {
        
        Task<TestResponseDto> AddQuestionsToTestAsync(int testId, List<int> questionIds);
        Task<bool> RemoveQuestionFromTestAsync(int testId, int questionId);
        Task<bool> ClearAllQuestionsFromTestAsync(int testId);
    }
}
