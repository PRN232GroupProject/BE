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
    public class LessonRepository : ILessonRepository
    {
        private readonly ILessonDAO _dao;

        public LessonRepository(ILessonDAO dao)
        {
            _dao = dao;
        }

        public Task<List<Lesson>> GetAllAsync() => _dao.GetAllAsync();
        public Task<Lesson?> GetLessonByIdAsync(int id) => _dao.GetByIdAsync(id);
        public Task<List<Lesson>> GetLessonsByChapterAsync(int chapterId) => _dao.GetByChapterIdAsync(chapterId);
        public Task<int> CreateLessonAsync(Lesson lesson) => _dao.CreateAsync(lesson);
        public Task<int> UpdateLessonAsync(Lesson lesson) => _dao.UpdateAsync(lesson);
        public Task<bool> DeleteLessonAsync(Lesson lesson) => _dao.DeleteAsync(lesson);
    }
}
