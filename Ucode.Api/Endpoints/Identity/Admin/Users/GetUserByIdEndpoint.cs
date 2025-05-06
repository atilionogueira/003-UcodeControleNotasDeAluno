using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Admin.Users;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.Admin.Users;

namespace Ucode.Api.Endpoints.Identity.Admin.Users
{
    public class GetUserByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
           => app.MapGet("admin/user/{id}", HandlerAsync)
               .WithName("Get: User by Id")
               .WithSummary("Get a user by Id")
               .WithDescription("Gets a specific user by their Id")
               .WithOrder(1)
               .Produces<Response<UserResponse>>();

        private static async Task<IResult> HandlerAsync(
            ClaimsPrincipal User,
            IUserAdminHandler handler,
            long id)
        {
            var request = new GetUserByIdRequest
            {
                Id = id,
                RequestedByUserId = User.Identity?.Name ?? string.Empty
            };

            var result = await handler.GetUserByIdAsync(request, CancellationToken.None);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.NotFound(result);
        }
    }
}

