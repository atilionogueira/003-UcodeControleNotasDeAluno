using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account;
using Ucode.Web.Security;

namespace Ucode.Web.Pages.Identity
{
    public partial class LoginPage : ComponentBase
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

        #region Properties

        public bool IsBusy { get; set; } = false;  //e para verficar se a página esta ocupada ou não.

        public LoginRequest InputModel { get; set; } = new();
        #endregion

        #region Override

        protected override async Task OnInitializedAsync()  // verificar o usuário esta logado 
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User; // Retorna o ClaimsPrincipal que contém o usuarios

            // if (user.Identity is not null && user.Identity.IsAuthenticated)  Colocando em um padrão
               if(user.Identity is { IsAuthenticated: true})    
                NavigationManager.NavigateTo("/");
        }

        #endregion

        #region Methods
        public async Task OnValidSubmitAsync() 
        {
            IsBusy = true;

            try
            {
             var result = await Handler.LoginAsync(InputModel);

                if (result.IsSuccess) 
                {
                    await AuthenticationStateProvider.GetAuthenticationStateAsync();
                    AuthenticationStateProvider.NotifyAuthenticationStateChanged();
                    NavigationManager.NavigateTo("/");
                }
                else 
                {
                    Snackbar.Add(result.Message, Severity.Error);
                }      
                
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }

        }
        #endregion

    }
}
