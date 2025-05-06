namespace Ucode.Core.Requests.Account.Admin.Users
{
    public class CreateUserRequest
    {
        public string? CreatedByUserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>(); // Lista de roles para o usuário

    }
}
