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
    public class LessonDAO : GenericRepository<Lesson>, ILessonDAO
    {
        private readonly ChemProjectDbContext _context;
        public LessonDAO(ChemProjectDbContext context) : base(context)
        {
            _context = context;
        }

        
        public new async Task<List<Lesson>> GetAllAsync()
        {
            return await _context.Lessons
                .AsNoTracking()
                .Include(l => l.Chapter)
                .Include(l => l.CreatedBy)
                .ToListAsync();
        }

        
        public new async Task<Lesson?> GetByIdAsync(int id)
        {
            return await _context.Lessons
                .AsNoTracking()
                .Include(l => l.Chapter)
                .Include(l => l.CreatedBy)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        
        public async Task<List<Lesson>> GetByChapterIdAsync(int chapterId)
        {
            return await _context.Lessons
                .AsNoTracking()
                .Where(x => x.ChapterId == chapterId)
                .Include(l => l.CreatedBy)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        }

        
        public new async Task<int> CreateAsync(Lesson lesson)
        {
            if (lesson == null) throw new ArgumentNullException(nameof(lesson));

            // set CreatedAt nếu chưa set
            if (lesson.CreatedAt == default)
                lesson.CreatedAt = DateTime.Now;

            _context.Lessons.Add(lesson);
            return await _context.SaveChangesAsync();
        }

        // Cập nhật lesson
        public new async Task<int> UpdateAsync(Lesson lesson)
        {
            if (lesson == null) throw new ArgumentNullException(nameof(lesson));

           
            var tracked = _context.Lessons.Local.FirstOrDefault(l => l.Id == lesson.Id);
            if (tracked == null)
            {
                _context.Lessons.Attach(lesson);
                _context.Entry(lesson).State = EntityState.Modified;
            }
            else
            {
                _context.Entry(tracked).CurrentValues.SetValues(lesson);
            }

            return await _context.SaveChangesAsync();
        }

        // Xóa lesson
        public async Task<bool> DeleteAsync(Lesson lesson)
        {
            if (lesson == null) return false;

            // đảm bảo entity được gắn vào context trước khi xoá
            var tracked = _context.Lessons.Local.FirstOrDefault(l => l.Id == lesson.Id);
            if (tracked == null)
            {
                _context.Lessons.Attach(lesson);
            }

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
