using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Test
{
    public class CreateTestSessionRequest
    {
        public int UserId { get; set; }
        public int TestId { get; set; }
    }
}
