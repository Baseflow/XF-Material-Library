using Xamarin.Forms;

namespace XF.Material.Forms.Views.Internals
{
    /// <summary>
    /// Used for rendering the <see cref="Xamarin.Forms.ListView"/> control in <see cref="XF.Material.Forms.Dialogs.MaterialSimpleDialog"/>.
    /// </summary>
    public sealed class MaterialDialogListView : ListView
    {
        public Command<int> ItemSelectedCommand { get; set; }

        internal MaterialDialogListView() { }
    }
}
