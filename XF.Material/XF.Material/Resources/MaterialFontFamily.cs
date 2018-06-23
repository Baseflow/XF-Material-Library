using Xamarin.Forms;

namespace XF.Material.Resources
{
    public class MaterialFontFamily : BindableObject
    {
        public static readonly BindableProperty RegularProperty = BindableProperty.Create(nameof(Regular), typeof(string), typeof(string));
        public static readonly BindableProperty MediumProperty = BindableProperty.Create(nameof(Medium), typeof(string), typeof(string));
        public static readonly BindableProperty BoldProperty = BindableProperty.Create(nameof(Bold), typeof(string), typeof(string));

        public string Regular
        {
            get => GetValue(RegularProperty).ToString();
            set => SetValue(RegularProperty, value);
        }

        public string Medium
        {
            get => GetValue(MediumProperty).ToString();
            set => SetValue(MediumProperty, value);
        }

        public string Bold
        {
            get => GetValue(BoldProperty).ToString();
            set => SetValue(BoldProperty, value);
        }
    }
}
