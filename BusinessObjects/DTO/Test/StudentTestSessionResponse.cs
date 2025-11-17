using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Test
{
    public class StudentTestSessionResponse
    {
        public int SessionId { get; set; }
        public int TestId { get; set; }
        public float? Score { get; set; }
        public string Status { get; set; } = "";
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public List<StudentAnswerResponse> Answers { get; set; } = new();
    }
}
