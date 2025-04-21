
using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Account
{
    public class RegisterRequest
    {

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Password { get; set; } = string.Empty;
    }
}
