using BusinessObjects.DTO.Chapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IChapterService
    {
        Task<ChapterResponse> CreateChapterAsync(CreateChapterRequest request);
        Task<ChapterResponse?> GetChapterByIdAsync(int chapterId);
        Task<List<ChapterResponse>> GetAllChaptersAsync();
        Task<ChapterResponse> UpdateChapterAsync(UpdateChapterRequest request);
        Task<bool> DeleteChapterAsync(int chapterId);
    }
}
