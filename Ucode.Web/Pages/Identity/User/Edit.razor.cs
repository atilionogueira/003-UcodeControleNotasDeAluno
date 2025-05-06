using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Admin.Users;

namespace Ucode.Web.Pages.Identity.User
{
    public partial class EditUserPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public UpdateUserRequest InputModel { get; set; } = new();
        #endregion

        #region Parameters
        [Parameter]
        public string Id { get; set; } = string.Empty;
        #endregion

        #region Services
        [Inject] public ISnackbar Snackbar { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;
        [Inject] public IUserAdminHandler Handler { get; set; } = null!;
        #endregion

        #region Lifecycle
        protected override async Task OnInitializedAsync()
        {
            if (!long.TryParse(Id, out var userId))
            {
                Snackbar.Add("ID inválido", Severity.Error);
                NavigationManager.NavigateTo("/users");
                return;
            }

            await LoadUserAsync(userId);
        }
        #endregion

        #region Methods

        private async Task LoadUserAsync(long userId)
        {
            IsBusy = true;

            try
            {
                var request = new GetUserByIdRequest { Id = userId };
                var response = await Handler.GetUserByIdAsync(request, CancellationToken.None);

                if (response.IsSuccess && response.Data is not null)
                {
                    InputModel = new UpdateUserRequest
                    {
                        Id = response.Data.Id,
                        UserName = response.Data.UserName,
                        Email = response.Data.Email,
                        PhoneNumber = response.Data.PhoneNumber
                    };
                }
                else
                {
                    Snackbar.Add("Usuário não encontrado", Severity.Warning);
                    NavigationManager.NavigateTo("/users");
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Erro ao carregar usuário: {ex.Message}", Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await Handler.UpdateUserAsync(InputModel, CancellationToken.None);

                if (result.IsSuccess)
                {
                    Snackbar.Add("Usuário atualizado com sucesso", Severity.Success);
                    NavigationManager.NavigateTo("/users");
                }
                else
                {
                    Snackbar.Add("Erro ao atualizar usuário", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Erro ao atualizar: {ex.Message}", Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion
    }
}
