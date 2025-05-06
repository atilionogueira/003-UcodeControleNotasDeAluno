namespace Ucode.Core.Requests.Account.Admin.Users
{
    public class DeleteUserRequest : Request
    {
        public long Id { get; set; }  // ID do usuário a ser excluído

        // Usando a palavra-chave 'new' para ocultar a propriedade UserId da classe base
        public string? DeletedByUserId { get; set; }  // ID do usuário que está realizando a exclusão (Auditabilidade)

    }
}
