using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Answer
{
    public class CreateAnswerRequest
    {
        public int SessionId { get; set; }
        public int QuestionId { get; set; }
        public string? SelectedAnswer { get; set; } // e.g., 'B'
        public bool IsCorrect { get; set; }
    }
}
