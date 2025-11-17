using BusinessObjects.DTO.Answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAnswerService
    {
        Task<List<AnswerResponse>> GetAllAnswersAsync();
        Task<AnswerResponse?> GetAnswerByIdAsync(int answerId);
        Task<AnswerResponse?> CreateAnswerAsync(CreateAnswerRequest answer);
        Task<AnswerResponse> UpdateAnswerAsync(UpdateAnswerRequest answer);
        Task<bool> DeleteAnswerAsync(int answerId);
    }
}
