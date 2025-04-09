
using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Grade
{
     public class UpdateGradeRequest : Request
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Tipo inválido")]
        public decimal Value { get; set; }       

        [Required(ErrorMessage = " Matrícula inválida")]
        public long EnrollmentId { get; set; }

        [Required(ErrorMessage = " Estudante inválida")]
        public long StudentId { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
