using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Course;
using Ucode.Core.Requests.Grade;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Grades
{
    public class DeleteGradeEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
              => app.MapDelete("/", HandlerAsync)
            .WithName("Delete: Grade")
            .WithSummary("Delete the Grade")
            .WithDescription("Delete the Grade")
            .WithOrder(3)
            .Produces<Response<Grade?>>();

        private static async Task<IResult> HandlerAsync(
            IGradeHandler handler,
            long id)
        {
            var request = new DeleteGradeRequest
            {
                UserId = "teste@teste.com.br",
                Id = id
            };

            var result = await handler.DeleteAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
