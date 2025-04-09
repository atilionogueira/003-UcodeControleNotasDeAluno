using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Enrollments;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Enrollments
{
    public class UpdateEnrollmentEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandleAsync)
            .WithName("Enrollment: Update")
            .WithSummary("Update a enrollment")
            .WithDescription("Update a enrollment")
            .WithOrder(2)
            .Produces<Response<Enrollment?>>();

        private static async Task<IResult> HandleAsync(
            IEnrollmentHandler handler,
            UpdateEnrollmentsRequest request,
            long id)
        {
            request.UserId = "teste@teste.com.br";
            request.Id = id;

            var result = await handler.UpdateAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
