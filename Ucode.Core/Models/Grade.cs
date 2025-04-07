namespace Ucode.Core.Models
{   
        public class Grade
        {
            public long Id { get; set; }
            public decimal Value { get; set; }           
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public DateTime? UpdatedAt { get; set; }

            public long EnrollmentId { get; set; }
            public Enrollment Enrollment { get; set; } = null!;

            public string UserId { get; set; } = string.Empty;
        }
}
