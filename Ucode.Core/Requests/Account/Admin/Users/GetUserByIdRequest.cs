namespace Ucode.Core.Requests.Account.Admin.Users
{
    public class GetUserByIdRequest
    {
        public long Id { get; set; }  // ID do usuário a ser excluído       
        public string? RequestedByUserId { get; set; }  // ID do usuário que está realizando a exclusão (Auditabilidade)

    }
}
