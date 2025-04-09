
using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Enrollments
{
    public class CreateEnrollmentRequest : Request
    {
        [Required(ErrorMessage = "Enrollment inválido")]
        public long StudentId { get; set; }

        [Required(ErrorMessage = "Course inválido")]
        public long CourseId { get; set; }
    }  
}
