using Xamarin.Forms;
using XF.Material.Forms.Resources;

namespace XF.Material.Forms.UI
{
    public class MaterialCircularLoadingView : Lottie.Forms.AnimationView
    {
        /// <summary>
        /// Default value for Width and Height if none are specified after object creation
        /// </summary>
        private const int DefaultSize = 30;
        private static LayoutOptions DefaultStretchBehavior = LayoutOptions.Center;

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialCircularLoadingView), Material.Color.Secondary);

        /// <summary>
        /// Gets or sets the tint color of the animation. The default value is the value of <see cref="MaterialColorConfiguration.Secondary"/>.
        /// </summary>
        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        public MaterialCircularLoadingView()
        {
            SetDynamicResource(TintColorProperty, MaterialConstants.Color.SECONDARY);
            RepeatMode = Lottie.Forms.RepeatMode.Infinite;

            HorizontalOptions = DefaultStretchBehavior;
            VerticalOptions = DefaultStretchBehavior;

            WidthRequest = DefaultSize;
            HeightRequest = DefaultSize;
        }
    }
}
