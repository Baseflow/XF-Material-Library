using Xamarin.Forms;
using static XF.Material.Forms.UI.MaterialButton;

namespace XF.Material.Forms.UI
{
    public interface IMaterialButton
    {
        MaterialColor BackgroundColor { get; set; }

        double BorderWidth { get; set; }

        int CornerRadius { get; set; }

        Color BorderColor { get; set; }

        MaterialButtonType ButtonType { get; set; }
    }
}
