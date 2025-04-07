using Microsoft.AspNetCore.Mvc;
using Ucode.Api.Common.Api;
using Ucode.Core;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Students;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Students
{
    public class GetAllStudentEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
                => app.MapGet("/", HandleAsync)
                    .WithName("Students: Get all")
                    .WithSummary("Recover all student")
                    .WithDescription("Recover all student")
                    .WithOrder(5)
                    .Produces<PagedResponse<List<Student>?>>();

        private static async Task<IResult> HandleAsync(
            IStudentHandler handler,
            [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery]int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllStudentRequest
            {
                UserId = "teste@teste.com.br",
                PageNumber = pageNumber,
                PageSize = pageSize,
            };


            var result = await handler.GetAllAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
