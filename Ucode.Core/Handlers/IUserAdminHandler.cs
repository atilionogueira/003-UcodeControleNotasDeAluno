using Ucode.Core.Requests.Account.Admin.Users;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.Admin.Users;


namespace Ucode.Core.Handlers
{
    public interface IUserAdminHandler
    {

        Task<Response<UserResponse>> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken);
        Task<Response<UserResponse>> UpdateUserAsync(UpdateUserRequest request, CancellationToken cancellationToken);
        Task<Response<UserResponse>> DeleteUserAsync(DeleteUserRequest request, CancellationToken cancellationToken);
        Task<Response<UserResponse>> GetUserByIdAsync(GetUserByIdRequest request, CancellationToken cancellationToken);
        Task<PagedResponse<List<UserResponse>>> GetAllUsersAsync(GetAllUsersRequest request, CancellationToken cancellationToken);
       

    }
}
