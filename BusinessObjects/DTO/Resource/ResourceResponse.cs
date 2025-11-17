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
        public bool? IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
