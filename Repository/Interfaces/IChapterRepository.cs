using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IChapterRepository
    {
        Task<Chapter?> CreateChapterAsync(Chapter chapter);
        Task<Chapter?> GetChapterByIdAsync(int chapterId);
        Task<List<Chapter>> GetAllChaptersAsync();
        Task<bool> UpdateChapterAsync(Chapter chapter);
        Task<bool> DeleteChapterAsync(int chapterId);
        Task<bool> ChapterNameExistsAsync(string chapterName, int grade, int? excludeId = null);
    }
}
