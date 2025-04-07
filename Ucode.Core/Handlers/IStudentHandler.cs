using Ucode.Core.Models;
using Ucode.Core.Requests.Students;
using Ucode.Core.Responses;

namespace Ucode.Core.Handlers
{
    public interface IStudentHandler
    {
        Task<Response<Student?>> CreateAsync(CreateStudentRequest request);
        Task<Response<Student?>> UpdateAsync(UpdateStudentRequest request);
        Task<Response<Student?>> DeleteAsync(DeleteStudentRequest request);
        Task<Response<Student?>> GetByIdAsync(GetStudentByIdRequest request);
        Task<PagedResponse<List<Student>>> GetAllAsync(GetAllStudentRequest request);

    }
}
