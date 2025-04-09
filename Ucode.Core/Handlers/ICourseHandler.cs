
using Ucode.Core.Models;
using Ucode.Core.Requests.Course;
using Ucode.Core.Responses;

namespace Ucode.Core.Handlers
{
    public interface ICourseHandler
    {
        Task<Response<Course?>> CreateAsync(CreateCourseRequest request);
        Task<Response<Course?>> UpdateAsync(UpdateCourseRequest request);
        Task<Response<Course?>> DeleteAsync(DeleteCourseRequest request);
        Task<Response<Course?>> GetByIdAsync(GetCourseByRequest request);
        Task<PagedResponse<List<Course>>> GetAllAsync(GetAllCourseRequest request);

    }
}
