using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XF.Material.Effects;
using XF.Material.Resources;
using XF.Material.Resources.Typography;

namespace XF.Material.Views
{
    public class MaterialButton : Button
    {
        public static readonly BindableProperty AllCapsProperty = BindableProperty.Create(nameof(AllCaps), typeof(bool), typeof(MaterialButton), true);
        public static readonly BindableProperty ButtonTypeProperty = BindableProperty.Create(nameof(ButtonType), typeof(MaterialButtonType), typeof(MaterialButton), MaterialButtonType.Elevated);

        /// <summary>
        /// Gets or sets whether the text of this button should be capitalized.
        /// </summary>
        public bool AllCaps
        {
            get => (bool)GetValue(AllCapsProperty);
            set => SetValue(AllCapsProperty, value);
        }

        /// <summary>
        /// Gets or sets the type of this button.
        /// </summary>
        public MaterialButtonType ButtonType
        {
            get => (MaterialButtonType)GetValue(ButtonTypeProperty);
            set => SetValue(ButtonTypeProperty, value);
        }

        public MaterialButton()
        {
            this.SetValue(MaterialEffectsUtil.LetterSpacingProperty, MaterialTypeScale.Button);
            this.SetDynamicResource(FontFamilyProperty, MaterialConstants.MATERIAL_FONTFAMILY_MEDIUM);
            this.SetDynamicResource(FontSizeProperty, MaterialConstants.MATERIAL_FONTSIZE_BUTTON);
            this.SetDynamicResource(CornerRadiusProperty, MaterialConstants.MATERIAL_BUTTON_CORNERRADIUS);
            this.SetDynamicResource(BackgroundColorProperty, MaterialConstants.MATERIAL_COLOR_SECONDARY);
            this.SetDynamicResource(TextColorProperty, MaterialConstants.MATERIAL_COLOR_ONSECONDARY);
            this.SetDynamicResource(HeightRequestProperty, MaterialConstants.MATERIAL_BUTTON_HEIGHT);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.Style))
            {
                this.Style.Setters.ForEach(s => this.SetValue(s.Property, s.Value));
            }
        }

        public enum MaterialButtonType
        {
            Elevated,
            Flat,
            Outlined,
            Text
        }
    }
}
