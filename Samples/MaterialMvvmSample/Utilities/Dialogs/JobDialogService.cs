using System.Threading.Tasks;
using XF.Material.Maui.UI.Dialogs;

namespace MaterialMvvmSample.Utilities.Dialogs
{
    public class JobDialogService : BaseDialogService, IJobDialogService
    {
        public Task<string> AddNewJob()
        {
            return DialogFacade
                   .InputAsync(title: "Add a new job",
                    message: "Enter the job title:",
                    inputPlaceholder: "Job Title",
                    confirmingText: "Add", configuration: new XF.Material.Maui.UI.Dialogs.Configurations.MaterialInputDialogConfiguration
                    {
                        BackgroundColor = Color.FromArgb("#2c3e50"),
                        InputTextColor = Colors.White,
                        TintColor = Colors.White,
                        TitleTextColor = Colors.White,
                        MessageTextColor = Colors.White,
                        InputPlaceholderColor = Colors.White.MultiplyAlpha((float)0.6)
                    });
        }

        public Task AlertExistingJob(string jobTitle)
        {
            return Alert($"{jobTitle} already exists.");
        }

        public Task<bool?> DeleteJob(string jobTitle)
        {
            return DialogFacade
                   .ConfirmAsync(title: "Confirm Delete",
                   message: $"Are you sure do you want to delete {jobTitle}?",
                   confirmingText: "Delete");
        }

        public Task<string> EditJob(string jobTitle)
        {
            return DialogFacade
                   .InputAsync(title: "Edit job",
                    message: "Enter new job title:",
                    inputText: jobTitle,
                    inputPlaceholder: "Job Title",
                    confirmingText: "Change");
        }

        public Task JobDeleted()
        {
            return DialogFacade
                   .SnackbarAsync("Job deleted", MaterialSnackbar.DurationShort);
        }
    }
}
