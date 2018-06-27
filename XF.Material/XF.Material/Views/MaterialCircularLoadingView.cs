using Xamarin.Forms;
using XF.Material.Resources;

namespace XF.Material.Views
{
    public class MaterialCircularLoadingView : Lottie.Forms.AnimationView
    {
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(MaterialCircularLoadingView), Color.Accent);

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public MaterialCircularLoadingView()
        {
            this.SetDynamicResource(ColorProperty, MaterialConstants.MATERIAL_COLOR_SECONDARY);
        }
    }
}
