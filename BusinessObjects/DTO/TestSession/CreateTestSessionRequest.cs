using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.TestSession
{
    public class CreateTestSessionRequest
    {
        public int UserId { get; set; }
        public int TestId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public float? Score { get; set; }
        public string Status { get; set; } = string.Empty; // 'in_progress', 'completed'
    }
}
