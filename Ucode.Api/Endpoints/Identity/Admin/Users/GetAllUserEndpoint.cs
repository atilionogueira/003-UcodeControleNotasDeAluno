using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Admin.Users;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.Admin.Users;

namespace Ucode.Api.Endpoints.Identity.Admin.Users
{
    public class GetAllUserEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("admin/user", HandleAsync)
                .WithName("Identity: GetAllUsers")
                .WithSummary("Get all users")
                .WithDescription("Retrieve all users")
                .WithOrder(1)
                .Produces<PagedResponse<List<UserResponse>>>()
                .ProducesValidationProblem();

        private static async Task<IResult> HandleAsync(
            IUserAdminHandler handler,
            CancellationToken cancellationToken) // Asegure-se de passar o CancellationToken
        {
            var request = new GetAllUsersRequest();  // Crie o request, se necessário
            var result = await handler.GetAllUsersAsync(request, cancellationToken);  // Chama o método adequado

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}

