using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace MaterialMvvmSample.Utilities.Dialogs
{
    public class JobDialogService : BaseDialogService, IJobDialogService
    {
        public Task<string> AddNewJob()
        {
            return this.DialogFacade
                   .InputAsync(title: "Add a new job",
                    message: "Enter the job title:",
                    inputPlaceholder: "Job Title",
                    confirmingText: "Add");
        }

        public Task AlertExistingJob(string jobTitle)
        {
            return this.Alert($"{jobTitle} already exists.");
        }

        public Task<bool> DeleteJob(string jobTitle)
        {
            return this.DialogFacade
                   .ConfirmAsync(title: "Confirm Delete",
                   message: $"Are you sure do you want to delete {jobTitle}?",
                   confirmingText: "Delete");
        }

        public Task<string> EditJob(string jobTitle)
        {
            return this.DialogFacade
                   .InputAsync(title: "Edit job",
                    message: "Enter new job title:",
                    inputText: jobTitle,
                    inputPlaceholder: "Job Title",
                    confirmingText: "Change");
        }

        public Task JobDeleted()
        {
            return this.DialogFacade
                   .SnackbarAsync("Job deleted", MaterialSnackbar.DurationShort);
        }
    }
}