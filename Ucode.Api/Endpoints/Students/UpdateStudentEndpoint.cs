using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Students;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Students
{
    public class UpdateStudentEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapPut("/{id}", HandleAsync)
                    .WithName("Students: Update")
                    .WithSummary("Update the student")
                    .WithDescription("Update the student")
                    .WithOrder(2)
                    .Produces<Response<Student?>>();
        
        private static async Task<IResult>HandleAsync(
            ClaimsPrincipal user,
            IStudentHandler handler,
            UpdateStudentRequest request,
            long id)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            request.Id = id;

            var result = await handler.UpdateAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
            
    }
}
