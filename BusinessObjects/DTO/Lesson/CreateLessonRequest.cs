using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Lesson
{
    public class CreateLessonRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Objectives { get; set; }
        public string? Content { get; set; }

        public int ChapterId { get; set; }
        public int CreatedById { get; set; }
    }
}
