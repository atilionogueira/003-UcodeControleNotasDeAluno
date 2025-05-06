namespace Ucode.Core.Requests.Account.Admin.Users
{
    public class UpdateUserRequest
    {

        public long Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UpdatedByUserId { get; set; }  // ID do usuário que está realizando a atualização (Auditabilidade)
    }
}
