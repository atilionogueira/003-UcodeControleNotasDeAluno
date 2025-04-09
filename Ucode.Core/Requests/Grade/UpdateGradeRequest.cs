
using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Grade
{
     public class UpdateGradeRequest : Request
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Tipo inválido")]
        public decimal Value { get; set; }       

        [Required(ErrorMessage = "Grade inválida")]
        public long EnrollmentId { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
