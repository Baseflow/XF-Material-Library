using Xamarin.Forms;

namespace XF.Material.Forms.UI
{
    public interface IMaterialButtonControl
    {
        Color BackgroundColor { get; set; }

        Color BorderColor { get; set; }

        double BorderWidth { get; set; }

        MaterialButtonType ButtonType { get; set; }

        int CornerRadius { get; set; }

        Color DisabledBackgroundColor { get; set; }

        Color PressedBackgroundColor { get; set; }
    }
}