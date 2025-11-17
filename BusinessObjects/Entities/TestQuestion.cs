using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Entities
{
    public class TestQuestion
    {
        public int TestId { get; set; }
        public int QuestionId { get; set; }

        public Test Test { get; set; } = null!;
        public Question Question { get; set; } = null!;
    }
}