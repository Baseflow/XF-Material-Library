using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XF.Material.Forms.Resources.Typography;

namespace XF.Material.Forms.UI
{
    public class MaterialLabel : Label
    {
        public const string MaterialLineHeightPropertyName = "MaterialLineHeight";

        public static readonly BindableProperty LetterSpacingProperty = BindableProperty.Create(nameof(LetterSpacing), typeof(double), typeof(MaterialLabel), 0.0);

        public new static readonly BindableProperty LineHeightProperty = BindableProperty.Create(MaterialLineHeightPropertyName, typeof(double), typeof(MaterialLabel), 1.4);

        public static readonly BindableProperty TypeScaleProperty = BindableProperty.Create(nameof(TypeScale), typeof(MaterialTypeScale), typeof(MaterialLabel), MaterialTypeScale.None);

        private bool _fontFamilyChanged;
        private bool _fontSizeChanged;
        private bool _letterSpacingChanged;
        private bool _fontAttributeChanged;

        /// <summary>
        /// Gets or sets the letter spacing of this label's text.
        /// </summary>
        public double LetterSpacing
        {
            get => (double)this.GetValue(LetterSpacingProperty);
            set => this.SetValue(LetterSpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets the multiplier that determines the line height of this label.
        /// </summary>
        public new double LineHeight
        {
            get => (double)this.GetValue(LineHeightProperty);
            set => this.SetValue(LineHeightProperty, value);
        }

        /// <summary>
        /// Gets or sets the type scale used for this label.
        /// </summary>
        public MaterialTypeScale TypeScale
        {
            get => (MaterialTypeScale)this.GetValue(TypeScaleProperty);
            set => this.SetValue(TypeScaleProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(this.TypeScale):
                    this.OnTypeScaleChanged(this.TypeScale);
                    break;
                case nameof(this.FontSize):
                    _fontSizeChanged = true;
                    break;
                case nameof(this.FontFamily):
                    _fontFamilyChanged = true;
                    break;
                case nameof(this.LetterSpacing):
                    _letterSpacingChanged = true;
                    break;
                case nameof(this.FontAttributes):
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
                this.LetterSpacing = Convert.ToDouble(Application.Current.Resources[letterSpacingKey]);
            }
            if (!_fontFamilyChanged)
            {
                var fontFamilyKey = $"Material.FontFamily.{materialTypeScale.ToString()}";
                this.FontFamily = Application.Current.Resources[fontFamilyKey]?.ToString();
            }
            if (!_fontSizeChanged)
            {
                var fontSizeKey = $"Material.FontSize.{materialTypeScale.ToString()}";
                this.FontSize = Convert.ToDouble(Application.Current.Resources[fontSizeKey]);
            }

            if (_fontAttributeChanged) return;
            switch (materialTypeScale)
            {
                case MaterialTypeScale.H6:
                case MaterialTypeScale.Subtitle2:
                case MaterialTypeScale.Button:
                    this.FontAttributes = FontAttributes.Bold;
                    break;
            }
        }
    }
}