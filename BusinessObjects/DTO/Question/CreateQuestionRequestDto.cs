using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Question
{
    public class CreateQuestionRequestDto
    {
        public int? LessonId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public Dictionary<string, string> Options { get; set; } // Ví dụ: {"A": "Đáp án A", "B": "Đáp án B"}

        [Required]
        public string CorrectAnswer { get; set; } // Ví dụ: "A"

        public string Explanation { get; set; } // HTML

        [Required]
        public string Difficulty { get; set; }
    }
}
