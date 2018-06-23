using Xamarin.Forms;

namespace XF.Material.Views
{
    public class MaterialCard : Frame, IMaterialView
    {
        public static readonly BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(int), typeof(int), 0);

        public int Elevation
        {
            get => (int)GetValue(ElevationProperty);
            set => SetValue(ElevationProperty, value);
        }

        //public int Elevation { get; set; }
    }
}
