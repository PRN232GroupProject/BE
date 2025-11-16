using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Resource
{
    public class ResourceResponse
    {
        public int ResourceId { get; set; }
        public int LessonId { get; set; }
        public string ResourceTitle { get; set; }
        public string? ResourceType { get; set; }
        public string? ResourceUrl { get; set; }
        public string? ResourceDescription { get; set; }
    }
}
