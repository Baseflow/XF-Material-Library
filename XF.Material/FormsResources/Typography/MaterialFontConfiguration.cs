using Microsoft.Maui;
using Font = Microsoft.Maui.Font;

namespace XF.Material.Maui.Resources.Typography
{
    /// <summary>
    /// Class that provides typography theme configuration based on https://material.io/design/typography.
    /// </summary>
    public sealed class MaterialFontConfiguration : BindableObject
    {
        /// <summary>
        /// Backing field for the bindable property <see cref="Body1"/>.
        /// </summary>
        public static readonly BindableProperty Body1Property = BindableProperty.Create(nameof(Body1), typeof(string), typeof(MaterialFontConfiguration), Font.Default.Family);

        /// <summary>
        /// Backing field for the bindable property <see cref="Body2"/>.
        /// </summary>
        public static readonly BindableProperty Body2Property = BindableProperty.Create(nameof(Body2), typeof(string), typeof(MaterialFontConfiguration), Font.Default.Family);

        /// <summary>
        /// Backing field for the bindable property <see cref="Button"/>.
        /// </summary>
        public static readonly BindableProperty ButtonProperty = BindableProperty.Create(nameof(Button), typeof(string), typeof(MaterialFontConfiguration), Font.Default.Family);

        /// <summary>
        /// Backing field for the bindable property <see cref="Caption"/>.
        /// </summary>
        public static readonly BindableProperty CaptionProperty = BindableProperty.Create(nameof(Caption), typeof(string), typeof(MaterialFontConfiguration), Font.Default.Family);

        /// <summary>
        /// Backing field for the bindable property <see cref="H1"/>.
        /// </summary>
        public static readonly BindableProperty H1Property = BindableProperty.Create(nameof(H1), typeof(string), typeof(MaterialFontConfiguration), Font.Default.Family);

        /// <summary>
        /// Backing field for the bindable property <see cref="H2"/>.
        /// </summary>
        public static readonly BindableProperty H2Property = BindableProperty.Create(nameof(H2), typeof(string), typeof(MaterialFontConfiguration), Font.Default.Family);

        /// <summary>
        /// Backing field for the bindable property <see cref="H3"/>.
        /// </summary>
        public static readonly BindableProperty H3Property = BindableProperty.Create(nameof(H3), typeof(string), typeof(MaterialFontConfiguration), Font.Default.Family);

        /// <summary>
        /// Backing field for the bindable property <see cref="H4"/>.
        /// </summary>
        public static readonly BindableProperty H4Property = BindableProperty.Create(nameof(H4), typeof(string), typeof(MaterialFontConfiguration), Font.Default.Family);

        /// <summary>
        /// Backing field for the bindable property <see cref="H5"/>.
        /// </summary>
        public static readonly BindableProperty H5Property = BindableProperty.Create(nameof(H5), typeof(string), typeof(MaterialFontConfiguration), Font.Default.Family);

        /// <summary>
        /// Backing field for the bindable property <see cref="H6"/>.
        /// </summary>
        public static readonly BindableProperty H6Property = BindableProperty.Create(nameof(H6), typeof(string), typeof(MaterialFontConfiguration), Font.Default.Family);

        /// <summary>
        /// Backing field for the bindable property <see cref="Overline"/>.
        /// </summary>
        public static readonly BindableProperty OverlineProperty = BindableProperty.Create(nameof(Overline), typeof(string), typeof(MaterialFontConfiguration), Font.Default.Family);

        /// <summary>
        /// Backing field for the bindable property <see cref="Subtitle1"/>.
        /// </summary>
        public static readonly BindableProperty Subtitle1Property = BindableProperty.Create(nameof(Subtitle1), typeof(string), typeof(MaterialFontConfiguration), Font.Default.Family);

        /// <summary>
        /// Backing field for the bindable property <see cref="Subtitle2"/>.
        /// </summary>
        public static readonly BindableProperty Subtitle2Property = BindableProperty.Create(nameof(Subtitle2), typeof(string), typeof(MaterialFontConfiguration), Font.Default.Family);

        /// <summary>
        /// Body 1 font family, used for long-form writing and small text sizes.
        /// </summary>
        public string Body1
        {
            get => GetValue(Body1Property)?.ToString();
            set => SetValue(Body1Property, value);
        }

        /// <summary>
        /// Body 2 font family, used for long-form writing and small text sizes.
        /// </summary>
        public string Body2
        {
            get => GetValue(H1Property)?.ToString();
            set => SetValue(H1Property, value);
        }

        /// <summary>
        /// Button font family, used by different types of buttons.
        /// </summary>
        public string Button
        {
            get => GetValue(ButtonProperty)?.ToString();
            set => SetValue(ButtonProperty, value);
        }

        /// <summary>
        /// Caption font family, used for annotations or to introduce a headline text.
        /// </summary>
        public string Caption
        {
            get => GetValue(CaptionProperty)?.ToString();
            set => SetValue(CaptionProperty, value);
        }

        /// <summary>
        /// Headline 1 font family, used by large text on the screen.
        /// </summary>
        public string H1
        {
            get => GetValue(H1Property)?.ToString();
            set => SetValue(H1Property, value);
        }

        /// <summary>
        /// Headline 2 font family, used by large text on the screen.
        /// </summary>
        public string H2
        {
            get => GetValue(H2Property)?.ToString();
            set => SetValue(H2Property, value);
        }

        /// <summary>
        /// Headline 3 font family, used by large text on the screen.
        /// </summary>
        public string H3
        {
            get => GetValue(H3Property)?.ToString();
            set => SetValue(H3Property, value);
        }

        /// <summary>
        /// Headline 4 font family, used by large text on the screen.
        /// </summary>
        public string H4
        {
            get => GetValue(H4Property)?.ToString();
            set => SetValue(H4Property, value);
        }

        /// <summary>
        /// Headline 5 font family, used by large text on the screen.
        /// </summary>
        public string H5
        {
            get => GetValue(H5Property)?.ToString();
            set => SetValue(H5Property, value);
        }

        /// <summary>
        /// Headline 6 font family, used by large text on the screen.
        /// </summary>
        public string H6
        {
            get => GetValue(H6Property)?.ToString();
            set => SetValue(H6Property, value);
        }

        /// <summary>
        /// Overline font family, used for annotations or to introduce a headline text.
        /// </summary>
        public string Overline
        {
            get => GetValue(OverlineProperty)?.ToString();
            set => SetValue(OverlineProperty, value);
        }

        /// <summary>
        /// Subtitle 1 font family, used by medium-emphasis text.
        /// </summary>
        public string Subtitle1
        {
            get => GetValue(Subtitle1Property)?.ToString();
            set => SetValue(Subtitle1Property, value);
        }

        /// <summary>
        /// Subtitle 2 font family, used by medium-emphasis text.
        /// </summary>
        public string Subtitle2
        {
            get => GetValue(Subtitle2Property)?.ToString();
            set => SetValue(Subtitle2Property, value);
        }
    }
}
