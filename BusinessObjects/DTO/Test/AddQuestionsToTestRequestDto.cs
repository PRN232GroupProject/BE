using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Test
{
    public class AddQuestionsToTestRequestDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "At least one question ID is required.")]
        public List<int> QuestionIds { get; set; }
    }
}
