
using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Grade
{
    public class CreateGradeRequest : Request
    {
        [Required(ErrorMessage ="Tipo inválido")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "EnrollmentId inválida")]
        public long EnrollmentId { get; set; }       
    }
}
