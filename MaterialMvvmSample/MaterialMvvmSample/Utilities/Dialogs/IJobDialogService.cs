using System.Threading.Tasks;

namespace MaterialMvvmSample.Utilities.Dialogs
{
    public interface IJobDialogService
    {
        Task<string> AddNewJob();

        Task<string> EditJob(string jobTitle);

        Task<bool?> DeleteJob(string jobTitle);

        Task AlertExistingJob(string jobTitle);

        Task JobDeleted();
    }
}
