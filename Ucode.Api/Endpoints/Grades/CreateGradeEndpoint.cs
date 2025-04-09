using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Grade;
using Ucode.Core.Responses;


namespace Ucode.Api.Endpoints.Grades
{
    public class CreateGradeEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
               => app.MapPost("/", HandlerAsync)
            .WithName("Grades: Create")
            .WithSummary("Create New Grade")
            .WithDescription("Create New Grade")
            .WithOrder(1)
            .Produces<Response<Grade?>>();


        private static async Task<IResult> HandlerAsync(
           IGradeHandler handler,
           CreateGradeRequest request)
        {
            request.UserId = "teste@teste.com.br";
            var result = await handler.CreateAsync(request);

            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);

        }
    }
}
