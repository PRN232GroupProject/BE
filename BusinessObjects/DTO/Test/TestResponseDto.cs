using BusinessObjects.DTO.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Test
{
    public class TestResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // '15_MINUTES', 'SEMESTER_EXAM', etc.
        public int DurationMinutes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int TotalQuestions { get; set; }
        public List<QuestionResponseDto> Questions { get; set; }

        public TestResponseDto()
        {
            Questions = new List<QuestionResponseDto>();
        }
    }
}
