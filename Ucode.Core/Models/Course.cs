namespace Ucode.Core.Models
{ 
        public class Course
        {
            public long Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public int DurationInHours { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public DateTime? UpdatedAt { get; set; }

            public string UserId { get; set; } = string.Empty;
        }
  

}
