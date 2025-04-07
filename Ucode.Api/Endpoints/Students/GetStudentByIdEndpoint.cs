using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Students;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Students
{
    public class GetStudentByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
                    .WithName("Students: Get By Id")
                    .WithSummary("Recover a student")
                    .WithDescription("Recover a student")
                    .WithOrder(4)
                    .Produces<Response<Student?>>();

        private static async Task<IResult> HandleAsync(
            IStudentHandler handler,
            long id)
        {
            var request = new GetStudentByIdRequest
            {
                UserId = "teste@teste.com.br",
                Id = id,
            };


            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    } 
             
}
