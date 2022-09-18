using System.Windows.Input;
using Microsoft.Maui;

namespace XF.Material.Maui.UI.Dialogs.Internals
{
    /// <summary>
    /// Used as the ItemSource type of MaterialSimpleDialog's list.
    /// </summary>
    internal class ActionModel : BindableObject
    {
        public static readonly BindableProperty ItemWidthProperty = BindableProperty.Create(nameof(ItemWidth), typeof(double), typeof(ActionModel), 0.0);

        public int Index { get; set; }

        public bool IsSelected { get; set; }

        public ImageSource Image { get; set; }

        public string Text { get; set; }

        public string FontFamily { get; set; }

        public Color TextColor { get; set; }

        public Command<int> SelectedCommand { get; set; }

        public double ItemWidth
        {
            get => (double)GetValue(ItemWidthProperty);
            set => SetValue(ItemWidthProperty, value);
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Used as the ItemSource type of MaterialMenuDialog's list.
    /// </summary>
    internal class MenuActionModel : ActionModel
    {
        public ICommand SizeChangeCommand { get; set; }
    }
}
