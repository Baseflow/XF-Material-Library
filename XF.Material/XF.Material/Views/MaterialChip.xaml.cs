using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialChip : Frame
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(string), string.Empty);

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(Color), Color.FromHex("#DE000000"));

        public static readonly BindableProperty ActionImageProperty = BindableProperty.Create(nameof(ActionImage), typeof(ImageSource), typeof(ImageSource), default(ImageSource));

        public static readonly BindableProperty ActionImageTappedCommandProperty = BindableProperty.Create(nameof(ActionImageTappedCommand), typeof(ICommand), typeof(ICommand), default(Command));

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(string), default(string));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public ImageSource ActionImage
        {
            get => (ImageSource)GetValue(ActionImageProperty);
            set => SetValue(ActionImageProperty, value);
        }

        public Command ActionImageTappedCommand
        {
            get => (Command)GetValue(ActionImageTappedCommandProperty);
            set => SetValue(ActionImageTappedCommandProperty, value);
        }

        public MaterialChip()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.Text))
            {
                ChipLabel.Text = this.Text;
            }

            else if (propertyName == nameof(this.TextColor))
            {
                ChipLabel.TextColor = this.TextColor;
            }

            else if (propertyName == nameof(this.FontFamily))
            {
                ChipLabel.FontFamily = this.FontFamily;
            }

            else if (propertyName == nameof(this.ActionImage))
            {
                ChipImage.IsVisible = this.ActionImage != null;
                ChipImage.Source = this.ActionImage;
            }

            else if (propertyName == nameof(this.ActionImageTappedCommand))
            {
                if (this.ActionImage != null && this.ActionImageTappedCommand != null)
                {
                    ChipImage.GestureRecognizers.Add(new TapGestureRecognizer { Command = this.ActionImageTappedCommand, NumberOfTapsRequired = 1 });
                }

                else if (this.ActionImageTappedCommand == null)
                {
                    ChipImage.GestureRecognizers.Clear();
                    ChipImage.Source = null;
                }
            }
        }
    }
}