using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Test
{
    public class StudentAnswerResponse
    {
        public int QuestionId { get; set; }
        public string? SelectedAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
