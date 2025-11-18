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
    public class TestQuestionDAO : GenericRepository<TestQuestion>, ITestQuestionDAO
    {
        public TestQuestionDAO(ChemProjectDbContext context) : base(context)
        {
        }

        public async Task<int> AddQuestionsToTestAsync(int testId, List<int> questionIds)
        {
            try
            {
                var existingQuestionIds = await _context.TestQuestions
                    .Where(tq => tq.TestId == testId)
                    .Select(tq => tq.QuestionId)
                    .ToListAsync();

                var newQuestionIds = questionIds.Distinct().Except(existingQuestionIds);

                var newTestQuestions = newQuestionIds
                    .Select(qId => new TestQuestion
                    {
                        TestId = testId,
                        QuestionId = qId
                    });

                if (!newTestQuestions.Any())
                {
                    return 0; 
                }

                await _context.TestQuestions.AddRangeAsync(newTestQuestions);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding questions to test: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> RemoveQuestionFromTestAsync(int testId, int questionId)
        {
            try
            {
                var testQuestion = await _context.TestQuestions
                    .FirstOrDefaultAsync(tq => tq.TestId == testId && tq.QuestionId == questionId);

                if (testQuestion == null)
                {
                    return false;
                }

                _context.TestQuestions.Remove(testQuestion);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing question from test: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ClearAllQuestionsFromTestAsync(int testId)
        {
            try
            {
                var testQuestions = await _context.TestQuestions
                    .Where(tq => tq.TestId == testId)
                    .ToListAsync();

                if (!testQuestions.Any())
                {
                    return true; 
                }

                _context.TestQuestions.RemoveRange(testQuestions);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing questions from test: {ex.Message}");
                throw;
            }
        }
    }
}
