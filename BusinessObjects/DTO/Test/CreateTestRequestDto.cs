using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Test
{
    public class CreateTestRequestDto
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Type { get; set; } // '15_MINUTES', '45_MINUTES', etc.

        [Required]
        [Range(1, 600)] // Từ 1 phút đến 10 tiếng
        public int DurationMinutes { get; set; }
    }
}
