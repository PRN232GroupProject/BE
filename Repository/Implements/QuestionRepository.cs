using BusinessObjects.DAO.Implements;
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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IQuestionDAO _questionDao;

        public QuestionRepository(IQuestionDAO questionDao)
        {
            _questionDao = questionDao;
        }

        public async Task<Question?> CreateQuestionAsync(Question question)
        {
            return await _questionDao.CreateQuestionAsync(question);
        }

        public async Task<Question?> GetQuestionByIdAsync(int questionId)
        {
            return await _questionDao.GetQuestionByIdAsync(questionId);
        }

        public async Task<List<Question>> GetQuestionsAsync(int? lessonId, string? difficulty)
        {
            return await _questionDao.GetQuestionsAsync(lessonId, difficulty);
        }

        public async Task<bool> UpdateQuestionAsync(Question question)
        {
            return await _questionDao.UpdateQuestionAsync(question);
        }

        public async Task<bool> DeleteQuestionAsync(int questionId)
        {
            return await _questionDao.DeleteQuestionAsync(questionId);
        }
        public async Task<bool> IsQuestionInUseAsync(int questionId)
        {
            return await _questionDao.IsQuestionInUseAsync(questionId);
        }

    }
}
