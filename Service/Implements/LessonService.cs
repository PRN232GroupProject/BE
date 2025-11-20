using BusinessObjects.DTO.Lesson;
using BusinessObjects.Mapper;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implements
{
    public class LessonService : ILessonService

    {
        private readonly ILessonRepository _lessonRepo;
        private readonly IMapperlyMapper _mapper;
        public LessonService(ILessonRepository lessonRepo, IMapperlyMapper mapper)
        {
            _lessonRepo = lessonRepo;
            _mapper = mapper;
        }
        public async Task<List<LessonResponse>> GetAllAsync()
        {
            var lessons = await _lessonRepo.GetAllAsync();
            return _mapper.LessonsToLessonResponses(lessons);
        }
        public async Task<LessonResponse?> GetLessonByIdAsync(int id)
        {
            var lesson = await _lessonRepo.GetLessonByIdAsync(id);
            return lesson == null ? null : _mapper.LessonToLessonResponse(lesson);
        }

        public async Task<List<LessonResponse>> GetLessonsByChapterAsync(int chapterId)
        {
            var lessons = await _lessonRepo.GetLessonsByChapterAsync(chapterId);
            return _mapper.LessonsToLessonResponses(lessons);
        }

        public async Task<LessonResponse> CreateLessonAsync(CreateLessonRequest request)
        {
            var lesson = _mapper.CreateLessonRequestToLesson(request);
            lesson.CreatedAt = DateTime.UtcNow;

            await _lessonRepo.CreateLessonAsync(lesson);

            return _mapper.LessonToLessonResponse(lesson);
        }

        public async Task<LessonResponse> UpdateLessonAsync(int id, UpdateLessonRequest request)
        {
            var existing = await _lessonRepo.GetLessonByIdAsync(id);
            _mapper.UpdateLessonFromRequest(request, existing);

            await _lessonRepo.UpdateLessonAsync(existing);

            return _mapper.LessonToLessonResponse(existing);
        }

        public async Task<bool> DeleteLessonAsync(int id)
        {
            var lesson = await _lessonRepo.GetLessonByIdAsync(id);
            if (lesson == null) throw new KeyNotFoundException("Lesson not found");

            return await _lessonRepo.DeleteLessonAsync(lesson);
        }
    }
}
