namespace BusinessObjects.Entities
{
    public class StudentTestSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public float? Score { get; set; }
        public string Status { get; set; } = string.Empty; // 'in_progress', 'completed'

        public User User { get; set; } = null!;
        public Test Test { get; set; } = null!;

        public ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
    }
}