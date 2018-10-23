using System;
using Xamarin.Forms;

namespace XF.Material.Forms.Views.Internals
{
    /// <summary>
    /// Used for rendering the <see cref="ListView"/> control in <see cref="Dialogs.MaterialSimpleDialog"/>.
    /// </summary>
    public sealed class MaterialDialogListView : ListView
    {
        public Command<int> ItemSelectedCommand { get; set; }

        public bool HasRipple { get; set; } = true;

        internal MaterialDialogListView() { }
    }
}
