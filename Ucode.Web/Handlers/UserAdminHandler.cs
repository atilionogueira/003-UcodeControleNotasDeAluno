using System.Net.Http.Json;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Admin.Users;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.Admin.Users;


namespace Ucode.Web.Handlers
{
    public class UserAdminHandler(IHttpClientFactory httpClientFactory) : IUserAdminHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<PagedResponse<List<UserResponse>>> GetAllUsersAsync(GetAllUsersRequest request, CancellationToken cancellationToken)
           => await _client.GetFromJsonAsync<PagedResponse<List<UserResponse>>>("v1/identity/admin/user", cancellationToken)
                ?? new PagedResponse<List<UserResponse>>(null, 400, "Não foi possível obter os usuários");

        public async Task<Response<UserResponse>> GetUserByIdAsync(GetUserByIdRequest request, CancellationToken cancellationToken)
          => await _client.GetFromJsonAsync<Response<UserResponse>>($"v1/Identity/admin/user/{request.Id}", cancellationToken)
                ?? new Response<UserResponse>(null, 400, "Não foi possível obter o usuário");
       
        public async Task<Response<UserResponse>> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _client.PostAsJsonAsync("v1/identity/admin/user", request, cancellationToken);
            return await result.Content.ReadFromJsonAsync<Response<UserResponse>>()
                ?? new Response<UserResponse>(null, 400, "Falha ao criar o usuário");
        }

        public async Task<Response<UserResponse>> UpdateUserAsync(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _client.PutAsJsonAsync($"v1/identity/admin/user/{request.Id}", request, cancellationToken);
            return await result.Content.ReadFromJsonAsync<Response<UserResponse>>()
                ?? new Response<UserResponse>(null, 400, "Falha ao atualizar o usuário");
        }

        public async Task<Response<UserResponse>> DeleteUserAsync(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _client.DeleteAsync($"v1/identity/admin/user/{request.Id}", cancellationToken);
            return await result.Content.ReadFromJsonAsync<Response<UserResponse>>()
                ?? new Response<UserResponse>(null, 400, "Falha ao excluir o usuário");
        }      

        
    }
}
