using Microsoft.Maui;

namespace XF.Material.Maui.UI
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

        MaterialElevation Elevation { get; set; }
    }
}