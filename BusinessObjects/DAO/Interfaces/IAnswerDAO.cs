using BusinessObjects.DAO.Base.Interfaces;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DAO.Interfaces
{
    public interface IAnswerDAO : IGenericRepository<StudentAnswer>
    {
        Task<List<StudentAnswer>> GetAllAnswersAsync();
        Task<StudentAnswer?> GetAnswerByIdAsync(int answerId);
        Task<StudentAnswer?> CreateAnswerAsync(StudentAnswer answer);
        Task<bool> UpdateAnswerAsync(StudentAnswer answer);
        Task<bool> DeleteAnswerAsync(int answerId);
    }
}
