using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Entities
{
    public class StudentAnswer
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int QuestionId { get; set; }
        public string? SelectedAnswer { get; set; } // e.g., 'B'
        public bool IsCorrect { get; set; }

        public StudentTestSession Session { get; set; } = null!;
        public Question Question { get; set; } = null!;
    }
}