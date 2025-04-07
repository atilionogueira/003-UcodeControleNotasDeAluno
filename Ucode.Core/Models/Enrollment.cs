    namespace Ucode.Core.Models
    {
        public class Enrollment
        {
            public long Id { get; set; }

            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public DateTime? UpdatedAt { get; set; }

            public long StudentId { get; set; }
            public Student Student { get; set; } = null!;
            public long CourseId { get; set; }
            public Course Course { get; set; } = null!;

            public string UserId { get; set; } = string.Empty;
        }
    }

