using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DAO.Interfaces
{
    public interface ILessonDAO
    {
        Task<List<Lesson>> GetAllAsync();
        Task<Lesson?> GetByIdAsync(int id);
        Task<List<Lesson>> GetByChapterIdAsync(int chapterId);
        Task<int> CreateAsync(Lesson lesson);
        Task<int> UpdateAsync(Lesson lesson);
        Task<bool> DeleteAsync(Lesson lesson);
    }
}
