using Microsoft.AspNetCore.Identity;
using Ucode.Api.Models;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account;
using Ucode.Core.Responses;

namespace Ucode.Api.Handlers
{
    public class AccountHandler : IAccountHandler
    {
        private readonly UserManager<User> _userManager;

        public AccountHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Response<string>> RegisterAsync(RegisterRequest request)
        {
            // Verifica se já existe usuário com o mesmo email
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new Response<string>(null, 400, "Usuário com este e-mail já existe.");
            }

            // Cria o novo usuário
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                EmailConfirmed = false // ajustar conforme regra
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new Response<string>(null, 400, $"Erro ao registrar: {errors}");
            }

            return new Response<string>(user.Id.ToString(), 201, "Usuário registrado com sucesso");
        }
        public Task<Response<string>> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        
    }
}
