using System.Threading.Tasks;
using XF.Material.Maui.UI.Dialogs;

namespace MaterialMvvmSample.Utilities.Dialogs
{
    public abstract class BaseDialogService
    {
        public static IMaterialDialog DialogFacade => MaterialDialog.Instance;

        protected static Task Alert(string message)
        {
            return DialogFacade.AlertAsync(message);
        }
    }
}
