using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Enrollments;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Enrollments
{
    public class DeleteEnrollmentsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/{id}", HandlerAsync)
               .WithName("Enrollment: Delete")
               .WithSummary("Delete the enreollment")
               .WithDescription("Delete the enreollment")
               .WithOrder(3)
               .Produces<Response<Enrollment?>>();

        private static async Task<IResult> HandlerAsync(
            ClaimsPrincipal user,
            IEnrollmentHandler handler,
            long id)
        {
            var request = new DeleteEnrollmentsRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };

            var result = await handler.DeleteAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }       
    }
}
