using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Account.Admin.Users
{
    public class CreateUserRequest
    {       
        public string? CreatedByUserId { get; set; }

        [Required(ErrorMessage = "Nome de usuário é obrigatório")]
        [MaxLength(50, ErrorMessage = "O nome de usuário deve ter no máximo 50 caracteres")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nome completo é obrigatório")]
        [MaxLength(100, ErrorMessage = "O nome completo deve ter no máximo 100 caracteres")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [MaxLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Número de telefone inválido")]
        [MaxLength(13, ErrorMessage = "O número de telefone deve ter no máximo 13 caracteres")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres")]
        public string Password { get; set; } = string.Empty;       
    }
}


