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
    public class ChapterDAO : GenericRepository<Chapter>, IChapterDAO
    {
        public ChapterDAO(ChemProjectDbContext context) : base(context)
        {
        }

        public async Task<Chapter?> CreateChapterAsync(Chapter chapter)
        {
            try
            {
                // Check if chapter name already exists for the same grade
                var exists = await ChapterNameExistsAsync(chapter.Name, chapter.Grade);
                if (exists)
                {
                    return null;
                }

                await _context.Chapters.AddAsync(chapter);
                await _context.SaveChangesAsync();

                return chapter;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating chapter: {ex.Message}");
                throw;
            }
        }

        public async Task<Chapter?> GetChapterByIdAsync(int chapterId)
        {
            try
            {
                return await _context.Chapters
                    .Include(c => c.Lessons)
                    .FirstOrDefaultAsync(c => c.Id == chapterId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting chapter: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Chapter>> GetAllChaptersAsync()
        {
            try
            {
                return await _context.Chapters
                    .Include(c => c.Lessons)
                    .OrderBy(c => c.Grade)
                    .ThenBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all chapters: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateChapterAsync(Chapter chapter)
        {
            try
            {
                if (chapter == null)
                {
                    return false;
                }

                // Check if another chapter with same name exists
                var exists = await ChapterNameExistsAsync(chapter.Name, chapter.Grade, chapter.Id);
                if (exists)
                {
                    Console.WriteLine("Another chapter with same name exists");
                    return false;
                }

                // Detach any existing tracked entity
                var existingEntity = _context.Chapters.Local
                    .FirstOrDefault(c => c.Id == chapter.Id);

                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).State = EntityState.Detached;
                }

                // Update the chapter
                _context.Chapters.Update(chapter);
                var result = await _context.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating chapter: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> DeleteChapterAsync(int chapterId)
        {
            try
            {
                var chapter = await _context.Chapters.FindAsync(chapterId);
                if (chapter == null)
                {
                    Console.WriteLine($"Chapter with ID {chapterId} not found");
                    return false;
                }

                _context.Chapters.Remove(chapter);
                var result = await _context.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting chapter: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> ChapterNameExistsAsync(string chapterName, int grade, int? excludeId = null)
        {
            try
            {
                var query = _context.Chapters
                    .AsNoTracking()
                    .Where(c => c.Name.ToLower() == chapterName.ToLower()
                                && c.Grade == grade);

                if (excludeId.HasValue)
                {
                    query = query.Where(c => c.Id != excludeId.Value);
                }

                return await query.AnyAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking chapter name: {ex.Message}");
                throw;
            }
        }
    }
}
