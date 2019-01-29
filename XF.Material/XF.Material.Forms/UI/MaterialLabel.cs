using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XF.Material.Forms.Resources.Typography;

namespace XF.Material.Forms.UI
{
    public class MaterialLabel : Label
    {
        public const string MaterialLineHeight = "MaterialLineHeight";

        public static new readonly BindableProperty LineHeightProperty = BindableProperty.Create(MaterialLineHeight, typeof(double), typeof(MaterialLabel), 1.4);

        public static readonly BindableProperty LetterSpacingProperty = BindableProperty.Create(nameof(LetterSpacing), typeof(double), typeof(MaterialLabel), 0.0);

        public static readonly BindableProperty TypeScaleProperty = BindableProperty.Create(nameof(TypeScale), typeof(MaterialTypeScale), typeof(MaterialLabel), MaterialTypeScale.None);

        private bool _fontSizeChanged;
        private bool _fontFamilyChanged;
        private bool _letterSpacingChanged;

        public double LetterSpacing
        {
            get => (double)this.GetValue(LetterSpacingProperty);
            set => this.SetValue(LetterSpacingProperty, value);
        }

        public new double LineHeight
        {
            get => (double)this.GetValue(LineHeightProperty);
            set => this.SetValue(LineHeightProperty, value);
        }

        public MaterialTypeScale TypeScale
        {
            get => (MaterialTypeScale) this.GetValue(TypeScaleProperty);
            set => this.SetValue(TypeScaleProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if(propertyName == nameof(this.TypeScale))
            {
                this.OnTypeScaleChanged(this.TypeScale);
            }

            if(propertyName == nameof(this.FontSize))
            {
                _fontSizeChanged = true;
            }

            if (propertyName == nameof(this.FontFamily))
            {
                _fontFamilyChanged = true;
            }

            if (propertyName == nameof(this.LetterSpacing))
            {
                _letterSpacingChanged = true;
            }
        }

        protected void OnTypeScaleChanged(MaterialTypeScale materialTypeScale)
        {
            if(materialTypeScale == MaterialTypeScale.None)
            {
                return;
            }

            var letterSpacingKey = $"Material.LetterSpacing.{materialTypeScale.ToString()}";
            
            if (!_fontSizeChanged)
            {
                this.LetterSpacing = Convert.ToDouble(Application.Current.Resources[letterSpacingKey]);
            }

            var fontFamilyKey = $"Material.FontFamily.{materialTypeScale.ToString()}";

            if (!_fontSizeChanged)
            {
                this.FontFamily = Application.Current.Resources[fontFamilyKey].ToString();
            }

            var fontSizeKey = $"Material.FontSize.{materialTypeScale.ToString()}";

            if(!_fontSizeChanged)
            {
                this.FontSize = Convert.ToDouble(Application.Current.Resources[fontSizeKey]); ;
            }
        }
    }
}
