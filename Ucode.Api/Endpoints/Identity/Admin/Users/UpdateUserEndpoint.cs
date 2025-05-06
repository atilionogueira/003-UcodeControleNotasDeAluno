using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Admin.Users;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Identity.Admin.Users
{
    public class UpdateUserEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("admin/user/{id}", HandlerAsync)
                .WithName("Update: User")
                .WithSummary("Update an existing User")
                .WithDescription("Update an existing User")
                .WithOrder(2)
                .Produces<Response<string>>();

        private static async Task<IResult> HandlerAsync(
            ClaimsPrincipal user,
            IUserAdminHandler handler,
            long id, // ID do usuário a ser atualizado
            UpdateUserRequest request) // Parâmetros de entrada
        {
            // Setando o UserId que está fazendo a atualização (auditabilidade)
            request.UpdatedByUserId = user.Identity?.Name ?? string.Empty;
            request.Id = id;

            // Chamando o handler para realizar a atualização do usuário
            var result = await handler.UpdateUserAsync(request, CancellationToken.None);

            return result.IsSuccess
                ? TypedResults.Ok(result)  // Caso de sucesso
                : TypedResults.BadRequest(result);  // Caso de falha
        }
    }
}
