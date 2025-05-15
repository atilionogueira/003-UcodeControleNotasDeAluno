using Microsoft.AspNetCore.Mvc;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Identity
{
    public class RegisterEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
           => app.MapPost("/register-basic", HandleAsync)
               .WithName("Register: Account")
               .WithTags("Identity")
               .WithSummary("Registro de usuário")
               .WithDescription("Cria um novo usuário com email, senha, nome completo e telefone.")
               .Produces<Response<string>>(201)
               .Produces<Response<string>>(400);

        private static async Task<IResult> HandleAsync(
            [FromBody] RegisterRequest request,
            [FromServices] IAccountHandler handler)
        {
            var result = await handler.RegisterAsync(request);

            return result.IsSuccess
                ? TypedResults.Created($"/v1/identity/register/{result.Data}", result)
                : TypedResults.BadRequest(result);
        }
    }
}
