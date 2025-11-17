using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IQuestionRepository
    {
        Task<Question?> CreateQuestionAsync(Question question);
        Task<Question?> GetQuestionByIdAsync(int questionId);
        Task<List<Question>> GetQuestionsAsync(int? lessonId, string? difficulty);
        Task<bool> UpdateQuestionAsync(Question question);
        Task<bool> DeleteQuestionAsync(int questionId);

        Task<bool> IsQuestionInUseAsync(int questionId);
    }
}
