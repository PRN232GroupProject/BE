using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Question
{
    public class QuestionResponseDto
    {
        public int Id { get; set; }
        public int? LessonId { get; set; }
        public string Content { get; set; }

        public Dictionary<string, string> Options { get; set; }

        public string CorrectAnswer { get; set; }
        public string Explanation { get; set; } 
        public string Difficulty { get; set; }
    }
}
