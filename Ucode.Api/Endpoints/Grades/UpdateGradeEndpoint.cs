using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Course;
using Ucode.Core.Requests.Grade;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Grades
{
    public class UpdateGradeEndpoint : IEndpoint
    { 
        public static void Map(IEndpointRouteBuilder app)
             => app.MapPut("/", HandlerAsync)
            .WithName("Grade : Update")
            .WithSummary("Update the Grade")
            .WithDescription("Update the Grade")
            .WithOrder(2)
            .Produces<Response<Grade?>>();

        private static async Task<IResult> HandlerAsync(
            IGradeHandler handler,
            UpdateGradeRequest request,
            long id)
        {
            request.UserId = "teste@teste.com.br";
            request.Id = id;

            var result = await handler.UpdateAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(request);
        }        
    }
}
