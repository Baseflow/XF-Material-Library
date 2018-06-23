using Xamarin.Forms;
using System.Linq;
using XF.Material.Resources;

namespace XF.Material.Views
{
    public class MaterialButton : Button
    {
       // public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(Color), defaultValue: GetDefaultBackgroundColor());

        public static int DefaultCornerRadius = 4;

        /// <summary>
        /// The color of this button's background. By default, this will be based on the <see cref="XF.Material.Resources.MaterialColor.Secondary"/>.
        /// </summary>
        //public new Color BackgroundColor
        //{
        //    get => (Color)GetValue(BackgroundColorProperty);
        //    set => SetValue(BackgroundColorProperty, value);
        //}

        public MaterialButton()
        {
            this.MinimumHeightRequest = 36;
            this.HeightRequest = 36;
            this.MinimumWidthRequest = 64;
            this.CornerRadius = DefaultCornerRadius;
            this.SetDynamicResource(BackgroundColorProperty, MaterialConstants.MATERIAL_COLOR_SECONDARY);
            this.SetDynamicResource(TextColorProperty, MaterialConstants.MATERIAL_COLOR_ONSECONDARY);
            this.SetDynamicResource(FontFamilyProperty, MaterialConstants.MATERIAL_FONTFAMILY_MEDIUM);
        }

        private static Color GetDefaultBackgroundColor()
        {
            var res = Application.Current.Resources;

            res.TryGetValue("Material.Color.Secondary", out object secondaryResult);

            if (((Color)secondaryResult).IsDefault)
            {
                res.TryGetValue("Material.Color.Primary", out object primaryResult);

                if (((Color)primaryResult).IsDefault)
                {
                    return Color.Accent;
                }

                else if (primaryResult is Color primaryColor)
                {
                    return primaryColor;
                }

                return Color.Accent;
            }

            else if (secondaryResult is Color secondaryColor)
            {
                return secondaryColor;
            }

            return Color.Accent;
        }
    }
}
