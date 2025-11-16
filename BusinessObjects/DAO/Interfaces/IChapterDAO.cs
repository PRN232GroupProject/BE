using BusinessObjects.DAO.Base.Interfaces;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DAO.Interfaces
{
    public interface IChapterDAO : IGenericRepository<Chapter>
    {
        Task <List<Chapter>> GetAllChaptersAsync();
        Task <Chapter?> GetChapterByIdAsync(int chapterId);
        Task<Chapter?> CreateChapterAsync(Chapter chapter);
        Task<bool> UpdateChapterAsync(Chapter chapter);
        Task<bool> DeleteChapterAsync(int chapterId);
        Task<bool> ChapterNameExistsAsync(string chapterName, int grade, int? excludeId = null);

    }
}
