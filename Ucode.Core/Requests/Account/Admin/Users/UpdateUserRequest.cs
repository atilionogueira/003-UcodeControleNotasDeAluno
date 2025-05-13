using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Account.Admin.Users
{
    public class UpdateUserRequest
    {       

        [Required(ErrorMessage = "Id do usuário é obrigatório")]
        public long Id { get; set; }

        [MaxLength(50, ErrorMessage = "O nome de usuário deve ter no máximo 50 caracteres")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Nome completo é obrigatório")]
        [MaxLength(100, ErrorMessage = "O nome completo deve ter no máximo 100 caracteres")]
        public string FullName { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [MaxLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Número de telefone inválido")]
        [MaxLength(13, ErrorMessage = "O número de telefone deve ter no máximo 13 caracteres")]
        public string? PhoneNumber { get; set; }  // ID do usuário que está realizando a atualização (Auditabilidade)

        public string? UpdatedByUserId { get; set; } 
    }
}