
using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Account
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "O campo Nome completo é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome deve ter até 200 caracteres.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Nome de usuário é obrigatório.")]
        [StringLength(180, ErrorMessage = "O nome de usuário deve ter até 180 caracteres.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido.")]
        [StringLength(180)]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "O número de telefone é inválido.")]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Password { get; set; } = string.Empty;

    }
}
