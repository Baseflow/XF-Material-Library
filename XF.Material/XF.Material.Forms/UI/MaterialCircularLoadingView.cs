using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XF.Material.Forms.Resources;

namespace XF.Material.Forms.UI
{
    public class MaterialCircularLoadingView : Lottie.Forms.AnimationView
    {
        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialCircularLoadingView), Material.Color.Secondary);

        /// <summary>
        /// Gets or sets the tint color of the animation. The default value is the value of <see cref="MaterialColorConfiguration.Secondary"/>.
        /// </summary>
        public Color TintColor
        {
            get => (Color) this.GetValue(TintColorProperty);
            set => this.SetValue(TintColorProperty, value);
        }

        public MaterialCircularLoadingView()
        {
            this.SetDynamicResource(TintColorProperty, MaterialConstants.Color.SECONDARY);
            this.Loop = true;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(this.Animation))
            {
                return;
            }

            base.OnPropertyChanged(propertyName);
        }
    }
}
