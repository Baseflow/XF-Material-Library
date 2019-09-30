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
        public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialChip), default(Color));
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialChip), default(string));
        public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(ImageSource), typeof(MaterialChip), default(ImageSource));
        public static readonly BindableProperty ImageTintColorProperty = BindableProperty.Create(nameof(ImageTintColor), typeof(Color), typeof(MaterialChip), default(Color));
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialChip), Color.FromHex("#DE000000"));
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialChip), string.Empty);

        private bool _canExecute;

        public MaterialChip()
        {
            this.InitializeComponent();
        }

        public event EventHandler ActionImageTapped;

        public ImageSource ActionImage
        {
            get => (ImageSource)this.GetValue(ActionImageProperty);
            set => this.SetValue(ActionImageProperty, value);
        }

        public Command ActionImageTappedCommand
        {
            get => (Command)this.GetValue(ActionImageTappedCommandProperty);
            set => this.SetValue(ActionImageTappedCommandProperty, value);
        }

        public Color ActionImageTintColor
        {
            get => (Color)this.GetValue(ActionImageTintColorProperty);
            set => this.SetValue(ActionImageTintColorProperty, value);
        }

        public new Color BackgroundColor
        {
            get => (Color)this.GetValue(BackgroundColorProperty);
            set => this.SetValue(BackgroundColorProperty, value);
        }

        public string FontFamily
        {
            get => (string)this.GetValue(FontFamilyProperty);
            set => this.SetValue(FontFamilyProperty, value);
        }

        public ImageSource Image
        {
            get => (ImageSource)this.GetValue(ImageProperty);
            set => this.SetValue(ImageProperty, value);
        }

        public Color ImageTintColor
        {
            get => (Color)this.GetValue(ImageTintColorProperty);
            set => this.SetValue(ImageTintColorProperty, value);
        }

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public Color TextColor
        {
            get => (Color)this.GetValue(TextColorProperty);
            set => this.SetValue(TextColorProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(this.BackgroundColor))
            {
                ChipContainer.BackgroundColor = this.BackgroundColor;
            }
            else
            {
                base.OnPropertyChanged(propertyName);

                switch (propertyName)
                {
                    case nameof(this.Text):
                        ChipLabel.Text = this.Text;
                        break;
                    case nameof(this.ActionImageTintColor):
                        ChipActionImage.TintColor = this.ActionImageTintColor;
                        break;
                    case nameof(this.ImageTintColor):
                        ChipImage.TintColor = this.ImageTintColor;
                        break;
                    case nameof(this.TextColor):
                        ChipLabel.TextColor = this.TextColor;
                        break;
                    case nameof(this.FontFamily):
                        ChipLabel.FontFamily = this.FontFamily;
                        break;
                    case nameof(this.Image):
                        ChipImageContainer.IsVisible = this.Image != null;
                        ChipImage.Source = this.Image;
                        break;
                    case nameof(this.ActionImage):
                        {
                            ChipActionImage.Source = this.ActionImage;
                            ChipActionImage.IsVisible = this.ActionImage != null;

                            if (this.ActionImage != null && ChipActionImage.GestureRecognizers.Count <= 0)
                            {
                                ChipActionImage.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(this.ActionImageTapHandled, () => !_canExecute), NumberOfTapsRequired = 1 });
                            }
                            else if (this.ActionImage == null)
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
            this.ActionImageTappedCommand?.Execute(null);
            this.ActionImageTapped?.Invoke(this, new EventArgs());
            _canExecute = false;
        }
    }
}