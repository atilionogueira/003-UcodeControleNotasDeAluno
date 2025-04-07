using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Course;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Courses
{
    public class UpdateCourseEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapPut("/", HandlerAsync)
            .WithName("Course : Update")
            .WithSummary("Update the Course")
            .WithDescription("Update the Course")
            .WithOrder(2)
            .Produces<Response<Course?>>();

        private static async Task<IResult> HandlerAsync(
            ICourseHandler handler,
            UpdateCourseRequest request,
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
