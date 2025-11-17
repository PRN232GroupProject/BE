using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int CreatedById { get; set; }

        public string? Type { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? Options { get; set; } // store JSON
        public string? CorrectAnswer { get; set; }
        public string? Explanation { get; set; }
        public string? Difficulty { get; set; }
        public DateTime CreatedAt { get; set; }

        public Lesson Lesson { get; set; } = null!;
        public User CreatedBy { get; set; } = null!;
        public Question()
        {
            CreatedAt = DateTime.UtcNow;
            // (Không còn khởi tạo TestQuestions)
        }
    }
}
