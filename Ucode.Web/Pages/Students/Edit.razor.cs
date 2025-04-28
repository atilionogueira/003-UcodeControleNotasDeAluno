using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Students;

namespace Ucode.Web.Pages.Students
{
    public partial class EditStudentPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public UpdateStudentRequest InputModel { get; set; } = new();

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
        public IStudentHandler Handler { get; set; } = null!;

        #endregion

        #region Overrides
        protected override async Task OnInitializedAsync()
        {
            GetStudentByIdRequest? request = null;
            try
            {
                request = new GetStudentByIdRequest
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
                    InputModel = new UpdateStudentRequest
                    {
                        Id = response.Data.Id,
                        Name = response.Data.Name,
                        BirthDate = response.Data.BirthDate,
                        Email = response.Data.Email,
                        Gender = response.Data.Gender,
                        //UpdatedAt = DateTime.UtcNow,
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
                    Snackbar.Add("Estudante atualizado", Severity.Success);
                    NavigationManager.NavigateTo("/students");
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
