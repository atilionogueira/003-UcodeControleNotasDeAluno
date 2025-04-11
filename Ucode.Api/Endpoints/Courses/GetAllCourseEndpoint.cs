using Microsoft.AspNetCore.Mvc;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Course;
using Ucode.Core.Responses;
using Ucode.Core;
using System.Security.Claims;

namespace Ucode.Api.Endpoints.Courses
{
    public class GetAllCourseEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
              => app.MapGet("/", HandlerAsync)
            .WithName("Courses: Get all")
            .WithSummary("Recover All course")
            .WithDescription("Recover All course")
            .WithOrder(5)
            .Produces<PagedResponse<List<Course>?>>();

        private static async Task<IResult> HandlerAsync(
            ClaimsPrincipal user,
            ICourseHandler Handler,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllCourseRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var result = await Handler.GetAllAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
