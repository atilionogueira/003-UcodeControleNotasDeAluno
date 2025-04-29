using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Security.Cryptography;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Course;

namespace Ucode.Web.Pages.Courses
{
    public partial class CreateCoursePage : ComponentBase
    {
        #region Properties
        public bool isBusy { get; set; } = false;
        public CreateCourseRequest InputModel { get; set; } = new();
        #endregion
        #region Services
        [Inject]
        public ICourseHandler Handler { get; set; } = null!;
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
                var result = await Handler.CreateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add(result.Message, Severity.Error);
                    NavigationManager.NavigateTo("/courses");
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
                isBusy = false;
            }
        }
        #endregion
    }
}
