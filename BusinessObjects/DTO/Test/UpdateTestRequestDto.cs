using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Test
{
    public class UpdateTestRequestDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [Range(1, 600)]
        public int DurationMinutes { get; set; }
    }
}
