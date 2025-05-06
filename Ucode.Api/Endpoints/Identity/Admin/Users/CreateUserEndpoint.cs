using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using Ucode.Core.Requests.Account.Admin.Users;

namespace Ucode.Api.Endpoints.Identity.Admin.Users
{
    public class CreateUserEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("admin/user", HandleAsync)
                .WithName("Identity: CreateUser")
                .WithSummary("Create a new user")
                .WithDescription("Creates a new user in the system.")
                .WithOrder(2)
                .Produces<Response<string>>()  // A resposta esperada é Response<string>
                .ProducesValidationProblem();  // Para tratamento de erro de validação

        private static async Task<IResult> HandleAsync(
            [FromBody] CreateUserRequest request,  // Captura os dados do corpo da requisição
            IUserAdminHandler handler,                  // Injeta o IUserHandler
            CancellationToken cancellationToken)   // Recebe o token de cancelamento
        {
            if (request == null)
            {
                return Results.BadRequest("Invalido user data.");
            }

            // Chama o método CreateUserAsync para criar o usuário
            var result = await handler.CreateUserAsync(request, cancellationToken);

            // Se a criação do usuário for bem-sucedida, retorna um status 201 Created com o ID do usuário
            return result.IsSuccess
                ? Results.Created($"/v1/identity/users/{result.Data}", result)  // A URL pode ser ajustada conforme o seu caso
                : Results.BadRequest(result); // Retorna BadRequest em caso de erro
        }
    }
}

