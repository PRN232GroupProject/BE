using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAnswerRepository
    {
        Task<List<StudentAnswer>> GetAllAnswersAsync();
        Task<StudentAnswer?> GetAnswerByIdAsync(int answerId);
        Task<StudentAnswer?> CreateAnswerAsync(StudentAnswer answer);
        Task<bool> UpdateAnswerAsync(StudentAnswer answer);
        Task<bool> DeleteAnswerAsync(int answerId);
    }
}
