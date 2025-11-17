using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Question
{
    public class UpdateQuestionRequestDto
    {
        [Required]
        public int Id { get; set; }

        public int? LessonId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public Dictionary<string, string> Options { get; set; }

        [Required]
        public string CorrectAnswer { get; set; }

        public string Explanation { get; set; }

        [Required]
        public string Difficulty { get; set; }
    }
}
