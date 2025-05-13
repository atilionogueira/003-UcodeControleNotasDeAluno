using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ucode.Core.Responses;
using Ucode.Core.Requests.Account.Admin.Users;
using Ucode.Core.Responses.Account.Admin.Users;
using Ucode.Api.Models;

namespace Ucode.Core.Handlers
{
    public class UserAdminHandler(UserManager<User> userManager) : IUserAdminHandler
    {

        public async Task<PagedResponse<List<UserResponse>>> GetAllUsersAsync(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = userManager.Users
                    .AsNoTracking()
                    .OrderBy(u => u.UserName);

                var total = await query.CountAsync(cancellationToken);

                var users = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(u => new UserResponse
                    {
                        Id = u.Id,
                        UserName = u.UserName ?? string.Empty,
                        Email = u.Email ?? string.Empty,
                        PhoneNumber = u.PhoneNumber ?? string.Empty,
                        FullName = u.FullName ?? string.Empty,
                        IsEmailConfirmed = u.EmailConfirmed
                    })
                    .ToListAsync(cancellationToken);

                return new PagedResponse<List<UserResponse>>(users, total, request.PageNumber, request.PageSize);
            }
            catch (Exception ex)
            {
                return new PagedResponse<List<UserResponse>>(null, 500, $"Erro ao consultar os usuários: {ex.Message}");
            }
        }

        public async Task<Response<UserResponse>> GetUserByIdAsync(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userManager.FindByIdAsync(request.Id.ToString());

                if (user == null)
                    return new Response<UserResponse>(null, 404, "Usuário não encontrado.");

                var userResponse = new UserResponse
                {
                    Id = user.Id,
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    PhoneNumber = user.PhoneNumber ?? string.Empty,
                    FullName = user.FullName ?? string.Empty,
                    IsEmailConfirmed = user.EmailConfirmed
                };

                return new Response<UserResponse>(userResponse, 200, "Usuário encontrado.");
            }
            catch (Exception ex)
            {
                return new Response<UserResponse>(null, 500, $"Erro ao recuperar o usuário: {ex.Message}");
            }
        }
        public async Task<Response<UserResponse>> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new User
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,                    
                    FullName = request.FullName
                };

                var result = await userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                    return new Response<UserResponse>(null, 400, string.Join("; ", result.Errors.Select(e => e.Description)));

                var userResponse = new UserResponse
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FullName = user.FullName,
                    IsEmailConfirmed = user.EmailConfirmed
                };

                return new Response<UserResponse>(userResponse, 201, "Usuário criado com sucesso.");
            }
            catch (Exception ex)
            {
                return new Response<UserResponse>(null, 500, $"Erro ao criar o usuário: {ex.Message}");
            }
        }

        public async Task<Response<UserResponse>> UpdateUserAsync(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userManager.FindByIdAsync(request.Id.ToString());

                if (user == null)
                    return new Response<UserResponse>(null, 404, "Usuário não encontrado.");

                user.UserName = request.UserName ?? user.UserName;
                user.Email = request.Email ?? user.Email;
                user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
                user.FullName = request.FullName ?? user.FullName;

                var result = await userManager.UpdateAsync(user);

                if (!result.Succeeded)
                    return new Response<UserResponse>(null, 400, string.Join("; ", result.Errors.Select(e => e.Description)));

                var userResponse = new UserResponse
                {
                    Id = user.Id,
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    PhoneNumber = user.PhoneNumber ?? string.Empty,
                    FullName = user.FullName ?? string.Empty,
                    IsEmailConfirmed = user.EmailConfirmed
                };

                return new Response<UserResponse>(userResponse, 200, "Usuário atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return new Response<UserResponse>(null, 500, $"Erro ao atualizar o usuário: {ex.Message}");
            }
        }

        public async Task<Response<UserResponse>> DeleteUserAsync(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userManager.FindByIdAsync(request.Id.ToString());

                if (user == null)
                    return new Response<UserResponse>(null, 404, "Usuário não encontrado.");

                var result = await userManager.DeleteAsync(user);

                if (!result.Succeeded)
                    return new Response<UserResponse>(null, 400, string.Join("; ", result.Errors.Select(e => e.Description)));

                var userResponse = new UserResponse
                {
                    Id = user.Id, 
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    PhoneNumber = user.PhoneNumber ?? string.Empty,
                    FullName = user.FullName ?? string.Empty,
                    IsEmailConfirmed = user.EmailConfirmed
                };

                return new Response<UserResponse>(userResponse, 200, "Usuário excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return new Response<UserResponse>(null, 500, $"Erro ao excluir o usuário: {ex.Message}");
            }
        }    
             
    }
}
