using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Enrollments;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Enrollments
{
    public class GetEnrollmentByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
            .WithName("Enrollment: Get By Id")
            .WithSummary("Retrieves all enreollment")
            .WithDescription("Retrieves all enreollment")
            .WithOrder(4)
            .Produces<Response<Enrollment?>>();

        private static async Task<IResult> HandleAsync(
            IEnrollmentHandler handler,
            long id)
        {
            var request = new GetEnrollmentsByIdRequest
            {
                UserId = "teste@teste.com.br",
                Id = id
            };

            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
