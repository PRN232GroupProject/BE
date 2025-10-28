using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Objectives { get; set; }
        public string? Content { get; set; }
        public int? CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }

        public Chapter Chapter { get; set; } = null!;
        public User? CreatedBy { get; set; }

        public ICollection<Resource> Resources { get; set; } = new List<Resource>();
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
