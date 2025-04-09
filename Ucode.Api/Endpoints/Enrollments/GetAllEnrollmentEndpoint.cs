using Microsoft.AspNetCore.Mvc;
using Ucode.Api.Common.Api;
using Ucode.Core.Responses;
using Ucode.Core;
using Ucode.Core.Models;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Enrollments;

namespace Ucode.Api.Endpoints.Enrollments
{
    public class GetAllEnrollmentEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapGet("/", HandleAsync)
            .WithName("Enreollment: Get All")
            .WithSummary("Retrieves all enreollment")
            .WithDescription("Retrieves all enreollment")
            .WithOrder(5)
            .Produces<PagedResponse<List<Enrollment>?>>();

        private static async Task<IResult> HandleAsync(
            IEnrollmentHandler handler,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllEnrollmentRequest
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
