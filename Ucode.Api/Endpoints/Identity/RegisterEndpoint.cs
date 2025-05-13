using Microsoft.AspNetCore.Mvc;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account;
using Ucode.Core.Requests.Account.Admin.Users;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.Admin.Users;

namespace Ucode.Api.Endpoints.Identity
{
    public class RegisterEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/v1/identity/register", HandleAsync)
                  .WithName("Identity: RegisterUser")
                  .WithSummary("Register a new user")
                  .WithDescription("Registers a new user using ASP.NET Identity")
                  .WithOrder(1)
                  .Produces<Response<UserResponse>>()
                  .ProducesValidationProblem()
                  .AllowAnonymous();

        private static async Task<IResult> HandleAsync(
            [FromBody] RegisterRequest request,
            IAccountHandler handler)
           
        {
            if (request == null)
                return Results.BadRequest(new Response<string>("Dados inválidos para registro."));

            var result = await handler.RegisterAsync(request);
            
             return result.IsSuccess
                 ? Results.Created($"/v1/identity/users/{request.Email}", result)
                 : Results.BadRequest(result);
            
            
        }
    }
}

