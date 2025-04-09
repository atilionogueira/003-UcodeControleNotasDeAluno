using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Course;
using Ucode.Core.Requests.Grade;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Grades
{
    public class GetGradeByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
              => app.MapGet("/{id}", HandlerAsync)
            .WithName("Grade: Get By Id")
            .WithSummary("Recover a Grade")
            .WithDescription("Recover a Grade")
            .WithOrder(4)
            .Produces<Response<Grade?>>();

        private static async Task<IResult> HandlerAsync(
            IGradeHandler handler,
            long id)
        {
            var request = new GetGradeByIdRequest
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
