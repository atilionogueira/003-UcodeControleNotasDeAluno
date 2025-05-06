namespace Ucode.Core.Responses.Account.Admin.Users
{
    public class UserResponse
    {
        public long Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
