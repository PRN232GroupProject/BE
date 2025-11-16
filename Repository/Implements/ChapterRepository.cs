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
    public class ChapterRepository : IChapterRepository
    {
        private readonly IChapterDAO _chapterDao;

        public ChapterRepository(IChapterDAO chapterDao)
        {
            _chapterDao = chapterDao;
        }

        public async Task<Chapter?> CreateChapterAsync(Chapter chapter)
        {
            return await _chapterDao.CreateChapterAsync(chapter);
        }

        public async Task<Chapter?> GetChapterByIdAsync(int chapterId)
        {
            return await _chapterDao.GetChapterByIdAsync(chapterId);
        }

        public async Task<List<Chapter>> GetAllChaptersAsync()
        {
            return await _chapterDao.GetAllChaptersAsync();
        }

        public async Task<bool> UpdateChapterAsync(Chapter chapter)
        {
            return await _chapterDao.UpdateChapterAsync(chapter);
        }

        public async Task<bool> DeleteChapterAsync(int chapterId)
        {
            return await _chapterDao.DeleteChapterAsync(chapterId);
        }

        public async Task<bool> ChapterNameExistsAsync(string chapterName, int grade, int? excludeId = null)
        {
            return await _chapterDao.ChapterNameExistsAsync(chapterName, grade, excludeId);
        }
    }
}
