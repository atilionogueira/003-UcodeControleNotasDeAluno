using Ucode.Core.Models;
using Ucode.Core.Requests.Enrollments;
using Ucode.Core.Responses;

namespace Ucode.Core.Handlers
{
    public interface IEnrollmentHandler 
    {
        Task<Response<Enrollment?>> CreateAsync(CreateEnrollmentRequest request);
        Task<Response<Enrollment?>> UpdateAsync(UpdateEnrollmentsRequest request);
        Task<Response<Enrollment?>> DeleteAsync(DeleteEnrollmentsRequest request);
        Task<Response<Enrollment?>> GetByIdAsync(GetEnrollmentsByIdRequest request);
        Task<PagedResponse<List<Enrollment>>> GetAllAsync(GetAllEnrollmentRequest request);

    }
}
