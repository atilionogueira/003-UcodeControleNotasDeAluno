using System.ComponentModel.DataAnnotations;
using Ucode.Core.Emuns;

namespace Ucode.Core.Requests.Students
{
    public class UpdateStudentRequest : Request
    {
        public long  Id { get; set; }

        [Required(ErrorMessage = "Nome inválido")]
        [MaxLength(100, ErrorMessage = "O nome deve conter até 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email fornecido é inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data de nascimento é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Data de nascimento deve ser uma data válida.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Data de nascimento é obrigatória.")]
        public DateTime? UpdatedAt { get; set; } 

        [Required(ErrorMessage = "Gênero é obrigatório.")]
        public EGender Gender { get; set; }

    }
}
