using BusinessObjects.DTO.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ILessonService
    {
        Task<List<LessonResponse>> GetAllAsync();
        Task<LessonResponse?> GetLessonByIdAsync(int id);
        Task<List<LessonResponse>> GetLessonsByChapterAsync(int chapterId);
        Task<LessonResponse> CreateLessonAsync(CreateLessonRequest request);
        Task<LessonResponse> UpdateLessonAsync(int id, UpdateLessonRequest request);
        Task<bool> DeleteLessonAsync(int id);
    }
}
