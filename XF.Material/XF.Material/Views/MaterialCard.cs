using Xamarin.Forms;
using XF.Material.Resources;

namespace XF.Material.Views
{
    public class MaterialCard : Frame, IMaterialView
    {
        public static readonly BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(int), typeof(MaterialCard), 1);

        public int Elevation
        {
            get => (int)GetValue(ElevationProperty);
            set => SetValue(ElevationProperty, value);
        }

        public MaterialCard()
        {
            this.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.SURFACE);
        }
    }
}
