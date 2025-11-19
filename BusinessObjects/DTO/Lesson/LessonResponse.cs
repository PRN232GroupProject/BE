using BusinessObjects.DTO.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Lesson
{
    public class LessonResponse
    {
        public int LessonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Objectives { get; set; }
        public string? Content { get; set; }
        public int? CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ResourceName { get; set; } = string.Empty;
        public List<ResourceResponse> Resources { get; set; } = new List<ResourceResponse>();

    }
}
