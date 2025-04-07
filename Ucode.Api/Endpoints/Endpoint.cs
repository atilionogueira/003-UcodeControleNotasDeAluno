
using Ucode.Api.Common.Api;
using Ucode.Api.Endpoints.Courses;
using Ucode.Api.Endpoints.Students;


namespace Ucode.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app
                .MapGroup("");

            endpoints.MapGroup("v1/students")
                .WithTags("Students")
                //.RequireAuthorization()
                .MapEndpoint<CreateStudentEnpoint>()
                .MapEndpoint<UpdateStudentEndpoint>()
                .MapEndpoint<DeleteStudentEndpoint>()
                .MapEndpoint<GetStudentByIdEndpoint>()
                .MapEndpoint<GetAllStudentEndpoint>();

            endpoints.MapGroup("v1/courses")
               .WithTags("Course")
               //.RequireAuthorization()
               .MapEndpoint<CreateCourseEndpoint>()
               .MapEndpoint<UpdateCourseEndpoint>()
               .MapEndpoint<DeleteCourseEndpoint>()
               .MapEndpoint<GetCourseByIdEndpoint>()
               .MapEndpoint<GetAllCourseEndpoint>();
        }
        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
    

