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
    public class AnswerDAO : GenericRepository<StudentAnswer>, IAnswerDAO
    {
        public AnswerDAO(ChemProjectDbContext context) : base(context)
        {
        }

        public async Task<StudentAnswer?> CreateAnswerAsync(StudentAnswer answer)
        {
            try
            {
                await _context.StudentAnswers.AddAsync(answer);
                await _context.SaveChangesAsync();
                return answer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating answer: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> DeleteAnswerAsync(int answerId)
        {
            try
            {
                var answer = await _context.StudentAnswers.FindAsync(answerId);
                if (answer == null)
                {
                    return false;
                }
                _context.StudentAnswers.Remove(answer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting answer: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<StudentAnswer>> GetAllAnswersAsync()
        {
            try
            {
                return await _context.StudentAnswers
                    .AsNoTracking()
                    .OrderBy(a => a.SessionId)
                    .ThenBy(a => a.QuestionId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all answers: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<StudentAnswer?> GetAnswerByIdAsync(int answerId)
        {
            try
            {
                return await _context.StudentAnswers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == answerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving answer by ID: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> UpdateAnswerAsync(StudentAnswer answer)
        {
            try
            {
                if (answer == null)
                {
                    return false;
                }

                var existingAnswer = _context.StudentAnswers.Local.FirstOrDefault(a => a.Id == answer.Id);
                if (existingAnswer != null)
                {
                    _context.Entry(existingAnswer).State = EntityState.Detached;
                }

                _context.StudentAnswers.Update(answer);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating answer: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
