using Xamarin.Forms;

namespace XF.Material.Forms.Resources.Typography
{
    /// <summary>
    /// App typography theme configuration based on https://material.io/design/typography.
    /// </summary>
    public sealed class MaterialFontConfiguration : BindableObject
    {
        public static readonly BindableProperty Body1Property = BindableProperty.Create(nameof(Body1), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty Body2Property = BindableProperty.Create(nameof(Body2), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty ButtonProperty = BindableProperty.Create(nameof(Button), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty CaptionProperty = BindableProperty.Create(nameof(Caption), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty H1Property = BindableProperty.Create(nameof(H1), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty H2Property = BindableProperty.Create(nameof(H2), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty H3Property = BindableProperty.Create(nameof(H3), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty H4Property = BindableProperty.Create(nameof(H4), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty H5Property = BindableProperty.Create(nameof(H5), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty H6Property = BindableProperty.Create(nameof(H6), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty OverlineProperty = BindableProperty.Create(nameof(Overline), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty Subtitle1Property = BindableProperty.Create(nameof(Subtitle1), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty Subtitle2Property = BindableProperty.Create(nameof(Subtitle2), typeof(string), typeof(MaterialFontConfiguration));
      
        /// <summary>
        /// Body 1 font family, used for long-form writing and small text sizes.
        /// </summary>
        public string Body1
        {
            get => this.GetValue(Body1Property)?.ToString();
            set => this.SetValue(Body1Property, value);
        }

        /// <summary>
        /// Body 2 font family, used for long-form writing and small text sizes.
        /// </summary>
        public string Body2
        {
            get => this.GetValue(H1Property)?.ToString();
            set => this.SetValue(H1Property, value);
        }

        /// <summary>
        /// Button font family, used by different types of buttons.
        /// </summary>
        public string Button
        {
            get => this.GetValue(ButtonProperty)?.ToString();
            set => this.SetValue(ButtonProperty, value);
        }

        /// <summary>
        /// Caption font family, used for annotations or to introduce a headline text.
        /// </summary>
        public string Caption
        {
            get => this.GetValue(CaptionProperty)?.ToString();
            set => this.SetValue(CaptionProperty, value);
        }

        /// <summary>
        /// Headline 1 font family, used by large text on the screen.
        /// </summary>
        public string H1
        {
            get => this.GetValue(H1Property)?.ToString();
            set => this.SetValue(H1Property, value);
        }

        /// <summary>
        /// Headline 2 font family, used by large text on the screen.
        /// </summary>
        public string H2
        {
            get => this.GetValue(H2Property)?.ToString();
            set => this.SetValue(H2Property, value);
        }

        /// <summary>
        /// Headline 3 font family, used by large text on the screen.
        /// </summary>
        public string H3
        {
            get => this.GetValue(H3Property)?.ToString();
            set => this.SetValue(H3Property, value);
        }

        /// <summary>
        /// Headline 4 font family, used by large text on the screen.
        /// </summary>
        public string H4
        {
            get => this.GetValue(H4Property)?.ToString();
            set => this.SetValue(H4Property, value);
        }

        /// <summary>
        /// Headline 5 font family, used by large text on the screen.
        /// </summary>
        public string H5
        {
            get => this.GetValue(H5Property)?.ToString();
            set => this.SetValue(H5Property, value);
        }

        /// <summary>
        /// Headline 6 font family, used by large text on the screen.
        /// </summary>
        public string H6
        {
            get => this.GetValue(H6Property)?.ToString();
            set => this.SetValue(H6Property, value);
        }

        /// <summary>
        /// Overline font family, used for annotations or to introduce a headline text.
        /// </summary>
        public string Overline
        {
            get => this.GetValue(OverlineProperty)?.ToString();
            set => this.SetValue(OverlineProperty, value);
        }

        /// <summary>
        /// Subtitle 1 font family, used by medium-emphasis text.
        /// </summary>
        public string Subtitle1
        {
            get => this.GetValue(Subtitle1Property)?.ToString();
            set => this.SetValue(Subtitle1Property, value);
        }

        /// <summary>
        /// Subtitle 2 font family, used by medium-emphasis text.
        /// </summary>
        public string Subtitle2
        {
            get => this.GetValue(Subtitle2Property)?.ToString();
            set => this.SetValue(Subtitle2Property, value);
        }
    }
}
