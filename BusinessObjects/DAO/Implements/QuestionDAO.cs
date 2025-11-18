using BusinessObjects.Context;
using BusinessObjects.DAO.Base.Implements;
using BusinessObjects.DAO.Interfaces;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DAO.Implements
{
    public class QuestionDAO : GenericRepository<Question>, IQuestionDAO
    {
        public QuestionDAO(ChemProjectDbContext context) : base(context)
        {
        }

        public async Task<Question?> CreateQuestionAsync(Question question)
        {
            try
            {
                await _context.Questions.AddAsync(question);
                await _context.SaveChangesAsync();
                return question;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating question: {ex.Message}");
                throw;
            }
        }

        public async Task<Question?> GetQuestionByIdAsync(int questionId)
        {
            try
            {
                return await _context.Questions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == questionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting question by id: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Question>> GetQuestionsAsync(int? lessonId, string? difficulty)
        {
            try
            {
                var query = _context.Questions.AsNoTracking();

                if (lessonId.HasValue)
                {
                    query = query.Where(q => q.LessonId == lessonId.Value);
                }

                if (!string.IsNullOrWhiteSpace(difficulty))
                {
                    query = query.Where(q => q.Difficulty.ToLower() == difficulty.ToLower());
                }

                return await query.OrderByDescending(q => q.CreatedAt).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting questions: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateQuestionAsync(Question question)
        {
            try
            {
                var existingEntity = _context.Questions.Local
                    .FirstOrDefault(q => q.Id == question.Id);

                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).State = EntityState.Detached;
                }

                _context.Questions.Update(question);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating question: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteQuestionAsync(int questionId)
        {
            try
            {
                var question = await _context.Questions.FindAsync(questionId);
                if (question == null)
                {
                    return false;
                }

                _context.Questions.Remove(question);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting question: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> IsQuestionInUseAsync(int questionId)
        {
            try
            {

                var inUse = await _context.TestQuestions
                    .AsNoTracking()
                    .AnyAsync(tq => tq.QuestionId == questionId);

                return inUse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if question is in use: {ex.Message}");
                throw;
            }
        }
    }
}
