using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Admin.Users;


namespace Ucode.Web.Pages.Identity.User
{
    public partial class CreateUserPage : ComponentBase
    {
        #region Properties
        public bool isBusy { get; set; } = false;
        public CreateUserRequest InputModel { get; set; } = new();
        #endregion

        #region Services
        [Inject]
        public IUserAdminHandler Handler { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        #endregion

        #region Methods
        public async Task OnValidSubmitAsync()
        {
            isBusy = true;

            try
            {
                // Correção: passando CancellationToken.None
                var result = await Handler.CreateUserAsync(InputModel, CancellationToken.None);

                if (result.IsSuccess)
                {
                    Snackbar.Add("Usuário criado com sucesso!", Severity.Success);
                    NavigationManager.NavigateTo("/users");
                }
                else
                {
                    Snackbar.Add(result.Message, Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Erro: {ex.Message}", Severity.Error);
            }
            finally
            {
                isBusy = false;
            }
        }
        #endregion
    }
}
