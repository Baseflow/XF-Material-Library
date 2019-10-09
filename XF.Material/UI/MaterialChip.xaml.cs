using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Forms.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialChip : ContentView
    {
        public static readonly BindableProperty ActionImageProperty = BindableProperty.Create(nameof(ActionImage), typeof(ImageSource), typeof(MaterialChip), default(ImageSource));
        public static readonly BindableProperty ActionImageTappedCommandProperty = BindableProperty.Create(nameof(ActionImageTappedCommand), typeof(ICommand), typeof(MaterialChip), default(Command));
        public static readonly BindableProperty ActionImageTintColorProperty = BindableProperty.Create(nameof(ActionImageTintColor), typeof(Color), typeof(MaterialChip), default(Color));
        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialChip), default(Color));
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialChip), default(string));
        public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(ImageSource), typeof(MaterialChip), default(ImageSource));
        public static readonly BindableProperty ImageTintColorProperty = BindableProperty.Create(nameof(ImageTintColor), typeof(Color), typeof(MaterialChip), default(Color));
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialChip), Color.FromHex("#DE000000"));
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialChip), string.Empty);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialChip), 14.0);

        private bool _canExecute;

        public MaterialChip()
        {
            InitializeComponent();
        }

        public event EventHandler ActionImageTapped;

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

        public Color ActionImageTintColor
        {
            get => (Color)GetValue(ActionImageTintColorProperty);
            set => SetValue(ActionImageTintColorProperty, value);
        }

        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public ImageSource Image
        {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public Color ImageTintColor
        {
            get => (Color)GetValue(ImageTintColorProperty);
            set => SetValue(ImageTintColorProperty, value);
        }

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

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(BackgroundColor))
            {
                ChipContainer.BackgroundColor = BackgroundColor;
            }
            else
            {
                base.OnPropertyChanged(propertyName);

                switch (propertyName)
                {
                    case nameof(Text):
                        ChipLabel.Text = Text;
                        break;
                    case nameof(FontSize):
                        ChipLabel.FontSize = FontSize;
                        break;
                    case nameof(ActionImageTintColor):
                        ChipActionImage.TintColor = ActionImageTintColor;
                        break;
                    case nameof(ImageTintColor):
                        ChipImage.TintColor = ImageTintColor;
                        break;
                    case nameof(TextColor):
                        ChipLabel.TextColor = TextColor;
                        break;
                    case nameof(FontFamily):
                        ChipLabel.FontFamily = FontFamily;
                        break;
                    case nameof(Image):
                        ChipImageContainer.IsVisible = Image != null;
                        ChipImage.Source = Image;
                        break;
                    case nameof(ActionImage):
                        {
                            ChipActionImage.Source = ActionImage;
                            ChipActionImage.IsVisible = ActionImage != null;

                            if (ActionImage != null && ChipActionImage.GestureRecognizers.Count <= 0)
                            {
                                ChipActionImage.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(ActionImageTapHandled, () => !_canExecute), NumberOfTapsRequired = 1 });
                            }
                            else if (ActionImage == null)
                            {
                                ChipActionImage.GestureRecognizers.Clear();
                            }

                            break;
                        }
                }
            }
        }

        private void ActionImageTapHandled()
        {
            _canExecute = true;
            ActionImageTappedCommand?.Execute(null);
            ActionImageTapped?.Invoke(this, new EventArgs());
            _canExecute = false;
        }
    }
}