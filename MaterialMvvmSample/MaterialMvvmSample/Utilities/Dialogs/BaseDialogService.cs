using System.Threading.Tasks;
using XF.Material.Forms.Ui.Dialogs;

namespace MaterialMvvmSample.Utilities.Dialogs
{
    public abstract class BaseDialogService
    {
        public IMaterialDialog DialogFacade => MaterialDialog.Instance;

        protected Task Alert(string message)
        {
            return this.DialogFacade.AlertAsync(message);
        }
    }
}
