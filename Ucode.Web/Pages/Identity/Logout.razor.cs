using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Web.Security;

namespace Ucode.Web.Pages.Identity
{
    public partial class LogoutPage : ComponentBase
    {
        #region Services

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IAccountHandler Handler { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

        #endregion

        #region Override

        protected override async Task OnInitializedAsync()  // verificar o usuário esta logado 
        {
            if (await AuthenticationStateProvider.CheckAuthenticatedAsync()) 
            {
                await Handler.LogoutAsync();
                await AuthenticationStateProvider.GetAuthenticationStateAsync(); // Atualizar o estado de autenticação
                AuthenticationStateProvider.NotifyAuthenticationStateChanged(); // Informa que a aplicação não esta logado
            }

            await base.OnInitializedAsync(); // para continuar com o fluxo da aplicação
        }

        #endregion


    }
}
