using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Type { get; set; } // '15_MINUTES', '45_MINUTES', 'SEMESTER_EXAM'
        public int DurationMinutes { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }

        public User CreatedBy { get; set; } = null!;

        public ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();
        public ICollection<StudentTestSession> StudentTestSessions { get; set; } = new List<StudentTestSession>();
    }
}