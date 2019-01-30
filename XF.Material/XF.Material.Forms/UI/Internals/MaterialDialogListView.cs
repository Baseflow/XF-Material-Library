using Xamarin.Forms;

namespace XF.Material.Forms.UI.Internals
{
    /// <summary>
    /// Used for rendering the <see cref="ListView"/> control in <see cref="Dialogs.MaterialSimpleDialog"/>.
    /// </summary>
    public sealed class MaterialDialogListView : ListView
    {
        internal static readonly BindableProperty ShouldShowScrollbarProperty = BindableProperty.Create(nameof(ShouldShowScrollbar), typeof(bool), typeof(MaterialDialogListView), false);

        public Command<int> ItemSelectedCommand { get; set; }

        public bool HasRipple { get; set; } = true;

        public bool ShouldShowScrollbar
        {
            get => (bool)this.GetValue(ShouldShowScrollbarProperty);
            set => this.SetValue(ShouldShowScrollbarProperty, value);
        }

        internal MaterialDialogListView() { }
    }
}
