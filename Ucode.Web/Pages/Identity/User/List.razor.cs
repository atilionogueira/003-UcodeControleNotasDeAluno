using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Admin.Users;
using Ucode.Core.Responses.Account.Admin.Users;

namespace Ucode.Web.Pages.Identity.User
{
    public class ListUserPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public List<UserResponse> Users { get; set; } = new();
        public string SearchTerm { get; set; } = string.Empty;
        #endregion

        #region Services
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        [Inject]
        public IUserAdminHandler Handler { get; set; } = null!;
        #endregion

        #region Override
        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetAllUsersRequest();
                var result = await Handler.GetAllUsersAsync(request, CancellationToken.None);

                if (result.IsSuccess)
                    Users = result.Data ?? new List<UserResponse>();
                else
                    Snackbar.Add(result.Message, Severity.Error);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Erro ao carregar os usuários: {ex.Message}", Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region Methods
        // Filtro de usuários baseado no SearchTerm
        public Func<UserResponse, bool> Filter => user =>
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;
            /*
            if (user.UserName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;
            if (user.Email.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;
            */
            if (!string.IsNullOrEmpty(user.UserName) &&
               user.UserName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            if (!string.IsNullOrEmpty(user.Email) &&
                user.Email.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        };

        // Excluir usuário
        public async void OnDeleteButtonClickedAsync(long id, string userName)
        {
            var result = await DialogService.ShowMessageBox(
                "ATENÇÃO",
                $"Você está prestes a excluir o usuário {userName}. Deseja continuar?",
                yesText: "Excluir",
                cancelText: "Cancelar");

            if (result is true)
                await OnDeleteAsync(id, userName);
        }

        // Método que executa a exclusão do usuário
        public async Task OnDeleteAsync(long id, string userName)
        {
            try
            {
                var request = new DeleteUserRequest { Id = id };
                var result = await Handler.DeleteUserAsync(request, CancellationToken.None);

                if (result.IsSuccess)
                {
                    Users.RemoveAll(x => x.Id == id);
                    Snackbar.Add($"Usuário {userName} excluído com sucesso", Severity.Success);
                }
                else
                {
                    Snackbar.Add(result.Message, Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Erro ao excluir o usuário: {ex.Message}", Severity.Error);
            }
        }
        #endregion
    
    }
}
