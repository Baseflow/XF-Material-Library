using Xamarin.Forms;

namespace XF.Material.Forms.Resources.Typography
{
    public class MaterialFontConfiguration : BindableObject
    {
        public static readonly BindableProperty H1Property = BindableProperty.Create(nameof(H1), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty H2Property = BindableProperty.Create(nameof(H2), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty H3Property = BindableProperty.Create(nameof(H3), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty H4Property = BindableProperty.Create(nameof(H4), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty H5Property = BindableProperty.Create(nameof(H5), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty H6Property = BindableProperty.Create(nameof(H6), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty Subtitle1Property = BindableProperty.Create(nameof(Subtitle1), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty Subtitle2Property = BindableProperty.Create(nameof(Subtitle2), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty Body1Property = BindableProperty.Create(nameof(Body1), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty Body2Property = BindableProperty.Create(nameof(Body2), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty ButtonProperty = BindableProperty.Create(nameof(Button), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty CaptionProperty = BindableProperty.Create(nameof(Caption), typeof(string), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty OverlineProperty = BindableProperty.Create(nameof(Overline), typeof(string), typeof(MaterialFontConfiguration));

        public string H1
        {
            get => GetValue(H1Property)?.ToString();
            set => SetValue(H1Property, value);
        }

        public string H2
        {
            get => GetValue(H2Property)?.ToString();
            set => SetValue(H2Property, value);
        }

        public string H3
        {
            get => GetValue(H3Property)?.ToString();
            set => SetValue(H3Property, value);
        }

        public string H4
        {
            get => GetValue(H4Property)?.ToString();
            set => SetValue(H4Property, value);
        }

        public string H5
        {
            get => GetValue(H5Property)?.ToString();
            set => SetValue(H5Property, value);
        }

        public string H6
        {
            get => GetValue(H6Property)?.ToString();
            set => SetValue(H6Property, value);
        }

        public string Subtitle1
        {
            get => GetValue(Subtitle1Property)?.ToString();
            set => SetValue(Subtitle1Property, value);
        }

        public string Subtitle2
        {
            get => GetValue(Subtitle2Property)?.ToString();
            set => SetValue(Subtitle2Property, value);
        }

        public string Body1
        {
            get => GetValue(Body1Property)?.ToString();
            set => SetValue(Body1Property, value);
        }

        public string Body2
        {
            get => GetValue(H1Property)?.ToString();
            set => SetValue(H1Property, value);
        }

        public string Button
        {
            get => GetValue(ButtonProperty)?.ToString();
            set => SetValue(ButtonProperty, value);
        }

        public string Caption
        {
            get => GetValue(CaptionProperty)?.ToString();
            set => SetValue(CaptionProperty, value);
        }

        public string Overline
        {
            get => GetValue(OverlineProperty)?.ToString();
            set => SetValue(OverlineProperty, value);
        }
    }
}
