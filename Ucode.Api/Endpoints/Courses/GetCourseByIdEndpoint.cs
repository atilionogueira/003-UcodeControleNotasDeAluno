using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Api.Models;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Course;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Courses
{
    public class GetCourseByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
           => app.MapGet("/{id}", HandlerAsync)
            .WithName("Course: Get By Id")
            .WithSummary("Recover a Course")
            .WithDescription("Recover a Course")
            .WithOrder(4)
            .Produces<Response<Course?>>();

        private static async Task<IResult> HandlerAsync(
            ClaimsPrincipal user,
            ICourseHandler handler,
            long id)
        {
            var request = new GetCourseByRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };

            var result = await handler.GetByIdAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
