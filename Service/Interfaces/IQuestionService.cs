using BusinessObjects.DTO.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IQuestionService
    {

        Task<QuestionResponseDto> CreateQuestionAsync(CreateQuestionRequestDto request);
        Task<QuestionResponseDto?> GetQuestionByIdAsync(int questionId);
        Task<List<QuestionResponseDto>> GetQuestionsAsync(int? lessonId, string? difficulty);
        Task<QuestionResponseDto> UpdateQuestionAsync(UpdateQuestionRequestDto request);
        Task<bool> DeleteQuestionAsync(int questionId);
    }
}
