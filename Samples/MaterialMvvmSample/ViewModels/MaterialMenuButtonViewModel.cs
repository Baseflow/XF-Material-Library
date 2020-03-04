using XF.Material.Forms.Models;

namespace MaterialMvvmSample.ViewModels
{
    public class MaterialMenuButtonViewModel : BaseViewModel
    {
        public MaterialMenuItem[] Actions => new MaterialMenuItem[]
        {
            new MaterialMenuItem
            {
                Text = "Edit"
            },
            new MaterialMenuItem
            {
                Text = "Delete"
            }
        };
    }
}
