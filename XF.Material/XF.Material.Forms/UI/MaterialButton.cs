using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XF.Material.Forms.Effects;
using XF.Material.Forms.Resources;
using XF.Material.Forms.Resources.Typography;

namespace XF.Material.Forms.UI
{
    /// <summary>
    /// A control that allow users to take actions, and make choices, with a single tap.
    /// </summary>
    public class MaterialButton : Button, IMaterialButtonControl
    {
        public const string MaterialButtonColorChanged = "BackgroundColorChanged";

        private static Color OutlinedBorderColor = Color.FromHex("#1E000000");

        public static readonly BindableProperty AllCapsProperty = BindableProperty.Create(nameof(AllCaps), typeof(bool), typeof(MaterialButton), true);

        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialButton), Material.Color.Secondary);

        public static readonly BindableProperty ButtonTypeProperty = BindableProperty.Create(nameof(ButtonType), typeof(MaterialButtonType), typeof(MaterialButton), MaterialButtonType.Elevated);

        public static readonly BindableProperty DisabledBackgroundColorProperty = BindableProperty.Create(nameof(DisabledBackgroundColor), typeof(Color), typeof(MaterialButton), default(Color));

        public static readonly BindableProperty PressedBackgroundColorProperty = BindableProperty.Create(nameof(PressedBackgroundColor), typeof(Color), typeof(MaterialButton), default(Color));

        private readonly string[] _colorPropertyNames = new string[] { nameof(BackgroundColor), nameof(PressedBackgroundColor), nameof(DisabledBackgroundColor) };

        public MaterialButton()
        {
            this.SetValue(MaterialTypographyEffect.TypeScaleProperty, MaterialTypeScale.Button);
            this.SetDynamicResource(FontFamilyProperty, MaterialConstants.FontFamily.BUTTON);
            this.SetDynamicResource(FontSizeProperty, MaterialConstants.MATERIAL_FONTSIZE_BUTTON);
            this.SetDynamicResource(FontAttributesProperty, MaterialConstants.MATERIAL_FONTATTRIBUTE_BOLD);
            this.SetDynamicResource(CornerRadiusProperty, MaterialConstants.MATERIAL_BUTTON_CORNERRADIUS);
            this.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.SECONDARY);
            this.SetDynamicResource(TextColorProperty, MaterialConstants.Color.ON_SECONDARY);
            this.SetDynamicResource(HeightRequestProperty, MaterialConstants.MATERIAL_BUTTON_HEIGHT);
        }

        /// <summary>
        /// Gets or sets whether the text of this button should be capitalized. The default value is true.
        /// </summary>
        public bool AllCaps
        {
            get => (bool)this.GetValue(AllCapsProperty);
            set => this.SetValue(AllCapsProperty, value);
        }

        /// <summary>
        /// Gets or sets the background color. The default value is based on the Color value of <see cref="MaterialColorConfiguration.Secondary"/> if you are using a Material resource, otherwise the default value is <see cref="Color.Accent"/>
        /// </summary>
        public new Color BackgroundColor
        {
            get => (Color)this.GetValue(BackgroundColorProperty);
            set => this.SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the type of this button. The default value is <see cref="MaterialButtonType.Elevated"/>
        /// </summary>
        public virtual MaterialButtonType ButtonType
        {
            get => (MaterialButtonType)this.GetValue(ButtonTypeProperty);
            set => this.SetValue(ButtonTypeProperty, value);
        }

        public Color DisabledBackgroundColor
        {
            get => (Color)this.GetValue(DisabledBackgroundColorProperty);
            set => this.SetValue(DisabledBackgroundColorProperty, value);
        }

        public Color PressedBackgroundColor
        {
            get => (Color)this.GetValue(PressedBackgroundColorProperty);
            set => this.SetValue(PressedBackgroundColorProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (_colorPropertyNames.Contains(propertyName))
            {
                base.OnPropertyChanged(MaterialButtonColorChanged);
            }
            else
            {
                base.OnPropertyChanged(propertyName);

                if(propertyName == nameof(this.ButtonType))
                {
                    this.ButtonTypeChanged(this.ButtonType);
                }

                if (propertyName == nameof(this.Style))
                {
                    this.SetStyleValues(this.Style);
                }
            }
        }

        private void SetStyleValues(Style style)
        {
            style?.Setters.ForEach(s =>
            {
                if (s.Value is DynamicResource d)
                {
                    this.SetDynamicResource(s.Property, d.Key);
                }
                else
                {
                    this.SetValue(s.Property, s.Value);
                }
            });
        }

        private void ButtonTypeChanged(MaterialButtonType buttonType)
        {
            if (buttonType == MaterialButtonType.Outlined && this.BorderColor.IsDefault)
            {
                this.BorderColor = OutlinedBorderColor;
            }

            if (buttonType == MaterialButtonType.Outlined && this.BorderWidth == (double)BorderWidthProperty.DefaultValue)
            {
                this.BorderWidth = 1;
            }

            if (buttonType == MaterialButtonType.Text || buttonType == MaterialButtonType.Outlined)
            {
                this.RemoveDynamicResource(TextColorProperty);
                this.SetDynamicResource(TextColorProperty, MaterialConstants.Color.SECONDARY);
            }
        }
    }
}