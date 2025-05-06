using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Admin.Users;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Identity.Admin.Users
{
    public class DeleteUserEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("admin/user/{id}", HandlerAsync)
                .WithName("Delete: User")
                .WithSummary("Delete the User")
                .WithDescription("Delete the User")
                .WithOrder(3)
                .Produces<Response<string>>();

        private static async Task<IResult> HandlerAsync(
            ClaimsPrincipal user,
            IUserAdminHandler handler,
            long id) // O id do usuário a ser excluído
        {
            var request = new DeleteUserRequest
            {
                UserId = user.Identity?.Name ?? string.Empty, // UserId do usuário autenticado
                Id = id  // Id do usuário a ser deletado
            };

            // Chama o método DeleteUserAsync do handler
            var result = await handler.DeleteUserAsync(request, CancellationToken.None);

            return result.IsSuccess
                ? TypedResults.Ok(result)  // Retorna sucesso com o Response
                : TypedResults.BadRequest(result);  // Caso haja falha, retorna BadRequest com a mensagem de erro
        }
    }
}

