using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XF.Material.Forms.Resources.Typography;

namespace XF.Material.Forms.UI
{
    public class MaterialLabel : Label
    {
        public const string MaterialLineHeightPropertyName = "MaterialLineHeight";

        public static readonly BindableProperty LetterSpacingProperty = BindableProperty.Create(nameof(LetterSpacing), typeof(double), typeof(MaterialLabel), 0.0);

        public static readonly BindableProperty TypeScaleProperty = BindableProperty.Create(nameof(TypeScale), typeof(MaterialTypeScale), typeof(MaterialLabel), MaterialTypeScale.None);

        private bool _fontFamilyChanged;
        private bool _fontSizeChanged;
        private bool _letterSpacingChanged;
        private bool _fontAttributeChanged;

        public MaterialLabel()
        {
            SetDynamicResource(LineHeightProperty, "Material.LineHeight");
        }

        /// <summary>
        /// Gets or sets the letter spacing of this label's text.
        /// </summary>
        public double LetterSpacing
        {
            get => (double)GetValue(LetterSpacingProperty);
            set => SetValue(LetterSpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets the type scale used for this label.
        /// </summary>
        public MaterialTypeScale TypeScale
        {
            get => (MaterialTypeScale)GetValue(TypeScaleProperty);
            set => SetValue(TypeScaleProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(TypeScale):
                    OnTypeScaleChanged(TypeScale);
                    break;
                case nameof(FontSize):
                    _fontSizeChanged = true;
                    break;
                case nameof(FontFamily):
                    _fontFamilyChanged = true;
                    break;
                case nameof(LetterSpacing):
                    _letterSpacingChanged = true;
                    break;
                case nameof(FontAttributes):
                    _fontAttributeChanged = true;
                    break;
            }
        }

        protected virtual void OnTypeScaleChanged(MaterialTypeScale materialTypeScale)
        {
            if (materialTypeScale == MaterialTypeScale.None)
            {
                return;
            }
            if (!_letterSpacingChanged)
            {
                var letterSpacingKey = $"Material.LetterSpacing.{materialTypeScale.ToString()}";
                LetterSpacing = Material.GetResource<double>(letterSpacingKey);
            }
            if (!_fontFamilyChanged)
            {
                var fontFamilyKey = $"Material.FontFamily.{materialTypeScale.ToString()}";
                FontFamily = Material.GetResource<string>(fontFamilyKey);
            }
            if (!_fontSizeChanged)
            {
                var fontSizeKey = $"Material.FontSize.{materialTypeScale.ToString()}";
                FontSize = Material.GetResource<double>(fontSizeKey);
            }

            if (_fontAttributeChanged)
            {
                return;
            }

            switch (materialTypeScale)
            {
                case MaterialTypeScale.H6:
                case MaterialTypeScale.Subtitle2:
                case MaterialTypeScale.Button:
                    FontAttributes = FontAttributes.Bold;
                    break;
            }
        }
    }
}
