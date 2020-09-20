using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XF.Material.Forms.Resources;

namespace XF.Material.Forms.UI
{
    /// <summary>
    /// A control that allow users to take actions, and make choices, with a single tap.
    /// </summary>
    public class MaterialButton : Button, IMaterialButtonControl
    {
        public const string MaterialButtonColorChanged = "BackgroundColorChanged";

        private static readonly Color OutlinedBorderColor = Color.FromHex("#1E000000");
        private readonly string[] _colorPropertyNames = { nameof(BackgroundColor), nameof(PressedBackgroundColor), nameof(DisabledBackgroundColor) };

        public static readonly BindableProperty AllCapsProperty = BindableProperty.Create(nameof(AllCaps), typeof(bool), typeof(MaterialButton), true);
        public static readonly BindableProperty ButtonTypeProperty = BindableProperty.Create(nameof(ButtonType), typeof(MaterialButtonType), typeof(MaterialButton), MaterialButtonType.Elevated);
        public static readonly BindableProperty DisabledBackgroundColorProperty = BindableProperty.Create(nameof(DisabledBackgroundColor), typeof(Color), typeof(MaterialButton), default(Color));
        public static readonly BindableProperty PressedBackgroundColorProperty = BindableProperty.Create(nameof(PressedBackgroundColor), typeof(Color), typeof(MaterialButton), default(Color));
        public static readonly BindableProperty LetterSpacingProperty = BindableProperty.Create(nameof(LetterSpacing), typeof(double), typeof(MaterialButton), 1.25);
        public static readonly BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(MaterialElevation), typeof(MaterialButton), new MaterialElevation(2, 8));

        public MaterialButton()
        {
            SetDynamicResource(FontFamilyProperty, MaterialConstants.FontFamily.BUTTON);
            SetDynamicResource(FontSizeProperty, MaterialConstants.MATERIAL_FONTSIZE_BUTTON);
            SetDynamicResource(FontAttributesProperty, MaterialConstants.MATERIAL_FONTATTRIBUTE_BOLD);
            SetDynamicResource(CornerRadiusProperty, MaterialConstants.MATERIAL_BUTTON_CORNERRADIUS);
            SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.SECONDARY);
            SetDynamicResource(TextColorProperty, MaterialConstants.Color.ON_SECONDARY);
            SetDynamicResource(HeightRequestProperty, MaterialConstants.MATERIAL_BUTTON_HEIGHT);
        }

        #region Dependency properties
        public MaterialElevation Elevation
        {
            get => (MaterialElevation)GetValue(ElevationProperty);
            set => SetValue(ElevationProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the text of this button should be capitalized. The default value is true.
        /// </summary>
        public bool AllCaps
        {
            get => (bool)GetValue(AllCapsProperty);
            set => SetValue(AllCapsProperty, value);
        }

        /// <summary>
        /// Gets or sets the letter spacing of this button's text.
        /// </summary>
        public double LetterSpacing
        {
            get => (double)GetValue(LetterSpacingProperty);
            set => SetValue(LetterSpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets the type of this button. The default value is <see cref="MaterialButtonType.Elevated"/>
        /// </summary>
        public virtual MaterialButtonType ButtonType
        {
            get => (MaterialButtonType)GetValue(ButtonTypeProperty);
            set => SetValue(ButtonTypeProperty, value);
        }

        public Color DisabledBackgroundColor
        {
            get => (Color)GetValue(DisabledBackgroundColorProperty);
            set => SetValue(DisabledBackgroundColorProperty, value);
        }

        public Color PressedBackgroundColor
        {
            get => (Color)GetValue(PressedBackgroundColorProperty);
            set => SetValue(PressedBackgroundColorProperty, value);
        }
        #endregion

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (_colorPropertyNames.Contains(propertyName))
            {
                base.OnPropertyChanged(MaterialButtonColorChanged);
            }
            else
            {
                base.OnPropertyChanged(propertyName);

                switch (propertyName)
                {
                    case nameof(ButtonType):
                        ButtonTypeChanged(ButtonType);
                        break;
                    case nameof(Style):
                        SetStyleValues(Style);
                        break;
                }
            }
        }

        private void SetStyleValues(Style style)
        {
            style?.Setters.ForEach(s =>
            {
                if (s.Value is DynamicResource d)
                {
                    SetDynamicResource(s.Property, d.Key);
                }
                else
                {
                    SetValue(s.Property, s.Value);
                }
            });
        }

        private void ButtonTypeChanged(MaterialButtonType buttonType)
        {
            if (buttonType == MaterialButtonType.Outlined && BorderColor.IsDefault)
            {
                BorderColor = OutlinedBorderColor;
            }

            if (buttonType == MaterialButtonType.Outlined && BorderWidth == (double)BorderWidthProperty.DefaultValue)
            {
                BorderWidth = 1;
            }

            if (buttonType == MaterialButtonType.Text || buttonType == MaterialButtonType.Outlined)
            {
                RemoveDynamicResource(TextColorProperty);
                SetDynamicResource(TextColorProperty, MaterialConstants.Color.SECONDARY);
            }
        }
    }
}
