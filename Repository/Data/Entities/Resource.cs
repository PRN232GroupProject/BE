using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Entities
{
    public class Resource
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Type { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }

        public Lesson Lesson { get; set; } = null!;
    }
}
