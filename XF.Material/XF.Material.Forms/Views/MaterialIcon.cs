using Xamarin.Forms;

namespace XF.Material.Forms.Views
{
    /// <summary>
    /// A view that shows an image icon that can be tinted.
    /// </summary>
    public class MaterialIcon : Image
    {
        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialIcon), Color.Default);

        /// <summary>
        /// Gets or sets the tint color of the image icon.
        /// </summary>
        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }
    }
}
