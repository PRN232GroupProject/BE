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
    public class AnswerRepository : IAnswerRepository
    {
        private readonly IAnswerDAO _answerDao;

        public AnswerRepository(IAnswerDAO answerDao)
        {
            _answerDao = answerDao;
        }

        public async Task<List<StudentAnswer>> GetAllAnswersAsync()
        {
            return await _answerDao.GetAllAnswersAsync();
        }

        public async Task<StudentAnswer?> GetAnswerByIdAsync(int answerId)
        {
            return await _answerDao.GetAnswerByIdAsync(answerId);
        }

        public async Task<StudentAnswer?> CreateAnswerAsync(StudentAnswer answer)
        {
            return await _answerDao.CreateAnswerAsync(answer);
        }

        public async Task<bool> UpdateAnswerAsync(StudentAnswer answer)
        {
            return await _answerDao.UpdateAnswerAsync(answer);
        }

        public async Task<bool> DeleteAnswerAsync(int answerId)
        {
            return await _answerDao.DeleteAnswerAsync(answerId);
        }
    }
}
