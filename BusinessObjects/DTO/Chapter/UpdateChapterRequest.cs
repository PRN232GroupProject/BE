using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Chapter
{
    public class UpdateChapterRequest
    {
        public int Id { get; set; }
        public string ChapterName { get; set; }
        public int Grade { get; set; }
        public string? Description { get; set; }
    }
}
