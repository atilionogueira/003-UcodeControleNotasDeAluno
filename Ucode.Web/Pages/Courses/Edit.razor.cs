using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Course;
using Ucode.Core.Requests.Students;

namespace Ucode.Web.Pages.Courses
{
    public partial class EditCoursePage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public UpdateCourseRequest InputModel { get; set; } = new();

        #endregion

        #region Parameters

        [Parameter]
        public string Id { get; set; } = string.Empty;
        #endregion

        #region Services
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ICourseHandler Handler { get; set; } = null!;

        #endregion

        #region Overrides
        protected override async Task OnInitializedAsync()
        {
            GetCourseByRequest? request = null;
            try
            {
                request = new GetCourseByRequest
                {
                    Id = long.Parse(Id)
                };
            }
            catch
            {
                Snackbar.Add("Parâmetro inválido.", Severity.Error);
                return;
            }

            if (request is null)
                return;

            IsBusy = true;
            try
            {
                await Task.Delay(3000);
                var response = await Handler.GetByIdAsync(request);

                if (response.IsSuccess && response.Data is not null)
                    InputModel = new UpdateCourseRequest
                    {
                        Id = response.Data.Id,
                        Name = response.Data.Name,
                        Description = response.Data.Description,
                        DurationInHours = response.Data.DurationInHours                     
                    };
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

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await Handler.UpdateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add("Curso atualizado", Severity.Success);
                    NavigationManager.NavigateTo("/courses");
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

