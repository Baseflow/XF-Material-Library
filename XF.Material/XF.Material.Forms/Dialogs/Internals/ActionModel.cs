using Xamarin.Forms;

namespace XF.Material.Forms.Dialogs.Internals
{
    /// <summary>
    /// Used as the ItemSource type of MaterialSimpleDialog's list.
    /// </summary>
    internal class ActionModel
    {
        public int Index { get; set; }

        public bool IsSelected { get; set; }

        public string Image { get; set; }

        public string Text { get; set; }

        public string FontFamily { get; set; }

        public Color TextColor { get; set; }

        public Command<int> SelectedCommand { get; set; }
    }
}
