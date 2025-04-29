using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Course;

namespace Ucode.Web.Pages.Courses
{
    public partial class ListCoursePage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public List<Course> Courses { get; set; } = [];
        public string SearchTerm { get; set; } = string.Empty;

        #endregion

        #region Services
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public IDialogService DialogService { get; set; } = null!;
        [Inject]
        public ICourseHandler Handler { get; set; } = null!;

        #endregion

        #region Override

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetAllCourseRequest();
                var result = await Handler.GetAllAsync(request);
                if (result.IsSuccess)
                    Courses = result.Data ?? [];

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

        public async void OnDeleteButtonClickedAsync(long id, string name)
        {
            var result = await DialogService.ShowMessageBox(
                "ATENÇÃO",
                $"Ao prosseguir o lançamento {name} será excluído. Esta ação é irreversível! Deseja continuar?",
                yesText: "EXCLUIR",
                cancelText: "Cancelar");

            if (result is true)
                await OnDeleteAsync(id, name);
            StateHasChanged();
        }

        public async Task OnDeleteAsync(long id, string name)
        {
            try
            {
                var request = new DeleteCourseRequest { Id = id };
                await Handler.DeleteAsync(request);
                Courses.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Curso {name} excluída", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        public Func<Course, bool> Filter => course =>
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;
            if (course.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;
            if (course.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;

        };
        #endregion
    }
}
