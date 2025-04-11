using Microsoft.AspNetCore.Mvc;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Course;
using Ucode.Core.Responses;
using Ucode.Core;
using Ucode.Core.Requests.Grade;
using System.Security.Claims;

namespace Ucode.Api.Endpoints.Grades
{
    public class GetAllGradeEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapGet("/", HandlerAsync)
            .WithName("Grade: Get all")
            .WithSummary("Recover All grade")
            .WithDescription("Recover All grade")
            .WithOrder(5)
            .Produces<PagedResponse<List<Grade>?>>();

        private static async Task<IResult> HandlerAsync(
            ClaimsPrincipal user,
            IGradeHandler Handler,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllGradeRequest
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
