using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Students;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Students
{
    public class DeleteStudentEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapDelete("/{id}", HandleAsync)
                    .WithName("Students: Delete")
                    .WithSummary("Delete the student")
                    .WithDescription("Delete the student")
                    .WithOrder(3)
                    .Produces<Response<Student?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IStudentHandler handler,           
            long id)
        {
            var request = new DeleteStudentRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id,
            };


            var result = await handler.DeleteAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
