using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Course;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Courses
{
    public class DeleteCourseEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/", HandlerAsync)
            .WithName("Delete: Course")
            .WithSummary("Delete the Course")
            .WithDescription("Delete the Course")
            .WithOrder(3)
            .Produces<Response<Course?>>();

        private static async Task<IResult> HandlerAsync(
            ClaimsPrincipal user,
            ICourseHandler handler,
            long id)
        {
            var request = new DeleteCourseRequest
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
