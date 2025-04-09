using Ucode.Core.Models;
using Ucode.Core.Requests.Grade;
using Ucode.Core.Responses;

namespace Ucode.Core.Handlers
{
    public interface IGradeHandler
    {
        Task<Response<Grade?>>CreateAsync(CreateGradeRequest request);
        Task<Response<Grade?>> UpdateAsync(UpdateGradeRequest request);
        Task<Response<Grade?>> DeleteAsync(DeleteGradeRequest request);
        Task<Response<Grade?>> GetByIdAsync(GetGradeByIdRequest request);
        Task<PagedResponse<List<Grade>>> GetAllAsync(GetAllGradeRequest request);


    }
}
