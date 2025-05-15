﻿using System.Net.Http.Json;
using System.Text;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account;
using Ucode.Core.Responses;

namespace Ucode.Web.Handlers
{
    public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<string>> LoginAsync(LoginRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);
            return result.IsSuccessStatusCode
                ? new Response<string>("Login realizado com sucesso", 200, "Login realizado com sucesso")
                : new Response<string>("null", 400, "Não foi possível realizar login");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request)
        {
            /*
            var result = await _client.PostAsJsonAsync("v1/identity/register-basic", request);

            return result.IsSuccessStatusCode
                ? new Response<string>("Cadastro realizado com sucesso!", 201, "Cadastro realizado com sucesso!")
                : new Response<string>(null, 400, "Não foi possível realizar o seu cadastro");
              */

            var result = await _client.PostAsJsonAsync("v1/identity/register-basic", request);

            var content = await result.Content.ReadFromJsonAsync<Response<string>>();

            return content ?? new Response<string>(null, 400, "Não foi possível realizar o seu cadastro");

        }

        public async Task LogoutAsync()
        {
            var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
            await _client.PostAsJsonAsync("v1/identity/logout", emptyContent);
        }


    }
}
