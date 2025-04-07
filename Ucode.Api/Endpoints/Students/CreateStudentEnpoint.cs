using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Students;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Students
{
    public class CreateStudentEnpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapPost("/", HandlerAsync)
            .WithName("Students: Create")
            .WithSummary("Create New Student")
            .WithDescription("Create New Student")
            .WithOrder(1)
            .Produces<Response<Student?>>();


        private static async Task<IResult> HandlerAsync(
           IStudentHandler handler,
           CreateStudentRequest request)
        {
            request.UserId = "teste@teste.com.br";
            var result = await handler.CreateAsync(request);

            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);

        }
    }
}
