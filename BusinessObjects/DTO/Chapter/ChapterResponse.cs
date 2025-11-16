using BusinessObjects.DTO.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Chapter
{
    public class ChapterResponse
    {
        public int ChapterId { get; set; }
        public string ChapterName { get; set; } 
        public int Grade { get; set; }
        public string? Description { get; set; }
        public List<LessonResponse> Lessons { get; set; } = new();
    }
}
