using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Students;

namespace Ucode.Web.Pages.Students
{
    public partial class ListaStudentPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public List<Student> Students { get; set; } = [];

        public string SearchTerm { get; set; } = string.Empty;
        #endregion

        #region Services
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public IDialogService DialogService { get; set; } = null!;  // Modal 
        [Inject]
        public IStudentHandler Handler { get; set; } = null!;

        #endregion

        #region overrides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetAllStudentRequest();
                var result = await Handler.GetAllAsync(request);
                if (result.IsSuccess)
                    Students = result.Data ?? [];
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

        #region Methods

        public async void OnDeleteButtonClickeAsync(long id, string name)
        {
            var result = await DialogService.ShowMessageBox(
                "Atenção", $"Ao prosseguir o(a) estudante {name} será excluída. Esta é um ação irreversível! Deseja continuar? ",
                yesText: "Excluir",
                cancelText: "Cancelar");

            if (result is true)
                await OnDeleteAsync(id, name);
            StateHasChanged(); // o StateHasChange automaticamente atualiza sem precisar fazer select no banco.   
        }

        public async Task OnDeleteAsync(long id, string name)
        {
            try
            {
                var request = new DeleteStudentRequest { Id = id };
                await Handler.DeleteAsync(request);
                Students.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Estudante {name} excluida", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        public Func<Student, bool> Filter => student =>
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;
            if (student.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;
            if (student.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;
            if (student.BirthDate.HasValue &&
                student.BirthDate.Value.ToString("dd/MM/yyyy").Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;

        };

        #endregion
    }
}
