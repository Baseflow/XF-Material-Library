using Xamarin.Forms;

namespace XF.Material.Views
{
    public class MaterialCard : Frame, IMaterialView
    {
        public static readonly BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(int), typeof(MaterialCard), 0);

        public int Elevation
        {
            get => (int)GetValue(ElevationProperty);
            set => SetValue(ElevationProperty, value);
        }
    }
}
