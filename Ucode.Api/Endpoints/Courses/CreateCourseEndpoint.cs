using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Course;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Courses
{
    public class CreateCourseEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapPost("/", HandleAsync)
            .WithName("Course: Course")
            .WithSummary("Create new Course")
            .WithDescription("Create new Course")
            .WithOrder(1)
            .Produces<Response<Course?>>();

        private static async Task<IResult> HandleAsync(
            ICourseHandler handler,
            CreateCourseRequest request)

        {
            request.UserId = "teste@teste.com.br";

            var result = await handler.CreateAsync(request);

            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);
        }
    }
}
