
using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Enrollments
{
    public class UpdateEnrollmentsRequest : Request
    {
        public long Id { get; set; }       

        [Required(ErrorMessage = "Student inválido")]
        public long StudentId { get; set; }

        [Required(ErrorMessage = "Course inválido")]
        public long CourseId { get; set; }

        [Required(ErrorMessage = "Course inválido")]
        public DateTime? UpdatedAt { get; set; }

    }
}
